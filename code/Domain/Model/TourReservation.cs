using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Domain.Model
{
    [Table("TourReservation")]
    public class TourReservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TourId { get; set; }

        [ForeignKey("TourId")]
        public virtual Tour? Tour { get; set; }

        public int GuideId { get; set; }

        [ForeignKey("GuideId")]
        public virtual Guide? Guide { get; set; }

        public int TouristsNumber { get; set; }
        public DateTime DateAndTime { get; set; }

        [Required]
        public virtual TourStates.Type TourState { get; set; }

        public virtual ICollection<TourParticipant> TourParticipants { get; set; } = new List<TourParticipant>();

        public TourReservation() { }

        public TourReservation(int id, int tourId, TourStates.Type state, int guideId, int touristsNumber)
        {
            Id = id;
            TourId = tourId;
            GuideId = guideId;
            TouristsNumber = touristsNumber;
            TourState = state;
        }

        public TourReservation(int tourId, TourStates.Type state, int guideId, int touristsNumber, DateTime dateTime)
        {
            TourId = tourId;
            GuideId = guideId;
            TouristsNumber = touristsNumber;
            TourState = state;
            DateAndTime = dateTime;
        }

        public TourReservation(int tourId, Tour tour, int guideId, int touristsNumber, ICollection<TourParticipant> tourParticipants)
        {
            TourId = tourId;
            Tour = tour;
            GuideId = guideId;
            TouristsNumber = touristsNumber;
            TourParticipants = tourParticipants;
        }

        public TourReservation(int tourId, int guideId, int touristsNumber, ICollection<TourParticipant> tourParticipants)
        {
            TourId = tourId;
            GuideId = guideId;
            TouristsNumber = touristsNumber;
            TourParticipants = tourParticipants;
        }
    }
}
