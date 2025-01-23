using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class ReservationState
    {
        public enum State
        {
            Reserved = 1,
            Active,
            Ended
        }
        //Example: Reservation is from 15.3 to 25.3 and the current date is 17.3 so the 'State' is 'Active'
        //Example: Reservation is from 22.3 to 26.3 and the current date is 17.3 so the 'State' is 'Reserved'


        public ReservationState() { }


    }
}
