using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface ITourReservationRepository : IBaseRepository<TourReservation>
    {
        TourReservation GetById(int id);
        List<TourReservation> GetByTourId(int tourId);
        List<TourReservation> GetByGuideId(int guideId);
        TourReservation Save(TourReservation tourReservation);
        void Update(TourReservation tourReservation);
        void Delete(TourReservation tourReservation);
        void DeleteById(int tourReservationId);
        Location? GetByCity(string city);
        List<Location>? GetByCountry(string country);
        List<Location>? GetByCountryAndCity(string country, string city);
        List<string> GetAllDistinctCountries();
        List<string> GetAllDistinctCities();
    }
}
