using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class ReservationModificationRequestState
    {
        public enum State
        {
            Pending = 1,
            Approved ,
            Denied
        }
        
        /// <summary>
        /// </summary>
        /// <returns>default return value if 'Denied'</returns>
        public static string StateToString(ReservationModificationRequestState.State state)
        {
            switch (state)
            {
                case ReservationModificationRequestState.State.Pending:
                    return "Pending";
                case ReservationModificationRequestState.State.Approved:
                    return "Approved";
                default:
                    return "Denied";
                //case ReservationModificationRequestState.State.Denied:
                //    return "Denied";
            }
        }
        /// <summary>
        /// </summary>
        /// <returns>Returns empty string on failure</returns>
        public static ReservationModificationRequestState.State StringToState(string state)
        {
            if (string.Equals(state, "Pending"))
                return ReservationModificationRequestState.State.Pending;
            if (string.Equals(state, "Approved"))
                return ReservationModificationRequestState.State.Approved;
            
            //if (string.Equals(state, "Denied"))
            return ReservationModificationRequestState.State.Denied;
        }

        public ReservationModificationRequestState() { }
    }
}
