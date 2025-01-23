using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class ComplexTourPartRepository : IComplexTourPartRepository
    {
        private readonly List<ComplexTourPart> complexTourParts;
        private readonly IDataHandler<ComplexTourPart> complexTourPartDataHandler;

        public ComplexTourPartRepository()
        {
            complexTourPartDataHandler = new ComplexTourPartDataHandler();
            complexTourParts = complexTourPartDataHandler.GetAll().ToList();
        }

        public List<ComplexTourPart> GetAll()
        {
            return complexTourParts;
        }

        public ComplexTourPart GetById(int id)
        {
            return complexTourParts.FirstOrDefault(ctp => ctp.Id == id);
        }

        public List<ComplexTourPart> GetByComplexTourRequestId(int complexTourRequestId)
        {
            return complexTourParts.Where(ctp => ctp.ComplexTourRequestId == complexTourRequestId).ToList();
        }

        public ComplexTourPart Save(ComplexTourPart complexTourPart)
        {
            complexTourParts.Add(complexTourPart);
            return complexTourPartDataHandler.SaveOneEntity(complexTourPart);
        }

        public void Update(ComplexTourPart complexTourPart)
        {
            complexTourPartDataHandler.Update(complexTourPart);
        }

        public void Delete(ComplexTourPart complexTourPart)
        {
            complexTourPartDataHandler.Delete(complexTourPart);
        }

        public void DeleteById(int complexTourPartId)
        {
            var complexTourPart = complexTourParts.Find(ctp => ctp.Id == complexTourPartId);
            if (complexTourPart != null)
            {
                complexTourPartDataHandler.DeleteById(complexTourPartId);
                complexTourParts.Remove(complexTourPart);
            }
        }

        void IBaseRepository<ComplexTourPart>.Save(ComplexTourPart entity)
        {
            throw new NotImplementedException();
        }
    }
}
