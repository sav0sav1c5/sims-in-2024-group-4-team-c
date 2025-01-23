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
    public class RenovationRepository:IRenovationRepository
    {
        private List<Renovation> _renovations;
        private readonly IDataHandler<Renovation> _renovationDataHandler;

        public RenovationRepository()
        {
            _renovationDataHandler = new RenovationDataHandler();
            _renovations = _renovationDataHandler.GetAll().ToList();
        }
        
        public void Delete(Renovation renovation)
        {
            _renovationDataHandler.Delete(renovation);
            _renovations.Remove(renovation);
        }

        public void Save(Renovation renovation)
        {
            if (renovation == null) return;
            _renovationDataHandler.SaveOneEntity(renovation);
            _renovations.Add(renovation);
        }

        public List<Renovation> GetAll() { 
            return _renovations; 
        }

        public void Update(Renovation renovation)
        {
            _renovationDataHandler.Update(renovation);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Renovation? GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
