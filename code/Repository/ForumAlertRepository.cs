using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class ForumAlertRepository : IForumAlertRepository
    {
        private List<ForumAlert> _forumAlerts;
        private readonly IDataHandler<ForumAlert> _forumAlertDataHandler;

        public ForumAlertRepository()
        {
            _forumAlertDataHandler = new ForumAlertDataHandler();
            _forumAlerts = _forumAlertDataHandler.GetAll().ToList();
        }
        public void Delete(ForumAlert entity)
        {
            _forumAlertDataHandler.Delete(entity);
            _forumAlerts.Remove(entity);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ForumAlert> GetAll()
        {
            return _forumAlerts;
        }

        public ForumAlert? GetById(int id)
        {
            return _forumAlerts.FirstOrDefault(forumAlert => forumAlert.Id == id);
        }

        public void Save(ForumAlert entity)
        {
            if (entity != null)
            {
                _forumAlertDataHandler.SaveOneEntity(entity);
                _forumAlerts.Add(entity);
            }
        }

        public void Update(ForumAlert entity)
        {
            _forumAlertDataHandler.Update(entity);
        }
    }
}
