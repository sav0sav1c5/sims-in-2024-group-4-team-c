using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class RateGuestDTO
    {
        public string AccommodationName { get; set; }
        public string GuestName { get; set; }
        public int OwnerId { get; set; }
        public int GuestId { get; set; }
        public int AccommodationId { get; set; }
        public int AccommodationReservationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public RateGuestDTO(string accommodationName, string guestName, int ownerId, int guestId, int accommodationId, int accommodationReservationId, DateOnly startDate, DateOnly endDate)
        {
            AccommodationName = accommodationName;
            GuestName = guestName;
            OwnerId = ownerId;
            GuestId = guestId;
            AccommodationId = accommodationId;
            AccommodationReservationId = accommodationReservationId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public RateGuestDTO()
        {
        }
    }
}
