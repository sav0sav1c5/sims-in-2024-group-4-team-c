using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Resources.DBAcces;
using System;
using System.Collections.Generic;

namespace BookingApp.Services.GuideServices
{
    public class TourParticipantService
    {
        private readonly ITourParticipantRepository _tourParticipantRepository = DependencyContainer.GetInstance<ITourParticipantRepository>();

        public TourParticipantService()
        {
        }

        public List<TourParticipant> GetAllTourParticipants()
        {
            return _tourParticipantRepository.GetAll();
        }

        public List<TourParticipant> GetTourParticipantsByTourReservationId(int tourReservationId)
        {
            return _tourParticipantRepository.GetByTourReservationId(tourReservationId);
        }
        public TourParticipant GetTourParticipantById(int id)
        {
            return _tourParticipantRepository.GetById(id);
        }

        public void SaveTourParticipant(TourParticipant tourParticipant)
        {
            _tourParticipantRepository.Save(tourParticipant);
        }

        public void UpdateTourParticipant(TourParticipant tourParticipant)
        {
            _tourParticipantRepository.Update(tourParticipant);
        }

        public void DeleteTourParticipantById(int id)
        {
            _tourParticipantRepository.DeleteById(id);
        }

        public void DeleteTourParticipant(TourParticipant tourParticipant)
        {
            _tourParticipantRepository.Delete(tourParticipant);
        }
        public void DeleteParticipantsByTourReservationId(int tourReservationId)
        {
            _tourParticipantRepository.DeleteParticipantsByTourReservationId(tourReservationId);
        }
    }
}
