using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    [Table("CurrentDate")]
    public class CurrentDate
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public CurrentDate() { }

        public CurrentDate(DateTime date)
        {
            Date = date;
        }

        public bool IsBeforeDate(DateTime date)
        {
            if (this.Date < date )
            {
                return true;
            }
            return false;
        }

        public bool IsBeforeDate(DateOnly date)
        {
            if (DateOnly.FromDateTime(this.Date) < date)
            {
                return true;
            }
            return false;
        }


    }
}
