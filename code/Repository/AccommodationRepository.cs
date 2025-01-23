using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private List<Accommodation> _accommodations;
        private readonly IDataHandler<Accommodation> _accommodationDataHandler;

        public AccommodationRepository()
        {
            _accommodationDataHandler = new AccommodationDataHandler();
            _accommodations = _accommodationDataHandler.GetAll().ToList();
        }

        public void DeleteById(int id)
        {
            _accommodationDataHandler.DeleteById(id);
            _accommodations.RemoveAll(accommodation => accommodation.Id == id);
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodationDataHandler.Delete(accommodation);
            _accommodations.Remove(accommodation);
        }

        public void Save(Accommodation accommodation)
        {
            if (accommodation == null) return;
            _accommodationDataHandler.SaveOneEntity(accommodation);
            _accommodations.Add(accommodation);   
        }

        public Accommodation? GetByName(string name)
        {
            return _accommodations.FirstOrDefault(accommodation => accommodation.Name == name);
            //return _accommodationDataHandler.GetAll().FirstOrDefault(accommodation => accommodation.Name == name);
        }

        /// <summary>
        /// Will cause errors if not eager loaded!
        /// </summary>
        public List<Accommodation> GetByNameLikeAndCountryAndCityAndNumberOfGuestsAndStayingPeriodAndType(string? name, string? country, string? city, int numberOfGuests, int stayingPeriod, AccommodationTypes.Type? accommodationType)
        {
            return _accommodations.FindAll(
            //return _accommodationDataHandler.GetAll().Where(
                accommodation => accommodation.Name.Contains(name == null ? "" : name)
                    && (country != null && country.Length > 0 ? accommodation.Location!.Country.Equals(country) : true)
                    && (city != null && city.Length > 0 ? accommodation.Location!.City.Equals(city) : true)
                    && accommodation.MaxGuests >= numberOfGuests
                    && accommodation.MinReservationDays <= stayingPeriod
                    && (accommodationType != null ? accommodation.AccommodationType == accommodationType : true)
            );
                    //).ToList();
        }

        public Accommodation? GetById(int id)
        {
            return _accommodations.FirstOrDefault(accommodations => accommodations.Id == id);
            //return _accommodationDataHandler.GetAll().FirstOrDefault(_accommodations => _accommodations.Id == id);
        }

        public List<Accommodation>? GetByMaxGuest(int maxGuest)
        {
            return _accommodations.FindAll(accommodations => accommodations.MaxGuests == maxGuest);
            //return _accommodationDataHandler.GetAll()
            //    .Where(_accommodations => _accommodations.MaxGuests == maxGuest)
            //    .ToList();
        }

        public List<Accommodation>? GetByLocationId(int locationId)
        {
            return _accommodations.FindAll(accommodations => accommodations.LocationId == locationId);
            //return _accommodationDataHandler.GetAll()
            //    .Where(_accommodations => _accommodations.LocationId == locationId)
            //    .ToList();
        }

        public List<Accommodation> GetAll() 
        {
            return _accommodations;
            //return _accommodationDataHandler.GetAll().ToList();
        }

        public Accommodation? GetByOwnerId(int id)
        {
            return _accommodations.FirstOrDefault(x => x.OwnerId == id);
            //return _accommodationDataHandler.GetAll()
            //    .FirstOrDefault(x => x.OwnerId == id);
        }

        public void Update(Accommodation accommodation)
        {
            _accommodationDataHandler.Update(accommodation);
        }
    }
}
