using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface IRenovationRepository : IBaseRepository<Renovation>
    {
        public void Delete(Renovation entity);


        public void DeleteById(int id);

        public List<Renovation> GetAll();

        public Renovation? GetById(int id);

        public void Save(Renovation entity);

        public void Update(Renovation entity);
    }
}
