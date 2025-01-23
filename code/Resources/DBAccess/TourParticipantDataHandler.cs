using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAcces
{
    public class TourParticipantDataHandler : IDataHandler<TourParticipant>
    {
        private DataContext dataContext = new DataContext();
        public IEnumerable<TourParticipant> GetAll()
        {
            return dataContext.TourParticipants.ToList();
        }

        public TourParticipant GetById(int id)
        {
            return dataContext.TourParticipants.FirstOrDefault(tourParticipant => tourParticipant.Id == id);
        }

        void IDataHandler<TourParticipant>.Save(IEnumerable<TourParticipant> entities)
        {
            throw new NotImplementedException();
        }

        TourParticipant IDataHandler<TourParticipant>.SaveOneEntity(TourParticipant tourParticipant)
        {
            dataContext.TourParticipants.Add(tourParticipant);
            dataContext.SaveChanges();
            return tourParticipant;
        }

        void IDataHandler<TourParticipant>.Delete(TourParticipant entity)
        {
            var existingTourParticipant = dataContext.TourParticipants.FirstOrDefault(tourParticipant => tourParticipant.Id == entity.Id);

            if (existingTourParticipant != null)
            {
                dataContext.TourParticipants.Remove(existingTourParticipant);
                dataContext.SaveChanges();
            }
        }

        void IDataHandler<TourParticipant>.DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        void IDataHandler<TourParticipant>.Update(TourParticipant entity)
        {
            var existingTourParticipant = dataContext.TourParticipants.FirstOrDefault(tourParticipant => tourParticipant.Id == entity.Id);

            if (existingTourParticipant != null)
            {
                existingTourParticipant.TourReservationId = entity.TourReservationId;
                existingTourParticipant.IsPresent = entity.IsPresent;

                dataContext.SaveChanges();
            }
        }

        public TourParticipant UpdateEntity(TourParticipant entity)
        {
            throw new NotImplementedException();
        }
    }
}
