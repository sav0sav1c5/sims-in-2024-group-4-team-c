using BookingApp.Services.GuestServices;
using BookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Domain.Model
{
    [Table("AccommodationReservation")]
    public class AccommodationReservation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Accommodation")]
        public int AccommodationId { get; set; }
        [Required]
        public virtual Accommodation? Accommodation { get; set; }

        [Required]
        [ForeignKey("User")]
        public int GuestId { get; set; }
        [Required]
        public virtual Guest? Guest { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int NumberOfGuests { get; set; } = 1;
        [Required]
        public int StayLength { get; set; } = 1;    //in days
        [Required]
        public virtual ReservationState.State ReservationState { get; set; }
        //public virtual ICollection<AccommodationReservationModificationRequest> ModificationRequests { get; } = new List<AccommodationReservationModificationRequest>();


        public AccommodationReservation() { }

        public void ApplyModificationRequest(AccommodationReservationModificationRequest request)
        {
            this.NumberOfGuests = request.NumberOfGuests;
            this.StayLength = request.StayLength;
            this.StartDate = request.StartDate;
        }

        public AccommodationReservation(DateTime startDate, int numberOfGuests, int stayLength)
        {
            StartDate = startDate;
            NumberOfGuests = numberOfGuests;
            StayLength = stayLength;
        }

        public AccommodationReservation(DateTime startDate, int numberOfGuests, int stayLength, int guestId, int accommodationId)
        {
            StartDate = startDate;
            NumberOfGuests = numberOfGuests;
            StayLength = stayLength;
            GuestId = guestId;
            AccommodationId = accommodationId;
        }

        public AccommodationReservation(DateTime startDate, int numberOfGuests, int stayLength, int guestId, int accommodationId, ReservationState.State reservationState)
        {
            StartDate = startDate;
            NumberOfGuests = numberOfGuests;
            StayLength = stayLength;
            GuestId = guestId;
            AccommodationId = accommodationId;
            ReservationState = reservationState;
        }

        public bool IsOverLappingWithDateRange(CalendarDateRange dateRange)
        {
            return DateRangeUtility.IsDateRangeOverLapping(
                new CalendarDateRange(this.StartDate, this.StartDate.AddDays(this.StayLength - 1))
                , dateRange
            );
        }

        public bool IsOverlappingWithAnyDateRanges(List<CalendarDateRange> dateRanges)
        {
            foreach (CalendarDateRange dateRange in dateRanges)
            {
                if (this.IsOverLappingWithDateRange(dateRange))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
