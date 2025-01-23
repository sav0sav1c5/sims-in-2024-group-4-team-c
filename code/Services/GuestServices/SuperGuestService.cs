using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.GuestServices
{
    public class SuperGuestService
    {
        #region Data
        private readonly IGuestRepository _guestRepository = DependencyContainer.GetInstance<IGuestRepository>();
        private readonly IAccommodationReservationRepository _accommodationReservationRepository = DependencyContainer.GetInstance<IAccommodationReservationRepository>();
        private readonly ICurrentDateRepository _currentDateRepository = DependencyContainer.GetInstance<ICurrentDateRepository>();
        private readonly DateTime CurrentDate;
        #endregion

        public SuperGuestService() 
        {
            CurrentDate = _currentDateRepository.Get()!.Date;
        }

        /// <summary>
        /// </summary>
        /// <returns>Guest object with updated SuperGuestCouponCount and IsSuperGuest fields.</returns>
        public Guest UpdateSuperGuestStatus(Guest guest)
        {
            if (IsSuperGuestRequirementFulfilled(guest))
            {
                if (!guest.IsSuperGuest)
                {
                    guest.SetToSuperGuest();
                }
            }
            else
            {
                guest.SetToNormalGuest();
            }
            _guestRepository.Update(guest);
            return guest;
        }

        public bool IsSuperGuestRequirementFulfilled(Guest guest) 
        {
            List<AccommodationReservation> guestsReservationsFromOneYearAgo =
                _accommodationReservationRepository.GetReservationsByGuestIdBetweenTimePoints(
                    guest.Id,
                    CurrentDate.AddYears(-1),
                    CurrentDate
                );
            AccommodationReservation latestGuestReservation = guestsReservationsFromOneYearAgo[^1];
            return guestsReservationsFromOneYearAgo.Count >= Guest.MinReservationsNeededForSuperGuest 
                && latestGuestReservation.StartDate.AddDays(latestGuestReservation.StayLength) < CurrentDate;
        }

        

        /// <summary>
        /// Updates the database aswel
        /// </summary>
        /// <param name="guestId"></param>
        public void DecreaseSuperGuestDiscountCouponByGuestId(int guestId)
        {
            Guest? guest = _guestRepository.GetById(guestId);
            if (guest == null || !guest.IsSuperGuest)
            {
                return ;
            }

            if (guest.SuperGuestDiscountCouponCount > 0)
            {
                guest.SuperGuestDiscountCouponCount -= 1;
                _guestRepository.Update(guest);
            }
            //return guest.SuperGuestDiscountCouponCount;
        }
    }
}
