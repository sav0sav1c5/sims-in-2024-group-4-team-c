using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.OwnerServices
{
    public class OwnerHomeService
    {
        private readonly IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();
        private readonly IAccommodationReservationReviewRepository _accommodationReservationReviewRepository = DependencyContainer.GetInstance<IAccommodationReservationReviewRepository>();
        private readonly IAccommodationReservationRepository _accommodationReservationRepository = DependencyContainer.GetInstance<IAccommodationReservationRepository>();
        private readonly IAccommodationReservationModificationRequestRepository _accommodationReservationModificationRequestRepository = DependencyContainer.GetInstance<IAccommodationReservationModificationRequestRepository>(); 
        public RateGuestService _rateGuestService = DependencyContainer.GetInstance<RateGuestService>();
        public int GetNumberOfAccommodations(int id)
        {
            return _accommodationRepository.GetAll().Where(accommodation => accommodation.OwnerId == id).Count();
        }

        public double GetAverageRating(int id)
        {
            var data = _accommodationReservationReviewRepository.GetAll().Where(review => review.OwnerId == id );
            data = data.Where(review => review.Direction == false);
            double sum = 0;
            double count = 0;

            foreach (var item in data)
            {


                sum += (double)item.Cleanliness.GetValueOrDefault() + (double)item.Correctness.GetValueOrDefault();


                count++;
            }

            
            return (double)sum / (count*2);
            
        }

        public int GetTotalGuests(int id)
        {
            int count = 0;
            var owners = _accommodationRepository.GetAll().Where(accommodation => accommodation.OwnerId == id);
            var data = _accommodationReservationRepository.GetAll().Where(accommodation => accommodation.ReservationState == Domain.Model.ReservationState.State.Ended);
            foreach( var ownerData in owners)
            {
                foreach(var reservation in data)
                {
                    if(ownerData.Id == reservation.AccommodationId)
                    {
                        count = count + reservation.NumberOfGuests;
                    }
                }
            }
            return count;
        }

        public string GetNumberRateGuests(int id)
        {
            string text = "";
            int count = 0;
            List<AccommodationReservation> accommodationReservations = _rateGuestService.GetAllReservations().ToList();
            foreach (var accommodationReservation in accommodationReservations)
            {
                _rateGuestService.UpdateStates(accommodationReservation);
                if (accommodationReservation.ReservationState == ReservationState.State.Ended)
                {
                    
                    var _accommodationReservationId = accommodationReservation.Id;
                    
                    

                    bool _verify = true;
                    var accommodationReservationData = _rateGuestService.GetAccommodationReservationReviewById(_accommodationReservationId, out _verify);
                    if (_verify == true)
                    {
                        if (accommodationReservationData == null || accommodationReservationData.Direction != false)
                        {
                            count++;
                        }
                    }

                }
            }

            text = "You can rate " + count + " guests.";
            return text;
        }

        public string GetReservationsNumber(int id)
        {
            string text = "";
            int count = 0;
            var data = _accommodationReservationModificationRequestRepository.GetAll().Where(request => request.RequestState == ReservationModificationRequestState.State.Pending).ToList();
            foreach(var element in data)
            {
                var reservationId = element.AccommodationReservationId;
                var reservation = _accommodationReservationRepository.GetById(reservationId);
                var accommodation = _accommodationRepository.GetById(reservation.AccommodationId);
                if(accommodation.OwnerId == id)
                {
                    count++;
                }
            }
            text = "You currently have " + count + " requests \n       to move reservation";
            return text;
        }

    }
}
