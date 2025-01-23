using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using BookingApp.DTOs;

namespace BookingApp.Domain.Model
{
    public class AccommodationReservationModificationRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("AccommodationReservation")]
        public int AccommodationReservationId { get; set; }
        [Required]
        public virtual AccommodationReservation? AccommodationReservation { get; set; }

        [Required]
        public virtual ReservationModificationRequestState.State RequestState { get; set; }

        public DateTime StartDate { get; set; }
        
        public int NumberOfGuests { get; set; } 
        
        public int StayLength { get; set; }

        public string OwnerComment { get; set; } = string.Empty;

        public AccommodationReservationModificationRequest()
        {            
            this.RequestState = ReservationModificationRequestState.State.Pending;
        }
        public AccommodationReservationModificationRequest( AccommodationReservation accommodationReservation)
        {
            this.RequestState = ReservationModificationRequestState.State.Pending;
            this.AccommodationReservationId = accommodationReservation.Id;
            this.AccommodationReservation = accommodationReservation;
            this.StartDate = accommodationReservation.StartDate;
            this.NumberOfGuests = accommodationReservation.NumberOfGuests;
            this.StayLength = accommodationReservation.StayLength;
        }
        /// <summary>
        /// AccommodationReservation field will not be set
        /// </summary>
        public AccommodationReservationModificationRequest(AccommodationReservationDTO accommodationReservationDTO)
        {
            this.RequestState = ReservationModificationRequestState.State.Pending;
            this.AccommodationReservationId = accommodationReservationDTO.Id;
            //this.AccommodationReservation = accommodationReservationDTO;      //!NOT SET!
            this.StartDate = accommodationReservationDTO.StartDate;
            this.NumberOfGuests = accommodationReservationDTO.NumberOfGuests;
            this.StayLength = accommodationReservationDTO.StayLength;
        }
    }
}
