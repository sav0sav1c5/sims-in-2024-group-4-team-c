using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface IForumRepository : IBaseRepository<Forum>
    {
        public void Delete(Forum entity);

        public void DeleteById(int id);

        public List<Forum> GetAll();

        public Forum? GetById(int id);

        public void Save(Forum entity);

        public void Update(Forum entity);

        public List<Forum> GetByCountryAndCity(string country, string city);

        public List<Forum> GetByCountry(string country);

        public List<Forum> GetByCity(string city);
    }
}
