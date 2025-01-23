using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    public class TourDataHandler : IDataHandler<Tour>
    {
        private DataContext dataContext = new DataContext();

        public void Delete(Tour entity)
        {
            try
            {
                dataContext.Entry(entity).Reload();
                dataContext.Tours.Remove(entity);
                dataContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle the exception, e.g., log the error or show a message to the user
            }
        }

        public void DeleteById(int entityId)
        {
            Tour tour = dataContext.Tours.FirstOrDefault(t => t.Id == entityId);
            if (tour != null)
            {
                dataContext.Tours.Remove(tour);
                dataContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Tour not found", nameof(entityId));
            }
        }

        public IEnumerable<Tour> GetAll()
        {
            return dataContext.Tours.ToList();
        }

        public void Save(IEnumerable<Tour> entities)
        {
            dataContext.Tours.AddRange(entities);
            dataContext.SaveChanges();
        }

        public Tour SaveOneEntity(Tour tour)
        {
            dataContext.Tours.Add(tour);
            dataContext.SaveChanges();
            return tour;
        }

        public void Update(Tour entity)
        {
            dataContext.Entry(entity).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        public Tour UpdateEntity(Tour entity)
        {
            throw new NotImplementedException();
        }
    }
}
