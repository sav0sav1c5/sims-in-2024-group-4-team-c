using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;

namespace BookingApp.Services.GuideServices
{
    public class TourAttendanceService
    {
        private readonly ITourAttendanceRepository _tourAttendanceRepository = DependencyContainer.GetInstance<ITourAttendanceRepository>();

        public TourAttendanceService()
        {
        }

        public List<TourAttendance> GetAllTourAttendances()
        {
            return _tourAttendanceRepository.GetAll();
        }
        public TourAttendance GetTourAttendanceById(int Id)
        {
            return _tourAttendanceRepository.GetById(Id);
        }

        public TourAttendance GetTourAttendanceByTouristId(int tourGuestId)
        {
            return _tourAttendanceRepository.GetByTouristId(tourGuestId);
        }
        public List<TourAttendance> GetTourAttendanceByReservationId(int tourReservationId)
        {
            return _tourAttendanceRepository.GetByReservationId(tourReservationId);
        }

        public TourAttendance SaveTourAttendance(TourAttendance tourAttendance)
        {
           return _tourAttendanceRepository.Save(tourAttendance);
        }

        public void UpdateTourAttendance(TourAttendance tourAttendance)
        {
            _tourAttendanceRepository.Update(tourAttendance);
        }

        public void DeleteTourAttendanceById(int id)
        {
            _tourAttendanceRepository.DeleteById(id);
        }

        public void DeleteTourAttendance(TourAttendance tourAttendance)
        {
            _tourAttendanceRepository.Delete(tourAttendance);
        }
    }
}
