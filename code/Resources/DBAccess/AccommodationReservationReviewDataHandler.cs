using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    internal class AccommodationReservationReviewDataHandler : IDataHandler<AccommodationReservationReview>
    {
        //private DataContext dataContext = new DataContext();
        //private DataContext dataContext = DependencyContainer.GetInstance<DataContext>();
        private DataContext dataContext = GlobalDataContext.DataContext;


        public void Delete(AccommodationReservationReview entity)
        {
            if(dataContext.AccommodationReservationReviews.Find(entity) != null)
                dataContext.AccommodationReservationReviews.Remove(entity);
        }

        public void DeleteById(int entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccommodationReservationReview> GetAll()
        {
            //return dataContext.AccommodationReservationReviews;
            return dataContext.AccommodationReservationReviews
                .Include(x => x.Accommodation)
                .Include(x => x.Accommodation!.Location)
                .Include(x => x.Owner)
                .Include(x => x.Guest)
                .Include(x => x.AccommodationReservation)
                .ToList();
        }

        public void Save(IEnumerable<AccommodationReservationReview> entities)
        {
            dataContext.AccommodationReservationReviews.AddRange(entities);
            dataContext.SaveChanges();
        }

        //public AccommodationReservationReview SaveOneEntity(AccommodationReservationReview entity)
        //{
        //    dataContext.AccommodationReservationReviews.Add(entity);
        //    dataContext.SaveChanges();
        //    return entity;
        //}

        public void Update(AccommodationReservationReview entity)
        {
            var existingRateGuest = dataContext.AccommodationReservationReviews .FirstOrDefault(accommodationReservationReview => accommodationReservationReview.Id == entity.Id);

            if(existingRateGuest != null)
            {
                existingRateGuest.Cleanliness = entity.Cleanliness;
                existingRateGuest.OwnerId = entity.OwnerId;
                existingRateGuest.AccommodationId = entity.AccommodationId;
                existingRateGuest.GuestId = entity.GuestId;
                existingRateGuest.RuleCompliance = entity.RuleCompliance;
                existingRateGuest.Comment = entity.Comment;
                existingRateGuest.Direction = entity.Direction;
                existingRateGuest.Correctness = entity.Correctness;
                dataContext.SaveChanges();
            }

        }

        public AccommodationReservationReview UpdateEntity(AccommodationReservationReview entity)
        {
            throw new NotImplementedException();
        }

        AccommodationReservationReview IDataHandler<AccommodationReservationReview>.SaveOneEntity(AccommodationReservationReview entity)
        {
            //if (dataContext.AccommodationReservationReviews.Find(entity.Id) == null)
            //{
            //    dataContext.AccommodationReservationReviews.Add(entity);
            //}
            dataContext.Entry(entity).State = EntityState.Detached;
            //dataContext.Entry(entity.Accommodation).State = EntityState.Detached;
            dataContext.AccommodationReservationReviews.Add(entity);
            dataContext.SaveChanges();
            return entity;
        }
    }
}
