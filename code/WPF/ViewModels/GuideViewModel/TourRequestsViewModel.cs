using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services;
using BookingApp.Services.GuideServices;
using BookingApp.View.GuideView;
using BookingApp.WPF.Views.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuideViewModel
{
    public class TourRequestsViewModel : ViewModelBase
    {
        public NavigationService NavigationService { get; set; }
        private readonly TourRequestService tourRequestService = DependencyContainer.GetInstance<TourRequestService>();
        private readonly TourService tourService = DependencyContainer.GetInstance<TourService>();
        private readonly TouristService TouristService = DependencyContainer.GetInstance<TouristService>();
        private readonly LocationService locationService = DependencyContainer.GetInstance<LocationService>();
        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();
        private readonly TourAttendanceService tourAttendanceService = DependencyContainer.GetInstance<TourAttendanceService>();
        private readonly TourParticipantService tourParticipantService = DependencyContainer.GetInstance<TourParticipantService>();
        public static Guide LoggedGuide { get; set; }
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; } = new ObservableCollection<TourRequestDTO>();

        private List<TourRequestDTO> allTourRequests; // Store all tour requests for filtering

        private TourRequestDTO _selectedRequest { get; set; }
        public TourRequestDTO SelectedRequest
        {
            get { return _selectedRequest; }
            set
            {
                _selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));
            }
        }

        private string selectedCountry;
        public string SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
                UpdateCities(selectedCountry); // Call the method to update cities
                FilterTourRequests(); // Optionally, you can also update the filtered tour requests based on the selected country change
            }
        }
        private string selectedCity;
        public string SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                FilterTourRequests();
            }
        }

        private int selectedNumberOfPeople;
        public int SelectedNumberOfPeople
        {
            get { return selectedNumberOfPeople; }
            set
            {
                selectedNumberOfPeople = value;
                FilterTourRequests();
            }
        }

        private string selectedLanguage;
        public string SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                selectedLanguage = value;
                FilterTourRequests();
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                FilterTourRequests();
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                FilterTourRequests();
            }
        }
        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); }
        }
        private ObservableCollection<string> _availableCountries;
        public ObservableCollection<string> AvailableCountries
        {
            get { return _availableCountries; }
            set { _availableCountries = value; OnPropertyChanged(nameof(AvailableCountries)); }
        }

        private ObservableCollection<string> _availableCities;
        public ObservableCollection<string> AvailableCities
        {
            get { return _availableCities; }
            set { _availableCities = value; OnPropertyChanged(nameof(AvailableCities)); }
        }

        private ObservableCollection<string> _availableLanguages;
        public ObservableCollection<string> AvailableLanguages
        {
            get { return _availableLanguages; }
            set { _availableLanguages = value; OnPropertyChanged(nameof(AvailableLanguages)); }
        }

        public RelayCommand AcceptTourRequestCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public TourRequestsViewModel(Guide guide, NavigationService _navigationService)
        {
            LoggedGuide = guide;
            NavigationService = _navigationService;
 
            LoadPendingTourRequestsForGuide();
            LoadAvailableLocations();
            LoadAvailableLanguages();

            AcceptTourRequestCommand = new RelayCommand(_ => AcceptTourRequest(), _ => CanAcceptTourRequest());
            BackCommand = new RelayCommand(_ => NavigateBack());
        }

        private void LoadPendingTourRequestsForGuide()
        {
            allTourRequests = tourRequestService.GetAllTourRequests()
                .Select(tr => new TourRequestDTO(tr))
                .ToList();
            StartDate = DateTime.Today.AddMonths(-1);
            EndDate = DateTime.Today.AddMonths(1);
            TourRequests.Clear();
            foreach (var tourRequest in allTourRequests)
            {
                tourRequest.Tourist = TouristService.GetTouristById(tourRequest.TouristId);
                if(tourRequest.TourRequestState == TourRequestStates.Type.Pending)
                {
                    TourRequests.Add(tourRequest);
                }
            }
        }
        

        private void LoadAvailableLocations()
        {
            var locations = locationService.GetAllLocations(); // Assuming GetAllLocations() returns a list of locations
            AvailableCountries = new ObservableCollection<string>(locations.Select(loc => loc.Country).Distinct());
            AvailableCities = new ObservableCollection<string>(locations.Select(loc => loc.City).Distinct());
        }
        private void LoadAvailableLanguages()
        {
            var tours = tourService.GetAllTours();
            AvailableLanguages = new ObservableCollection<string>(tours.Select(tour => tour.Language).Distinct());
        }

        // Add a method to update cities based on the selected country
        private void UpdateCities(string selectedCountry)
        {
            var locations = locationService.GetAllLocations(); // Assuming GetAllLocations() returns a list of locations
            AvailableCities = new ObservableCollection<string>(
                locations.Where(loc => loc.Country == selectedCountry).Select(loc => loc.City).Distinct()
            );
        }
        private void FilterTourRequests()
        {
            var filteredRequests = allTourRequests.Where(tr =>
                tr.TourRequestState == TourRequestStates.Type.Pending &&
                (SelectedNumberOfPeople == 0 || tr.NumberOfParticipants == SelectedNumberOfPeople) &&
                (string.IsNullOrEmpty(SelectedCountry) || tr.Country == SelectedCountry) &&
                (string.IsNullOrEmpty(SelectedCity) || tr.City == SelectedCity) &&
                (StartDate == default || EndDate == default || (tr.StartDate >= StartDate && tr.EndDate <= EndDate)) &&
                (string.IsNullOrEmpty(SelectedLanguage) || tr.Language == SelectedLanguage)
            ).ToList();

            TourRequests.Clear();
            foreach (var tourRequest in filteredRequests)
            {

                TourRequests.Add(tourRequest);
            }
        }
        private bool CanAcceptTourRequest()
        {
            return SelectedRequest != null;
        }

        private void AcceptTourRequest()
        {
            if (SelectedRequest != null)
            {
                var datePickerDialog = new DatePickerDialog
                {
                    DataContext = this,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };

                if (datePickerDialog.ShowDialog() == true)
                {
                    // Sada imate SelectedDate i možete nastaviti sa prihvatanjem zahteva
                    if (SelectedDate.HasValue)
                    {
                        if (IsGuideAlreadyBookedForDate())
                        {
                            Console.WriteLine("Guide is already booked for the selected date.");
                            return;
                        }

                        Location location = GetOrCreateLocation(SelectedRequest.Country, SelectedRequest.City);
                        Tour savedTour = CreateTour(location);

                        TourReservation savedReservation = CreateTourReservation(savedTour.Id);

                        TourAttendance savedAttendance = CreateTourAttendance(savedReservation.Id);

                        UpdateTourParticipants(savedAttendance.Id);

                        UpdateTourRequestState();

                        LoadPendingTourRequestsForGuide();

                        MessageBox.Show("Request accepted!");
                    }
                    else
                    {
                        Console.WriteLine("SelectedDate is not valid.");
                    }
                }
                else
                {
                    Console.WriteLine("Dialog was canceled.");
                }
            }
            else
            {
                Console.WriteLine("SelectedRequest is null.");
            }
        }


        private bool IsGuideAlreadyBookedForDate()
        {
            List<TourReservation> guideTours = tourReservationService.GetTourReservationsByGuideId(LoggedGuide.Id);
            return guideTours.Any(reservation => reservation.DateAndTime.Date == SelectedDate.Value.Date);
        }

        private Tour CreateTour(Location location)
        {
            Tour tour = new Tour(SelectedRequest.TourName, location.Id, SelectedRequest.Description, SelectedRequest.Language, LoggedGuide.Id);
            return tourService.SaveTour(tour);
        }

        private TourReservation CreateTourReservation(int tourId)
        {
            return tourReservationService.SaveTourReservation(
                new TourReservation(tourId, TourStates.Type.Pending, LoggedGuide.Id, SelectedRequest.NumberOfParticipants, SelectedDate.Value)
            );
        }

        private TourAttendance CreateTourAttendance(int reservationId)
        {
            TourAttendance attendance = new TourAttendance(SelectedRequest.TouristId, reservationId, null);
            attendance.IsPresent = false;
            attendance.IsConfirmed = false;
            return tourAttendanceService.SaveTourAttendance(attendance);
        }

        private void UpdateTourParticipants(int attendanceId)
        {
            if (SelectedRequest.TourParticipants != null)
            {
                foreach (var participant in SelectedRequest.TourParticipants)
                {
                    TourParticipant tourParticipant = new TourParticipant(
                        attendanceId, participant.FirstName, participant.Age, participant.LastName, participant.Email
                    );

                    tourParticipantService.UpdateTourParticipant(tourParticipant);
                }
            }
        }

        private void UpdateTourRequestState()
        {
            SelectedRequest.TourRequestState = TourRequestStates.Type.Accepted;
            SelectedRequest.NotifyTourist = true;
            TourRequest updatedRequest = new TourRequest(
                SelectedRequest.Id,
                SelectedRequest.TourName,
                SelectedRequest.StartDate,
                SelectedRequest.EndDate,
                SelectedRequest.Country,
                SelectedRequest.City,
                SelectedRequest.Language,
                SelectedRequest.Description,
                SelectedRequest.NumberOfParticipants,
                SelectedRequest.TourRequestState,
                SelectedRequest.NotifyTourist
            );
            tourRequestService.UpdateTourRequest(updatedRequest);
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
