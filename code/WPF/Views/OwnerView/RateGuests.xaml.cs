using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace BookingApp.View.OwnerView
{
    /// <summary>
    /// Interaction logic for RateGuests.xaml
    /// </summary>
    public partial class RateGuests : Page
    {

        /*
        public ObservableCollection<RateGuestViewModel> MyData { get; set; }
        private readonly AccommodationRepository accommodationRepository = new AccommodationRepository();
        private readonly AccommodationReservationRepository accommodationReservationRepository = new AccommodationReservationRepository();
        private readonly GuestRepository guestRepository = new GuestRepository();
        */

        public NavigationService navigationService;
        public RateGuests(NavigationService _navigationService)
        {
            navigationService = _navigationService;
            InitializeComponent();
            var viewModel = new RateGuestViewModel(navigationService);
            this.DataContext = viewModel;



        }



       

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dataContext = button.DataContext as RateGuestDTO;

            // Otvorite novi prozor i prenesite podatke
            RateView rateView = new RateView(dataContext,navigationService);
            this.NavigationService.Navigate(rateView);
        }
    }

    
}

