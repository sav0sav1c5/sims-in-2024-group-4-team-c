using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourRequestStates
    {
        public enum Type
        {
            Pending,
            Accepted,
            Declined
        }
        public TourRequestStates() { }

        public static string TypeToString(Type type)
        {
            switch (type)
            {
                case Type.Pending:
                    return "Pending";
                case Type.Accepted:
                    return "Accepted";
                case Type.Declined:
                    return "Declined";
                default:
                    return string.Empty;
            }
        }

        public static Type? StringToTourStateType(string type)
        {
            return string.Equals(type, "Pending") ? Type.Pending :
                   string.Equals(type, "Accepted") ? Type.Accepted :
                   string.Equals(type, "Declined") ? Type.Declined :
                   null;
        }
    }
}
