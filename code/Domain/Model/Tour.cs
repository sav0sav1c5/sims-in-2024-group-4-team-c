using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Domain.Model
{
    [Table("Tour")]
    public class Tour : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tour name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required.")]
        [ForeignKey("Location")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public virtual Location? Location { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Language is required.")]
        public string Language { get; set; } = string.Empty;

        [Required(ErrorMessage = "Maximum number of tourists is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Maximum number of tourists must be at least 1.")]
        public int MaxTouristNumber { get; set; } = 15;

        [Range(0, int.MaxValue, ErrorMessage = "Available seats must be a non-negative number.")]
        public int AvailableSeats { get; set; } = 15;

        [Required(ErrorMessage = "Guide is required.")]
        [ForeignKey("Guide")]
        public int GuideId { get; set; }

        public virtual Guide? Guide { get; set; }

        public virtual ICollection<Checkpoint> Checkpoints { get; set; } = new List<Checkpoint>();

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 hour.")]
        public int Duration { get; set; } = 2;

        public string Images { get; set; } = string.Empty;

        // Parameterless constructor
        public Tour() { }

        // Constructor with all parameters
        public Tour(int id, string name, int locationId, string description, string language, int maxTouristNumber, int availableSeats, int guideId, int duration, string images)
        {
            Id = id;
            Name = name;
            LocationId = locationId;
            Description = description;
            Language = language;
            MaxTouristNumber = maxTouristNumber;
            AvailableSeats = availableSeats;
            GuideId = guideId;
            Duration = duration;
            Images = images;
        }

        // Constructor with required parameters and default values for MaxTouristNumber, AvailableSeats, and Duration
        public Tour(string name, int locationId, string description, string language, int guideId, string images = "", int maxTouristNumber = 15, int availableSeats = 15, int duration = 2)
        {
            Name = name;
            LocationId = locationId;
            Description = description;
            Language = language;
            MaxTouristNumber = maxTouristNumber;
            AvailableSeats = availableSeats;
            GuideId = guideId;
            Duration = duration;
            Images = images;
        }

        // Custom validation logic
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Ensure that the available seats do not exceed the maximum tourist number
            if (AvailableSeats > MaxTouristNumber)
            {
                results.Add(new ValidationResult("Available seats cannot exceed the maximum number of tourists.", new[] { nameof(AvailableSeats) }));
            }

            return results;
        }
    }
}
