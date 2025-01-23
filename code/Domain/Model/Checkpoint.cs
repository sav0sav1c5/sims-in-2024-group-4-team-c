using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Domain.Model
{
    [Table("Checkpoint")]

    public class Checkpoint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Location")]
        public int LocationId { get; set; }

        [Required]
        public bool IsActive { get; set; } = false;

        [Required]
        public virtual Location? Location { get; set; }

        [Required]
        [ForeignKey("Tour")]
        public int TourId { get; set; }

        public virtual Tour? Tour { get; set; } = null!;

        public Checkpoint() { }

        public Checkpoint(int id, string name, int locationId, int tourId, bool isActive)
        {
            Id = id;
            Name = name;
            LocationId = locationId;
            TourId = tourId;
            IsActive = isActive;
        }
    }
}
