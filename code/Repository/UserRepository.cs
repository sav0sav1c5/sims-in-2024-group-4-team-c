using BookingApp.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Resources.DBAcces;


namespace BookingApp.Repository
{
    public class UserRepository
    {
        private List<User> _users;
        private readonly IDataHandler<User> _userDataHandler;

        public UserRepository()
        {
            _userDataHandler = new UserDataHandler();
            _users = _userDataHandler.GetAll().ToList();
        }
        public List<User> GetAll()
        {
            return _users;
        }

        public User? GetById(int id)
        {
            return _users.FirstOrDefault(user => user.Id == id);
        }

        public User? GetByUsername(string Username)
        {
            return _users.FirstOrDefault(users => users.EqualsUsername(Username));
        }

        public User? GetByPassword(string Password)
        {
            return _users.FirstOrDefault(users => users.EqualsUsername(Password));
        }

        public User? GetByFirstName(string FirstName)
        {
            return _users.FirstOrDefault(users => users.EqualsUsername(FirstName));
        }

        public User? GetByLastName(string LastName)
        {
            return _users.FirstOrDefault(users => users.EqualsUsername(LastName));
        }

        public User? GetByEmail(string Email)
        {
            return _users.FirstOrDefault(users => users.EqualsUsername(Email));
        }

        public void Update(User user)
        {
            _userDataHandler.Update(user);
        } 
    }
}
