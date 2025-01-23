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
    public class ComplexTourRequestRepository : IComplexTourRequestRepository
    {
        private readonly List<ComplexTourRequest> complexTourRequests;
        private readonly IDataHandler<ComplexTourRequest> complexTourRequestDataHandler;

        public ComplexTourRequestRepository()
        {
            complexTourRequestDataHandler = new ComplexTourRequestDataHandler();
            complexTourRequests = complexTourRequestDataHandler.GetAll().ToList();
        }

        public List<ComplexTourRequest> GetAll()
        {
            return complexTourRequests;
        }

        public ComplexTourRequest GetById(int id)
        {
            return complexTourRequests.FirstOrDefault(ctr => ctr.Id == id);
        }

        public List<ComplexTourRequest> GetByTouristId(int touristId)
        {
            return complexTourRequests.Where(ctr => ctr.TouristId == touristId).ToList();
        }

        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            complexTourRequests.Add(complexTourRequest);
            return complexTourRequestDataHandler.SaveOneEntity(complexTourRequest);
        }

        public void Update(ComplexTourRequest complexTourRequest)
        {
            complexTourRequestDataHandler.Update(complexTourRequest);
        }

        public void Delete(ComplexTourRequest complexTourRequest)
        {
            complexTourRequestDataHandler.Delete(complexTourRequest);
        }

        public void DeleteById(int complexTourRequestId)
        {
            var complexTourRequest = complexTourRequests.Find(ctr => ctr.Id == complexTourRequestId);
            if (complexTourRequest != null)
            {
                complexTourRequestDataHandler.DeleteById(complexTourRequestId);
                complexTourRequests.Remove(complexTourRequest);
            }
        }

        void IBaseRepository<ComplexTourRequest>.Save(ComplexTourRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
