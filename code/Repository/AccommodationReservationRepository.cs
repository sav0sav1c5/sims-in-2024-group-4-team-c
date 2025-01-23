using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationReservationRepository : IAccommodationReservationRepository
    {
        private List<AccommodationReservation> _reservations;
        private readonly IDataHandler<AccommodationReservation> _reservationDataHandler;
        private readonly ICurrentDateRepository _currentDateRepository = DependencyContainer.GetInstance<ICurrentDateRepository>();

        public AccommodationReservationRepository()
        {
            _reservationDataHandler = new AccommodationReservationDataHandler();
            _reservations = _reservationDataHandler.GetAll().ToList();
        }

        public void Save(AccommodationReservation reservation)
        {
            _reservationDataHandler.SaveOneEntity(reservation);
            _reservations.Add(reservation);
        }


        public AccommodationReservation? GetById(int id)
        {
            return _reservations.FirstOrDefault(x => x.Id == id);
            //return _reservationDataHandler.GetAll()
            //    .FirstOrDefault(reservation => reservation.Id == id);
        }

        public List<AccommodationReservation> GetByAccommodationId(int id)
        {
            return _reservations.FindAll(x => x.AccommodationId == id);
            //return _reservationDataHandler.GetAll()
            //    .Where(reservation => reservation.AccommodationId == id)
            //    .ToList();

        }

        public List<AccommodationReservation> GetAll()
        {
            return _reservations;
            //return _reservationDataHandler.GetAll().ToList();
        }

        public void Update(AccommodationReservation reservation)
        {
            _reservationDataHandler.Update(reservation);
            //updates are handled automaticaly since we save the reference in 'Save(AccommodationReservation reservation)'
            //var entry = _reservations.FirstOrDefault(x => x == reservation); 
            //{
            //    entry = reservation;
            //}
        }

        public void UpdateStates(AccommodationReservation reservation)
        {
            /*
            if(reservation.StartDate <= _currentDateRepository.Get().Date)
            {
                if (reservation.StartDate.AddDays(reservation.StayLength) > _currentDateRepository.Get().Date)
                {
                    TimeSpan diffrence = (_currentDateRepository.Get().Date - reservation.StartDate.AddDays(reservation.StayLength));
                    if (diffrence.Days == 6)
                    {
                        Delete(reservation);
                    }else
                    {
                        reservation.ReservationState = ReservationState.State.Ended;
                    }
                }
                else
                {
                    reservation.ReservationState = ReservationState.State.Active;
                }
            }else
            {
                reservation.ReservationState = ReservationState.State.Reserved;
            }
            Update(reservation);
            */

            DateTime currentDate = _currentDateRepository.Get().Date;

            if (reservation.StartDate > currentDate)
            {
                // Ako je početak rezervacije u budućnosti, stanje je "Reserved"
                reservation.ReservationState = ReservationState.State.Reserved;
            }
            else if (reservation.StartDate <= currentDate && currentDate < reservation.StartDate.AddDays(reservation.StayLength))
            {
                // Ako je rezervacija u toku, stanje je "Active"
                reservation.ReservationState = ReservationState.State.Active;
            }
            else
            {
                // Ako je rezervacija završena, ali nije prošlo 5 dana od završetka, stanje je "Ended"
                DateTime endDate = reservation.StartDate.AddDays(reservation.StayLength);
                if ((currentDate - endDate).Days <= 5)
                {
                    reservation.ReservationState = ReservationState.State.Ended;
                }
                else
                {
                    // Ako je prošlo 5 ili više dana od završetka rezervacije, brišemo rezervaciju
                    Delete(reservation);
                    return; // Nakon brisanja, nema potrebe za daljim ažuriranjem
                }
            }

            // Ažuriranje rezervacije u bazi podataka
            Update(reservation);
        }

        public void DeleteById(int id)
        {
            _reservationDataHandler.DeleteById(id);
            _reservations.RemoveAll(reservation => reservation.Id == id);
        }

        public void Delete(AccommodationReservation reservation)
        {
            _reservationDataHandler.Delete(reservation);
            //_reservations.RemoveAll(x => x == reservation);      
            _reservations.Remove(reservation);   //should work since we only know the references
        }

        public List<AccommodationReservation> GetByGuestId(int guestId)
        {
            return _reservations.FindAll(reservation => reservation.GuestId == guestId).ToList();
            //return _reservationDataHandler.GetAll()
            //    .Where(reservation => reservation.GuestId == guestId)
            //    .ToList();
        }

        public List<AccommodationReservation> GetReservationsByGuestIdBetweenTimePoints(int guestId, DateTime timePointStart, DateTime timePointEnd)
        {
            List<AccommodationReservation> guestsReservations = GetByGuestId(guestId);
            List<AccommodationReservation> result = new List<AccommodationReservation>();
            //sort by end date
            guestsReservations.Sort(
                (reservation1, reservation2) =>
                {
                    if (reservation1.StartDate.AddDays(reservation1.StayLength) < reservation2.StartDate.AddDays(reservation2.StayLength))
                        return -1;
                    else
                        return 1;
                }
            );

            foreach (AccommodationReservation reservation in guestsReservations)
            {
                if (reservation.ReservationState == ReservationState.State.Ended
                    && reservation.StartDate >= timePointStart
                    && reservation.StartDate <= timePointEnd)
                {
                    result.Add(reservation);
                }
            }
            return result;
        }

        public List<AccommodationReservation> GetAllByGuestIdAndLocationId(int guestId,int locationId) 
        {
            return _reservationDataHandler.GetAll()
                .Where(reservation => reservation.GuestId == guestId
                        && reservation.Accommodation.LocationId == locationId)
                .ToList();
        }
    }
}
