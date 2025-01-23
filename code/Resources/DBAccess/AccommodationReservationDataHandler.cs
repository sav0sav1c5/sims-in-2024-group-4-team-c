using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    public class AccommodationReservationDataHandler : IDataHandler<AccommodationReservation>
    {
        //private DataContext dataContext = new DataContext();
        //private DataContext dataContext = DependencyContainer.GetInstance<DataContext>();
        private DataContext dataContext = GlobalDataContext.DataContext;

        IEnumerable<AccommodationReservation> IDataHandler<AccommodationReservation>.GetAll()
        {
            return dataContext.AccommodationReservations
                .Include(reservation => reservation.Accommodation)
                .Include(reservation => reservation.Accommodation!.Location)
                .Include(reservation => reservation.Guest)
                .ToList();
        }

        void IDataHandler<AccommodationReservation>.Save(IEnumerable<AccommodationReservation> entities)
        {
            throw new NotImplementedException();
        }

        AccommodationReservation IDataHandler<AccommodationReservation>.SaveOneEntity(AccommodationReservation reservation)
        {
            var localEntry = dataContext.AccommodationReservations
                .Local.FirstOrDefault(e => e.Id.Equals(reservation.Id));
            
            //detach the entity
            if (localEntry != null)
            {
                dataContext.Entry(localEntry).State = EntityState.Detached;
            }
            //Set the modified flag
            dataContext.Entry(reservation).State = EntityState.Modified;

            dataContext.AccommodationReservations.Add(reservation);
            dataContext.SaveChanges();
            return reservation;
        }

        void IDataHandler<AccommodationReservation>.Delete(AccommodationReservation reservation)
        {
            var localEntry = dataContext.AccommodationReservations
                .Find(reservation.Id);      //queries the database and tracks it
                //.Local.FirstOrDefault(e => e.Id.Equals(reservation.Id));  //Does this do the same?
                

            //disable tracking by detaching it
            if (localEntry != null)
            {
                dataContext.Entry(localEntry).State = EntityState.Detached;

            }
            //Set the modified flag so that the context updates it
            dataContext.Entry(reservation).State = EntityState.Modified;

            dataContext.AccommodationReservations.Remove(reservation);
            dataContext.SaveChanges();
            //throw new NotImplementedException();
        }

        void IDataHandler<AccommodationReservation>.DeleteById(int id)
        {
            var localEntry = dataContext.AccommodationReservations
                .Find(id);      //queries the database and tracks it
                                //.Local.FirstOrDefault(e => e.Id.Equals(reservation.Id));  //Does this do the same?

            //disable tracking by detaching it
            if (localEntry != null)
            {
                dataContext.Entry(localEntry).State = EntityState.Detached;
                
                //Set the modified flag so that the context updates it
                dataContext.Entry(localEntry).State = EntityState.Modified;

                dataContext.AccommodationReservations.Remove(localEntry);
                dataContext.SaveChanges();
            }
            //throw new NotImplementedException();
        }

        void IDataHandler<AccommodationReservation>.Update(AccommodationReservation entity)
        {
            var localEntry = dataContext.AccommodationReservations.Find(entity.Id);

            if(localEntry != null)
            {
                localEntry.ReservationState = entity.ReservationState;
            }

            dataContext.SaveChanges();
        }

        public AccommodationReservation UpdateEntity(AccommodationReservation entity)
        {
            throw new NotImplementedException();
        }
    }
}
