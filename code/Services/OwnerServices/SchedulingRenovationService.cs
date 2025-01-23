using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.OwnerServices
{
    public class SchedulingRenovationService
    {
        private readonly ILocationRepository _locationRepository = DependencyContainer.GetInstance<ILocationRepository>();
        private readonly IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();
        private readonly IAccommodationReservationRepository _accommodationReservationRepository = DependencyContainer.GetInstance<IAccommodationReservationRepository>(); 
        private readonly IRenovationRepository _renovationRepository = DependencyContainer.GetInstance<IRenovationRepository>();

        public List<Accommodation> LoadData(int id)
        {
            List<Accommodation> data = _accommodationRepository.GetAll().Where(accommodation => accommodation.OwnerId == id).ToList();
            return data;
        }

        public void SaveRenovation(Renovation renovation)
        {
            _renovationRepository.Save(renovation);
        }

        public List<Renovation> AvailableTermins(SchedulingRenovationDTO schedulingRenovationDTO)
        {
            List<Renovation> renovations = new List<Renovation>();
            var reservations = _accommodationReservationRepository.GetAll().Where(reservation => reservation.AccommodationId == schedulingRenovationDTO.AccommodationId);
            int estimated = schedulingRenovationDTO.EstimatedDuration;
            // Skup za praćenje već rezervisanih datuma
            HashSet<DateRange> reservedDates = new HashSet<DateRange>();
            HashSet<DateRange> renovationDates = new HashSet<DateRange>();

            // Prolazimo kroz sve rezervacije i dodajemo rezervisane datume u skup
            foreach (var reservation in reservations)
            {
                    DateRange date = new DateRange(DateOnly.FromDateTime(reservation.StartDate), DateOnly.FromDateTime(reservation.StartDate.AddDays(reservation.StayLength)));
                
                    reservedDates.Add(date);
                
            }

            // Prolazimo kroz sve datume u opsegu renoviranja i dodajemo samo one koji nisu rezervisani
            for (int i = 0; schedulingRenovationDTO.StartDate.AddDays(i) <= schedulingRenovationDTO.EndDate; i++)
            {
                DateRange currentDate = new DateRange(schedulingRenovationDTO.StartDate.AddDays(i), schedulingRenovationDTO.StartDate.AddDays(i+estimated));
                if (!reservedDates.Contains(currentDate))
                {
                    if(!renovationDates.Contains(currentDate) && schedulingRenovationDTO.StartDate.AddDays(i + estimated) <= schedulingRenovationDTO.EndDate)
                    {
                    renovationDates.Add(currentDate);
                    SchedulingRenovationDTO dto = new SchedulingRenovationDTO(schedulingRenovationDTO.AccommodationId, schedulingRenovationDTO.StartDate.AddDays(i), schedulingRenovationDTO.StartDate.AddDays(i + estimated), estimated, true);
                    Renovation renovation = new Renovation(dto);
                    renovations.Add(renovation);
                    }
                }
            }

            return renovations;
        }


    }
    public struct DateRange
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public DateRange(DateOnly startDate, DateOnly endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
