using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using BookingApp.Services;
using BookingApp.Services.GuideServices;
using BookingApp.View.GuideView;
using BookingApp.WPF.Views.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuideViewModel
{
    public class TourDetailsViewModel : ViewModelBase
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
        public RelayCommand CancelTourCommand { get; }
        public RelayCommand TouristListCommand { get; }

        public TourDetailsViewModel(Guide guide, TourReservationDTO tour, NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoggedGuide = guide;
            SelectedTour = tour; // Initialize SelectedTour here
            LoadTourParticipants();
            LoadTourAttendances();
            LoadCheckpoints();

            // Initialize commands
            StartTourCommand = new RelayCommand(_=>StartSelectedTour());
            GoBackCommand = new RelayCommand(NavigateBack);
            CancelTourCommand = new RelayCommand(_ => CancelTour());
            TouristListCommand = new RelayCommand(_ => ShowTouristList());
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

            // After canceling the tour, navigate to the page with all tours
            NavigateToAllToursPage();
            
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

        private void NavigateToAllToursPage()
        {
            // Navigate to the page with all tours
            ScheduledToursView scheduledToursView = new ScheduledToursView(LoggedGuide, navigationService);
            navigationService.Navigate(scheduledToursView);
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


        private void LoadCheckpoints()
        {
            List<Checkpoint> checkpoints = checkPointService.GetCheckpointsByTourId(SelectedTour.Tour.Id);

            foreach (var checkpoint in checkpoints)
            {
                CheckpointDTO checkpointDTO = new CheckpointDTO(checkpoint);
                Checkpoints.Add(checkpointDTO);
            }
        }


        private void Button_Start_Tour(object sender, RoutedEventArgs e)
        {
            if (HasActiveTour())
            {
                MessageBox.Show("You already have an active tour. You can't start another one.");
                return;
            }

            StartSelectedTour();
        }

        private bool HasActiveTour()
        {
            List<Tour> tours = tourService.GetToursByGuideId(LoggedGuide.Id);
            foreach (var tour in tours)
            {
                foreach (var checkpoint in tour.Checkpoints)
                {
                    if (checkpoint.IsActive)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void StartSelectedTour()
        {
            List<TourReservation> reservations = tourReservationService.GetTourReservationsByGuideId(LoggedGuide.Id);

            foreach (var reservation in reservations)
            {
                if (reservation.TourState == TourStates.Type.Started)
                {
                    MessageBox.Show("Some tour is already started.");
                    return;
                }
            }
            Checkpoint startCheckpoint = SelectedTour.Tour.Checkpoints.First<Checkpoint>();
            startCheckpoint.IsActive = true;
            checkPointService.UpdateCheckpoint(startCheckpoint);

            SelectedTour.TourState = TourStates.Type.Started;
            //checkPointRepository.Save(startCheckpoint); // You may or may not need this line depending on your requirements
            MessageBox.Show("Tour started successfully.");
            TourReservation tourReservation = new TourReservation(SelectedTour.Id, SelectedTour.Tour.Id, SelectedTour.TourState, SelectedTour.GuideId, SelectedTour.TouristsNumber);
            tourReservationService.UpdateTourReservation(tourReservation);

            LiveTourView liveTourView = new LiveTourView(LoggedGuide, SelectedTour, navigationService);
            this.navigationService.Navigate(liveTourView);
        }


        private void NavigateBack(object sender)
        {
            Page ScheduledTour = new ScheduledToursView(LoggedGuide, navigationService);
            navigationService.Navigate(ScheduledTour);
        }
        private void ShowTouristList()
        {
            TouristListView touristListView = new TouristListView(LoggedGuide, SelectedTour, navigationService);
            navigationService.Navigate(touristListView);
        }
    }
}
