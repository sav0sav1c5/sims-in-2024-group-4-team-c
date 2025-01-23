using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Resources.DBAccess
{
    public class TourReviewDataHandler : IDataHandler<TourReview>
    {
        DataContext dataContext = new DataContext();

        public void Delete(TourReview entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TourReview> GetAll()
        {
            return dataContext.TourReviews.ToList();
        }

        public void Save(IEnumerable<TourReview> entities)
        {
            dataContext.TourReviews.AddRange(entities);
            dataContext.SaveChanges();
        }

        public TourReview Save(TourReview entity)
        {
            dataContext.TourReviews.Add(entity);
            dataContext.SaveChanges();
            return entity;
        }

        public TourReview SaveOneEntity(TourReview entity)
        {
            throw new NotImplementedException();
        }

        public TourReview Update(TourReview entity)
        {
            try
            {
                // Attach the TourAttendance entity to the DbContext
                dataContext.TourAttendances.Attach(entity.TourAttendance);

                var existingTourReview = dataContext.TourReviews.FirstOrDefault(tourReview => tourReview.Id == entity.Id);
                if (existingTourReview != null)
                {
                    existingTourReview.IsValid = false;
                    dataContext.SaveChanges();
                }
                return existingTourReview;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details for troubleshooting
                Console.WriteLine("DbUpdateException occurred: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                throw; // Rethrow the exception to propagate it to the caller
            }
        }
        public void Update(int id)
        {
            throw new NotImplementedException();
        }

        public TourReview UpdateEntity(TourReview entity)
        {
            try
            {
                // Attach the TourAttendance entity to the DbContext
                dataContext.TourAttendances.Attach(entity.TourAttendance);

                var existingTourReview = dataContext.TourReviews.FirstOrDefault(tourReview => tourReview.Id == entity.Id);
                if (existingTourReview != null)
                {
                    existingTourReview.IsValid = false;
                    dataContext.Update(existingTourReview);
                }
                return existingTourReview;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details for troubleshooting
                Console.WriteLine("DbUpdateException occurred: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

        void IDataHandler<TourReview>.Update(TourReview entity)
        {
            throw new NotImplementedException();
        }
    }
}