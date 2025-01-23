using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourStates
    {
        public enum Type
        {
            Pending,
            Started,
            Finished,
            Canceled
        }
        public TourStates() { }

        public static string TypeToString(Type type)
        {
            switch (type)
            {
                case Type.Pending:
                    return "Pending";
                case Type.Started:
                    return "Started";
                case Type.Finished:
                    return "Finished";
                case Type.Canceled:
                    return "Canceled";
                default:
                    return string.Empty;
            }
        }

        public static Type? StringToTourStateType(string type)
        {
            return string.Equals(type, "Pending") ? Type.Pending :
                   string.Equals(type, "Started") ? Type.Started :
                   string.Equals(type, "Finished") ? Type.Finished :
                   string.Equals(type, "Canceled") ? Type.Canceled :
                   null;
        }
    }
}
