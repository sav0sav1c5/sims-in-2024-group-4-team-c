using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using BookingApp.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Input;
using BookingApp.Services.GuideServices;
using System.Printing;
using BookingApp.DependencyInjection;
using BookingApp.Services;
using BookingApp.DTOs;
using BookingApp.WPF.Views.GuideView;
using Microsoft.VisualBasic;

namespace BookingApp.WPF.ViewModels.GuideViewModel
{
    public class LiveTourViewModel : ViewModelBase
    {
        public NavigationService navigationService { get; set; }

        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();
        private readonly CheckpointService checkPointService = DependencyContainer.GetInstance<CheckpointService>();
        private readonly TourParticipantService tourParticipantService = DependencyContainer.GetInstance<TourParticipantService>();
        private readonly TourAttendanceService tourAttendanceService = DependencyContainer.GetInstance<TourAttendanceService>();
        private readonly TouristService touristService = DependencyContainer.GetInstance<TouristService>();

        public TourReservationDTO SelectedTour { get; set; }

        public ObservableCollection<TourParticipantDTO> TourParticipants { get; set; } = new ObservableCollection<TourParticipantDTO>();
        public ObservableCollection<TourAttendanceDTO> TourAttendances { get; set; } = new ObservableCollection<TourAttendanceDTO>();

        public ObservableCollection<object> CombinedList { get; set; } = new ObservableCollection<object>();

        public ObservableCollection<CheckpointDTO> Checkpoints { get; set; } = new ObservableCollection<CheckpointDTO>();

        public string TourName => SelectedTour.Tour.Name;
        public static Guide LoggedGuide { get; set; }

        // RelayCommands
        public RelayCommand BackCommand { get; set; }
        public RelayCommand TouristListCommand { get; set; }
        public RelayCommand NextCheckpointCommand { get; set; }
        public RelayCommand CheckboxCheckedCommand { get; set; }
        public RelayCommand FinishTourCommand { get; set; }

        public LiveTourViewModel(Guide guide, TourReservationDTO tour, NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoggedGuide = guide ?? throw new ArgumentNullException(nameof(guide));
            SelectedTour = tour ?? throw new ArgumentNullException(nameof(tour));

            LoadTourParticipants();
            LoadTourAttendances();
            LoadCheckpoints();

            // Initialize RelayCommands
            BackCommand = new RelayCommand(NavigateBack);
            NextCheckpointCommand = new RelayCommand(NextCheckpoint);
            CheckboxCheckedCommand = new RelayCommand(CheckboxChecked);
            TouristListCommand = new RelayCommand(_=> ShowTouristList());
            FinishTourCommand = new RelayCommand(_ => FinishTour());
        }
        private void LoadTourAttendances()
        {

            List<TourAttendance> attendances = tourAttendanceService.GetTourAttendanceByReservationId(SelectedTour.Id);

            foreach (var attendance in attendances)
            {
                TouristDTO touristDTO = null;
                Tourist tourist = touristService.GetTouristById(attendance.TouristId);
                if (tourist != null)
                {
                    touristDTO = new TouristDTO(tourist);
                }

                TourAttendanceDTO attendanceDTO = new TourAttendanceDTO(attendance)
                {
                    Tourist = touristDTO,
                    CheckpointJoined = null // You can populate this if needed
                };

                TourAttendances.Add(attendanceDTO);
                CombinedList.Add(attendanceDTO);
            }
            
        }

        private void LoadTourParticipants()
        {
            List<TourParticipant> participants = tourParticipantService.GetTourParticipantsByTourReservationId(SelectedTour.Id);

            foreach (var participant in participants)
            {
                TourParticipantDTO participantDTO = new TourParticipantDTO(participant);

                TourParticipants.Add(participantDTO);
                CombinedList.Add(participantDTO);
            }
        }


        private void LoadCheckpoints()
        {
            Checkpoints.Clear();
            List<Checkpoint> checkpoints = checkPointService.GetCheckpointsByTourId(SelectedTour.TourId);

            foreach (var checkpoint in checkpoints)
            {
                CheckpointDTO checkpointDTO = new CheckpointDTO(checkpoint);
                Checkpoints.Add(checkpointDTO);
            }
        }
        private void Back(object sender)
        {
            Page GuideHomeView = new GuideHomeView(LoggedGuide, navigationService);
            navigationService.Navigate(GuideHomeView);
        }
        public void CheckboxChecked(object sender)
        {
            // Get the TourParticipant object corresponding to the checked CheckBox
            if (sender is CheckBox checkBox && checkBox.DataContext is TourParticipant participant)
            {
                // Update the IsPresent state of the TourParticipant to true
                participant.IsPresent = true;
                participant.CheckpointJoinedId = Checkpoints.FirstOrDefault(cp => cp.IsActive).Id;

                // Save the updated TourParticipant to the repository or database
                tourParticipantService.UpdateTourParticipant(participant);
            }
        }

        private void NextCheckpoint(object sender)
        {
            // Find the currently active checkpoint
            CheckpointDTO currentCheckpoint = Checkpoints.FirstOrDefault(cp => cp.IsActive);

            if (currentCheckpoint != null)
            {
                // Find the index of the current checkpoint
                int currentIndex = Checkpoints.IndexOf(currentCheckpoint);

                // If there is a next checkpoint in the list
                if (currentIndex < Checkpoints.Count - 1)
                {
                    // Deactivate the current checkpoint
                    currentCheckpoint.IsActive = false;

                    // Activate the next checkpoint
                    CheckpointDTO nextCheckpoint = Checkpoints[currentIndex + 1];
                    nextCheckpoint.IsActive = true;

                    // Save the changes to the repository or database
                    Checkpoint current = new Checkpoint(currentCheckpoint.Id, currentCheckpoint.Name, currentCheckpoint.LocationId, currentCheckpoint.TourId, currentCheckpoint.IsActive);
                    checkPointService.UpdateCheckpoint(current);
                    Checkpoint next = new Checkpoint(nextCheckpoint.Id, nextCheckpoint.Name, nextCheckpoint.LocationId, nextCheckpoint.TourId, nextCheckpoint.IsActive);
                    checkPointService.UpdateCheckpoint(next);

                    LoadCheckpoints();

                    MessageBox.Show("Switched to the next checkpoint successfully.");
                }
                else
                {
                    MessageBox.Show("You have reached the last checkpoint.");
                }
            }
            else
            {
                MessageBox.Show("No active checkpoint found.");
            }
        }

        private void RefreshCheckpointsUI()
        {
            // Clear and re-add checkpoints to update UI bindings
            Checkpoints.Clear();
            List<Checkpoint> updatedCheckpoints = checkPointService.GetCheckpointsByTourId(SelectedTour.Id);
            foreach (var checkpoint in updatedCheckpoints)
            {
                CheckpointDTO checkpointDTO = new CheckpointDTO(checkpoint);
                Checkpoints.Add(checkpointDTO);
            }
        }
        public void FinishTour()
        {
            TourReservation tourReservation = tourReservationService.GetTourReservationById(SelectedTour.Id);
            tourReservation.TourState = TourStates.Type.Finished;
            tourReservationService.UpdateTourReservation(tourReservation);
              
            MessageBox.Show("Tour finished successfully.");
            Back(null);
        }
        private void ShowTouristList()
        {
            LiveTouristListView touristListView = new LiveTouristListView(LoggedGuide, SelectedTour, navigationService);
            navigationService.Navigate(touristListView);
        }
        private void NavigateBack(object sender)
        {
            Page ScheduledTour = new ScheduledToursView(LoggedGuide, navigationService);
            navigationService.Navigate(ScheduledTour);
        }
    }
}
