using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;

namespace BookingApp.Repository
{
    public interface ITourRequestRepository : IBaseRepository<TourRequest>
    {
        TourRequest GetById(int id);
        List<TourRequest> GetByTouristId(int touristId);
        TourRequest Save(TourRequest tourRequest);
        void Update(TourRequest tourRequest);
        void Delete(TourRequest tourRequest);
        void DeleteById(int tourRequestId);
        Location? GetByCity(string city);
        List<Location>? GetByCountry(string country);
        List<Location>? GetByCountryAndCity(string country, string city);
        List<string> GetAllDistinctCountries();
        List<string> GetAllDistinctCities();
        List<TourRequest> GetByYear(int year);

    }
}
