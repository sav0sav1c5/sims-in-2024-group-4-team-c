using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Resources.DBAccess;
using BookingApp.View.GuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Input;
using System.Collections.ObjectModel;
using BookingApp.Services.GuideServices;
using BookingApp.DependencyInjection;
using BookingApp.DTOs;

namespace BookingApp.WPF.ViewModels.GuideViewModel
{
    public class ScheduledToursViewModel : ViewModelBase
    {
        public NavigationService navigationService { get; set; }
        private readonly CheckpointService checkPointService = DependencyContainer.GetInstance<CheckpointService>();
        private readonly TourService tourService = DependencyContainer.GetInstance<TourService>();
        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();
        private readonly VoucherService voucherService = DependencyContainer.GetInstance<VoucherService>();

        public static Guide LoggedGuide { get; set; }
        public ObservableCollection<TourReservationDTO> ScheduledTours { get; set; } = new ObservableCollection<TourReservationDTO>();
        public ObservableCollection<TourReservationDTO> tourReservations { get; set; } = new ObservableCollection<TourReservationDTO>();
        public ObservableCollection<TourAttendanceDTO> TourAttendances { get; set; } = new ObservableCollection<TourAttendanceDTO>();

        // RelayCommands
        public RelayCommand NavigateTourCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand CancelTourCommand { get; set; }
        public RelayCommand CreateTourCommand { get; set; }
        private TourReservationDTO _selectedTour { get; set; }
        public TourReservationDTO SelectedTour {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                NavigateToNextPage();
            }
        }

        public ScheduledToursViewModel(Guide guide, NavigationService _navigationService)
        {
            LoggedGuide = guide;
            navigationService = _navigationService;
            ScheduledTours = LoadPendingToursForGuide();

            // Initialize RelayCommands
            NavigateTourCommand = new RelayCommand(_ => NavigateToNextPage());
            BackCommand = new RelayCommand(_ => NavigateBack());
            CancelTourCommand = new RelayCommand(_ => CancelTour());
            CreateTourCommand = new RelayCommand(_ => CreateTourNavigate());
        }
        private bool CanExecute_Command(object obj)
        {
            return SelectedTour != null;
        }

        private void CancelTour()
        {
            // Step 1: Check if the tour is more than 48 hours away
            if (IsTourMoreThan48Hours())
            {
                // Step 1a: If true, then give vouchers to all attendances
                GiveVouchersToAttendances();
            }
            // Step 2: Cancel all reservations for the tour
            CancelReservations();

            ScheduledTours = LoadPendingToursForGuide(); // Update the ScheduledTours property

            OnPropertyChanged(nameof(ScheduledTours)); // Notify the UI that the ScheduledTours property has changed
        }

        private bool IsTourMoreThan48Hours()
        {
            // Parse the DateAndTime string back to a DateTime
            DateTime tourDate;
            if (DateTime.TryParse(SelectedTour.DateAndTime, out tourDate))
            {
                // Calculate the time difference between now and the tour's start time
                TimeSpan timeDifference = tourDate - DateTime.Now;

                // Check if the time difference is more than 48 hours
                return timeDifference.TotalHours > 48;
            }
            else
            {
                // Handle the error if the DateAndTime string cannot be parsed into a DateTime
                return false;
            }
        }


        private void GiveVouchersToAttendances()
        {
            // Get all participants for the selected tour
            ObservableCollection<TourAttendanceDTO> attendances = TourAttendances;

            // Give vouchers to each participant
            foreach (var attendance in attendances)
            {
                // Create a voucher for the participant and save it
                Voucher voucher = new Voucher($"Voucher for {SelectedTour.Tour.Name}", DateTime.Now.AddDays(365), attendance.TouristId);
                voucherService.SaveVoucher(voucher);
            }
        }

        private void CancelReservations()
        {
            SelectedTour.TourState = TourStates.Type.Canceled;

            TourReservation tourReservation = new TourReservation(SelectedTour.Id, SelectedTour.Tour.Id, SelectedTour.TourState, SelectedTour.GuideId, SelectedTour.TouristsNumber);
            tourReservationService.UpdateTourReservation(tourReservation);
        }
        private ObservableCollection<TourReservationDTO> LoadPendingToursForGuide()
        {
            // Get all tour reservations for the guide with the specified ID
            List<TourReservation> reservations = tourReservationService.GetTourReservationsByGuideId(LoggedGuide.Id);
            ObservableCollection<TourReservationDTO> reservationDtos = new ObservableCollection<TourReservationDTO>();

            foreach (var reservation in reservations.OrderBy(r => r.DateAndTime))
            {
                TourReservationDTO tourReservationDTO = new TourReservationDTO(reservation);
                if (tourReservationDTO.TourState == TourStates.Type.Pending)
                {
                    Tour tour = tourService.GetTourById(reservation.TourId);
                    tour.Checkpoints = checkPointService.GetCheckpointsByTourId(tour.Id);
                    if (tour != null)
                    {
                        TourDTO tourDTO = new TourDTO(tour);
                        tourReservationDTO.Tour = tourDTO;
                        reservationDtos.Add(tourReservationDTO);
                    }
                }
            }

            return reservationDtos;
        }

        private void NavigateToNextPage()
        {
            if (SelectedTour.TourState == TourStates.Type.Started)
            {
                Page LiveTourView = new LiveTourView(LoggedGuide, SelectedTour, navigationService);
                navigationService.Navigate(LiveTourView);
                return;
            }
            Page TourDetails = new TourDetails(LoggedGuide, SelectedTour, navigationService);
            navigationService.Navigate(TourDetails);
            
        }

        private void NavigateBack()
        {
            Page GuideHomeView = new GuideHomeView(LoggedGuide, navigationService);
            navigationService.Navigate(GuideHomeView);
        }
        private void CreateTourNavigate()
        {
            Page createTour = new CreateTour(LoggedGuide, navigationService);
            navigationService.Navigate(createTour);
        }
    }
}
