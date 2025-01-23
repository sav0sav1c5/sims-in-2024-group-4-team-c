using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.OwnerServices
{
    public class AccommodationRegisterService
    {
        #region Data
        private readonly ILocationRepository _locationRepository = DependencyContainer.GetInstance<ILocationRepository>();
        private IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();
        public ObservableCollection<string> ComboBoxAccommodationTypes { get; set; } = new ObservableCollection<string>();
        #endregion

        public List<Location> GetAllLocations()
        {
            return _locationRepository.GetAll();
        }

        public void SaveAccommodation(Accommodation accommodation)
        {
            _accommodationRepository.Save(accommodation);
        }

        public Location GetByCity(string city)
        {
            return _locationRepository.GetByCity(city);
        }

        public Location GetByCityAndCountry(string country,string city)
        {
            return _locationRepository.GetByCountryAndCity(country, city);
        }

        public void SaveLocation(Location location)
        {
            _locationRepository.Save(location);
        }

    }
}
