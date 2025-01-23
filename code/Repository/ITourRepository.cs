using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface ITourRepository : IBaseRepository<Tour>
    {
        Tour GetById(int id);
        Tour Save(Tour tour);
        void Delete(Tour tour); 
        List<Tour> GetByName(string name);
        List<Tour> GetByGuideId(int guideId);
        List<Tour> GetByState(string state);
        List<Tour> GetByCity(string city);
        List<Tour> GetByDuration(int duration);
        List<Tour> GetByLanguage(string language);
        List<Tour> GetByGuestsNumber(int guestsNumber);
    }
}
