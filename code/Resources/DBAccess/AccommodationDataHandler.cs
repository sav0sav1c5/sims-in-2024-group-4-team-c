using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    internal class AccommodationDataHandler:IDataHandler<Accommodation>
    {
        //private DataContext dataContext = new DataContext();
        //private DataContext dataContext = DependencyContainer.GetInstance<DataContext>();
        private DataContext dataContext = GlobalDataContext.DataContext;

        public IEnumerable<Accommodation> GetAll()
        {
            return dataContext.Accommodations
                .Include(accommodation => accommodation.Location)
                .Include(accommodation => accommodation.AccommodationReservations)
                .Include(accommodation => accommodation.Owner)
                .ToList();   
            //return dataContext.Accommodations.ToList();
        }

        void IDataHandler<Accommodation>.Save(IEnumerable<Accommodation> accommodations)
        {
            dataContext.Accommodations.AddRange(accommodations);
            dataContext.SaveChanges();
        }

        Accommodation IDataHandler<Accommodation>.SaveOneEntity(Accommodation accommodation)
        {
            dataContext.Accommodations.Add(accommodation);
            dataContext.SaveChanges();
            return accommodation;
        }

        void IDataHandler<Accommodation>.Delete(Accommodation accommodation)
        {
            if (accommodation != null)
            {
                dataContext.Remove(accommodation);
            }
        }

        void IDataHandler<Accommodation>.DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        void IDataHandler<Accommodation>.Update(Accommodation entity)
        {
            var existingAccommodation = dataContext.Accommodations.FirstOrDefault(accommodation => accommodation.Id == entity.Id);

            if(existingAccommodation != null)
            {
                existingAccommodation.Name = entity.Name;
                existingAccommodation.LocationId = entity.LocationId;
                existingAccommodation.AccommodationType = entity.AccommodationType;
                existingAccommodation.MaxGuests = entity.MaxGuests;
                existingAccommodation.MinReservationDays = entity.MinReservationDays;
                existingAccommodation.CancelationDeadline = entity.CancelationDeadline;
                existingAccommodation.Images = entity.Images;

                dataContext.SaveChanges();
            }
        }

        public Accommodation UpdateEntity(Accommodation entity)
        {
            throw new NotImplementedException();
        }
    }
}
