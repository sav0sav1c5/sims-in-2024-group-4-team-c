using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    internal class RenovationDataHandler : IDataHandler<Renovation>
    {
        private DataContext dataContext = GlobalDataContext.DataContext;

        public void Delete(Renovation entity)
        {
               if(entity != null)
            {
                dataContext.Remove(entity);
            }
        }

        public void DeleteById(int entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Renovation> GetAll()
        {
            return dataContext.Renovations.ToList();
        }

        public void Save(IEnumerable<Renovation> entities)
        {
            dataContext.Renovations.AddRange(entities);
            dataContext.SaveChanges();
        }

        public Renovation SaveOneEntity(Renovation entity)
        {
            dataContext.Renovations.Add(entity);
            dataContext.SaveChanges();
            return entity;
        }

        public void Update(Renovation entity)
        {
            var existingRenovation = dataContext.Renovations.FirstOrDefault(renovation => renovation.Id == entity.Id);
            if (existingRenovation != null)
            {
                existingRenovation.StartDate = entity.StartDate;
                existingRenovation.EndDate = entity.EndDate;
                existingRenovation.EstimatedDuration = entity.EstimatedDuration;
                existingRenovation.State = entity.State;
                existingRenovation.AccommodationId = entity.AccommodationId;
                existingRenovation.isCanceled = entity.isCanceled;

                dataContext.SaveChanges();
            }

        }

        public Renovation UpdateEntity(Renovation entity)
        {
            throw new NotImplementedException();
        }

        
    }
}
