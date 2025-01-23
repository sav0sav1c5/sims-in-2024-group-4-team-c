using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Resources.DBAccess;

namespace BookingApp.Resources.DBAcces
{
    public class TourRequestDataHandler : IDataHandler<TourRequest>
    {
        private DataContext dataContext = new DataContext();

        public IEnumerable<TourRequest> GetAll()
        {
            return dataContext.TourRequests.ToList();
        }

        public void Save(IEnumerable<TourRequest> entities)
        {
            dataContext.TourRequests.AddRange(entities);
            dataContext.SaveChanges();
        }

        public TourRequest SaveOneEntity(TourRequest entity)
        {
            dataContext.TourRequests.Add(entity);
            dataContext.SaveChanges();
            return entity;
        }

        public void Delete(TourRequest entity)
        {
            dataContext.TourRequests.Remove(entity);
            dataContext.SaveChanges();
        }

        public void DeleteById(int entityId)
        {
            var entity = dataContext.TourRequests.FirstOrDefault(e => e.Id == entityId);
            if (entity != null)
            {
                dataContext.TourRequests.Remove(entity);
                dataContext.SaveChanges();
            }
        }

        public void Update(TourRequest entity)
        {
            var existingTourRequest = dataContext.TourRequests.FirstOrDefault(tourRequest => tourRequest.Id == entity.Id);
            dataContext.TourRequests.Update(existingTourRequest);
            dataContext.SaveChanges();
        }

        public TourRequest UpdateEntity(TourRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
