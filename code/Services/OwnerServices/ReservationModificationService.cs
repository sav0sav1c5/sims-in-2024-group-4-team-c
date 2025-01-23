using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.OwnerServices
{
    public class ReservationModificationService
    {
        private IAccommodationReservationRepository _accommodationReservationRepository = DependencyContainer.GetInstance<IAccommodationReservationRepository>();
        private IAccommodationReservationModificationRequestRepository _accommodationReservationModificationRequestRepository = DependencyContainer.GetInstance<IAccommodationReservationModificationRequestRepository>();
        private IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();
        private IGuestRepository _guestRepository = DependencyContainer.GetInstance<IGuestRepository>();

        public List<Accommodation> getAccommodations()
        {
            return _accommodationRepository.GetAll();
        }

        public Accommodation getAccommodationById(int id)
        {
            return _accommodationRepository.GetById(id);
        }

        public AccommodationReservation getReservationById(int id)
        {
            return _accommodationReservationRepository.GetById(id);
        }

        public List<AccommodationReservation> getReservations()
        {
            return _accommodationReservationRepository.GetAll();
        }

        public List<AccommodationReservationModificationRequest> getModificationRequests()
        {
            return _accommodationReservationModificationRequestRepository.GetAll();
        }
        public AccommodationReservationModificationRequest getModificationRequest(int id)
        {
            return _accommodationReservationModificationRequestRepository.GetById(id);
        }

        public void updateModification(AccommodationReservationModificationRequest accommodationReservationModificationRequest)
        {
           
                _accommodationReservationModificationRequestRepository.Update(accommodationReservationModificationRequest);
            

        }

        public void updateReservation(AccommodationReservation accommodationReservation)
        {
            _accommodationReservationRepository.Update(accommodationReservation);
        }

        public Guest GetGuest(int id)
        {
            return _guestRepository.GetById(id);
        }

    }
}
