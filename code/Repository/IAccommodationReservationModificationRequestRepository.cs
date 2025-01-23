using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface IAccommodationReservationModificationRequestRepository : IBaseRepository<AccommodationReservationModificationRequest>
    {
        public List<AccommodationReservationModificationRequest> GetByGuestId(int id);
    }
}
