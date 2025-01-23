using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class LocationRepository : ILocationRepository
    {
        private List<Location> _locations;
        private readonly IDataHandler<Location> _locationDataHandler;

        public LocationRepository()
        {
            _locationDataHandler = new LocationDataHandler();
            _locations = _locationDataHandler.GetAll().ToList();
        }

        public void Save(Location location)
        {
            if (_locations == null) { }
            _locationDataHandler.SaveOneEntity(location);
            _locations!.Add(location);
        }

        public Location? GetByCity(string city)
        {
            return _locations.Find(locations => locations.City == city);
        }

        public List<Location>? GetByCountry(string country)
        {
            return _locations.FindAll(locations => locations.Country == country);
        }

        public Location? GetByCountryAndCity(string country, string city)
        {
            return _locations.Find(locations => locations.Country == country && locations.City == city);
        }

        public Location? GetById(int id)
        {
            return _locations.FirstOrDefault(locations => locations.Id == id);
        }

        public List<Location> GetAll()
        {
            return _locations;
        }

        public void Update(Location location)
        {
            _locationDataHandler.Update(location);
        }

        public void DeleteById(int id)
        {
            _locationDataHandler.DeleteById(id);
            _locations.RemoveAll(x => x.Id == id);
        }

        public void Delete(Location location)
        {
            _locationDataHandler.Delete(location);
            _locations.Remove(location);
        }

        public List<string> GetAllDistinctCountries()
        {
            return _locations.Select(location => location.Country)
                            .Distinct()
                            .ToList();
        }

        public List<string> GetAllDistinctCities()
        {
            return _locations.Select(location => location.City)
                            .Distinct()
                            .ToList();
        }

        public List<string> GetAllDistinctCitiesByCountry(string country)
        {
            return _locations.Where(location => location.Country.Equals(country))
                .Select(location => location.City)
                .Distinct()
                .ToList();
        }
    }
}
