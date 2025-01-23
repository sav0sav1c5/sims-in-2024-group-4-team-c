using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface ITourAttendanceRepository : IBaseRepository<TourAttendance>
    {
        List<TourAttendance> GetAll();
        TourAttendance GetById(int Id);
        TourAttendance GetByTouristId(int tourGuestId);
        List<TourAttendance> GetByReservationId(int reservationId);
        void Update(TourAttendance tourAttendance);
        TourAttendance Save(TourAttendance tourAttendance);
    }
}
