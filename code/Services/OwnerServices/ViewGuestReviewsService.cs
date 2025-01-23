using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.OwnerServices
{
    public class ViewGuestReviewsService
    {
        private readonly IAccommodationReservationReviewRepository _accommodationReservationReviewRepository = DependencyContainer.GetInstance<IAccommodationReservationReviewRepository>();
        private readonly IGuestRepository _guestRepository = DependencyContainer.GetInstance<IGuestRepository>();
        private readonly IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();


        public List<AccommodationReservationReview> GetAllReservationReviews()
        {
            return _accommodationReservationReviewRepository.GetAll();
        }


        public string GetAccommodationName(int accommodationId)
        {
            return _accommodationRepository.GetById(accommodationId).Name;
        }

        public Guest GetGuest(int guestId)
        {
            return _guestRepository.GetById(guestId);
        }




        
        
        

    }
}
