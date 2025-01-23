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
    public class AccommodationReservationModificationRequestDataHandler:IDataHandler<AccommodationReservationModificationRequest>
    {
        //private DataContext dataContext = new DataContext();
        //private DataContext dataContext = DependencyContainer.GetInstance<DataContext>();
        private DataContext dataContext = GlobalDataContext.DataContext;

        IEnumerable<AccommodationReservationModificationRequest> IDataHandler<AccommodationReservationModificationRequest>.GetAll()
        {
            return dataContext.AccommodationReservationModificationRequests
                .Include(request => request.AccommodationReservation)
                .Include(request => request.AccommodationReservation!.Accommodation)
                .Include(request => request.AccommodationReservation!.Accommodation!.Location)
                .ToList();
        }

        void IDataHandler<AccommodationReservationModificationRequest>.Save(IEnumerable<AccommodationReservationModificationRequest> entities)
        {

            throw new NotImplementedException();
        }

        AccommodationReservationModificationRequest IDataHandler<AccommodationReservationModificationRequest>.SaveOneEntity(AccommodationReservationModificationRequest request)
        {
            //throw new NotImplementedException();
            var localEntry = dataContext.AccommodationReservationModificationRequests
                .Local.FirstOrDefault(e => e.Id.Equals(request.Id));

            //detach the entity
            if (localEntry != null)
            {
                dataContext.Entry(localEntry).State = EntityState.Detached;
            }
            //Set the modified flag
            dataContext.Entry(request).State = EntityState.Modified;

            dataContext.AccommodationReservationModificationRequests.Add(request);
            dataContext.SaveChanges();
            return request;
        }

        void IDataHandler<AccommodationReservationModificationRequest>.Delete(AccommodationReservationModificationRequest request)
        {
            var localEntry = dataContext.AccommodationReservationModificationRequests
                .Find(request.Id);          //queries the database and tracks it
                                            //.Local.FirstOrDefault(e => e.Id.Equals(reservation.Id));  //Does this do the same?


            //disable tracking by detaching it
            if (localEntry != null)
            {
                dataContext.Entry(localEntry).State = EntityState.Detached;

            }
            //Set the modified flag so that the context updates it
            dataContext.Entry(request).State = EntityState.Modified;

            dataContext.AccommodationReservationModificationRequests.Remove(request);
            dataContext.SaveChanges();
        }

        void IDataHandler<AccommodationReservationModificationRequest>.DeleteById(int id)
        {
            var localEntry = dataContext.AccommodationReservationModificationRequests
                .Find(id);      //queries the database and tracks it
                                //.Local.FirstOrDefault(e => e.Id.Equals(reservation.Id));  //Does this do the same?

            //disable tracking by detaching it
            if (localEntry != null)
            {
                dataContext.Entry(localEntry).State = EntityState.Detached;

                //Set the modified flag so that the context updates it
                dataContext.Entry(localEntry).State = EntityState.Modified;

                dataContext.AccommodationReservationModificationRequests.Remove(localEntry);
                dataContext.SaveChanges();
            }
        }

        void IDataHandler<AccommodationReservationModificationRequest>.Update(AccommodationReservationModificationRequest entity)
        {
            var localEntry = dataContext.AccommodationReservationModificationRequests.Find(entity.Id);

            if (localEntry != null)
            {
                dataContext.Entry(localEntry).State = EntityState.Modified;
                localEntry.RequestState = entity.RequestState;
                localEntry.StartDate = entity.StartDate;
                localEntry.NumberOfGuests = entity.NumberOfGuests;
                localEntry.StayLength = entity.StayLength;
                localEntry.OwnerComment = entity.OwnerComment;
            }

            dataContext.SaveChanges();
        }

        public AccommodationReservationModificationRequest UpdateEntity(AccommodationReservationModificationRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
