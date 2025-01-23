using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Resources.DBAcces
{
    public class ComplexTourPartDataHandler : IDataHandler<ComplexTourPart>
    {
        private DataContext dataContext = new DataContext();

        public IEnumerable<ComplexTourPart> GetAll()
        {
            return dataContext.ComplexTourParts.ToList();
        }

        public void Save(IEnumerable<ComplexTourPart> entities)
        {
            dataContext.ComplexTourParts.AddRange(entities);
            dataContext.SaveChanges();
        }

        public ComplexTourPart SaveOneEntity(ComplexTourPart entity)
        {
            dataContext.ComplexTourParts.Add(entity);
            dataContext.SaveChanges();
            return entity;
        }

        public void Delete(ComplexTourPart entity)
        {
            dataContext.ComplexTourParts.Remove(entity);
            dataContext.SaveChanges();
        }

        public void DeleteById(int entityId)
        {
            var entity = dataContext.ComplexTourParts.FirstOrDefault(e => e.Id == entityId);
            if (entity != null)
            {
                dataContext.ComplexTourParts.Remove(entity);
                dataContext.SaveChanges();
            }
        }

        public void Update(ComplexTourPart entity)
        {
            var existingComplexTourPart = dataContext.ComplexTourParts.FirstOrDefault(ctp => ctp.Id == entity.Id);
            dataContext.ComplexTourParts.Update(existingComplexTourPart);
            dataContext.SaveChanges();
        }

        public ComplexTourPart UpdateEntity(ComplexTourPart entity)
        {
            throw new NotImplementedException();
        }
    }
}