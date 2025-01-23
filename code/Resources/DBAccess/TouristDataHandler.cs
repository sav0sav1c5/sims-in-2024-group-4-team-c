using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    internal class TouristDataHandler : IDataHandler<Tourist>
    {
        private DataContext dataContext = new DataContext();
        public IEnumerable<Tourist> GetAll()
        {
            return dataContext.Tourists.ToList();
        }

        void IDataHandler<Tourist>.Save(IEnumerable<Tourist> entities)
        {
            throw new NotImplementedException();
        }

        Tourist IDataHandler<Tourist>.SaveOneEntity(Tourist tourist)
        {
            dataContext.Tourists.Add(tourist);
            dataContext.SaveChanges();
            return tourist;
        }

        void IDataHandler<Tourist>.Delete(Tourist entity)
        {
            throw new NotImplementedException();
        }

        void IDataHandler<Tourist>.DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        void IDataHandler<Tourist>.Update(Tourist entity)
        {
            throw new NotImplementedException();
        }

        public Tourist UpdateEntity(Tourist entity)
        {
            throw new NotImplementedException();
        }
    }
}