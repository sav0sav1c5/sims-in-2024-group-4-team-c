using BookingApp.Domain.Model;
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
    /// Interaction logic for GuestViewReservationModificationRequestsPage.xaml
    /// </summary>
    public partial class GuestViewReservationModificationRequestsPage : Page
    {
        #region Data
        private GuestViewReservationModificationRequestsViewModel _guestViewReservationModificationRequestsViewModel;
        #endregion
        public GuestViewReservationModificationRequestsPage(GuestDTO loggedGuest, NavigationService navigationService)
        {
            InitializeComponent();
            _guestViewReservationModificationRequestsViewModel = new GuestViewReservationModificationRequestsViewModel(loggedGuest, navigationService);
            this.DataContext = _guestViewReservationModificationRequestsViewModel;
        }
    }
}
