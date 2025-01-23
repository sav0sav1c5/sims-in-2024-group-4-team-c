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
    public class TourReviewRepository : ITourReviewRepository
    {
        private List<TourReview> tourReviews;
        private readonly IDataHandler<TourReview> tourReviewDataHandler;

        public TourReviewRepository()
        {
            tourReviewDataHandler = new TourReviewDataHandler();
            tourReviews = tourReviewDataHandler.GetAll().ToList();
        }

        public void Save(TourReview tourReview)
        {
            tourReviews.Add(tourReview);
            tourReviewDataHandler.SaveOneEntity(tourReview);
        }

        public List<TourReview> GetByTourReservationId(int tourReservationId)
        {
            return tourReviews.Where(tgr => tgr.TourReservationId == tourReservationId).ToList();
        }
        public List<TourReview> GetByGuideId(int guideId)
        {
            return tourReviews.Where(tgr => tgr.GuideId == guideId).ToList();
        }
        public List<TourReview> GetAll()
        {
            return tourReviews;
        }

        public TourReview? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TourReview entity)
        {
            tourReviewDataHandler.Update(entity);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TourReview entity)
        {
            throw new NotImplementedException();
        }

        public TourReview UpdateEntity(TourReview entity)
        {
            return tourReviewDataHandler.UpdateEntity(entity);
        }

        public void Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}