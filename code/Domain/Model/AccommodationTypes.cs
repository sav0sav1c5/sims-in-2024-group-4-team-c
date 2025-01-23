using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class AccommodationTypes
    {
        public enum Type
        {
            Apartment,
            House,
            Cabin
        }
        public AccommodationTypes() { }

        public static string TypeToString(Type type)
        {
            switch (type)
            {
                case Type.Apartment:
                    return "Apartment";
                case Type.Cabin:
                    return "Cabin";
                case Type.House:
                    return "House";
                default:
                    return string.Empty;
            }
        }
        /// <summary>
        /// </summary>
        /// <returns>null if string is not an AccommodationType</returns>
        public static Type? StringToAccommodationType(string type)
        {
            return string.Equals(type, "Apartment") ? Type.Apartment :
                   string.Equals(type, "Cabin") ? Type.Cabin :
                   string.Equals(type, "House") ? Type.House :
                   null;
        }

    }
}
