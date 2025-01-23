using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;

namespace BookingApp.DTOs
{
    public class TourAttendanceDTO : ViewModelBase
    {
        private TourAttendance _tourAttendance;

        public int Id
        {
            get => _tourAttendance.Id;
            set
            {
                _tourAttendance.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int TouristId
        {
            get => _tourAttendance.TouristId;
            set
            {
                _tourAttendance.TouristId = value;
                OnPropertyChanged(nameof(TouristId));
            }
        }

        public TouristDTO Tourist { get; set; }

        public int TourReservationId
        {
            get => _tourAttendance.TourReservationId;
            set
            {
                _tourAttendance.TourReservationId = value;
                OnPropertyChanged(nameof(TourReservationId));
            }
        }

        public TourReservation TourReservation { get; set; }

        public bool IsPresent
        {
            get => _tourAttendance.IsPresent;
            set
            {
                _tourAttendance.IsPresent = value;
                OnPropertyChanged(nameof(IsPresent));
            }
        }

        public int Age
        {
            get => _tourAttendance.Age;
            set
            {
                _tourAttendance.Age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public bool IsConfirmed
        {
            get => _tourAttendance.IsConfirmed;
            set
            {
                _tourAttendance.IsConfirmed = value;
                OnPropertyChanged(nameof(IsConfirmed));
            }
        }

        public int? CheckpointJoinedId
        {
            get => _tourAttendance.CheckpointJoinedId;
            set
            {
                _tourAttendance.CheckpointJoinedId = value;
                OnPropertyChanged(nameof(CheckpointJoinedId));
            }
        }

        public Checkpoint CheckpointJoined { get; set; }

        public TourAttendanceDTO(TourAttendance tourAttendance)
        {
            _tourAttendance = tourAttendance;
            Tourist = new TouristDTO(tourAttendance.Tourist);
            TourReservation = tourAttendance.TourReservation;
            CheckpointJoined = tourAttendance.CheckpointJoined;
        }
    }
}
