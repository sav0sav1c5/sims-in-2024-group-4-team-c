using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;

namespace BookingApp.DTOs
{
    public class TouristDTO : ViewModelBase
    {
        private Tourist _tourist;

        public int Id
        {
            get => _tourist.Id;
            set
            {
                _tourist.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Username
        {
            get => _tourist.Username;
            set
            {
                _tourist.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string FirstName
        {
            get => _tourist.FirstName;
            set
            {
                _tourist.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _tourist.LastName;
            set
            {
                _tourist.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Email
        {
            get => _tourist.Email;
            set
            {
                _tourist.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public TouristDTO(Tourist tourist)
        {
            _tourist = tourist;
        }
    }
}
