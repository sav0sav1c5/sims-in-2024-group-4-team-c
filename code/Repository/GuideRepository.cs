using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    internal class GuideRepository : IGuideRepository
    {
        private List<Guide> guides;
        private readonly IDataHandler<Guide> guideDataHandler;

        public GuideRepository()
        {
            guideDataHandler = new GuideDataHandler();
            guides = guideDataHandler.GetAll().ToList();
        }

        public void Save(Guide guide)
        {
            if (guides == null)
            {
                guides = new List<Guide>(); // Inicijalizacija liste ako je null
            }
            guideDataHandler.SaveOneEntity(guide);
        }

        public Guide? GetById(int id)
        {
            return guides.FirstOrDefault(x => x.Id == id);
        }

        public List<Guide> GetAll()
        {
            return guides;
        }

        public void Update(Guide guide)
        {
            guideDataHandler.Update(guide);
        }

        public void DeleteById(int id)
        {
            guideDataHandler.DeleteById(id);
        }

        public void Delete(Guide guide)
        {
            guideDataHandler.Delete(guide);
        }
    }
}
