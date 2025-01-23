using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class OwnerRepository:IOwnerRepository
    {
        private List<Owner> owners;
        private readonly IDataHandler<Owner> ownerDataHandler;

        public OwnerRepository()
        {
            ownerDataHandler = new OwnerDataHandler();
            owners = ownerDataHandler.GetAll().ToList();
        }

        public void Save(Owner owner)
        {
            if (owner != null) { ownerDataHandler.SaveOneEntity(owner); }
        }

        public Owner? GetById(int id)
        {
            return owners.FirstOrDefault(owner => owner.Id == id);
        }

        public List<Owner> GetAll()
        {
            return owners;
        }

        public void Update(Owner owner)
        {
            ownerDataHandler.Update(owner);
        }

        public void DeleteById(int id)
        {
            ownerDataHandler.DeleteById(id);
        }

        public void Delete(Owner owner)
        {
            ownerDataHandler.Delete(owner);
        }

    }
}
