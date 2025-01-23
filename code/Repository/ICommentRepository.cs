using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        public void Delete(Comment entity);

        public void DeleteById(int id);

        public List<Comment> GetAll();

        public Comment? GetById(int id);

        public void Save(Comment entity);

        public void Update(Comment entity);
    }
}
