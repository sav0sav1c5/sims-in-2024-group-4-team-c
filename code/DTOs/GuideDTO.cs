using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;

namespace BookingApp.DTOs
{
    public class GuideDTO : ViewModelBase
    {
        private Guide _guide;

        public int Id
        {
            get => _guide.Id;
            set
            {
                _guide.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Username
        {
            get => _guide.Username;
            set
            {
                _guide.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string FirstName
        {
            get => _guide.FirstName;
            set
            {
                _guide.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _guide.LastName;
            set
            {
                _guide.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Email
        {
            get => _guide.Email;
            set
            {
                _guide.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public GuideDTO(Guide guide)
        {
            _guide = guide;
        }
    }
}
