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
    internal class TourParticipantRepository : ITourParticipantRepository
    {
        private List<TourParticipant> tourParticipant;
        private readonly IDataHandler<TourParticipant> tourParticipantDataHandler;

        public TourParticipantRepository()
        {
            tourParticipantDataHandler = new TourParticipantDataHandler();
            tourParticipant = tourParticipantDataHandler.GetAll().ToList();
        }

        public void Save(TourParticipant tourParticipant)
        {
            if (tourParticipant == null)
            {
                // Možemo izbaciti izuzetak, zabeležiti grešku, ili nešto drugo, u zavisnosti od poslovne logike
                throw new ArgumentNullException(nameof(tourParticipant), "TourParticipants cannot be null.");
            }
            tourParticipantDataHandler.SaveOneEntity(tourParticipant);
        }


        public List<TourParticipant> GetAll()
        {
            return tourParticipant;
        }
        public List<TourParticipant> GetByTourReservationId(int tourReservationId)
        {
            return tourParticipant.FindAll(tourParticipant => tourParticipant.TourReservationId == tourReservationId);
        }

        public void Update(TourParticipant tourParticipant)
        {
            tourParticipantDataHandler.Update(tourParticipant);
        }

        public void DeleteById(int id)
        {
            tourParticipantDataHandler.DeleteById(id);
        }

        public void Delete(TourParticipant tourParticipant)
        {
            tourParticipantDataHandler.Delete(tourParticipant);
        }
        public void DeleteParticipantsByTourReservationId(int tourReservationId)
        {
            var participantsToDelete = tourParticipant.Where(participant => participant.TourReservationId == tourReservationId).ToList();
            foreach (var participant in participantsToDelete)
            {
                tourParticipant.Remove(participant);
                tourParticipantDataHandler.Delete(participant);
            }
        }

        public TourParticipant? GetById(int id)
        {
            return tourParticipant.FirstOrDefault(tourParticipant => tourParticipant.Id == id);
        }
    }
}