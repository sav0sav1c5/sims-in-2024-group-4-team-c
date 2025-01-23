using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class TourRequestRepository : ITourRequestRepository
    {
        private List<TourRequest> tourRequests;
        private readonly IDataHandler<TourRequest> tourRequestDataHandler;

        public TourRequestRepository()
        {
            tourRequestDataHandler = new TourRequestDataHandler();
            tourRequests = tourRequestDataHandler.GetAll().ToList();
        }

        public List<TourRequest> GetAll()
        {
            return tourRequests;
        }

        public TourRequest GetById(int id)
        {
            TourRequest tourRequest = tourRequests.FirstOrDefault(tr => tr.Id == id);
            if (tourRequest == null)
            {
                throw new ArgumentException("Tour request not found", nameof(id));
            }
            return tourRequest;
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            tourRequests.Add(tourRequest);
            return tourRequestDataHandler.SaveOneEntity(tourRequest);
        }

        public void Update(TourRequest tourRequest)
        {
            tourRequestDataHandler.Update(tourRequest);
        }

        public void Delete(TourRequest tourRequest)
        {
            tourRequestDataHandler.Delete(tourRequest);
        }

        public void DeleteById(int tourRequestId)
        {
            var tourRequest = tourRequests.Find(tr => tr.Id == tourRequestId);
            if (tourRequest != null)
            {
                tourRequestDataHandler.DeleteById(tourRequestId);
                tourRequests.Remove(tourRequest);
            }
        }

        public Location? GetByCity(string city)
        {
            throw new NotImplementedException();
        }

        public List<Location>? GetByCountry(string country)
        {
            throw new NotImplementedException();
        }

        public List<Location>? GetByCountryAndCity(string country, string city)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllDistinctCountries()
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllDistinctCities()
        {
            throw new NotImplementedException();
        }

        void IBaseRepository<TourRequest>.Save(TourRequest entity)
        {
            throw new NotImplementedException();
        }

        public List<TourRequest> GetByTouristId(int touristId)
        {
            return tourRequests.Where(tr => tr.TouristId == touristId).ToList();
        }
        public List<TourRequest> GetByYear(int year)
        {
            return tourRequests.Where(tr => tr.StartDate.Year == year).ToList();
        }

    }
}
