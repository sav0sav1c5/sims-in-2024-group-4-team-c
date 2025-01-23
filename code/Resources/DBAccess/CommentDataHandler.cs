using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    internal class CommentDataHandler : IDataHandler<Comment>
    {
        private DataContext dataContext = GlobalDataContext.DataContext;
        public void Delete(Comment entity)
        {
            if(entity != null)
            {
                dataContext.Remove(entity);
            }
        }

        public void DeleteById(int entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetAll()
        {
            return dataContext.Comments.ToList();
        }

        public void Save(IEnumerable<Comment> entities)
        {
            dataContext.Comments.AddRange(entities);
            dataContext.SaveChanges();
        }

        public Comment SaveOneEntity(Comment entity)
        {
            //dataContext.Comments.Add(entity);
            //dataContext.SaveChanges();
            //return entity;
            var localEntry = dataContext.Comments
                .Local.FirstOrDefault(e => e.Id.Equals(entity.Id));

            //detach the entity
            if (localEntry != null)
            {
                dataContext.Entry(localEntry).State = EntityState.Detached;
            }
            //Set the modified flag
            dataContext.Entry(entity).State = EntityState.Modified;

            dataContext.Comments.Add(entity);
            dataContext.SaveChanges();
            return entity;
        }

        public void Update(Comment entity)
        {
            throw new NotImplementedException();
        }

        public Comment UpdateEntity(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
