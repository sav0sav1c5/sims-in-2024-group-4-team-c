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
using System.Windows.Shapes;
using System.Windows.Navigation;
using BookingApp.Domain.Model;
using BookingApp.Services;
using BookingApp.WPF.Views.GuestView;
using BookingApp.DTOs;
using BookingApp.Services.GuestServices;
using BookingApp.DependencyInjection;
using BookingApp.WPF.ViewModels.GuestViewModel;



namespace BookingApp.View.GuestView
{
    /// <summary>
    /// Interaction logic for GuestHomeView.xaml
    /// </summary>
    public partial class GuestHomeView : Window
    {
        private GuestDTO LoggedGuest { get; set; }
        public GuestHomeView(Guest guest)
        {
            InitializeComponent();
            LoggedGuest = new GuestDTO(UpdateSuperGuestStatus(guest));
            //LoggedGuest = new GuestDTO(guest);//old:

            //View my profile by default
            GuestMyProfilePage guestMyProfilePage = new GuestMyProfilePage(LoggedGuest, this.GuestWorkFrame.NavigationService);
            GuestWorkFrame.Navigate(guestMyProfilePage);
        }

        private Guest UpdateSuperGuestStatus(Guest guest)
        {
            SuperGuestService superGuestService = DependencyContainer.GetInstance<SuperGuestService>();
            return superGuestService.UpdateSuperGuestStatus(guest);
        }

        void ViewGuestSearchAccommodation(object sender, RoutedEventArgs e)
        {
            GuestSearchAccommodationPage guestSearchAccommodationPage = new GuestSearchAccommodationPage(LoggedGuest, this.GuestWorkFrame.NavigationService);
            GuestWorkFrame.Navigate(guestSearchAccommodationPage);
        }

        private void ViewGuestMyProfile(object sender, RoutedEventArgs e)
        {
            GuestMyProfilePage guestMyProfilePage = new GuestMyProfilePage(LoggedGuest, this.GuestWorkFrame.NavigationService); 
            GuestWorkFrame.Navigate(guestMyProfilePage);
        }

        private void ViewGuestBookings(object sender, RoutedEventArgs e)
        {
            GuestBookingsViewPage guestBookingsViewPage = new GuestBookingsViewPage(LoggedGuest, this.GuestWorkFrame.NavigationService);
            GuestWorkFrame.Navigate(guestBookingsViewPage);
        }

        private void ViewGuestReservationModificationRequestsPage(object sender, RoutedEventArgs e)
        {
            GuestViewReservationModificationRequestsPage guestViewReservationModificationRequestsPage = new GuestViewReservationModificationRequestsPage(LoggedGuest, this.GuestWorkFrame.NavigationService);
            GuestWorkFrame.Navigate(guestViewReservationModificationRequestsPage);
        }

        private void ViewGuestSearchForums(object sender, RoutedEventArgs e)
        {
            GuestSearchForumsPage guestSearchForumsPage = new GuestSearchForumsPage(LoggedGuest, this.GuestWorkFrame.NavigationService);
            GuestWorkFrame.Navigate(guestSearchForumsPage);
        }
    }
}
