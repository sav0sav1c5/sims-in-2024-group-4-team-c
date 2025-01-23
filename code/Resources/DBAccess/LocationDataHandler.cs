using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;

namespace BookingApp.Resources.DBAccess
{
    public class LocationDataHandler:IDataHandler<Location>
    {
        //private DataContext dataContext = new DataContext();
        //private DataContext dataContext = DependencyContainer.GetInstance<DataContext>();
        private DataContext dataContext = GlobalDataContext.DataContext;


        IEnumerable<Location> IDataHandler<Location>.GetAll()
        {
            return dataContext.Locations.ToList();
        }

        void IDataHandler<Location>.Save(IEnumerable<Location> entities)
        {
            throw new NotImplementedException();
        }

        Location IDataHandler<Location>.SaveOneEntity(Location location)
        {
            dataContext.Locations.Add(location);
            dataContext.SaveChanges();
            return location;
        }

        void IDataHandler<Location>.Delete(Location entity)
        {
            throw new NotImplementedException();
        }

        void IDataHandler<Location>.DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        void IDataHandler<Location>.Update(Location entity)
        {
            throw new NotImplementedException();
        }

        public Location UpdateEntity(Location entity)
        {
            throw new NotImplementedException();
        }
    }
}
