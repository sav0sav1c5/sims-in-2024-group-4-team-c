using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    [Table("ComplexTourRequest")]
    public class ComplexTourRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ComplexTourPart> Parts { get; set; } = new List<ComplexTourPart>();

        [ForeignKey("Tourist")]
        public int TouristId { get; set; }
        public virtual Tourist Tourist { get; set; }

        public ComplexTourRequest() { }

        public ComplexTourRequest(int id, string name, ICollection<ComplexTourPart> parts, int touristId)
        {
            Id = id;
            Name = name;
            Parts = parts;
            TouristId = touristId;
        }

        public ComplexTourRequest(string name, ICollection<ComplexTourPart> parts, int touristId)
        {
            Name = name;
            Parts = parts;
            TouristId = touristId;
        }
    }
}
