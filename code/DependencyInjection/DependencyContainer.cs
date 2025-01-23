using BookingApp.Repository;
using BookingApp.Resources.DBAccess;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BookingApp.DependencyInjection
{
    public sealed class DependencyContainer
    {
        private DependencyContainer()
        {
        }

        private static void PopulateInstances()
        {
            _instances.Add(typeof(ILocationRepository), new LocationRepository());
            _instances.Add(typeof(IGuestRepository), new GuestRepository());
            _instances.Add(typeof(ICurrentDateRepository), new CurrentDateRepository());
            _instances.Add(typeof(IAccommodationRepository), new AccommodationRepository());
            _instances.Add(typeof(IAccommodationReservationRepository), new AccommodationReservationRepository());
            _instances.Add(typeof(IAccommodationReservationModificationRequestRepository), new AccommodationReservationModificationRequestRepository());
            _instances.Add(typeof(ICheckpointRepository), new CheckPointRepository());
            _instances.Add(typeof(IGuideRepository), new GuideRepository());
            _instances.Add(typeof(ITourRepository), new TourRepository());
            _instances.Add(typeof(ITourParticipantRepository), new TourParticipantRepository());
            _instances.Add(typeof(IVoucherRepository), new VoucherRepository());
            _instances.Add(typeof(ITourAttendanceRepository), new TourAttendanceRepository());
            _instances.Add(typeof(ITouristRepository), new TouristRepository());
            _instances.Add(typeof(ITourReservationRepository), new TourReservationRepository());
            _instances.Add(typeof(IAccommodationReservationReviewRepository), new AccommodationReservationReviewRepository());
            _instances.Add(typeof(IOwnerRepository), new OwnerRepository());
            _instances.Add(typeof(ITourReviewRepository), new TourReviewRepository());
            _instances.Add(typeof(ITourRequestRepository), new TourRequestRepository());
            _instances.Add(typeof(IRenovationRepository), new RenovationRepository());
            _instances.Add(typeof(IForumRepository), new ForumRepository());
            _instances.Add(typeof(ICommentRepository), new CommentRepository());
            _instances.Add(typeof(IVoucherUseRepository), new VoucherUseRepository());
            _instances.Add(typeof(IForumAlertRepository), new ForumAlertRepository());
            _instances.Add(typeof(IComplexTourRequestRepository), new ComplexTourRequestRepository());
            _instances.Add(typeof(IComplexTourPartRepository), new ComplexTourPartRepository());
        }

        //This is a work around if we have a dependency loop
        private static Dictionary<Type, object> _instances
            = new Dictionary<Type, object>();
            //{
                //TODO:Add all your Repository interfaces here
                //Note: services or any other classes that do not need IOC (Inversion Of Control)
                //do not need to be added here because they will be added in 'GetInstance'
                //Example: GuestService does not have an interface therefore I do not need to add it manually 
                //{ typeof(ILocationRepository), new LocationRepository() },
                //{ typeof(IGuestRepository), new GuestRepository()  },
                //{ typeof(ICurrentDateRepository), new CurrentDateRepository() }
                //{ typeof(IAccommodationRepository), new AccommodationRepository() },
                //{ typeof(IAccommodationReservationRepository), new AccommodationReservationRepository() },
                //{ typeof(IAccommodationReservationModificationRequestRepository), new AccommodationReservationModificationRequestRepository() }
            //};
            


        public static T GetInstance<T>() where T : class
        {
            //Initialization (since we don't have a constructor)
            if (_instances.Count <= 0)
            {
                PopulateInstances();
            }

            Type serviceType = typeof(T);

            if (_instances.ContainsKey(serviceType))
            {
                return (_instances[serviceType] as T)!;
            }

            T instance = CreateInstance<T>();
            _instances.Add(serviceType, instance);
            return instance;
        }

        private static T CreateInstance<T>() where T : class
        {
            //Note: all services should have a default constructor
            return Activator.CreateInstance<T>();
        }
    }
}
