using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.OwnerServices
{
    public class StatisticMonthService
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

        public List<int> getAllMonths()
        {
            List<int> months = Enumerable.Range(1, 12).ToList();
            return months;
        }

        public int GetAccommodationReservationByYearAndMonth(int year,int month, int accommodationId)
        {
            return _accommodationReservationRepository.GetAll().Where(reservation => reservation.AccommodationId == accommodationId && reservation.StartDate.Year == year  && reservation.StartDate.Month == month).Count();
        }

        public int GetCanceledReservationByYearAndMonth(int year,int month, int accommodationId)
        {
            int count = 0;
            var reservationModification = _accommodationReservationModificationRequestRepository.GetAll().Where(reservation => reservation.RequestState == ReservationModificationRequestState.State.Denied && reservation.StartDate.Year == year && reservation.StartDate.Month == month).ToList();
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

        public int GetModifiedReservationByYearAndMonth(int year,int month, int accommodationId)
        {
            int count = 0;
            var reservationModification = _accommodationReservationModificationRequestRepository.GetAll().Where(reservation => reservation.RequestState == ReservationModificationRequestState.State.Approved && reservation.StartDate.Year == year && reservation.StartDate.Month == month).ToList();
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

        public int GetRenovationRequestsByYearAndMonth(int year,int month, int accommodationId)
        {
            int count = 0;
            var reviews = _accommodationReservationReviewRepository.GetAll().Where(accommodation => accommodation.AccommodationId == accommodationId && accommodation.IsInNeedOfRenovation == true).ToList();
            foreach (var review in reviews)
            {
                var reservation = _accommodationReservationRepository.GetAll().Where(accommodation => accommodation.Id == review.AccommodationReservationId).First();
                if (reservation.StartDate.Year == year && reservation.StartDate.Month == month)
                {
                    count++;
                }
            }
            return count;
        }


    }
}
