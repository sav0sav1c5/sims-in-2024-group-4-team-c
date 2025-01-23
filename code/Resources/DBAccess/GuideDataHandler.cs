using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Resources.DBAccess
{
    internal class GuideDataHandler : IDataHandler<Guide>
    {
        private DataContext dataContext = new DataContext();

        public IEnumerable<Guide> GetAll()
        {
            return dataContext.Guides.ToList();
        }

        public void Save(IEnumerable<Guide> entities)
        {
            dataContext.Guides.AddRange(entities);
            dataContext.SaveChanges();
        }

        public Guide SaveOneEntity(Guide guide)
        {
            dataContext.Guides.Add(guide);
            dataContext.SaveChanges();
            return guide;
        }

        public void Delete(Guide entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Guide entity)
        {
            throw new NotImplementedException();
        }

        public Guide UpdateEntity(Guide entity)
        {
            throw new NotImplementedException();
        }
    }
}
