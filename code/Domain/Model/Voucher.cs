using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    [Table("Voucher")]
    public class Voucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public int TouristId { get; set; }

        [ForeignKey("TouristId")]
        public virtual Tourist Tourist { get; set; }
        public bool IsGlobal { get; set; }

        public Voucher() { }

        public Voucher(string name, DateTime expirationDate, int touristId)
        {
            Name = name;
            ExpirationDate = expirationDate;
            TouristId = touristId;
            IsGlobal = false;
        }
    }
}
