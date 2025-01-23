using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.View;
using BookingApp.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.OwnerServices
{
    public class RateGuestService
    {
        #region Data
        
        private readonly IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();
        private readonly IAccommodationReservationRepository _accommodationReservationRepository = DependencyContainer.GetInstance<IAccommodationReservationRepository>();
        private readonly IGuestRepository _guestRepository = DependencyContainer.GetInstance<IGuestRepository>();
        private readonly IAccommodationReservationReviewRepository _accommodationReservationReviewRepository = DependencyContainer.GetInstance<IAccommodationReservationReviewRepository>();
        #endregion

        
        public AccommodationReservationReview GetAccommodationReservationReviewById(int id, out bool _verify)
        {
           _verify = true;
           return _accommodationReservationReviewRepository.GetById(id,out _verify);
        }

        public List<AccommodationReservation> GetAllReservations()
        {
           return _accommodationReservationRepository.GetAll();
        }

        public void UpdateStates(AccommodationReservation reservation)
        {
            _accommodationReservationRepository.UpdateStates(reservation);
        }
        /*
         rateGuestViewModel.AccommodationName = _accommodationRepository.GetByOwnerId(UserSession.Instance.Id).Name;
                    rateGuestViewModel.GuestName = _guestRepository.GetById(accommodationReservation.GuestId).FirstName + " " + _guestRepository.GetById(accommodationReservation.GuestId).LastName;
                    rateGuestViewModel.OwnerId = UserSession.Instance.Id;
                    rateGuestViewModel.AccommodationId = _accommodationRepository.GetByOwnerId(UserSession.Instance.Id).Id;
         */
        public string GetAccommodationName(int id)
        {
            return _accommodationRepository.GetById(id).Name;
        }

        public string GetGuestFullNameByGuestId(int id)
        {
            return _guestRepository.GetById(id).FirstName + " " + _guestRepository.GetById(id).LastName;
        }

        public int GetAccommodationId()
        {
            return _accommodationRepository.GetByOwnerId(UserSession.Instance.Id).Id;
        }


    }
}
