using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services;
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

namespace BookingApp.View.GuestView
{
    /// <summary>
    /// Interaction logic for GuestViewReservation.xaml
    /// </summary>
    public partial class GuestViewReservationPage : Page
    {
        #region Data
        private GuestViewReservationViewModel _guestViewReservationViewModel;
        #endregion
        public GuestViewReservationPage(GuestDTO loggedGuest,AccommodationReservationDTO accommodationReservationDTO, NavigationService navigationService)
        {
            InitializeComponent();
            _guestViewReservationViewModel = new GuestViewReservationViewModel(loggedGuest, accommodationReservationDTO, navigationService) ;
            this.DataContext = _guestViewReservationViewModel;
        }

    }
}
