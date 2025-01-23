using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using BookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.Services.GuestServices
{
    public class GuestService
    {
        #region Data
        private readonly IGuestRepository _guestRepository = DependencyContainer.GetInstance<IGuestRepository>();
        private readonly IOwnerRepository _ownerRepository = DependencyContainer.GetInstance<IOwnerRepository>();
        private readonly IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();
        private readonly ILocationRepository _locationRepository = DependencyContainer.GetInstance<ILocationRepository>();
        private readonly IAccommodationReservationRepository _accommodationReservationRepository = DependencyContainer.GetInstance<IAccommodationReservationRepository>();
        private readonly ICurrentDateRepository _currentDateRepository = DependencyContainer.GetInstance<ICurrentDateRepository>();
        private readonly IAccommodationReservationModificationRequestRepository _accommodationReservationModificationRequestRepository = DependencyContainer.GetInstance<IAccommodationReservationModificationRequestRepository>();
        private readonly IAccommodationReservationReviewRepository _accommodationReservationReviewRepository = DependencyContainer.GetInstance<IAccommodationReservationReviewRepository>();
        private readonly DateTime CurrentDate;
        #endregion

        public GuestService()
        {
            CurrentDate = _currentDateRepository.Get()!.Date;
        }
        public GuestService(DateTime currentDate)
        {
            CurrentDate = currentDate;
        }

        public DateTime GetCurrentDate()
        {
            return CurrentDate;
        }

        public Owner? GetOwnerById(int id)
        {
            return _ownerRepository.GetById(id);
        }

        public Guest? GetGuestById(int id)
        {
            return _guestRepository.GetById(id);
        }

        public Accommodation? GetAccommodationById(int id)
        {
            return _accommodationRepository.GetById(id);
        }

        public List<string> GetAllLocationDistinctCountries()
        {
            return _locationRepository.GetAllDistinctCountries();
        }

        public List<string> GetAllLocationDistinctCities()
        {
            return _locationRepository.GetAllDistinctCities();
        }

        public List<string> GetAllLocationDistinctCitiesByCountry(string country)
        {
            return _locationRepository.GetAllDistinctCitiesByCountry(country);
        }

        public List<Accommodation> GetAccommodationsByNameLikeAndCountryAndCityAndNumberOfGuestsAndStayingPeriodAndType(string? name, string? country, string? city, int numberOfGuests, int stayingPeriod, AccommodationTypes.Type? accommodationType)
        {
            return _accommodationRepository.GetByNameLikeAndCountryAndCityAndNumberOfGuestsAndStayingPeriodAndType(
                name
                , country
                , city
                , numberOfGuests
                , stayingPeriod
                , accommodationType
                );
        }

        public List<AccommodationDTO> GetAllAccommodationDTOs()
        {
            List<Accommodation>? accommodations = _accommodationRepository.GetAll();
            List<AccommodationDTO> accommodationDTOs = new List<AccommodationDTO>();
            foreach (var accommodation in accommodations)
            {
                accommodationDTOs.Add(new AccommodationDTO(accommodation));
            }
            return accommodationDTOs;
        }

        public Location? GetLocationByCountryAndCity(string country, string city)
        {
            return _locationRepository.GetByCountryAndCity(country, city);
        }
    }
}
