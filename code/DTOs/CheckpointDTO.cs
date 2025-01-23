using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.DTOs
{
    public class CheckpointDTO : ViewModelBase
    {
        private Checkpoint _checkpoint;

        public int Id
        {
            get => _checkpoint.Id;
            set
            {
                _checkpoint.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => _checkpoint.Name;
            set
            {
                _checkpoint.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int LocationId
        {
            get => _checkpoint.LocationId;
            set
            {
                _checkpoint.LocationId = value;
                OnPropertyChanged(nameof(LocationId));
            }
        }

        public bool IsActive
        {
            get => _checkpoint.IsActive;
            set
            {
                _checkpoint.IsActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        public int TourId
        {
            get => _checkpoint.TourId;
            set
            {
                _checkpoint.TourId = value;
                OnPropertyChanged(nameof(TourId));
            }
        }

        public CheckpointDTO(Checkpoint checkpoint)
        {
            _checkpoint = checkpoint;
        }
    }
}
