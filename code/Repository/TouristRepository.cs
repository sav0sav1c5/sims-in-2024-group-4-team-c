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
    internal class TouristRepository : ITouristRepository
    {
        private List<Tourist> tourists;
        private readonly IDataHandler<Tourist> touristDataHandler;

        public TouristRepository()
        {
            touristDataHandler = new TouristDataHandler();
            tourists = touristDataHandler.GetAll().ToList();
        }

        public void Save(Tourist tourist)
        {
            if (tourists == null)
            {
                tourists = new List<Tourist>();
            }
            touristDataHandler.SaveOneEntity(tourist);
        }


        public Tourist? GetById(int id)
        {
            return tourists.FirstOrDefault(x => x.Id == id);
        }

        public List<Tourist> GetAll()
        {
            return tourists;
        }

        public void Update(Tourist tourist)
        {
            touristDataHandler.Update(tourist);
        }

        public void DeleteById(int id)
        {
            touristDataHandler.DeleteById(id);
        }

        public void Delete(Tourist tourist)
        {
            touristDataHandler.Delete(tourist);
        }
    }
}