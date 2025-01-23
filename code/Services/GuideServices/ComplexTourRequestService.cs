using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.GuideServices
{
    public class ComplexTourRequestService
    {
        private readonly IComplexTourRequestRepository _complexTourRequestRepository = DependencyContainer.GetInstance<IComplexTourRequestRepository>();

        public ComplexTourRequestService()
        {
        }

        public List<ComplexTourRequest> GetAllComplexTourRequests()
        {
            return _complexTourRequestRepository.GetAll();
        }

        public ComplexTourRequest GetComplexTourRequestById(int id)
        {
            return _complexTourRequestRepository.GetById(id);
        }

        public List<ComplexTourRequest> GetComplexTourRequestsByTouristId(int touristId)
        {
            return _complexTourRequestRepository.GetByTouristId(touristId);
        }

        public void SaveComplexTourRequest(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequestRepository.Save(complexTourRequest);
        }

        public void UpdateComplexTourRequest(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequestRepository.Update(complexTourRequest);
        }

        public void DeleteComplexTourRequestById(int id)
        {
            _complexTourRequestRepository.DeleteById(id);
        }

        public void DeleteComplexTourRequest(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequestRepository.Delete(complexTourRequest);
        }
    }
}
