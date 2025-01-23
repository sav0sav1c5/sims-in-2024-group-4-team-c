using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Services.GuestServices
{
    public class GuestAccommodationReservationService
    {
        #region Data
        private readonly IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();
        private readonly IGuestRepository _guestRepository = DependencyContainer.GetInstance<IGuestRepository>();
        private readonly IAccommodationReservationRepository _accommodationReservationRepository = DependencyContainer.GetInstance<IAccommodationReservationRepository>();
        private readonly ICurrentDateRepository _currentDateRepository = DependencyContainer.GetInstance<ICurrentDateRepository>();
        private readonly DateTime CurrentDate;
        #endregion

        #region Services
        private readonly SuperGuestService _superGuestService = DependencyContainer.GetInstance<SuperGuestService>();
        #endregion

        public GuestAccommodationReservationService()
        {
            CurrentDate = _currentDateRepository.Get()!.Date;
        }

        public List<AccommodationReservation> GetAllAccommodationReservationsByGuestId(int guestId)
        {
            return _accommodationReservationRepository.GetByGuestId(guestId);
        }

        public AccommodationReservation? GetAccommodationReservationById(int id)
        {
            return _accommodationReservationRepository.GetById(id);
        }

        public List<AccommodationReservation> GetAccommodationReservationsByAccommodationId(int id)
        {
            return _accommodationReservationRepository.GetByAccommodationId(id);
        }

        public List<AccommodationReservationDTO> GetAllAccommodationReservationDTOsByGuestId(int guestId)
        {
            List<AccommodationReservation> reservations = _accommodationReservationRepository.GetByGuestId(guestId);
            List<AccommodationReservationDTO> reservationDTOs = new List<AccommodationReservationDTO>();
            foreach (var reservation in reservations)
            {
                reservationDTOs.Add(new AccommodationReservationDTO(reservation));
            }
            return reservationDTOs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns empty string on success and error message on error</returns>
        public string CancelReservation(AccommodationReservation accommodationReservation)
        {
            return CancelReservation(accommodationReservation.Id);
            //if (accommodationReservation.ReservationState != ReservationState.State.Reserved)
            //{
            //    return "Reservation is not in reserved state therefore it can not be canceled";
            //}
            //else if (accommodationReservation.Accommodation == null)
            //{
            //    return "Internal server error";
            //}


            ////Note: 'accommodationReservationCancelationDeadLineDate' the user can't cancel on this date
            //DateOnly accommodationReservationCancelationDeadLineDate = DateOnly.FromDateTime(
            //    accommodationReservation.StartDate.AddDays(-accommodationReservation.Accommodation.CancelationDeadline)
            //);

            //if (DateOnly.FromDateTime(CurrentDate) >= accommodationReservationCancelationDeadLineDate)
            //{
            //    return "Failed to cancel reservation due to cancelation dead line passing";
            //}


            //_accommodationReservationRepository.Delete(accommodationReservation);
            //return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns empty string on success and error message on error</returns>
        public string CancelReservation(int accommodationReservationId)
        {
            AccommodationReservation? accommodationReservation = _accommodationReservationRepository!.GetById(accommodationReservationId);
            if (accommodationReservation == null)
            {
                return "Failed to find reservation";
            }
            if (accommodationReservation.ReservationState != ReservationState.State.Reserved)
            {
                return "Reservation is not in reserved state therefore it can not be canceled";
            }
            else if (accommodationReservation.Accommodation == null)
            {
                return "Internal server error";
            }

            //Note: 'accommodationReservationCancelationDeadLineDate' the user can't cancel on this date
            DateOnly accommodationReservationCancelationDeadLineDate = DateOnly.FromDateTime(
                accommodationReservation.StartDate.AddDays(-accommodationReservation.Accommodation.CancelationDeadline)
            );

            if (DateOnly.FromDateTime(CurrentDate) >= accommodationReservationCancelationDeadLineDate)
            {
                return "Failed to cancel reservation due to cancelation dead line passing";
            }


            _accommodationReservationRepository.Delete(accommodationReservation);
            return string.Empty;
        }

        public int BookAccommodation(Accommodation accommodation, AccommodationReservation accommodationReservation, List<CalendarDateRange> reservedAccommodationDateRanges)
        {
            SetAccommodationReservationConnections(accommodationReservation);

            if (!accommodation.AreAccommodationReservationRequirementsSatisfied(accommodationReservation, reservedAccommodationDateRanges))
            {
                return -1;
            }

            _superGuestService.DecreaseSuperGuestDiscountCouponByGuestId(accommodationReservation.GuestId);

            _accommodationReservationRepository.Save(accommodationReservation);
            return 0;
        }

        public void SetAccommodationReservationConnections(AccommodationReservation accommodationReservation)
        {
            if (accommodationReservation.Accommodation == null)
            {
                accommodationReservation.Accommodation = _accommodationRepository.GetById(accommodationReservation.AccommodationId);
            }
            if (accommodationReservation.Guest == null)
            {
                accommodationReservation.Guest = _guestRepository.GetById(accommodationReservation.GuestId);
            }
        }

        public bool GuestHasAccommodationReservationOnLocationId(int guestId, int locationId)
        {
            return _accommodationReservationRepository.GetAllByGuestIdAndLocationId(guestId, locationId).Count > 0 ? true : false;
        }
    }
}
