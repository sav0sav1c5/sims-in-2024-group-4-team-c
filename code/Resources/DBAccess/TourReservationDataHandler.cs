using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Resources.DBAccess;

namespace BookingApp.Resources.DBAcces
{
    public class TourReservationDataHandler : IDataHandler<TourReservation>
    {
        private DataContext dataContext = new DataContext();

        public IEnumerable<TourReservation> GetAll()
        {
            return dataContext.TourReservations.ToList();
        }

        public void Save(IEnumerable<TourReservation> entities)
        {
            dataContext.TourReservations.AddRange(entities);
            dataContext.SaveChanges();
        }

        public TourReservation SaveOneEntity(TourReservation entity)
        {
            dataContext.TourReservations.Add(entity);
            dataContext.SaveChanges();
            return entity;
        }

        public void Delete(TourReservation entity)
        {
            dataContext.TourReservations.Remove(entity);
            dataContext.SaveChanges();
        }

        public void DeleteById(int entityId)
        {
            var entity = dataContext.TourReservations.FirstOrDefault(e => e.Id == entityId);
            if (entity != null)
            {
                dataContext.TourReservations.Remove(entity);
                dataContext.SaveChanges();
            }
        }

        public void Update(TourReservation entity)
        {
            var existingTourReservation = dataContext.TourReservations.FirstOrDefault(tourReservation => tourReservation.Id == entity.Id);
            existingTourReservation.TourState = entity.TourState;

            dataContext.TourReservations.Update(existingTourReservation);
            dataContext.SaveChanges();
        }

        public TourReservation UpdateEntity(TourReservation entity)
        {
            throw new NotImplementedException();
        }
    }
}