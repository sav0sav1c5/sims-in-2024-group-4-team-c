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
    internal class GuestDataHandler: IDataHandler<Guest>
    {
        //private DataContext dataContext = new DataContext();
        //private DataContext dataContext = DependencyContainer.GetInstance<DataContext>();
        private DataContext dataContext = GlobalDataContext.DataContext;

        public IEnumerable<Guest> GetAll()
        {
            dataContext.Guests
                //.Include(guest => guest.AccommodationReservations)
                .ToList();   
            return dataContext.Guests.ToList();
        }

        void IDataHandler<Guest>.Save(IEnumerable<Guest> entities)
        {
            throw new NotImplementedException();
        }

        Guest IDataHandler<Guest>.SaveOneEntity(Guest guest)
        {
            dataContext.Guests.Add(guest);
            dataContext.SaveChanges();
            return guest;
        }

        void IDataHandler<Guest>.Delete(Guest entity)
        {
            throw new NotImplementedException();
        }

        void IDataHandler<Guest>.DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        void IDataHandler<Guest>.Update(Guest entity)
        {
            var localEntry = dataContext.Guests.Find(entity.Id);

            if (localEntry != null)
            {
                localEntry.Copy(entity);
            }
            dataContext.SaveChanges();
        }

        public Guest UpdateEntity(Guest entity)
        {
            throw new NotImplementedException();
        }
    }
}
