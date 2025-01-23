using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Tourist : User
    {
        public Tourist()
        {
            UserType = UserType.Tourist;
        }
        public Tourist(string username, string password, string firstName, string lastName, string email)
        {
            UserType = UserType.Tourist;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}