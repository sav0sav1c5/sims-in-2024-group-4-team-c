using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BookingApp.Domain.Model
{
    [Table("TourParticipant")]
    public class TourParticipant
    {
        [ForeignKey("TourReservation")]
        public int? TourReservationId { get; set; }

        public virtual TourReservation? TourReservation { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public int Age { get; set; }
        [Required]
        public bool IsPresent { get; set; } = false;

        [ForeignKey("Checkpoint")]
        public int? CheckpointJoinedId { get; set; } // Checkpoint where the participant joined the tour

        [ForeignKey("TourRequest")]
        public int? TourRequestId { get; set; } // Tour request that the participant is a part of


        public TourParticipant() { }

        public TourParticipant(int reservationId, string firstName, int age, string lastName, string email)
        {
            Age = age;
            TourReservationId = reservationId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsPresent = false;
        }
        public TourParticipant(string firstName, int requestId, int age, string lastName, string email)
        {
            Age = age;
            TourRequestId = requestId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsPresent = false;
        }
        public TourParticipant(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsPresent = false;
        }
        public TourParticipant(int reservationId, string firstName, string lastName, string email)
        {
            TourReservationId = reservationId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsPresent = false;
        }
    }
}
