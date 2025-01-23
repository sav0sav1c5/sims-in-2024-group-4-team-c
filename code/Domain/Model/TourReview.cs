using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    [Table("TourReview")]
    public class TourReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Knowledge { get; set; }

        public int Language { get; set; }

        public int Interestingness { get; set; }

        public string Comment { get; set; }

        public string ReviewImages { get; set; }

        public int GuideId { get; set; }

        public int TourReservationId { get; set; }
        public virtual TourReservation TourReservation { get; set; }
        public bool IsValid { get; set; }

        public int TourAttendanceId { get; set; }

        [ForeignKey("TourAttendanceId")]
        public virtual TourAttendance TourAttendance { get; set; }

        public TourReview() { }

        public TourReview(int knowledge, int language, int interestingness, string comment, string images, int guideId, int tourReservationId, int attendanceId)
        {
            Knowledge = knowledge;
            Language = language;
            Interestingness = interestingness;
            Comment = comment;
            ReviewImages = images;
            GuideId = guideId;
            TourReservationId = tourReservationId;
            TourAttendanceId = attendanceId;
            IsValid = true;

        }
    }
}