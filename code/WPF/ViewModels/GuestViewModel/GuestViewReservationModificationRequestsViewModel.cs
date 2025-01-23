using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Resources.DBAccess;
using BookingApp.Services.GuestServices;
using BookingApp.WPF.Views.GuestView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuestViewModel
{
    public class GuestViewReservationModificationRequestsViewModel
    {
        #region Data
        private readonly GuestService _guestService = DependencyContainer.GetInstance<GuestService>();
        private readonly GuestAccommodationReservationModificationRequestService _reservationModificationRequestService = DependencyContainer.GetInstance<GuestAccommodationReservationModificationRequestService>();
        public List<ReservationModificationDTO> FoundReservationModificationRequestsDTO { get; set; } = new List<ReservationModificationDTO>();
        public GuestDTO LoggedGuest { get; private set; }
        public NavigationService NavigationService;
        #endregion

        #region RelayCommands
        public RelayCommand ViewModificationRequestCMD { get; private set; }
        #endregion


        public GuestViewReservationModificationRequestsViewModel(GuestDTO loggedGuest, NavigationService navigationService)
        {
            LoggedGuest = loggedGuest;
            NavigationService = navigationService;
            FoundReservationModificationRequestsDTO = _reservationModificationRequestService.GetAccommodationReservationModificationRequestDTOByGuestId(loggedGuest.Id);
            ViewModificationRequestCMD = new RelayCommand(ViewModificationRequestCMD_func);
        }

        public void ViewModificationRequestCMD_func(object paramater)
        {
            if (paramater is not ReservationModificationDTO)
            {
                return;
            }

            NavigationService.Navigate( new GuestViewReservationModificationRequestPage(
                LoggedGuest,
                NavigationService, 
                (ReservationModificationDTO)paramater)
                );
        }
        

    }
}
