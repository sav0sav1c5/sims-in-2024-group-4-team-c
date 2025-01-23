using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using BookingApp.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationReservationReviewRepository:IAccommodationReservationReviewRepository
    {
        private List<AccommodationReservationReview> rateGuests;
        private readonly IDataHandler<AccommodationReservationReview> rateGuestsDataHandler;

        public AccommodationReservationReviewRepository()
        {
            rateGuestsDataHandler = new AccommodationReservationReviewDataHandler();
            rateGuests = rateGuestsDataHandler.GetAll().ToList();
        }

        public void Delete(AccommodationReservationReview rateGuest)
        {
            rateGuestsDataHandler.Delete(rateGuest);
        }

        public AccommodationReservationReview GetById(int id, out bool verify)
        {
            var pomocna = rateGuests.FindAll(accommodation => accommodation.AccommodationReservationId == id);
            int duzina = pomocna.Count();
            verify = true;
            if(duzina > 1)
            {
                verify = false;
                return null;
            }
            return rateGuests.FirstOrDefault(accommodations => accommodations.AccommodationReservationId == id && accommodations.Direction != false);
        }

        public void Save(AccommodationReservationReview rateGuest)
        {
            if (rateGuest == null) return;
            rateGuestsDataHandler.SaveOneEntity(rateGuest);
            rateGuests.Add(rateGuest);
        }

        public List<AccommodationReservationReview> GetAll()
        {
            return rateGuests;
        }

        public void Update(AccommodationReservationReview rateGuest)
        {
            rateGuestsDataHandler.Update(rateGuest);
        }
    }
}
