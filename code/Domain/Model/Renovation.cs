using BookingApp.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    [Table("Renovation")]
    public class Renovation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int AccommodationId { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }  
        public int EstimatedDuration { get; set; }
        // if state is false renovation is passed 
        // if it is true renovation is not yet started
        public bool State { get; set; }
        public bool isCanceled { get; set; }

        public Renovation()
        {

        }

        public Renovation(Renovation renovation)
        {
            AccommodationId = renovation.AccommodationId;
            StartDate = renovation.StartDate;
            EndDate = renovation.EndDate;
            EstimatedDuration =renovation.EstimatedDuration;
            State = renovation.State;
            isCanceled = renovation.isCanceled;
        }
        public Renovation(int accommodationId, DateOnly startDate, DateOnly endDate, int estimatedDuration, bool state, bool _isCanceled)
        {
            
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            EstimatedDuration = estimatedDuration;
            State = state;
            isCanceled = _isCanceled;
        }

        public Renovation(SchedulingRenovationDTO schedulingRenovationDTO)
        {
            AccommodationId = schedulingRenovationDTO.AccommodationId;
            StartDate = schedulingRenovationDTO.StartDate;
            EndDate = schedulingRenovationDTO.EndDate;
            EstimatedDuration = schedulingRenovationDTO.EstimatedDuration;
            State = schedulingRenovationDTO.State;
        }
    }
}
