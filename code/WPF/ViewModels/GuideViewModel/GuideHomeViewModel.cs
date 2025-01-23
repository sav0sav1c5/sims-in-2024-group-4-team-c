using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuideServices;
using BookingApp.View.GuideView;
using BookingApp.WPF.Views.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuideViewModel
{
    public class GuideHomeViewModel : ViewModelBase
    {
        private readonly CheckpointService checkPointService = DependencyContainer.GetInstance<CheckpointService>();
        private readonly TourService tourService = DependencyContainer.GetInstance<TourService>();
        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();

        public NavigationService navigationService { get; set; }
        public static Guide LoggedGuide { get; set; }
        public RelayCommand ScheduledToursCommand { get; private set; }
        public RelayCommand NavigateTourCommand { get; set; }
        public RelayCommand CreateTourCommand { get; set; }

        public ObservableCollection<TourReservationDTO> TodayScheduledTours { get; set; } = new ObservableCollection<TourReservationDTO>();
        private TourReservationDTO _selectedTour { get; set; }
        public TourReservationDTO SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                NavigateToNextPage();
            }
        }

        public GuideHomeViewModel(Guide guide, NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoggedGuide = guide ?? throw new ArgumentNullException(nameof(guide));

            TodayScheduledTours = LoadPendingToursForGuide();
            ScheduledToursCommand = new RelayCommand(ScheduledToursClick);
            NavigateTourCommand = new RelayCommand(_ => NavigateToNextPage());
            CreateTourCommand = new RelayCommand(CreateTourClick);
        }
        private ObservableCollection<TourReservationDTO> LoadPendingToursForGuide()
        {
            // Get today's date
            DateTime today = DateTime.Today;

            // Get all tour reservations for the guide with the specified ID
            List<TourReservation> reservations = tourReservationService.GetTourReservationsByGuideId(LoggedGuide.Id);
            ObservableCollection<TourReservationDTO> reservationDtos = new ObservableCollection<TourReservationDTO>();

            foreach (var reservation in reservations)
            {
                // Check if the reservation's date is today's date
                if (reservation.DateAndTime.Date == today || reservation.TourState == TourStates.Type.Started)
                {
                    TourReservationDTO tourReservationDTO = new TourReservationDTO(reservation);
                    if (tourReservationDTO.TourState == TourStates.Type.Pending || tourReservationDTO.TourState == TourStates.Type.Started)
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
        private void ScheduledToursClick(object sender)
        {
            Page ScheduledTour = new ScheduledToursView(LoggedGuide, navigationService);
            navigationService.Navigate(ScheduledTour);
        }
        private void CreateTourClick(object sender)
        {
            Page createTour = new CreateTour(LoggedGuide, navigationService);
            navigationService.Navigate(createTour);
        }
    }
}
