using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;
using BookingApp.Resources.DBAccess;


namespace BookingApp.Resources.DBAcces
{
    public class UserDataHandler : IDataHandler<User>
    {
        private DataContext dataContext = new DataContext();

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return dataContext.Users.ToList();  
        }

        public void Save(IEnumerable<User> entities)
        {
            dataContext.Users.AddRange(entities);

            dataContext.SaveChanges();
        }

        public User SaveOneEntity(User entity)
        {
            dataContext.Users.Add(entity);

            dataContext.SaveChanges();

            return entity;
        }

        public void Update(User entity)
        {
            var existingUser = dataContext.Users.FirstOrDefault(user => user.Id == entity.Id);

            if (existingUser != null)
            {
                //existingUser.Status = entity.Status;
                //existingUser.StatusChangedDate = entity.StatusChangedDate;

                dataContext.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {

        }

        public User UpdateEntity(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
