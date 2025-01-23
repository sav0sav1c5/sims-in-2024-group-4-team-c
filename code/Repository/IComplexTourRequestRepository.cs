using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface IComplexTourRequestRepository : IBaseRepository<ComplexTourRequest>
    {
        ComplexTourRequest GetById(int id);
        List<ComplexTourRequest> GetByTouristId(int touristId);
        ComplexTourRequest Save(ComplexTourRequest complexTourRequest);
        void Update(ComplexTourRequest complexTourRequest);
        void Delete(ComplexTourRequest complexTourRequest);
        void DeleteById(int complexTourRequestId);
    }
}
