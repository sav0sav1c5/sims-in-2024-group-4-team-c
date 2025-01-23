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
    public class AccommodationReservationModificationRequestRepository : IAccommodationReservationModificationRequestRepository
    {
        private List<AccommodationReservationModificationRequest> _requests;
        private readonly IDataHandler<AccommodationReservationModificationRequest> _requestDataHandler;

        public AccommodationReservationModificationRequestRepository()
        {
            _requestDataHandler = new AccommodationReservationModificationRequestDataHandler();
            _requests = _requestDataHandler.GetAll().ToList();
        }

        public void Delete(AccommodationReservationModificationRequest entity)
        {
            _requestDataHandler.Delete(entity);
            _requests.Remove(entity);
        }

        public void DeleteById(int id)
        {
            _requestDataHandler.DeleteById(id);
            _requests.RemoveAll(x => x.Id == id);
        }

        public List<AccommodationReservationModificationRequest> GetAll()
        {
            return _requests;
        }

        public AccommodationReservationModificationRequest? GetById(int id)
        {
            return _requests.FirstOrDefault(x => x.Id == id);
        }

        public void Save(AccommodationReservationModificationRequest entity)
        {
            _requestDataHandler.SaveOneEntity(entity);
            _requests.Add(entity);
        }

        public void Update(AccommodationReservationModificationRequest entity)
        {
            _requestDataHandler.Update(entity);
        }

        //NOTE: if eager loading is removed this will break
        public List<AccommodationReservationModificationRequest> GetByGuestId(int id)
        {
            return _requests.FindAll(
                //request => (request.AccommodationReservation == null ? false : request.AccommodationReservation.GuestId == id)
                request => request.AccommodationReservation!.GuestId == id 
                );
        }
    }
}
