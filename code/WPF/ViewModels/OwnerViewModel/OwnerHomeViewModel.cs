using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Services.OwnerServices;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModel
{
    class OwnerHomeViewModel : ViewModelBase
    {

        
        private OwnerHomeService _ownerHomeService = DependencyContainer.GetInstance<OwnerHomeService>();

        public int TotalAccommodations {get;set;}
        public double AverageRating { get; set; }
        public int TotalGuests { get; set; }

        public string RateGuestNumber { get; set; }

        public string ReservationsNumber { get; set; }

        public OwnerHomeViewModel(Owner owner)
        {
            
            LoadData(owner);

        }

        public OwnerHomeViewModel()
        {
            LoadData2();
        }

        private void LoadData2()
        {
            TotalAccommodations = _ownerHomeService.GetNumberOfAccommodations(UserSession.Instance.Id);
            AverageRating = _ownerHomeService.GetAverageRating(UserSession.Instance.Id);
            TotalGuests = _ownerHomeService.GetTotalGuests(UserSession.Instance.Id);
            RateGuestNumber = _ownerHomeService.GetNumberRateGuests(UserSession.Instance.Id);
            ReservationsNumber = _ownerHomeService.GetReservationsNumber(UserSession.Instance.Id);
        }

        private void LoadData(Owner owner)
        {
            TotalAccommodations = _ownerHomeService.GetNumberOfAccommodations(owner.Id);
            AverageRating = _ownerHomeService.GetAverageRating(owner.Id);
            TotalGuests = _ownerHomeService.GetTotalGuests(owner.Id);
            RateGuestNumber = _ownerHomeService.GetNumberRateGuests(owner.Id);
            ReservationsNumber = _ownerHomeService.GetReservationsNumber(owner.Id);
        }
    }
}
