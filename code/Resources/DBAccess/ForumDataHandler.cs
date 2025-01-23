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
    internal class ForumDataHandler : IDataHandler<Forum>
    {
        private DataContext dataContext = GlobalDataContext.DataContext;
        public void Delete(Forum entity)
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

        public IEnumerable<Forum> GetAll()
        {
            return dataContext.Forums
                .Include(forum => forum.Comments)
                .Include(forum => forum.Location)
                .Include(forum => forum.Author)
                .ToList();
        }

        public void Save(IEnumerable<Forum> entities)
        {
            dataContext.Forums.AddRange(entities);
            dataContext.SaveChanges();
        }

        public Forum SaveOneEntity(Forum entity)
        {
            //dataContext.Forums.Add(entity);
            //dataContext.SaveChanges();
            //return entity;

            var localEntry = dataContext.Forums
                .Local.FirstOrDefault(e => e.Id.Equals(entity.Id));

            //detach the entity
            if (localEntry != null)
            {
                dataContext.Entry(localEntry).State = EntityState.Detached;
            }
            //Set the modified flag
            dataContext.Entry(entity).State = EntityState.Modified;

            dataContext.Forums.Add(entity);
            dataContext.SaveChanges();
            return entity;
        }

        public void Update(Forum entity)
        {
            //throw new NotImplementedException();
            var existingEntity= dataContext.Forums.FirstOrDefault(x => x.Id == entity.Id);

            if (existingEntity != null)
            {
                existingEntity.Name = entity.Name;
                existingEntity.Description = entity.Description;
                existingEntity.AuthorId = entity.AuthorId;
                existingEntity.Author = entity.Author;
                existingEntity.LocationId = entity.LocationId;
                existingEntity.Location = entity.Location;
                existingEntity.Comments = entity.Comments;
                existingEntity.IsClosed = entity.IsClosed;
                existingEntity.IsUseful = entity.IsUseful;
                existingEntity.OwnerComments = entity.OwnerComments;
                existingEntity.GuestComments = entity.GuestComments;

                dataContext.SaveChanges();
            }
        }

        public Forum UpdateEntity(Forum entity)
        {
            throw new NotImplementedException();
        }
    }
}
