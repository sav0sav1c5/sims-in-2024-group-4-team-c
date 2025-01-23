using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    [Table("TourAttendance")]
    public class TourAttendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TouristId { get; set; }

        [ForeignKey("TouristId")]
        public virtual Tourist Tourist { get; set; }

        public int TourReservationId { get; set; }

        [ForeignKey("TourReservationId")]
        public virtual TourReservation TourReservation { get; set; }

        public bool IsPresent { get; set; }
        [Required]
        public int Age { get; set;}

        public bool IsConfirmed { get; set; }

        [ForeignKey("CheckpointId")]
        public int? CheckpointJoinedId { get; set; }
        public virtual Checkpoint CheckpointJoined { get; set; }

        public TourAttendance() { }

        public TourAttendance(int touristId, int tourReservationId, bool isPresent, int age, bool isConfirmed, int checkpointId)
        {
            Age = age;
            TouristId = touristId;
            TourReservationId = tourReservationId;
            IsPresent = false;
            IsConfirmed = false;
            CheckpointJoinedId = checkpointId;
        }

        public TourAttendance(int touristId, int tourReservationId, TourReservation tourReservation)
        {
            TouristId = touristId;
            TourReservationId = tourReservationId;
            TourReservation = tourReservation;
            IsPresent = false;
            IsConfirmed = false;
        }
    }
}