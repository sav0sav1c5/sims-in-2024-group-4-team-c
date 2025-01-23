using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal interface IOwnerRepository
    {
        void Save(Owner owner);

        Owner? GetById(int id);

        List<Owner> GetAll();

        void Update(Owner owner);

        void DeleteById(int id);

        void Delete(Owner owner);
    }
}
