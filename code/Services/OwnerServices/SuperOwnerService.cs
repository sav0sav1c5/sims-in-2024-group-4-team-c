using BookingApp.DependencyInjection;
using BookingApp.Repository;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.OwnerServices
{
    public class SuperOwnerService
    {
        private readonly IAccommodationReservationReviewRepository _accommodationReservationReviewRepository = DependencyContainer.GetInstance<IAccommodationReservationReviewRepository>();


        public bool isSuperOwner()
        {
            var data = _accommodationReservationReviewRepository.GetAll().Where(review => review.OwnerId == UserSession.Instance.Id);
            double sum = 0;
            double count = 0;

            foreach (var item in data)
            {


                sum += (double)item.Cleanliness.GetValueOrDefault() + (double)item.Correctness.GetValueOrDefault();


                count++;
            }

            if (count > 50)
            {
                return (double)sum / count >= 4.5;
            }

            return false;
        }
    }
}
