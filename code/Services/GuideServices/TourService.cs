using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services.GuideServices
{
    public class TourService
    {
        private readonly ITourRepository _tourRepository = DependencyContainer.GetInstance<ITourRepository>();

        public TourService()
        {
        }

        public List<Tour> GetAllTours()
        {
            return _tourRepository.GetAll();
        }

        public Tour GetTourById(int id)
        {
            return _tourRepository.GetById(id);
        }

        public Tour SaveTour(Tour tour)
        {
            return _tourRepository.Save(tour);
        }
        public void DeleteTour(Tour tour)
        {
            _tourRepository.Delete(tour);
        }

        public List<Tour> SearchTours(string searchTerm)
        {
            return _tourRepository.GetByName(searchTerm);
        }

        public List<Tour> SearchToursByLocation(string city, string state)
        {
            // Combine filters using LINQ for efficiency and readability
            return _tourRepository.GetAll()
                .Where(t => t.Location.City.Equals(city, StringComparison.OrdinalIgnoreCase) &&
                            t.Location.Country.Equals(state, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Tour> FilterToursByDuration(int duration)
        {
            return _tourRepository.GetByDuration(duration);
        }

        public List<Tour> FilterToursByLanguage(string language)
        {
            return _tourRepository.GetByLanguage(language);
        }

        public List<Tour> FilterToursByGuestsNumber(int guestsNumber)
        {
            return _tourRepository.GetByGuestsNumber(guestsNumber);
        }

        public List<Tour> GetToursByGuideId(int guideId)
        {
            return _tourRepository.GetByGuideId(guideId);
        }
    }
}
