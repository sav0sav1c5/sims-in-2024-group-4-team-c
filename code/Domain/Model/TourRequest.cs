using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    [Table("TourRequest")]
    public class TourRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [ForeignKey("Tourist")]
        public int TouristId { get; set; }
        public virtual Tourist Tourist { get; set; } 
  
        [Required]
        public string TourName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Description { get; set; }

        public int NumberOfParticipants { get; set; }
        public bool NotifyTourist { get; set; }

        [Required]
        public virtual TourRequestStates.Type TourRequestState { get; set; }
        public virtual ICollection<TourParticipant> TourParticipants { get; set; } = new List<TourParticipant>();
        
        public TourRequest() { }

        public TourRequest(int id, string tourName, DateTime startDate, DateTime endDate, string country, string city, string language, string description, int numberOfParticipants, TourRequestStates.Type tourRequestState, bool notify)
        {
            Id = id;
            TourName = tourName;
            StartDate = startDate;
            EndDate = endDate;
            Country = country;
            City = city;
            Language = language;
            Description = description;
            NumberOfParticipants = numberOfParticipants;
            TourRequestState = tourRequestState;
            NotifyTourist = false;
            NotifyTourist = notify;
        }

        public TourRequest(string tourName, DateTime startDate, DateTime endDate, string country, string city, string language, string description, List<TourParticipant> participants)
        {
            TourName = tourName;
            StartDate = startDate;
            EndDate = endDate;
            Country = country;
            City = city;
            Language = language;
            Description = description;
            TourParticipants = participants;           
        }
    }
}
