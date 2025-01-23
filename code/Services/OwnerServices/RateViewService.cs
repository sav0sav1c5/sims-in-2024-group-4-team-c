using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.OwnerServices
{
    public class RateViewService
    {
        private IAccommodationReservationReviewRepository _accommodationReservationReviewRepository = DependencyContainer.GetInstance<IAccommodationReservationReviewRepository>();

        public void SaveAccommodationReservationReview(AccommodationReservationReview accommodationReservationReview)
        {
            _accommodationReservationReviewRepository.Save(accommodationReservationReview);
        }
    }
}
