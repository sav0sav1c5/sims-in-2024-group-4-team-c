using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class StatisticDTO
    {
        public int Year { get; set; }
        public string Month { get; set; }

        public int AccommodationReservations { get; set; }
        public int CanceledReservations { get; set; }
        public int ModifiedReservations { get; set; }
        public int RenovationRequests { get; set; }

        public StatisticDTO(int year, int month, int accommodationReservations, int canceledReservations, int modifiedReservations, int renovationRequests)
        {
            Year = year;
            Month = MonthFromNumberToString(month);
            AccommodationReservations = accommodationReservations;
            CanceledReservations = canceledReservations;
            ModifiedReservations = modifiedReservations;
            RenovationRequests = renovationRequests;
        }

        public StatisticDTO()
        {
        }

        public string MonthFromNumberToString(int month)
        {
            string Month = "";
            switch (month)
            {
                case 1:
                    Month = "January";
                    break;
                case 2:
                    Month = "February";
                    break;
                case 3:
                    Month = "March";
                    break;
                case 4:
                    Month = "April";
                    break;
                case 5:
                    Month = "May";
                    break;
                case 6:
                    Month = "June";
                    break;
                case 7:
                    Month = "July";
                    break;
                case 8:
                    Month = "August";
                    break;
                case 9:
                    Month = "September";
                    break;
                case 10:
                    Month = "October";
                    break;
                case 11:
                    Month = "November";
                    break;
                case 12:
                    Month = "December";
                    break;
                default:
                    Month = "Unknown";
                    break;
            }
            return Month;
        }
    }
}
