using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Resources.DBAcces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services.GuideServices
{
    public class TourReservationService
    {
        private readonly ITourReservationRepository _tourReservationRepository = DependencyContainer.GetInstance<ITourReservationRepository>();

        public TourReservationService()
        {
        }

        public List<TourReservation> GetAllTourReservations()
        {
            return _tourReservationRepository.GetAll();
        }

        public TourReservation GetTourReservationById(int id)
        {
            return _tourReservationRepository.GetById(id);
        }

        public List<TourReservation> GetTourReservationsByTourId(int tourId)
        {
            return _tourReservationRepository.GetByTourId(tourId);
        }

        public List<TourReservation> GetTourReservationsByGuideId(int guideId)
        {
            return _tourReservationRepository.GetByGuideId(guideId);
        }

        public TourReservation SaveTourReservation(TourReservation tourReservation)
        {
           return _tourReservationRepository.Save(tourReservation);
        }

        public void UpdateTourReservation(TourReservation tourReservation)
        {
            _tourReservationRepository.Update(tourReservation);
        }

        public void DeleteTourReservationById(int id)
        {
            _tourReservationRepository.DeleteById(id);
        }

        public void DeleteTourReservation(TourReservation tourReservation)
        {
            _tourReservationRepository.Delete(tourReservation);
        }

        public TourReservation FindFirstReservation(int tourId)
        {
            List<TourReservation> reservations = _tourReservationRepository.GetByTourId(tourId);
            return reservations.OrderBy(r => r.DateAndTime).FirstOrDefault();
        }

        public List<DateTime> GetReservationDatesForTour(int tourId)
        {
            List<TourReservation> reservations = _tourReservationRepository.GetByTourId(tourId);
            List<DateTime> reservationDates = reservations.Select(r => r.DateAndTime).ToList();
            return reservationDates;
        }
    }
}
