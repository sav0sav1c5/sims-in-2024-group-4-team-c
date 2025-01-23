using BookingApp.DTOs;
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
    /// Interaction logic for GuestViewReservationModificationRequestPage.xaml
    /// </summary>
    public partial class GuestViewReservationModificationRequestPage : Page
    {
        #region Data
        private GuestViewReservationModificationRequestViewModel _guestViewReservationModificationRequestViewModel;
        #endregion

        public GuestViewReservationModificationRequestPage(GuestDTO loggedGuest, NavigationService navigationService, ReservationModificationDTO reservationModificationDTO)
        {
            InitializeComponent();
            _guestViewReservationModificationRequestViewModel = new GuestViewReservationModificationRequestViewModel(loggedGuest, navigationService, reservationModificationDTO);
            this.DataContext = _guestViewReservationModificationRequestViewModel;
            _guestViewReservationModificationRequestViewModel.AccommodationImageGallery.RefreshFrontImage(AccommodationImageGallery);
        }
    }
}
