using BookingApp.Domain.Model;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface ILocationRepository : IBaseRepository<Location>
    {
        public Location? GetByCity(string city);

        public List<Location>? GetByCountry(string country);

        public Location? GetByCountryAndCity(string country, string city);

        public List<string> GetAllDistinctCountries();

        public List<string> GetAllDistinctCities();

        public List<string> GetAllDistinctCitiesByCountry(string country);
    }
}
