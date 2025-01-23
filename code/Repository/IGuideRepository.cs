using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface IGuideRepository : IBaseRepository<Guide>
    {
        void Save(Guide guide);
        Guide? GetById(int id);
        List<Guide> GetAll();
        void Update(Guide guide);
        void DeleteById(int id);
        void Delete(Guide guide);
    }
}
