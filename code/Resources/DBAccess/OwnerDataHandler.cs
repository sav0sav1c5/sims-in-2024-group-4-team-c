using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    internal class OwnerDataHandler:IDataHandler<Owner>
    {
        //private DataContext dataContext = new DataContext();
        //private DataContext dataContext = DependencyContainer.GetInstance<DataContext>();
        private DataContext dataContext = GlobalDataContext.DataContext;

        public void Delete(Owner entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Owner> GetAll()
        {
            return dataContext.Owners.ToList();
        }

        public void Save(IEnumerable<Owner> owners)
        {
            dataContext.Owners.AddRange(owners);
            dataContext.SaveChanges();
        }

        public Owner SaveOneEntity(Owner owner)
        {
            dataContext.Owners.Add(owner);
            dataContext.SaveChanges();
            return owner;
        }

        public void Update(Owner entity)
        {
            throw new NotImplementedException();
        }

        public Owner UpdateEntity(Owner entity)
        {
            throw new NotImplementedException();
        }
    }
}
