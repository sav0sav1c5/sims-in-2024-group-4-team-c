using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.OwnerServices
{
    public class StatisticService
    {
        private readonly IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();
        private readonly IAccommodationReservationModificationRequestRepository _accommodationReservationModificationRequestRepository = DependencyContainer.GetInstance<IAccommodationReservationModificationRequestRepository>();
        private readonly IAccommodationReservationRepository _accommodationReservationRepository = DependencyContainer.GetInstance<IAccommodationReservationRepository>();
        private readonly IAccommodationReservationReviewRepository _accommodationReservationReviewRepository = DependencyContainer.GetInstance<IAccommodationReservationReviewRepository>();

        public List<Accommodation> GetAllOwnerAccommodations(int id)
        {
            List<Accommodation> data = _accommodationRepository.GetAll().Where(accommodation => accommodation.OwnerId == id).ToList();
            return data;
        }

        public List<int> getAllYears(int accommodationId)
        {
            HashSet<int> uniqueYears = new HashSet<int>();
            var reservations = _accommodationReservationRepository.GetAll().Where(reservation => reservation.AccommodationId == accommodationId);
            foreach(var reservation in reservations)
            {
                uniqueYears.Add(reservation.StartDate.Year);
            }

            var reservationModification = _accommodationReservationModificationRequestRepository.GetAll().Where(reservation => reservation.RequestState == ReservationModificationRequestState.State.Denied).ToList();
            foreach(var resMod in reservationModification)
            {
                var temp = _accommodationReservationRepository.GetAll().Where(reservation => reservation.Id == resMod.AccommodationReservationId).First();
                if(temp.AccommodationId == accommodationId)
                {
                    uniqueYears.Add(resMod.StartDate.Year);
                }
            }
            var reservationModification1 = _accommodationReservationModificationRequestRepository.GetAll().Where(reservation => reservation.RequestState == ReservationModificationRequestState.State.Approved).ToList();
            foreach (var resMod in reservationModification1)
            {
                var temp = _accommodationReservationRepository.GetAll().Where(reservation => reservation.Id == resMod.AccommodationReservationId).First();
                if (temp.AccommodationId == accommodationId)
                {
                    uniqueYears.Add(resMod.StartDate.Year);
                }
            }
            List<int> years = new List<int>();
            foreach(var y in uniqueYears)
            {
                years.Add(y);
            }

            return years;

        }

        public int GetAccommodationReservationByYear(int year, int accommodationId)
        {
            return _accommodationReservationRepository.GetAll().Where(reservation => reservation.AccommodationId == accommodationId && reservation.StartDate.Year == year).Count();  
        }

        public int GetCanceledReservationByYear(int year,int accommodationId)
        {
            int count = 0;
            var reservationModification = _accommodationReservationModificationRequestRepository.GetAll().Where(reservation => reservation.RequestState == ReservationModificationRequestState.State.Denied && reservation.StartDate.Year == year).ToList();
            foreach (var resMod in reservationModification)
            {
                var temp = _accommodationReservationRepository.GetAll().Where(reservation => reservation.Id == resMod.AccommodationReservationId).First();
                if (temp.AccommodationId == accommodationId)
                {
                    count++;
                }
            }
            return count;
        }

        public int GetModifiedReservationByYear(int year, int accommodationId)
        {
            int count = 0;
            var reservationModification = _accommodationReservationModificationRequestRepository.GetAll().Where(reservation => reservation.RequestState == ReservationModificationRequestState.State.Approved && reservation.StartDate.Year == year).ToList();
            foreach (var resMod in reservationModification)
            {
                var temp = _accommodationReservationRepository.GetAll().Where(reservation => reservation.Id == resMod.AccommodationReservationId).First();
                if (temp.AccommodationId == accommodationId)
                {
                    count++;
                }
            }
            return count;
        }

        public int GetRenovationRequestsByYear(int year, int accommodationId)
        {
            int count = 0;
            var reviews = _accommodationReservationReviewRepository.GetAll().Where(accommodation => accommodation.AccommodationId == accommodationId && accommodation.IsInNeedOfRenovation == true).ToList();
            foreach(var review in reviews)
            {
                var reservation = _accommodationReservationRepository.GetAll().Where(accommodation => accommodation.Id == review.AccommodationReservationId).First();
                if(reservation.StartDate.Year == year)
                {
                    count++;
                }
            }
            return count;
        }
        
    }
}
