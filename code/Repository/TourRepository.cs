using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourRepository : ITourRepository
    {
        private List<Tour> tours;
        private readonly IDataHandler<Tour> tourDataHandler;
        private LocationRepository locationRepository;

        public TourRepository()
        {
            tourDataHandler = new TourDataHandler();
            locationRepository = new LocationRepository();
            tours = tourDataHandler.GetAll().ToList();
        }
        public List<Tour> GetAll()
        {
            return tours;
        }

        public Tour GetById(int id)
        {
            Tour? tour = tours.FirstOrDefault(t => t.Id == id);
            if (tour == null)
            {
                throw new ArgumentException("Tour not found", nameof(id));
            }
            return tour;
        }

        public Tour Save(Tour tour)
        {
            tours.Add(tour);
            return tourDataHandler.SaveOneEntity(tour);
        }

        public List<Tour> GetByName(string name)
        {
            List<Tour> filteredTours = new List<Tour>();
            foreach (Tour tour in tours)
            {
                if (tour.Name.Contains(name))
                    filteredTours.Add(tour);
            }
            return filteredTours;
        }
        public List<Tour> GetByGuideId(int guideId)
        {
            List<Tour> filteredTours = new List<Tour>();
            foreach (Tour tour in tours)
            {
                if (tour.GuideId == guideId)
                    filteredTours.Add(tour);
            }
            return filteredTours;
        }

        public List<Tour> GetByState(string state)
        {

            List<Tour> filteredTours = new List<Tour>();
            foreach (Tour tour in tours)
            {
                Location? location = locationRepository.GetById(tour.LocationId);
                if (location != null && location.Country.Equals(state))
                    filteredTours.Add(tour);
            }
            return filteredTours;
        }

        public List<Tour> GetByCity(string city)
        {

            List<Tour> filteredTours = new List<Tour>();
            foreach (Tour tour in tours)
            {
                Location? location = locationRepository.GetById(tour.LocationId);
                if (location != null && location.City.Equals(city))
                    filteredTours.Add(tour);
            }
            return filteredTours;
        }


        public List<Tour> GetByDuration(int duration)
        {

            List<Tour> filteredTours = new List<Tour>();
            foreach (Tour tour in tours)
            {
                if (tour.Duration == duration)
                    filteredTours.Add(tour);
            }
            return filteredTours;
        }

        public List<Tour> GetByLanguage(string language)
        {

            List<Tour> filteredTours = new List<Tour>();
            foreach (Tour tour in tours)
            {
                if (tour.Language.Contains(language))
                    filteredTours.Add(tour);
            }
            return filteredTours;
        }

        public List<Tour> GetByGuestsNumber(int guestsNumber)
        {

            List<Tour> filteredTours = new List<Tour>();
            foreach (Tour tour in tours)
            {
                if (tour.MaxTouristNumber >= guestsNumber)
                    filteredTours.Add(tour);
            }
            return filteredTours;
        }

        public void Delete(Tour entity)
        {
            tours.Remove(entity);
            tourDataHandler.Delete(entity);
        }

        void IBaseRepository<Tour>.Save(Tour entity)
        {
            Save(entity);
        }

        public void Update(Tour entity)
        {
            // Implement update logic if needed
        }

        public void DeleteById(int id)
        {
            Tour tour = GetById(id);
            if (tour != null)
            {
                Delete(tour);
            }
        }
    }
}