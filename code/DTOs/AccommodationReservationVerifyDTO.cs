using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    class AccommodationReservationVerifyDTO
    {

        public int Id { get; set; }
        
        public int AccommodationId { get; set; }
        
        
        public int GuestId { get; set; }
       
        
        public int OwnerId { get; set; }
        
        public int AccommodationReservationId { get; set; }
        
        
        public int? Cleanlines { get; set; } = 0;
        
        public int? RuleCompliance { get; set; } = 0;
        
        public int? Correctnes { get; set; } = 0;
        
        public string? Comment { get; set; } = string.Empty;

        public bool Direction { get; set; }

        public bool Verify { get; set; }

        public AccommodationReservationVerifyDTO(AccommodationReservationReview accommodationReservationReview, bool _verify)
        {
            Id = accommodationReservationReview.Id;
            AccommodationId = accommodationReservationReview.AccommodationId;
            GuestId = accommodationReservationReview.GuestId;
            OwnerId = accommodationReservationReview.OwnerId;
            AccommodationReservationId = accommodationReservationReview.AccommodationReservationId;
            Cleanlines = accommodationReservationReview.Cleanliness;
            RuleCompliance = accommodationReservationReview.RuleCompliance;
            Correctnes = accommodationReservationReview.Correctness;
            Comment = accommodationReservationReview.Comment;
            Direction = accommodationReservationReview.Direction;
            Verify = _verify;
        }
    }
}
