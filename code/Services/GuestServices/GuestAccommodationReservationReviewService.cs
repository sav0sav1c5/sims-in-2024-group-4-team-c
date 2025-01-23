using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.GuestServices
{
    public class GuestAccommodationReservationReviewService
    {
        #region Data
        private readonly IAccommodationReservationReviewRepository _accommodationReservationReviewRepository = DependencyContainer.GetInstance<IAccommodationReservationReviewRepository>();
        private readonly IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();
        private readonly IAccommodationReservationRepository _accommodationReservationRepository = DependencyContainer.GetInstance<IAccommodationReservationRepository>();
        private readonly IOwnerRepository _ownerRepository = DependencyContainer.GetInstance<IOwnerRepository>();
        private readonly IGuestRepository _guestRepository = DependencyContainer.GetInstance<IGuestRepository>();
        private readonly ICurrentDateRepository _currentDateRepository = DependencyContainer.GetInstance<ICurrentDateRepository>();
        private readonly DateTime CurrentDate;
        #endregion

        public GuestAccommodationReservationReviewService()
        {
            CurrentDate = _currentDateRepository.Get()!.Date;
        }

        public void SaveAccommodationReservationReview(AccommodationReservationReview accommodationReservationReview)
        {
            _accommodationReservationReviewRepository.Save(accommodationReservationReview);
        }
        /// <summary>
        /// Sets the connections using the repositories 
        /// </summary>
        public void SaveAccommodationReservationReview(RateOwnerDTO rateOwnerDTO)
        {
            AccommodationReservationReview accommodationReservationReview = new AccommodationReservationReview(rateOwnerDTO);
            accommodationReservationReview.Accommodation = _accommodationRepository.GetById(accommodationReservationReview.AccommodationId);
            accommodationReservationReview.Owner = _ownerRepository.GetById(accommodationReservationReview.OwnerId);
            accommodationReservationReview.AccommodationReservation = _accommodationReservationRepository.GetById(accommodationReservationReview.AccommodationReservationId);
            accommodationReservationReview.Guest = _guestRepository.GetById(accommodationReservationReview.GuestId);
            _accommodationReservationReviewRepository.Save(accommodationReservationReview);
        }
        public List<AccommodationReservationReview> GetAccommodationReservationReviewsByAccommodationReservationId(int id)
        {
            return _accommodationReservationReviewRepository.GetAll()
                .Where(x => x.AccommodationReservationId == id).ToList();
        }

        /// <summary>
        /// reviewDTO must have valid AccommodationId, GuestId... else those fields will not be set
        /// </summary>
        //private AccommodationReservationReviewDTO SetAccommodationReservationReviewDTOConnections(AccommodationReservationReviewDTO reviewDTO)
        //{
        //    //Setting up the fields that are only used for displaying data
        //    Accommodation? reviewDTOAccommodation = _accommodationRepository.GetById(reviewDTO.AccommodationId);
        //    if (reviewDTOAccommodation != null)
        //    {
        //        reviewDTO.AccommodationName = reviewDTOAccommodation.Name;
        //        reviewDTO.AccommodationCity = reviewDTOAccommodation.Location!.City;
        //        reviewDTO.AccommodationCountry = reviewDTOAccommodation.Location!.Country;
        //        reviewDTO.AccommodationCountry = reviewDTOAccommodation.Location!.Country;
        //    }
        //    AccommodationReservation? reviewDTOAccommodationReservation = _accommodationReservationRepository.GetById(reviewDTO.AccommodationReservationId);
        //    if (reviewDTOAccommodationReservation != null)
        //    {
        //        reviewDTO.StartDate = reviewDTOAccommodationReservation.StartDate;
        //        reviewDTO.StayLength = reviewDTOAccommodationReservation.StayLength;
        //        reviewDTO.NumberOfGuests = reviewDTOAccommodationReservation.NumberOfGuests;
        //    }
        //    Owner? reviewDTOAccommodationOwner = _ownerRepository.GetById(reviewDTO.OwnerId);
        //    if (reviewDTOAccommodationOwner != null)
        //    {
        //        reviewDTO.OwnerName = reviewDTOAccommodationOwner.FirstName;
        //    }
        //    Guest? reviewDTOGuest = _guestRepository.GetById(reviewDTO.GuestId);
        //    if (reviewDTOGuest != null)
        //    {
        //        reviewDTO.GuestName = reviewDTOGuest.FirstName;
        //    }
        //    return reviewDTO;
        //}

        public AccommodationReservationReviewDTO GetOwnerAccommodationReservationReviewDTOByAccommodationReservationId(int id)
        {
            List<AccommodationReservationReview> reviews = _accommodationReservationReviewRepository.GetAll()
                .Where(x => x.AccommodationReservationId == id)
                .Where(x => x.Direction == true)
                .ToList();
            if (reviews.Count != 1)
            {
                return new AccommodationReservationReviewDTO();
            }
            AccommodationReservationReviewDTO reviewDTO = new AccommodationReservationReviewDTO(reviews.ElementAt(0));
            return reviewDTO;
            //return SetAccommodationReservationReviewDTOConnections(reviewDTO);
        }

        public bool OwnerAccommodationReservationReviewExists(AccommodationReservation accommodationReservation)
        {
            return _accommodationReservationReviewRepository.GetAll()
                .Where(x => x.AccommodationReservationId == accommodationReservation.Id)
                .Where(x => x.Direction == true )
                .ToList()
                .Count() >= 1;
        }
        public bool OwnerAccommodationReservationReviewExists(int accommodationReservationId)
        {
            return _accommodationReservationReviewRepository.GetAll()
                .Where(x => x.AccommodationReservationId == accommodationReservationId)
                .Where(x => x.Direction == true)
                .ToList()
                .Count() >= 1;
        }

        public bool GuestAccommodationReservationReviewExists(AccommodationReservation accommodationReservation)
        {
            return _accommodationReservationReviewRepository.GetAll()
                .Where(x => x.AccommodationReservationId == accommodationReservation.Id)
                .Where(x => x.Direction == false)
                .ToList()
                .Count() >= 1;
        }
        public bool GuestAccommodationReservationReviewExists(int accommodationReservationId)
        {
            return _accommodationReservationReviewRepository.GetAll()
                .Where(x => x.AccommodationReservationId == accommodationReservationId)
                .Where(x => x.Direction == false)
                .ToList()
                .Count() >= 1;
        }

        public bool IsGuestReviewPostingPeriodOver(AccommodationReservation reservation)
        {
            DateOnly reservationEndDate = DateOnly.FromDateTime(reservation.StartDate.AddDays(reservation.StayLength));
            if (DateOnly.FromDateTime(CurrentDate).AddDays(-5) <= reservationEndDate
                && reservationEndDate <= DateOnly.FromDateTime(CurrentDate))
            {
                return false;
            }
            return true;
        }
        public bool IsGuestReviewPostingPeriodOver(int reservationId)
        {
            AccommodationReservation? reservation = _accommodationReservationRepository!.GetById(reservationId);
            if (reservation == null)
            {
                return true;
            }
            DateOnly reservationEndDate = DateOnly.FromDateTime(reservation.StartDate.AddDays(reservation.StayLength));
            if (DateOnly.FromDateTime(CurrentDate).AddDays(-5) <= reservationEndDate
                && reservationEndDate <= DateOnly.FromDateTime(CurrentDate))
            {
                return false;
            }
            return true;
        }
        
    }
}
