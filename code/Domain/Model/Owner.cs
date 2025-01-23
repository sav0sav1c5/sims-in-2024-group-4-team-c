using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Owner : User
    {
        public Owner()
        {
            UserType = UserType.Owner;
        }

        public Owner(string username, string password, string firstName, string lastName, string email)
        {
            UserType = UserType.Owner;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
