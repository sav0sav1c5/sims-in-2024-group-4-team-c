using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.OwnerServices
{
    public class ManageRenovationService
    {
        private IRenovationRepository _renovationRepository = DependencyContainer.GetInstance<IRenovationRepository>();
        private IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();

        public void updateRenovation(Renovation renovation)
        {
            _renovationRepository.Update(renovation);
        }
        public Renovation getRenovationById(int id)
        {
            return _renovationRepository.GetAll().Where(renovation => renovation.Id == id).First();
        }
        public List<Accommodation> getOwnerLocations()
        {
            return _accommodationRepository.GetAll().Where(accommodation => accommodation.OwnerId == UserSession.Instance.Id).ToList();
        }

        public List<Renovation> getRenovations()
        {
            List<Renovation> renovations = new List<Renovation>();
            var data = getOwnerLocations();
            foreach(var element in data)
            {
                var reno = _renovationRepository.GetAll().Where(renovation => renovation.AccommodationId == element.Id && renovation.isCanceled != true).ToList();
                renovations.AddRange(reno);
                
            }

            return renovations;
        }
    }
}
