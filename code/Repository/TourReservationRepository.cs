using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;

namespace BookingApp.Repository
{
    public class TourReservationRepository : ITourReservationRepository
    {
        private List<TourReservation> tourReservations;
        private readonly IDataHandler<TourReservation> tourReservationDataHandler;

        public TourReservationRepository()
        {
            tourReservationDataHandler = new TourReservationDataHandler();
            tourReservations = tourReservationDataHandler.GetAll().ToList();
        }

        public List<TourReservation> GetAll()
        {
            return tourReservations;
        }

        public TourReservation GetById(int id)
        {
            TourReservation? tourReservation = tourReservations.FirstOrDefault(tourReservation => tourReservation.Id == id);
            if (tourReservation == null)
            {
                throw new ArgumentException("Tour not found", nameof(id));
            }
            return tourReservation;
        }

        public List<TourReservation> GetByTourId(int tourId)
        {
            return tourReservations.FindAll(tourReservation => tourReservation.TourId == tourId);
        }
        public List<TourReservation> GetByGuideId(int guideId)
        {
            return tourReservations.FindAll(tourReservation => tourReservation.GuideId == guideId);
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            tourReservations.Add(tourReservation);
            return tourReservationDataHandler.SaveOneEntity(tourReservation);
        }

        public void Update(TourReservation tourReservation)
        {
            tourReservationDataHandler.Update(tourReservation);
        }

        public void Delete(TourReservation tourReservation)
        {
            tourReservationDataHandler.Delete(tourReservation);
        }

        public void DeleteById(int tourReservationId)
        {
            var tourReservation = tourReservations.Find(tr => tr.Id == tourReservationId);
            if (tourReservation != null)
            {
                tourReservationDataHandler.DeleteById(tourReservationId);
                tourReservations.Remove(tourReservation);
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

        void IBaseRepository<TourReservation>.Save(TourReservation entity)
        {
            throw new NotImplementedException();
        }
    }
}
