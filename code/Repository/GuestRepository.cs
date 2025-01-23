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
    internal class GuestRepository : IGuestRepository
    {
        private List<Guest> _guests;
        private readonly IDataHandler<Guest> _guestDataHandler;

        public GuestRepository()
        {
            _guestDataHandler = new GuestDataHandler();
            _guests = _guestDataHandler.GetAll().ToList();
        }

        public void Save(Guest guest)
        {
            if (_guests == null)
            {
                //TODO?: create the list? what should we do?
            }
            _guestDataHandler.SaveOneEntity(guest);
            _guests!.Add(guest);
        }


        public Guest? GetById(int id)
        {
            return _guests.FirstOrDefault(x => x.Id == id);
        }

        public List<Guest> GetAll()
        {
            return _guests;
        }

        public void Update(Guest guest)
        {
            _guestDataHandler.Update(guest);
        }

        public void DeleteById(int id)
        {
            _guestDataHandler.DeleteById(id);
            _guests.RemoveAll(x => x.Id == id);
        }

        public void Delete(Guest guest)
        {
            _guestDataHandler.Delete(guest);
            _guests.Remove(guest);
        }
    }
}
