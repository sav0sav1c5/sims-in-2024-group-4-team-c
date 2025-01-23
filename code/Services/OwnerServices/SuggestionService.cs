using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.Services.OwnerServices
{
    public class SuggestionService
    {
        private readonly ILocationRepository _locationRepository = DependencyContainer.GetInstance<ILocationRepository>();
        private readonly IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();
        private readonly IAccommodationReservationRepository _accommodationReservationRepository = DependencyContainer.GetInstance<IAccommodationReservationRepository>();
        
        public LocationDTO getRecommendedLocation()
        {
            LocationDTO _location = new LocationDTO();
            _location.count = 0;
            List<LocationDTO> locationDTOs = new List<LocationDTO>();

            var accommodationReservations = _accommodationReservationRepository.GetAll();
            foreach(var accommodationReservation in  accommodationReservations)
            {
                bool found = false;
                Accommodation accommodation = _accommodationRepository.GetAll().Where(accommodation => accommodation.Id == accommodationReservation.AccommodationId).FirstOrDefault();
                Location location = _locationRepository.GetAll().Where(location => location.Id == accommodation.LocationId).FirstOrDefault();
                foreach(var locationDTO in locationDTOs)
                {
                    if(locationDTO.Id == location.Id)
                    {
                        found = true;
                        locationDTO.count = locationDTO.count + 1;
                    }
                }

                if(found != true)
                {
                    LocationDTO novi = new LocationDTO(location.Id, 0);
                    locationDTOs.Add(novi);
                }
            }

            foreach(var location in  locationDTOs)
            {
                if(_location.count < location.count)
                {
                    _location = location;
                }
            }

            _location.City = _locationRepository.GetById(_location.Id).City;
            _location.Country = _locationRepository.GetById(_location.Id).Country;
            

            return _location;
        }

        public LocationDTO getLocationForDelete(int userId)
        {
            LocationDTO _location = new LocationDTO();
            _location.count = 1000000;
            List<LocationDTO> locationDTOs = new List<LocationDTO>();

            var accommodationReservations = _accommodationReservationRepository.GetAll();
            foreach (var accommodationReservation in accommodationReservations)
            {
                bool found = false;
                Accommodation accommodation = _accommodationRepository.GetAll().Where(accommodation => accommodation.Id == accommodationReservation.AccommodationId && accommodation.OwnerId == userId).FirstOrDefault();
                Location location = _locationRepository.GetAll().Where(location => location.Id == accommodation.LocationId).FirstOrDefault();
                foreach (var locationDTO in locationDTOs)
                {
                    if (locationDTO.Id == location.Id)
                    {
                        found = true;
                        locationDTO.count = locationDTO.count + 1;
                    }
                }

                if (found != true)
                {
                    LocationDTO novi = new LocationDTO(location.Id, 0);
                    locationDTOs.Add(novi);
                }
            }
            foreach (var location in locationDTOs)
            {
                if (_location.count < location.count)
                {
                    _location = location;
                }
            }

            _location.City = _locationRepository.GetById(_location.Id).City;
            _location.Country = _locationRepository.GetById(_location.Id).Country;


            return _location;

            
        }

        



    }
}
