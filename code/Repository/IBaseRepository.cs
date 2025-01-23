using BookingApp.Domain.Model;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        public void Save(T entity);

        public T? GetById(int id);

        public List<T> GetAll();

        public void Update(T entity);

        public void DeleteById(int id);

        public void Delete(T entity);
    }
}
