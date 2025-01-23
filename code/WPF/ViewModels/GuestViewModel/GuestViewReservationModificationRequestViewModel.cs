using BookingApp.DependencyInjection;
using BookingApp.DTOs;
using BookingApp.Services.GuestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuestViewModel
{
    public class GuestViewReservationModificationRequestViewModel
    {
        #region Data
        private readonly GuestService _guestService = DependencyContainer.GetInstance<GuestService>();
        private readonly GuestAccommodationReservationModificationRequestService _reservationModificationRequestService = DependencyContainer.GetInstance<GuestAccommodationReservationModificationRequestService>();
        public ReservationModificationDTO ReservationModificationRequestDTO { get; set; }
        public GuestDTO LoggedGuest { get; private set; }
        public NavigationService NavigationService;
        public ImageGallery AccommodationImageGallery { get; set; }
        #endregion

        #region RelayCommands
        public RelayCommand ShowNextAccommodationImageCMD { get; private set; }
        public RelayCommand ShowPreviousAccommodationImageCMD { get; private set; }
        #endregion

        public GuestViewReservationModificationRequestViewModel(GuestDTO loggedGuest, NavigationService navigationService, ReservationModificationDTO reservationModificationDTO)
        {
            LoggedGuest = loggedGuest;
            NavigationService = navigationService;
            ReservationModificationRequestDTO = reservationModificationDTO;

            List<string> accommImages = [.. ReservationModificationRequestDTO.AccommodationImages.Split(',')];
            AccommodationImageGallery = new ImageGallery(accommImages);

            ShowNextAccommodationImageCMD = new RelayCommand(AccommodationImageGallery.ShowNextImage);
            ShowPreviousAccommodationImageCMD = new RelayCommand(AccommodationImageGallery.ShowPreviousImage);
        }

    }
}
