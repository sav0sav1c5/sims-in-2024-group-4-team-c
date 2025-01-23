using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface IAccommodationReservationRepository : IBaseRepository<AccommodationReservation>
    {
        public List<AccommodationReservation> GetReservationsByGuestIdBetweenTimePoints(int guestId, DateTime timePointStart, DateTime timePointEnd);

        public List<AccommodationReservation> GetByGuestId(int guestId);
        void Save(AccommodationReservation reservation);

        AccommodationReservation? GetById(int id);

        List<AccommodationReservation> GetByAccommodationId(int id);

        List<AccommodationReservation> GetAll();

        void Update(AccommodationReservation reservation);

        void UpdateStates(AccommodationReservation reservation);

        void DeleteById(int id);

        void Delete(AccommodationReservation reservation);

        public List<AccommodationReservation> GetAllByGuestIdAndLocationId(int guestId, int locationId);

    }
}
