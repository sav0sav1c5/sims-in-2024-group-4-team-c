using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Repository
{
    public interface IComplexTourPartRepository : IBaseRepository<ComplexTourPart>
    {
        ComplexTourPart GetById(int id);
        List<ComplexTourPart> GetByComplexTourRequestId(int complexTourRequestId);
        ComplexTourPart Save(ComplexTourPart complexTourPart);
        void Update(ComplexTourPart complexTourPart);
        void Delete(ComplexTourPart complexTourPart);
        void DeleteById(int complexTourPartId);
    }
}
