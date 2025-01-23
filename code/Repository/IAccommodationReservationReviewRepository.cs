using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal interface IAccommodationReservationReviewRepository
    {
        void Delete(AccommodationReservationReview rateGuest);
        void Save(AccommodationReservationReview rateGuest);
        List<AccommodationReservationReview> GetAll();
        void Update(AccommodationReservationReview rateGuest);

        AccommodationReservationReview GetById(int id, out bool verify);
    }
}
