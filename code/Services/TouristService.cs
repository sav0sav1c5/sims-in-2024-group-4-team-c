using BookingApp.DependencyInjection;
using BookingApp.Repository;
using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services
{
    public class TouristService
    {
        private readonly ITouristRepository _touristRepository = DependencyContainer.GetInstance<ITouristRepository>();

        public TouristService()
        {
        }

        public List<Tourist> GetAllTourists()
        {
            return _touristRepository.GetAll();
        }

        public Tourist GetTouristById(int id)
        {
            return _touristRepository.GetById(id);
        }

        public void SaveTourist(Tourist tourist)
        {
            _touristRepository.Save(tourist);
        }

        public void UpdateTourist(Tourist tourist)
        {
            _touristRepository.Update(tourist);
        }

        public void DeleteTouristById(int id)
        {
            _touristRepository.DeleteById(id);
        }

        public void DeleteTourist(Tourist tourist)
        {
            _touristRepository.Delete(tourist);
        }
    }
}
