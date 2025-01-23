using BookingApp.DependencyInjection;
using BookingApp.Repository;
using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Services.GuideServices
{
    public class GuideService
    {
        private readonly IGuideRepository _guideRepository = DependencyContainer.GetInstance<IGuideRepository>();

        public GuideService()
        {
        }

        public List<Guide> GetAllGuides()
        {
            return _guideRepository.GetAll();
        }

        public Guide GetGuideById(int id)
        {
            return _guideRepository.GetById(id);
        }

        public void SaveGuide(Guide guide)
        {
            _guideRepository.Save(guide);
        }

        public void UpdateGuide(Guide guide)
        {
            _guideRepository.Update(guide);
        }

        public void DeleteGuideById(int id)
        {
            _guideRepository.DeleteById(id);
        }

        public void DeleteGuide(Guide guide)
        {
            _guideRepository.Delete(guide);
        }
    }
}
