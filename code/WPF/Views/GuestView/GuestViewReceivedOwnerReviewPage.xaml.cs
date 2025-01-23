using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.View;
using BookingApp.WPF.ViewModels.GuestViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.GuestView
{
    /// <summary>
    /// Interaction logic for GuestViewReceivedOwnerReviewPage.xaml
    /// </summary>
    public partial class GuestViewReceivedOwnerReviewPage : Page
    {
        #region Data
        private GuestViewReceivedOwnerReviewViewModel _guestViewReceivedOwnerReviewViewModel;
        #endregion
        public GuestViewReceivedOwnerReviewPage(GuestDTO loggedGuest, AccommodationReservationDTO accommodationReservationDTO, NavigationService navigationService)
        {
            InitializeComponent();
            _guestViewReceivedOwnerReviewViewModel = new GuestViewReceivedOwnerReviewViewModel(loggedGuest, accommodationReservationDTO, navigationService);
            this.DataContext = _guestViewReceivedOwnerReviewViewModel;

            //Show the Accommodation image
            //_guestViewReceivedOwnerReviewViewModel.RefreshAccommodationImage(AccommodationImageGallery);
            _guestViewReceivedOwnerReviewViewModel.AccommodationImageGallery.RefreshFrontImage(AccommodationImageGallery);
        }

    }
}
