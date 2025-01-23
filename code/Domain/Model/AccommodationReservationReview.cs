using BookingApp.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BookingApp.Domain.Model
{
    [Table("AccommodationReservationReview")]
    public class AccommodationReservationReview
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Accommodation")]
        public int AccommodationId { get; set; }
        public virtual Accommodation? Accommodation { get; set; }
        [Required]
        [ForeignKey("User")]
        public int GuestId { get; set; }
        public virtual Guest? Guest { get; set; }
        [Required]
        [ForeignKey("User")]
        public int OwnerId { get; set; }
        public virtual Owner? Owner { get; set; }
        [Required]
        [ForeignKey("AccommodationReservation")]
        public int AccommodationReservationId { get; set; }
        public virtual AccommodationReservation? AccommodationReservation { get; set; }
        public int? Cleanliness { get; set; } = 0;
        public int? RuleCompliance { get; set; } = 0;
        public int? Correctness { get; set; } = 0;
        public string? Comment { get; set; } = string.Empty;

        /// <summary>
        /// Ideja je da ako je 0 ili false gost je ocjenio 
        /// A ukoliko je 1 ili ti true tada je vlasnik ocjenio
        /// </summary>
        [Required]
        public bool Direction { get; set; }

        public bool? IsInNeedOfRenovation { get; set; } = false;
        public int? RenovationNeed { get; set; } = 0;
        /// <summary>
        /// Paths to images seperated by ','
        /// </summary>
        public string? Images { get; set; } = string.Empty;
        //Check DataContext::OnModelCreation() to see how its implemented

        public AccommodationReservationReview(){ }

        /// <summary>
        /// Konstruktor za ownera
        /// </summary>
        public AccommodationReservationReview(int accommodationId, int guestId, int ownerId, int cleanlines, int ruleCompliance, string comment, bool direction,int accommodationReservationId)
        {
            AccommodationId = accommodationId;
            GuestId = guestId;
            OwnerId = ownerId;
            Cleanliness = cleanlines;
            RuleCompliance = ruleCompliance;
            Comment = comment;
            Direction = direction;
            Correctness = 0;         //not a parameter of the ctor
            AccommodationReservationId = accommodationReservationId;
        }
        /// <summary>
        /// ctor for guest
        /// </summary>
        public AccommodationReservationReview(int accommodationId, int guestId, int ownerId, int cleanlines, int ruleCompliance, string comment, bool direction, int accommodationReservationId, int correctness, bool isInNeedOfRenovation, int renovationNeed)
        {
            AccommodationId = accommodationId;
            GuestId = guestId;
            OwnerId = ownerId;
            Cleanliness = cleanlines;
            RuleCompliance = ruleCompliance;
            Comment = comment;
            Direction = direction;
            AccommodationReservationId = accommodationReservationId;
            IsInNeedOfRenovation = isInNeedOfRenovation;
            RenovationNeed = renovationNeed;
            Correctness = correctness;     
        }
        /// <summary>
        /// ctor for guest
        /// </summary>
        public AccommodationReservationReview(RateOwnerDTO rateOwnerDTO)
        {
            AccommodationId = rateOwnerDTO.AccommodationId;
            GuestId = rateOwnerDTO.GuestId;
            OwnerId = rateOwnerDTO.OwnerId;
            Cleanliness = rateOwnerDTO.ReviewCleanlines;
            RuleCompliance = 0;
            Comment = rateOwnerDTO.ReviewComment;
            Direction = false;
            AccommodationReservationId = rateOwnerDTO.AccommodationReservationId;
            IsInNeedOfRenovation = rateOwnerDTO.IsAccommodationInNeedOfRenovation;
            RenovationNeed = rateOwnerDTO.AccommodationRenovationNeed;
            Correctness = rateOwnerDTO.ReviewCorrectnes;
        }
    }
}
