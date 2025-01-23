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
    public class GuestAccommodationReservationModificationRequestService
    {
        #region Data
        private readonly IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();
        private readonly IAccommodationReservationModificationRequestRepository _accommodationReservationModificationRequestRepository = DependencyContainer.GetInstance<IAccommodationReservationModificationRequestRepository>();
        private readonly IAccommodationReservationRepository _accommodationReservationRepository = DependencyContainer.GetInstance<IAccommodationReservationRepository>();
        private readonly ICurrentDateRepository _currentDateRepository = DependencyContainer.GetInstance<ICurrentDateRepository>();
        private readonly DateTime CurrentDate;
        #endregion

        public GuestAccommodationReservationModificationRequestService()
        {
            CurrentDate = _currentDateRepository.Get()!.Date;
        }

        public List<AccommodationReservationModificationRequest> GetAccommodationReservationModificationRequestByGuestId(int id)
        {
            return _accommodationReservationModificationRequestRepository.GetByGuestId(id);
        }
        
        public List<ReservationModificationDTO> GetAccommodationReservationModificationRequestDTOByGuestId(int id)
        {
            List<ReservationModificationDTO> result = new List<ReservationModificationDTO>();
            List < AccommodationReservationModificationRequest > requests = _accommodationReservationModificationRequestRepository.GetByGuestId(id);
            foreach (var request in requests)
            {
                result.Add(new ReservationModificationDTO(request) );
            }
            return result;
        }

        public string SendAccommodationReservationModificationRequest(AccommodationReservationModificationRequest request)
        {

            Accommodation targetAccommodation = request.AccommodationReservation!.Accommodation!;

            //Use a copy so you don't accidentaly overwrite the reservation with the update
            AccommodationReservation updatedReservation = new AccommodationReservation();
            updatedReservation.ApplyModificationRequest(request);

            DateOnly requestStartDate = DateOnly.FromDateTime(request.StartDate);
            if (requestStartDate < DateOnly.FromDateTime(CurrentDate))
            {
                return "Start date set before current date!";
            }

            List<CalendarDateRange> reservedDateRanges = targetAccommodation.GetAccommodationReservedDateRanges();

            //Remove the AccommodationReservation date range that we are editing
            CalendarDateRange reservationDateRange = new CalendarDateRange(request.AccommodationReservation.StartDate, request.AccommodationReservation.StartDate.AddDays(request.StayLength - 1));

            reservedDateRanges.RemoveAll(reservation =>
                    DateOnly.FromDateTime(reservation.Start) == DateOnly.FromDateTime(reservationDateRange.Start)
                    && DateOnly.FromDateTime(reservation.End) == DateOnly.FromDateTime(reservationDateRange.End)
                    );
            //NOTE: If none are left then its highly likely that someone was messing with the database putting in random dates
            


            if (!targetAccommodation.AreAccommodationReservationRequirementsSatisfied(updatedReservation, reservedDateRanges))
            {
                return "Accommodation requirements not satisfied!";
            }

            _accommodationReservationModificationRequestRepository.Save(request);
            return "Request successfully sent!";
        }
    }
}
