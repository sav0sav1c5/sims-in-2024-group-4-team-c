using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    public class ComplexTourRequestDataHandler : IDataHandler<ComplexTourRequest>
    {
        private DataContext dataContext = new DataContext();

        public IEnumerable<ComplexTourRequest> GetAll()
        {
            return dataContext.ComplexTourRequests.ToList();
        }

        public void Save(IEnumerable<ComplexTourRequest> entities)
        {
            dataContext.ComplexTourRequests.AddRange(entities);
            dataContext.SaveChanges();
        }

        public ComplexTourRequest SaveOneEntity(ComplexTourRequest entity)
        {
            dataContext.ComplexTourRequests.Add(entity);
            dataContext.SaveChanges();
            return entity;
        }

        public void Delete(ComplexTourRequest entity)
        {
            dataContext.ComplexTourRequests.Remove(entity);
            dataContext.SaveChanges();
        }

        public void DeleteById(int entityId)
        {
            var entity = dataContext.ComplexTourRequests.FirstOrDefault(e => e.Id == entityId);
            if (entity != null)
            {
                dataContext.ComplexTourRequests.Remove(entity);
                dataContext.SaveChanges();
            }
        }

        public void Update(ComplexTourRequest entity)
        {
            var existingComplexTourRequest = dataContext.ComplexTourRequests.FirstOrDefault(ctr => ctr.Id == entity.Id);
            dataContext.ComplexTourRequests.Update(existingComplexTourRequest);
            dataContext.SaveChanges();
        }

        public ComplexTourRequest UpdateEntity(ComplexTourRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
