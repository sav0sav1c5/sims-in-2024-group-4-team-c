using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class LocationDTO:ViewModelBase
    {
        private Location _location;

        public int Id
        {
            get => _location.Id;
            set
            {
                _location.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string City
        {
            get => _location.City;
            set
            {
                _location.City = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public string Country
        {
            get => _location.Country;
            set
            {
                _location.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public int count { get; set; } = 0;

        public LocationDTO() { }

        public LocationDTO(int id, int count)
        {
            _location = new Location();
            Id = id;
            this.count = count;
        }
    }
}
