using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuideServices;
using BookingApp.Services;
using BookingApp.View.GuideView;
using BookingApp.WPF.Views.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;

namespace BookingApp.WPF.ViewModels.GuideViewModel
{
    public class TouristListViewModel : ViewModelBase
    {
        public NavigationService navigationService { get; set; }

        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();
        private readonly TourParticipantService tourParticipantService = DependencyContainer.GetInstance<TourParticipantService>();
        private readonly TouristService touristService = DependencyContainer.GetInstance<TouristService>();
        private readonly TourAttendanceService tourAttendanceService = DependencyContainer.GetInstance<TourAttendanceService>();
        private readonly CheckpointService checkPointService = DependencyContainer.GetInstance<CheckpointService>();
        private readonly TourService tourService = DependencyContainer.GetInstance<TourService>();
        private readonly VoucherService voucherService = DependencyContainer.GetInstance<VoucherService>();

        public TourReservationDTO SelectedTour { get; set; }

        public static Guide LoggedGuide { get; set; }
        public ObservableCollection<TourParticipantDTO> TourParticipants { get; set; } = new ObservableCollection<TourParticipantDTO>();
        public ObservableCollection<TourAttendanceDTO> TourAttendances { get; set; } = new ObservableCollection<TourAttendanceDTO>();
        public ObservableCollection<object> CombinedList { get; set; } = new ObservableCollection<object>();

        public ObservableCollection<CheckpointDTO> Checkpoints { get; set; } = new ObservableCollection<CheckpointDTO>();

        public string TourName => SelectedTour.Tour.Name;

        public RelayCommand StartTourCommand { get; }
        public RelayCommand GoBackCommand { get; }
        public RelayCommand TourRequestsCommand { get; }
        public RelayCommand TouristListCommand { get; }

        public TouristListViewModel(Guide guide, TourReservationDTO tour, NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoggedGuide = guide;
            SelectedTour = tour; // Initialize SelectedTour here
            LoadTourParticipants();
            LoadTourAttendances();
            TourRequestsCommand = new RelayCommand(NavigateToTourRequests);
            GoBackCommand = new RelayCommand(NavigateBack);

        }

        private void LoadTourParticipants()
        {
            // Get the list of TourReservations for the current Guide
            List<TourParticipant> participants = new List<TourParticipant>();
            participants = tourParticipantService.GetTourParticipantsByTourReservationId(SelectedTour.Id);

            foreach (var participant in participants)
            {
                TourParticipantDTO participantDto = new TourParticipantDTO(participant);
                SelectedTour.TourParticipants.Add(participantDto);
                TourParticipants.Add(participantDto);
                CombinedList.Add(new ParticipantAttendance { FirstName = participantDto.FirstName, LastName = participantDto.LastName });
            }
        }
        private void LoadTourAttendances()
        {
            // Get the list of TourReservations for the current Guide
            List<TourAttendance> attendances = new List<TourAttendance>();

            attendances = tourAttendanceService.GetTourAttendanceByReservationId(SelectedTour.Id);
            foreach (var attendance in attendances)
            {
                attendance.Tourist = touristService.GetTouristById(attendance.TouristId);
                TourAttendanceDTO attendanceDto = new TourAttendanceDTO(attendance);
                TourAttendances.Add(attendanceDto);
                CombinedList.Add(new ParticipantAttendance { FirstName = attendanceDto.Tourist.FirstName, LastName = attendanceDto.Tourist.LastName });
            }
        }

        private void NavigateBack(object sender)
        {
            Page tourDetails = new TourDetails(LoggedGuide, SelectedTour, navigationService);
            navigationService.Navigate(tourDetails);
        }
        private void NavigateToTourRequests(object sender)
        {
            Page tourRequests = new TourRequestsView(LoggedGuide, navigationService);
            navigationService.Navigate(tourRequests);
        }
    }
    public class ParticipantAttendance
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
