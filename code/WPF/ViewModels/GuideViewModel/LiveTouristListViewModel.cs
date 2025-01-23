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

namespace BookingApp.WPF.ViewModels.GuideViewModel
{
    public class LiveTouristListViewModel : ViewModelBase
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
        public RelayCommand CheckboxCheckedCommand { get; }
        public RelayCommand GoBackCommand { get; }
        public RelayCommand TourRequestsCommand { get; }
        public RelayCommand TouristListCommand { get; }

        public LiveTouristListViewModel(Guide guide, TourReservationDTO tour, NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoggedGuide = guide;
            SelectedTour = tour; // Initialize SelectedTour here
            LoadTourParticipants();
            LoadTourAttendances();
            TourRequestsCommand = new RelayCommand(NavigateToTourRequests);
            GoBackCommand = new RelayCommand(NavigateBack);
            CheckboxCheckedCommand = new RelayCommand(CheckboxChecked);
        }

        private void LoadTourParticipants()
        {
            // Get the list of TourReservations for the current Guide
            List<TourParticipant> participants = new List<TourParticipant>();
            participants = tourParticipantService.GetTourParticipantsByTourReservationId(SelectedTour.Id);
            participants.Sort((x, y) => x.Id.CompareTo(y.Id));
            foreach (var participant in participants)
            {
                TourParticipantDTO participantDto = new TourParticipantDTO(participant);
                SelectedTour.TourParticipants.Add(participantDto);
                TourParticipants.Add(participantDto);
                string checkpointName = "";
                if (participantDto.CheckpointJoinedId.HasValue)
                {
                    if (participantDto.CheckpointJoinedId != 0)
                    {
                        participantDto.CheckpointJoined = checkPointService.GetCheckpointById(participantDto.CheckpointJoinedId.Value);
                        checkpointName = participantDto.CheckpointJoined != null ? participantDto.CheckpointJoined.Name : "";
                    }
                }
                CombinedList.Add(new LiveParticipantAttendance { Id = participant.Id, FirstName = participantDto.FirstName, LastName = participantDto.LastName, CheckpointJoined = checkpointName, IsPresent = participantDto.IsPresent });
            }
        }
        private void LoadTourAttendances()
        {

            // Get the list of TourReservations for the current Guide
            List<TourAttendance> attendances = new List<TourAttendance>();

            attendances = tourAttendanceService.GetTourAttendanceByReservationId(SelectedTour.Id);
            attendances.Sort((x, y) => x.Id.CompareTo(y.Id));
            foreach (var attendance in attendances)
            {
                attendance.Tourist = touristService.GetTouristById(attendance.TouristId);
                TourAttendanceDTO attendanceDto = new TourAttendanceDTO(attendance);
                TourAttendances.Add(attendanceDto);
                string checkpointName = "";
                if (attendanceDto.CheckpointJoinedId.HasValue)
                {
                    if (attendanceDto.CheckpointJoinedId != 0)
                    {
                        attendanceDto.CheckpointJoined = checkPointService.GetCheckpointById(attendanceDto.CheckpointJoinedId.Value);
                        checkpointName = attendanceDto.CheckpointJoined != null ? attendanceDto.CheckpointJoined.Name : "";
                    }
                }

                CombinedList.Add(new LiveParticipantAttendance { Id = attendance.Id, FirstName = attendanceDto.Tourist.FirstName, LastName = attendanceDto.Tourist.LastName, CheckpointJoined = checkpointName, IsPresent = attendanceDto.IsPresent });
            }
        }

        public void CheckboxChecked(object sender)
        {
            LiveParticipantAttendance participant = (LiveParticipantAttendance)sender;

            List<TourParticipant> participants = tourParticipantService.GetAllTourParticipants();
            bool test = false;
            foreach(var part in participants)
            {
                if(part.FirstName == participant.FirstName && part.LastName == participant.LastName)
                {
                    test = true;
                    break;
                }
            }

            if(test)
            {
                TourParticipant tourParticipant = tourParticipantService.GetTourParticipantById((participant.Id));
                tourParticipant.IsPresent = participant.IsPresent;
                tourParticipant.CheckpointJoinedId = participant.CheckpointJoinedId;

                List<Checkpoint> checkpoints = checkPointService.GetCheckpointsByTourId(SelectedTour.TourId);

                int currentCheckpointIndex = 0;
                foreach (var checkpoint in checkpoints)
                {
                    if (checkpoint.IsActive)
                    {
                        currentCheckpointIndex = checkpoint.Id;
                        break;
                    }
                }

                tourParticipant.CheckpointJoinedId = currentCheckpointIndex;

                if(participant.IsPresent)
                {
                    tourParticipantService.UpdateTourParticipant(tourParticipant);
                } else
                {
                    tourParticipant.CheckpointJoinedId = 0;
                    tourParticipantService.UpdateTourParticipant(tourParticipant);
                }
                CombinedList.Clear();
                LoadTourParticipants();
                LoadTourAttendances();
                OnPropertyChanged(nameof(TourParticipants));
                OnPropertyChanged(nameof(CombinedList));
            } else
            {
                TourAttendance tourAttendance = tourAttendanceService.GetTourAttendanceById((participant.Id));
                tourAttendance.IsPresent = participant.IsPresent;
                tourAttendance.CheckpointJoinedId = participant.CheckpointJoinedId;

                List<Checkpoint> checkpoints = checkPointService.GetCheckpointsByTourId(SelectedTour.TourId);

                int currentCheckpointIndex = 0;
                foreach (var checkpoint in checkpoints)
                {
                    if (checkpoint.IsActive)
                    {
                        currentCheckpointIndex = checkpoint.Id;
                        break;
                    }
                }
                tourAttendance.CheckpointJoinedId = currentCheckpointIndex;

                if (participant.IsPresent)
                {
                    tourAttendanceService.UpdateTourAttendance(tourAttendance);
                }
                else
                {
                    tourAttendance.CheckpointJoinedId = 0;
                    tourAttendanceService.UpdateTourAttendance(tourAttendance);
                }
                CombinedList.Clear();
                LoadTourParticipants();
                LoadTourAttendances();
                OnPropertyChanged(nameof(TourAttendances));
                OnPropertyChanged(nameof(CombinedList));
            }
        }


        private void NavigateBack(object sender)
        {
            Page LiveTourView = new LiveTourView(LoggedGuide, SelectedTour, navigationService);
            navigationService.Navigate(LiveTourView);
        }
        private void NavigateToTourRequests(object sender)
        {
            Page tourRequests = new TourRequestsView(LoggedGuide, navigationService);
            navigationService.Navigate(tourRequests);
        }
    }
    public class LiveParticipantAttendance
    {
        public int Id { get; set; }
        public int CheckpointJoinedId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CheckpointJoined { get; set; }
        public bool IsPresent { get; set; }
    }
}
