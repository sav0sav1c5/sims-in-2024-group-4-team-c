using BookingApp.Domain.Model;
using BookingApp.Repository;
using System.Collections.Generic;

namespace BookingApp.Services
{
    public class ComplexTourPartService
    {
        private readonly IComplexTourPartRepository _complexTourPartRepository;

        public ComplexTourPartService()
        {
            _complexTourPartRepository = new ComplexTourPartRepository();
        }

        public List<ComplexTourPart> GetAllComplexTourParts()
        {
            return _complexTourPartRepository.GetAll();
        }

        public ComplexTourPart GetComplexTourPartById(int id)
        {
            return _complexTourPartRepository.GetById(id);
        }

        public List<ComplexTourPart> GetComplexTourPartsByComplexTourRequestId(int complexTourRequestId)
        {
            return _complexTourPartRepository.GetByComplexTourRequestId(complexTourRequestId);
        }

        public void SaveComplexTourPart(ComplexTourPart complexTourPart)
        {
            _complexTourPartRepository.Save(complexTourPart);
        }

        public void UpdateComplexTourPart(ComplexTourPart complexTourPart)
        {
            _complexTourPartRepository.Update(complexTourPart);
        }

        public void DeleteComplexTourPartById(int id)
        {
            _complexTourPartRepository.DeleteById(id);
        }

        public void DeleteComplexTourPart(ComplexTourPart complexTourPart)
        {
            _complexTourPartRepository.Delete(complexTourPart);
        }
    }
}
