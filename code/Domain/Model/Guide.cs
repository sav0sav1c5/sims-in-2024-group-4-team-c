using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Guide : User
    {
        public Guide()
        {
            UserType = UserType.Guide;
        }
        public Guide(string username, string password, string firstName, string lastName, string email)
        {
            UserType = UserType.Guide;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
