using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services.GuideServices
{
    public class TourReviewService
    {
        private readonly ITourReviewRepository _tourReviewRepository = DependencyContainer.GetInstance<ITourReviewRepository>();

        public TourReviewService()
        {
        }

        public List<TourReview> GetAllTourReviews()
        {
            return _tourReviewRepository.GetAll();
        }
        public List<TourReview> GetTourReviewsByGuideId(int guideId)
        {
            return _tourReviewRepository.GetByGuideId(guideId);
        }
        public List<TourReview> GetTourReviewsByTourReservationId(int reservationId)
        {
            return _tourReviewRepository.GetByTourReservationId(reservationId);
        }

        public TourReview GetTourReviewById(int id)
        {
            return _tourReviewRepository.GetById(id);
        }

        public void SaveTourReview(TourReview tourReview)
        {
            _tourReviewRepository.Save(tourReview);
        }
        public TourReview UpdateTourReview(TourReview tourReview)
        {
            return _tourReviewRepository.UpdateEntity(tourReview);
        }

        public void DeleteTourReview(TourReview tourReview)
        {
            _tourReviewRepository.Delete(tourReview);
        }

        // Add more methods as needed for specific functionalities related to tour reviews
    }
}
