using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface ITourParticipantRepository : IBaseRepository<TourParticipant>
    {
        void Save(TourParticipant tourParticipant);
        List<TourParticipant> GetAll();
        void DeleteParticipantsByTourReservationId(int tourReservationId);
        List<TourParticipant> GetByTourReservationId(int tourReservationId);
        void Update(TourParticipant tourParticipant);
        void DeleteById(int id);
        void Delete(TourParticipant tourParticipant);
    }
}
