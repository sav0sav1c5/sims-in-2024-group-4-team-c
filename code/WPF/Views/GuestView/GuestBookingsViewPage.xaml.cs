using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services;
using BookingApp.Services.GuestServices;
using BookingApp.WPF.ViewModels.GuestViewModel;
using BookingApp.WPF.Views.GuestView;
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
    /// Interaction logic for GuestBookingsView.xaml
    /// </summary>
    public partial class GuestBookingsViewPage : Page
    {
        #region Data
        private GuestBookingsViewModel _guestBookingsViewModel;
        #endregion



        public GuestBookingsViewPage(GuestDTO loggedGuest, NavigationService navigationService)
        {
            InitializeComponent();
            _guestBookingsViewModel = new GuestBookingsViewModel(loggedGuest, navigationService);
            this.DataContext = _guestBookingsViewModel;
            ReservationsDataGrid.Items.Refresh();         //////////////////////////////////////////////////////
        }
        

    }
}
