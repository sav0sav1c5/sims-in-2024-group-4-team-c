using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    public class TourAttendanceDataHandler : IDataHandler<TourAttendance>
    {
        private DataContext dataContext = new DataContext();

        void IDataHandler<TourAttendance>.Delete(TourAttendance entity)
        {
            throw new NotImplementedException();
        }

        void IDataHandler<TourAttendance>.DeleteById(int entityId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TourAttendance> IDataHandler<TourAttendance>.GetAll()
        {
            return dataContext.TourAttendances.ToList();
        }

        public void Save(IEnumerable<TourAttendance> entities)
        {
            dataContext.TourAttendances.AddRange(entities);

            dataContext.SaveChanges();
        }

        TourAttendance IDataHandler<TourAttendance>.SaveOneEntity(TourAttendance entity)
        {
            if (entity.TourReservation != null)
            {
                dataContext.TourReservations.Attach(entity.TourReservation);
            }
            dataContext.TourAttendances.Add(entity);
            dataContext.SaveChanges();
            return entity;
        }

        void IDataHandler<TourAttendance>.Update(TourAttendance entity)
        {
            var existingTourAttendance = dataContext.TourAttendances.FirstOrDefault(tourAttendance => tourAttendance.TouristId == entity.TouristId && tourAttendance.TourReservationId == entity.TourReservationId);

            if (existingTourAttendance != null)
            {
                existingTourAttendance.IsPresent = entity.IsPresent;
                existingTourAttendance.IsConfirmed = entity.IsConfirmed;
                dataContext.Update(existingTourAttendance);
            }
        }

        public TourAttendance UpdateEntity(TourAttendance entity)
        {
            throw new NotImplementedException();
        }
    }
}