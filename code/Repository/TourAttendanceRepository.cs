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
    public class TourAttendanceRepository : ITourAttendanceRepository
    {
        private List<TourAttendance> tourAttendances;
        private readonly IDataHandler<TourAttendance> tourAttendanceDataHandler;

        public TourAttendanceRepository()
        {
            tourAttendanceDataHandler = new TourAttendanceDataHandler();
            tourAttendances = tourAttendanceDataHandler.GetAll().ToList();
        }
        public List<TourAttendance> GetAll()
        {
            return tourAttendances;
        }

        public TourAttendance GetByTouristId(int tourGuestId)
        {
            return tourAttendances.FirstOrDefault(tourAttendance => tourAttendance.TouristId == tourGuestId);
        }

        public List<TourAttendance> GetByReservationId(int reservationId)
        {
            List<TourAttendance> filteredTourAttendance = new List<TourAttendance>();

            foreach (TourAttendance tourAttendance in tourAttendances)
            {
                if (tourAttendance.TourReservationId == reservationId)
                    filteredTourAttendance.Add(tourAttendance);
            }

            return filteredTourAttendance;
        }

        public void Update(TourAttendance tourAttendance)
        {
            tourAttendanceDataHandler.Update(tourAttendance);
        }

        public TourAttendance Save(TourAttendance tourAttendance)
        {
            tourAttendances.Add(tourAttendance);
            return tourAttendanceDataHandler.SaveOneEntity(tourAttendance);
        }

        void IBaseRepository<TourAttendance>.Save(TourAttendance entity)
        {
            throw new NotImplementedException();
        }

        public TourAttendance? GetById(int id)
        {
            return tourAttendances.FirstOrDefault(tourAttendance => tourAttendance.Id == id);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TourAttendance entity)
        {
            throw new NotImplementedException();
        }
    }
}