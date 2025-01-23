using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;

namespace BookingApp.Domain.Model
{
    [Table("Accommodation")]
    public class Accommodation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public virtual Location? Location { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<AccommodationReservation> AccommodationReservations { get; } = new List<AccommodationReservation>();
        [Required]
        public virtual AccommodationTypes.Type AccommodationType { get; set; }
        [Required]
        public int MaxGuests { get; set; }
        [Required]
        public int MinReservationDays { get; set; }
        [Required]
        public int CancelationDeadline { get; set; }
        [Required]
        public string Images { get; set; } = string.Empty;
        [Required]
        public int OwnerId { get; set; }
        public virtual Owner? Owner { get; set; }
        public Accommodation() { }

        public Accommodation(string name)
        {
            Name = name;
        }
        public Accommodation(string name, int locationId, AccommodationTypes.Type accommodationType, int maxGuests, int minReservationDays, int cancelationDeadline, string Images, int ownerId)
        {
            Name = name;
            LocationId = locationId;
            AccommodationType = accommodationType;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            CancelationDeadline = cancelationDeadline;
            this.Images = Images;
            OwnerId = ownerId;
        }

        public Accommodation(string name, int locationId, AccommodationTypes.Type accommodationType, int maxGuests, int minReservationDays)
        {
            Name = name;
            LocationId = locationId;
            AccommodationType = accommodationType;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
        }

        public Accommodation(string name, int maxGuests, int cancelationDeadline, int ownerId, int locationId, int minReservationDays, AccommodationTypes.Type accommodationType, string images)
        {
            Name = name;
            MaxGuests = maxGuests;
            CancelationDeadline = cancelationDeadline;
            OwnerId = ownerId;
            LocationId = locationId;
            MinReservationDays = minReservationDays;
            AccommodationType = accommodationType;
            Images = images;
        }

        public bool AreAccommodationReservationRequirementsSatisfied(AccommodationReservation accommodationReservation, List<CalendarDateRange> reservedAccommodationDateRanges)
        {
            if (this.MaxGuests < accommodationReservation.NumberOfGuests
                || this.MinReservationDays > accommodationReservation.StayLength)
            {
                MessageBox.Show("Accommodation requirements not satisfied!");
                return false;
            }
            if (accommodationReservation.IsOverlappingWithAnyDateRanges(reservedAccommodationDateRanges))   //!
            {
                MessageBox.Show("Picked reservation date is overlapping with another reservation date!");
                return false;
            }

            return true;
        }

        public List<CalendarDateRange> GetAccommodationReservedDateRanges()
        {
            if (this.AccommodationReservations.Count <= 0)
            {
                return new List<CalendarDateRange>();
            }
            
            List<CalendarDateRange> reservedDateRanges = new List<CalendarDateRange>();
            foreach (AccommodationReservation it in this.AccommodationReservations)
            {
                if (it.ReservationState != ReservationState.State.Reserved)
                {
                    continue;
                }
                reservedDateRanges.Add(
                    new CalendarDateRange(it.StartDate, it.StartDate.AddDays(it.StayLength - 1))
                );
            }
            return reservedDateRanges;
        }

    }
}
