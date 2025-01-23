using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;

namespace BookingApp.DTOs
{
    public class TourParticipantDTO : ViewModelBase
    {
        private TourParticipant _tourParticipant;

        public int Id
        {
            get => _tourParticipant.Id;
            set
            {
                _tourParticipant.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int TourReservationId
        {
            get => (int)_tourParticipant.TourReservationId;
            set
            {
                _tourParticipant.TourReservationId = value;
                OnPropertyChanged(nameof(TourReservationId));
            }
        }

        public TourReservationDTO TourReservation { get; set; }

        public string FirstName
        {
            get => _tourParticipant.FirstName;
            set
            {
                _tourParticipant.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _tourParticipant.LastName;
            set
            {
                _tourParticipant.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Email
        {
            get => _tourParticipant.Email;
            set
            {
                _tourParticipant.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public int Age
        {
            get => _tourParticipant.Age;
            set
            {
                _tourParticipant.Age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public bool IsPresent
        {
            get => _tourParticipant.IsPresent;
            set
            {
                _tourParticipant.IsPresent = value;
                OnPropertyChanged(nameof(IsPresent));
            }
        }

        public int? CheckpointJoinedId
        {
            get => _tourParticipant.CheckpointJoinedId;
            set
            {
                _tourParticipant.CheckpointJoinedId = value;
                OnPropertyChanged(nameof(CheckpointJoinedId));
            }
        }

        public Checkpoint CheckpointJoined { get; set; }

        public TourParticipantDTO(TourParticipant tourParticipant)
        {
            _tourParticipant = tourParticipant;
        }
    }
}
