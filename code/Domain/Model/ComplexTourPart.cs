using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    [Table("ComplexTourPart")]
    public class ComplexTourPart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string TourName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime ApprovedDate { get; set; }

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
        public int GuideId { get; set;}

        [Required]
        public virtual TourRequestStates.Type TourRequestState { get; set; }
        public virtual ICollection<TourParticipant> TourParticipants { get; set; } = new List<TourParticipant>();

        public int ComplexTourRequestId { get; set; }

        [ForeignKey("ComplexTourRequestId")]
        public virtual ComplexTourRequest ComplexTourRequest { get; set; }

        public ComplexTourPart() { }

        public ComplexTourPart(string tourName, DateTime startDate, DateTime endDate, DateTime approvedDate, string country, string city, string language, string description, int numberOfParticipants, TourRequestStates.Type tourRequestState, ICollection<TourParticipant> tourParticipants, int complexTourRequestId)
        {
            TourName = tourName;
            StartDate = startDate;
            EndDate = endDate;
            ApprovedDate = approvedDate;
            Country = country;
            City = city;
            Language = language;
            Description = description;
            NumberOfParticipants = numberOfParticipants;
            NotifyTourist = false;
            TourRequestState = tourRequestState;
            TourParticipants = tourParticipants;
            ComplexTourRequestId = complexTourRequestId;
        }

        public ComplexTourPart(string tourName, DateTime startDate, DateTime endDate, DateTime approvedDate, string country, string city, string language, string description, ICollection<TourParticipant> tourParticipants, int complexTourRequestId)
        {
            TourName = tourName;
            StartDate = startDate;
            EndDate = endDate;
            ApprovedDate = approvedDate;
            Country = country;
            City = city;
            Language = language;
            Description = description;
            TourParticipants = tourParticipants;
            ComplexTourRequestId = complexTourRequestId;
        }

        public ComplexTourPart(int id, string tourName, DateTime startDate, DateTime endDate, DateTime approvedDate, string country, string city, string language, string description, int numberOfParticipants, bool notifyTourist, TourRequestStates.Type tourRequestState, ICollection<TourParticipant> tourParticipants, int complexTourRequestId, int guideId)
        {
            Id = id;
            TourName = tourName;
            StartDate = startDate;
            EndDate = endDate;
            ApprovedDate = approvedDate;
            Country = country;
            City = city;
            Language = language;
            Description = description;
            NumberOfParticipants = numberOfParticipants;
            NotifyTourist = false;
            TourRequestState = tourRequestState;
            TourParticipants = tourParticipants;
            ComplexTourRequestId = complexTourRequestId;
            GuideId = guideId;
        }
    }
}
