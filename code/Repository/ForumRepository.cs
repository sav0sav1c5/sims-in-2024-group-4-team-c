using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class ForumRepository : IForumRepository
    {
        private List<Forum> _forums;
        private readonly IDataHandler<Forum> _forumDataHandler;

        public ForumRepository()
        {
            _forumDataHandler = new ForumDataHandler();
            _forums = _forumDataHandler.GetAll().ToList();
        }


        public void Delete(Forum entity)
        {
            _forumDataHandler.Delete(entity);
            _forums.Remove(entity);
        }

        public void DeleteById(int id)
        {
            _forumDataHandler.DeleteById(id);
            _forums.RemoveAll(forum => forum.Id == id);
        }

        public List<Forum> GetAll()
        {
            //throw new NotImplementedException();
            return _forums;
        }

        public Forum? GetById(int id)
        {
            return _forums.Where(forum => forum.Id == id).FirstOrDefault();
        }

        public void Save(Forum entity)
        {
            if (entity == null) return;
            _forumDataHandler.SaveOneEntity(entity);
            _forums.Add(entity);
        }

        public void Update(Forum entity)
        {
            //throw new NotImplementedException();
            _forumDataHandler.Update(entity);
        }

        public List<Forum> GetByCountryAndCity(string country,string city)
        {
            return _forums.FindAll(
                forum => forum.Location.Country.Equals(country)
                    && forum.Location.City.Equals(city)
                );
        }

        public List<Forum> GetByCountry(string country)
        {
            return _forums.FindAll(
                forum => forum.Location.Country.Equals(country)
                );
        }

        public List<Forum> GetByCity(string city)
        {
            return _forums.FindAll(
                forum => forum.Location.City.Equals(city)
                );
        }
    }
}
