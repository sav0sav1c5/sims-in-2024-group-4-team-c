using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface ITouristRepository : IBaseRepository<Tourist>
    {
        void Save(Tourist tourist);
        Tourist? GetById(int id);
        List<Tourist> GetAll();
        void Update(Tourist tourist);
        void DeleteById(int id);
        void Delete(Tourist tourist);
    }
}
