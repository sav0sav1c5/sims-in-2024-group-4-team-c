using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services;
using BookingApp.Services.GuideServices;
using BookingApp.WPF.Views.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuideViewModel
{
    public class ComplexTourRequestsViewModel : ViewModelBase
    {
        public NavigationService NavigationService { get; set; }

        private readonly ComplexTourPartService complexTourPartService = DependencyContainer.GetInstance<ComplexTourPartService>();
        private readonly ComplexTourRequestService complexTourRequestService = DependencyContainer.GetInstance<ComplexTourRequestService>();
        private readonly TourService tourService = DependencyContainer.GetInstance<TourService>();
        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();
        private readonly TourAttendanceService tourAttendanceService = DependencyContainer.GetInstance<TourAttendanceService>();
        private readonly TourParticipantService tourParticipantService = DependencyContainer.GetInstance<TourParticipantService>();
        private readonly TourRequestService tourRequestService = DependencyContainer.GetInstance<TourRequestService>();
        private readonly LocationService locationService = DependencyContainer.GetInstance<LocationService>();

        public RelayCommand AcceptTourRequestCommand { get; private set; }
        private ComplexTourPart _complexTourPart { get; set; }
        public ComplexTourPart ComplexTourPart
        {
            get { return _complexTourPart; }
            set
            {
                _complexTourPart = value;
                OnPropertyChanged(nameof(ComplexTourPart));
            }
        }
        private ComplexTourRequest _complexTourRequest { get; set; }
        public ComplexTourRequest ComplexTourRequest
        {
            get { return _complexTourRequest; }
            set
            {
                _complexTourRequest = value;
                OnPropertyChanged(nameof(ComplexTourRequest));
            }
        }
        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); }
        }
        public ObservableCollection<ComplexTourRequest> ComplexTourRequests { get; set; } = new ObservableCollection<ComplexTourRequest>();

        public static Guide LoggedGuide { get; set; }

        public RelayCommand BackCommand { get; set; }

        public ComplexTourRequestsViewModel(Guide guide, NavigationService _navigationService)
        {
            LoggedGuide = guide;
            NavigationService = _navigationService;

            LoadPendingComplexTourRequests();

            AcceptTourRequestCommand = new RelayCommand(AcceptTourRequest, _ => CanAcceptTourRequest());
            BackCommand = new RelayCommand(_ => NavigateBack());
        }

        public void LoadPendingComplexTourRequests()
        {
            ComplexTourRequests.Clear(); // Clear existing requests before loading new ones

            List<ComplexTourRequest> complexTourRequests = complexTourRequestService.GetAllComplexTourRequests();

            foreach (var request in complexTourRequests)
            {
                request.Parts = complexTourPartService.GetComplexTourPartsByComplexTourRequestId(request.Id);

                if (IsGuideAlreadyBookedForRequest(request.Parts))
                {
                    continue;
                }

                ComplexTourRequests.Add(request);
            }

            NotifyChanges();
        }

        private bool IsGuideAlreadyBookedForRequest(IEnumerable<ComplexTourPart> parts)
        {
            return parts.Any(part => part.TourRequestState == TourRequestStates.Type.Accepted && part.GuideId == LoggedGuide.Id);
        }

        private void NotifyChanges()
        {
            OnPropertyChanged(nameof(ComplexTourRequests));
            OnPropertyChanged(nameof(ComplexTourRequest));
            OnPropertyChanged(nameof(ComplexTourPart));
        }


        private bool CanAcceptTourRequest()
        {
            return true; // Allow accepting requests without specific validation for now
        }

        private void AcceptTourRequest(object parameter)
        {
            if (parameter is ComplexTourPart complexTourPart)
            {
                ComplexTourPart = complexTourPart;

                var availableDates = GetAvailableDates(complexTourPart.StartDate, complexTourPart.EndDate);
                if (availableDates.Count == 0)
                {
                    MessageBox.Show("No available dates within the selected range.");
                    return;
                }

                var datePickerDialog = new ComplexRequestDates(complexTourPart.StartDate, complexTourPart.EndDate, availableDates);
                if (datePickerDialog.ShowDialog() == true && datePickerDialog.SelectedDate.HasValue)
                {
                    SelectedDate = datePickerDialog.SelectedDate.Value;

                    if (IsGuideAlreadyBookedForDate())
                    {
                        MessageBox.Show("Guide is already booked for the selected date.");
                        return;
                    }

                    ProcessAcceptedTourRequest(complexTourPart);
                }
                else
                {
                    Console.WriteLine("Selected date is null.");
                }
            }
        }

        private void ProcessAcceptedTourRequest(ComplexTourPart complexTourPart)
        {
            Location location = GetOrCreateLocation(complexTourPart.Country, complexTourPart.City);
            Tour savedTour = CreateTour(location);

            TourReservation savedReservation = CreateTourReservation(savedTour.Id);
            TourAttendance savedAttendance = CreateTourAttendance(savedReservation.Id);

            UpdateTourParticipants(savedAttendance.Id);
            UpdateComplexTourRequestPart();

            LoadPendingComplexTourRequests(); // Reload pending complex tour requests to refresh the view

            MessageBox.Show("Request accepted!");

            NotifyRequestChanges();
        }

        private void NotifyRequestChanges()
        {
            OnPropertyChanged(nameof(ComplexTourRequests));
            OnPropertyChanged(nameof(ComplexTourRequest));
            OnPropertyChanged(nameof(ComplexTourPart));
        }




        private List<DateTime> GetAvailableDates(DateTime? startDate, DateTime? endDate)
        {
            List<DateTime> availableDates = new List<DateTime>();

            if (startDate.HasValue && endDate.HasValue)
            {
                // Generate a list of dates between the start and end dates
                DateTime currentDate = startDate.Value.Date;
                while (currentDate <= endDate.Value.Date)
                {
                    availableDates.Add(currentDate);
                    currentDate = currentDate.AddDays(1);
                }

                // Check if the guide is already booked for any of these dates
                List<DateTime> bookedDates = GetBookedDatesForGuide();
                availableDates = availableDates.Except(bookedDates).ToList();
            }

            return availableDates;
        }
        private List<DateTime> GetBookedDatesForGuide()
        {
            List<TourReservation> guideTours = tourReservationService.GetTourReservationsByGuideId(LoggedGuide.Id);
            return guideTours.Select(reservation => reservation.DateAndTime.Date).ToList();
        }

        private bool IsGuideAlreadyBookedForDate()
        {
            List<TourReservation> guideTours = tourReservationService.GetTourReservationsByGuideId(LoggedGuide.Id);
            return guideTours.Any(reservation => reservation.DateAndTime.Date == SelectedDate.Value.Date);
        }

        private Tour CreateTour(Location location)
        {
            Tour tour = new Tour(ComplexTourPart.TourName, location.Id, ComplexTourPart.Description, ComplexTourPart.Language, LoggedGuide.Id);
            return tourService.SaveTour(tour);
        }

        private TourReservation CreateTourReservation(int tourId)
        {
            return tourReservationService.SaveTourReservation(
                new TourReservation(tourId, TourStates.Type.Pending, LoggedGuide.Id, ComplexTourPart.NumberOfParticipants, SelectedDate.Value)
            );
        }

        private TourAttendance CreateTourAttendance(int reservationId)
        {
            ComplexTourRequest complexTourRequest = complexTourRequestService.GetComplexTourRequestById(ComplexTourPart.ComplexTourRequestId);

            TourAttendance attendance = new TourAttendance(complexTourRequest.TouristId, reservationId, null);
            attendance.IsPresent = false;
            attendance.IsConfirmed = false;
            return tourAttendanceService.SaveTourAttendance(attendance);
        }

        private void UpdateTourParticipants(int attendanceId)
        {
            if (ComplexTourPart.TourParticipants != null)
            {
                foreach (var participant in ComplexTourPart.TourParticipants)
                {
                    TourParticipant tourParticipant = new TourParticipant(
                        attendanceId, participant.FirstName, participant.Age, participant.LastName, participant.Email
                    );

                    tourParticipantService.UpdateTourParticipant(tourParticipant);
                }
            }
        }

        private void UpdateComplexTourRequestPart()
        {
            ComplexTourPart.TourRequestState = TourRequestStates.Type.Accepted;
            ComplexTourPart.NotifyTourist = true;
            ComplexTourPart.ApprovedDate = SelectedDate.Value;
            ComplexTourPart.GuideId = LoggedGuide.Id;
            // Assuming ApprovedDate and TourParticipants are available as properties of ComplexTourPart
            ComplexTourPart updatedComplexRequestPart = new ComplexTourPart(
                ComplexTourPart.Id,
                ComplexTourPart.TourName,
                ComplexTourPart.StartDate,
                ComplexTourPart.EndDate,
                ComplexTourPart.ApprovedDate, // Ensure ApprovedDate is set somewhere before this call
                ComplexTourPart.Country,
                ComplexTourPart.City,
                ComplexTourPart.Language,
                ComplexTourPart.Description,
                ComplexTourPart.NumberOfParticipants,
                ComplexTourPart.NotifyTourist,
                ComplexTourPart.TourRequestState,
                ComplexTourPart.TourParticipants, // Ensure TourParticipants is set somewhere before this call
                ComplexTourPart.ComplexTourRequestId,
                ComplexTourPart.GuideId
            );

            complexTourPartService.UpdateComplexTourPart(updatedComplexRequestPart);
        }


        private Location GetOrCreateLocation(string country, string city)
        {
            Location? location = locationService.GetAllLocations()
                .FirstOrDefault(l => l.Country == country && l.City == city);

            if (location == null)
            {
                location = new Location(city, country);
                locationService.SaveLocation(location);
            }

            return location;
        }

        private void NavigateBack()
        {
            Page guideProfile = new GuideProfileView(LoggedGuide, NavigationService);
            NavigationService.Navigate(guideProfile);
        }
    }
}

