using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Repository
{
    public interface ITourReviewRepository : IBaseRepository<TourReview>
    {
        public List<TourReview> GetByTourReservationId(int tourReservationId);
        public List<TourReview> GetByGuideId(int guideId);

        public List<TourReview> GetAll();
        public TourReview UpdateEntity(TourReview entity);
        public void Update(int id);
    }
}
