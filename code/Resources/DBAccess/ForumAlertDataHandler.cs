using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    internal class ForumAlertDataHandler : IDataHandler<ForumAlert>
    {
        private DataContext dataContext = GlobalDataContext.DataContext;
        public void Delete(ForumAlert entity)
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

        public IEnumerable<ForumAlert> GetAll()
        {
            return dataContext.ForumAlert.ToList();
        }

        public void Save(IEnumerable<ForumAlert> entities)
        {
            dataContext.ForumAlert.AddRange(entities);
            dataContext.SaveChanges();
        }

        public ForumAlert SaveOneEntity(ForumAlert entity)
        {
            dataContext.ForumAlert.Add(entity);
            dataContext.SaveChanges();
            return entity;
        }

        public void Update(ForumAlert entity)
        {
            var existingForumAlert = dataContext.ForumAlert.FirstOrDefault(forumAlert => forumAlert.Id == entity.Id);

            if(existingForumAlert != null)
            {
                existingForumAlert.OwnerId = entity.OwnerId;
                existingForumAlert.ForumId = entity.ForumId;
                existingForumAlert.isOpened = entity.isOpened;

                dataContext.SaveChanges();
            }
        }

        public ForumAlert UpdateEntity(ForumAlert entity)
        {
            throw new NotImplementedException();
        }
    }
}
