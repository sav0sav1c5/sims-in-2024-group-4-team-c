using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuestViewModel
{
    public class GuestViewReceivedOwnerReviewViewModel : ViewModelBase
    {
        #region Data
        public GuestDTO LoggedGuest { get; private set; }
        public AccommodationReservationReviewDTO AccommodationReservationReviewDTO { get; private set; }
        public NavigationService NavigationService { get; private set; }
        public ImageGallery AccommodationImageGallery { get; set; }
        #endregion

        #region RadioButtonData
        private bool[] _cleanlinessRatings = new bool[] { false, false, false, false, false };
        public bool[] CleanlinessRatings
        {
            get => _cleanlinessRatings;
            set => _cleanlinessRatings = value;
        }
        private bool[] _disciplineRatings = new bool[] { false, false, false, false, false };
        public bool[] DisciplineRatings
        {
            get => _disciplineRatings;
            set => _disciplineRatings = value;
        }
        #endregion

        #region Service 
        private readonly GuestService _guestService = DependencyContainer.GetInstance<GuestService>();
        private readonly GuestAccommodationReservationReviewService _reservationReviewService = DependencyContainer.GetInstance<GuestAccommodationReservationReviewService>();
        #endregion

        #region RelayCommands
        public RelayCommand ShowNextAccommodationImageCMD { get; private set; }
        public RelayCommand ShowPreviousAccommodationImageCMD { get; private set; }
        #endregion

        public GuestViewReceivedOwnerReviewViewModel(GuestDTO loggedGuest, AccommodationReservationDTO accommodationReservationDTO, NavigationService navigationService)
        {
            this.LoggedGuest = loggedGuest;
            NavigationService = navigationService;
            this.AccommodationReservationReviewDTO = _reservationReviewService.GetOwnerAccommodationReservationReviewDTOByAccommodationReservationId(accommodationReservationDTO.Id);

            if (AccommodationReservationReviewDTO.CleanlinessInt != null && AccommodationReservationReviewDTO.RuleCompliance != null)
            {
                CleanlinessRatings[(int)AccommodationReservationReviewDTO.CleanlinessInt - 1] = true;
                DisciplineRatings[(int)AccommodationReservationReviewDTO.RuleCompliance - 1] = true;
            }

            List<string> accommImages = [.. accommodationReservationDTO.AccommodationImages.Split(',')];
            AccommodationImageGallery = new ImageGallery(accommImages);

            ShowNextAccommodationImageCMD = new RelayCommand(AccommodationImageGallery.ShowNextImage);
            ShowPreviousAccommodationImageCMD = new RelayCommand(AccommodationImageGallery.ShowPreviousImage);
        }

    }
}
