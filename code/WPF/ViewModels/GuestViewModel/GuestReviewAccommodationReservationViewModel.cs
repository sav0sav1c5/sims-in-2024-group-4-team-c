using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Navigation;
using BookingApp.View.GuestView;
using System.Windows.Controls;
using System.Reflection.Metadata;

namespace BookingApp.WPF.ViewModels.GuestViewModel
{
    public class GuestReviewAccommodationReservationViewModel : ViewModelBase
    {
        #region Data
        public readonly GuestDTO LoggedGuest;
        public AccommodationReservationDTO AccommodationReservationDTO { get; private set; }
        public NavigationService NavigationService { get; private set; }
        public string GuestReviewComment { get; set; } = string.Empty;
        public string NewImagePath { get; set; } = string.Empty;
        public ImageGallery ReviewImageGallery { get; set; }
        public ImageGallery AccommodationImageGallery { get; set; }
        #endregion

        #region RadioButtonData
        private bool []_correctnessRatings = new bool[] { true, false, false, false, false };
        public bool[]CorrectnessRatings
        {
            get => _correctnessRatings;
            set => _correctnessRatings = value;
        }
        private int SelectedCorrectnessRating
        {
            get { return Array.IndexOf(_correctnessRatings, true); }
        }
        private bool[] _cleanlinessRatings = new bool[] { true, false, false, false, false };
        public bool[] CleanlinessRatings
        {
            get => _cleanlinessRatings;
            set => _cleanlinessRatings = value;
        }
        private int SelectedCleanlinessRating
        {
            get { return Array.IndexOf(_cleanlinessRatings, true); }
        }
        private bool[] _renovationNeedRatings = new bool[] { true, false, false, false, false };
        public bool[] RenovationNeedRatings
        {
            get => _renovationNeedRatings;
            set
            {
                _renovationNeedRatings = value;
                OnPropertyChanged(nameof(RenovationNeedRatings));
            }
        }
        private int SelectedRenovationNeedRating
        {
            get { return Array.IndexOf(_renovationNeedRatings, true); }
        }
        private bool[] _isInNeedOfRenovations = new bool[] { true, false };
        public bool[] IsInNeedOfRenovations
        {
            get => _isInNeedOfRenovations;
            set => _isInNeedOfRenovations = value;
        }
        public bool IsInNeedOfRenovation
        {
            get => IsInNeedOfRenovations[0] == true;
        }
        #endregion

        #region RelayCommands
        public RelayCommand UploadImageCMD { get; private set; }
        public RelayCommand RemoveUploadedImageCMD { get; private set; }
        public RelayCommand ViewPreviousImageCMD { get; private set; }
        public RelayCommand ViewNextImageCMD { get; private set; }
        public RelayCommand PostReviewCMD { get; private set; }
        public RelayCommand ShowNextAccommodationImageCMD { get; private set; }
        public RelayCommand ShowPreviousAccommodationImageCMD { get; private set; }
        #endregion

        #region Services
        private GuestService _guestService = DependencyContainer.GetInstance<GuestService>();
        private GuestAccommodationReservationReviewService _reservationReviewService = DependencyContainer.GetInstance<GuestAccommodationReservationReviewService>();
        #endregion

        public GuestReviewAccommodationReservationViewModel(GuestDTO loggedGuest, AccommodationReservationDTO accommodationReservationDTO, NavigationService navigationService)
        {
            LoggedGuest = loggedGuest;
            AccommodationReservationDTO = accommodationReservationDTO;
            NavigationService = navigationService;
            ReviewImageGallery = new ImageGallery();
            UploadImageCMD = new RelayCommand(UploadImageCMD_func);
            RemoveUploadedImageCMD = new RelayCommand(RemoveUploadedImageCMD_func);
            PostReviewCMD = new RelayCommand(PostReviewCMD_func);
            ViewPreviousImageCMD = new RelayCommand(ReviewImageGallery.ShowPreviousImage);
            ViewNextImageCMD = new RelayCommand(ReviewImageGallery.ShowNextImage);

            List<string> accommImages = [.. accommodationReservationDTO.AccommodationImages.Split(',')];
            AccommodationImageGallery = new ImageGallery(accommImages);

            ShowNextAccommodationImageCMD = new RelayCommand(AccommodationImageGallery.ShowNextImage);
            ShowPreviousAccommodationImageCMD = new RelayCommand(AccommodationImageGallery.ShowPreviousImage);
        }


        public void PostReviewCMD_func(object paramater)
        {
            //NOTE: '_accommodationReservation' must be in 'ReservationState.State.Ended' and the guests review posting period should not be over
            Accommodation? targetAccommodation = _guestService.GetAccommodationById(AccommodationReservationDTO.AccommodationId);
            if (targetAccommodation == null)
            {
                MessageBox.Show("Failed to find target accommodation!");
                return;
            }

            RateOwnerDTO reviewDTO = new RateOwnerDTO(
                AccommodationReservationDTO.AccommodationId
                , AccommodationReservationDTO.Id
                , AccommodationReservationDTO.GuestId
                , targetAccommodation.OwnerId
                , SelectedCleanlinessRating + 1
                , SelectedCorrectnessRating + 1
                , GuestReviewComment
                , IsInNeedOfRenovation
                , SelectedRenovationNeedRating + 1
                , string.Join(',', ReviewImageGallery.Images)
                );

            //Will set the connections
            _reservationReviewService.SaveAccommodationReservationReview(reviewDTO);

            MessageBox.Show("Review successfully saved");
            GuestBookingsViewPage guestBookingsViewPage = new GuestBookingsViewPage(LoggedGuest, NavigationService);
            NavigationService.Navigate(guestBookingsViewPage);
        }

        public void UploadImageCMD_func(object imageControl)
        {
            if (NewImagePath.Length <= 0)
                return;
            ReviewImageGallery.Add(NewImagePath);
            ReviewImageGallery.RefreshFrontImage( (System.Windows.Controls.Image) imageControl);
            MessageBox.Show("Image uploaded successfully");
        }

        public void RemoveUploadedImageCMD_func(object paramater)
        {
            ReviewImageGallery.RemoveViewingImage(paramater);
            MessageBox.Show("Image successfully removed!");
        }

    }
}
