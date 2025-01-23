using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Services.GuideServices
{
    public class TourRequestService
    {
        private readonly ITourRequestRepository _tourRequestRepository = DependencyContainer.GetInstance<ITourRequestRepository>();

        public TourRequestService()
        {
        }

        public List<TourRequest> GetAllTourRequests()
        {
            return _tourRequestRepository.GetAll();
        }

        public TourRequest GetTourRequestById(int id)
        {
            return _tourRequestRepository.GetById(id);
        }

        public List<TourRequest> GetTourRequestsByTouristId(int touristId)
        {
            return _tourRequestRepository.GetByTouristId(touristId);
        }

        public void SaveTourRequest(TourRequest tourRequest)
        {
            _tourRequestRepository.Save(tourRequest);
        }

        public void UpdateTourRequest(TourRequest tourRequest)
        {
            _tourRequestRepository.Update(tourRequest);
        }

        public void DeleteTourRequestById(int id)
        {
            _tourRequestRepository.DeleteById(id);
        }

        public void DeleteTourRequest(TourRequest tourRequest)
        {
            _tourRequestRepository.Delete(tourRequest);
        }

        public List<TourRequest> GetTourRequestsByYear(int year)
        {
            return _tourRequestRepository.GetByYear(year);
        }

        public List<TourRequest> GetDeclinedTourRequestsByTouristId(int touristId)
        {
            var allTourRequests = _tourRequestRepository.GetByTouristId(touristId);

            return allTourRequests.Where(tr => tr.TourRequestState == TourRequestStates.Type.Declined).ToList();
        }

    }
}
