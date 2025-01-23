using BookingApp.DependencyInjection;
using BookingApp.Repository;
using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.GuideServices
{
    public class LocationService
    {
        private readonly ILocationRepository _locationRepository = DependencyContainer.GetInstance<ILocationRepository>();

        public LocationService()
        {

        }

        public Location GetLocationById(int id)
        {
            return _locationRepository.GetById(id);
        }

        public List<Location> GetAllLocations()
        {
            return _locationRepository.GetAll();
        }

        public void SaveLocation(Location location)
        {
            _locationRepository.Save(location);
        }

        public void UpdateLocation(Location location)
        {
            _locationRepository.Update(location);
        }

        public void DeleteLocationById(int id)
        {
            _locationRepository.DeleteById(id);
        }

        public void DeleteLocation(Location location)
        {
            _locationRepository.Delete(location);
        }

        public Location GetLocationByCity(string city)
        {
            return _locationRepository.GetByCity(city);
        }

        public List<Location> GetLocationsByCountry(string country)
        {
            return _locationRepository.GetByCountry(country);
        }

        public Location GetLocationsByCountryAndCity(string country, string city)
        {
            return _locationRepository.GetByCountryAndCity(country, city);
        }

        public List<string> GetAllDistinctCountries()
        {
            return _locationRepository.GetAllDistinctCountries();
        }

        public List<string> GetAllDistinctCities()
        {
            return _locationRepository.GetAllDistinctCities();
        }
    }
}
