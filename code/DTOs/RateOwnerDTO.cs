using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class RateOwnerDTO
    {
        public int AccommodationId { get; set; }
        public int AccommodationReservationId { get; set; }
        public int GuestId { get; set; }
        public int OwnerId { get; set; }
        public int ReviewCleanlines { get; set; }
        public int ReviewCorrectnes { get; set; } 
        public string ReviewComment { get; set; } = string.Empty;
        public bool? IsAccommodationInNeedOfRenovation { get; set; }
        public int? AccommodationRenovationNeed { get; set; }
        public string? ReviewImages { get; set; } = string.Empty;

        public RateOwnerDTO()
        {
        }
        public RateOwnerDTO(int accommodationId, int accommodationReservationId, int guestId, int ownerId, int reviewCleanlines, int reviewCorrectnes, string reviewComment, bool isAccommodationInNeedOfRenovation, int accommodationRenovationNeed, string reviewImages)
        {
            AccommodationId = accommodationId;
            AccommodationReservationId = accommodationReservationId;
            GuestId = guestId;
            OwnerId = ownerId;
            ReviewCleanlines = reviewCleanlines;
            ReviewCorrectnes = reviewCorrectnes;
            ReviewComment = reviewComment;
            IsAccommodationInNeedOfRenovation = isAccommodationInNeedOfRenovation;
            AccommodationRenovationNeed = accommodationRenovationNeed;
            ReviewImages = reviewImages;
        }
    }
}
