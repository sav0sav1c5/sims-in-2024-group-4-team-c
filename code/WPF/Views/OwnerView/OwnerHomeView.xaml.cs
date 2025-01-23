using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels.OwnerViewModel;
using BookingApp.WPF.Views.OwnerView;
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

namespace BookingApp.View.OwnerView
{
    /// <summary>
    /// Interaction logic for OwnerHomeView.xaml
    /// </summary>
    public partial class OwnerHomeView : Window
    {

        public OwnerHomeView()
        {
            InitializeComponent();
            var viewModel = new OwnerHomeViewModel();
            this.DataContext = viewModel;
        }
        public OwnerHomeView(Owner owner)
        {
            InitializeComponent();
            var viewModel = new OwnerHomeViewModel(owner);
            this.DataContext = viewModel;
        }

        private void OpenAccommodationRegister(object sender, RoutedEventArgs e)
        {
            AccommodationRegister accommodationRegister = new AccommodationRegister(OwnerWorkFrame.NavigationService);
            OwnerWorkFrame.Navigate(accommodationRegister);
        }

        private void OpenStatistics(object sender, RoutedEventArgs e)
        {
            Statistics statistics = new Statistics(OwnerWorkFrame.NavigationService);
            OwnerWorkFrame.Navigate(statistics);
        }

        private void OpenRateGuests(object sender, RoutedEventArgs e)
        {
            RateGuests rateGuests = new RateGuests(OwnerWorkFrame.NavigationService);
            OwnerWorkFrame.Navigate(rateGuests);
        }

        private void OpenViewGuestReviews(object sender, RoutedEventArgs e)
        {
            ViewGuestReviews viewGuestReviews = new ViewGuestReviews(OwnerWorkFrame.NavigationService);
            OwnerWorkFrame.Navigate(viewGuestReviews);
        }

        private void OpenSuperOwner(object sender, RoutedEventArgs e)
        {
            SuperOwner viewSuperOwner = new SuperOwner(OwnerWorkFrame.NavigationService);
            OwnerWorkFrame.Navigate(viewSuperOwner);
        }

        

        private void OpenReservationModification(object sender, RoutedEventArgs e)
        {
            ReservationModification viewReservationModification = new ReservationModification(OwnerWorkFrame.NavigationService);
            OwnerWorkFrame.Navigate(viewReservationModification);
        }

        private void OpenScheudleRenovation(object sender, RoutedEventArgs e)
        {
            SchedulingRenovation viewScheaduleRenovation = new SchedulingRenovation(OwnerWorkFrame.NavigationService);
            OwnerWorkFrame.Navigate(viewScheaduleRenovation);
        }

        private void OpenManageRenovation(object sender, RoutedEventArgs e)
        {
            ManageRenovation viewManageRenovation = new ManageRenovation(OwnerWorkFrame.NavigationService);
            OwnerWorkFrame.Navigate(viewManageRenovation);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
{
    // Promenite boju pozadine menija na svetlo plavu boju (#FFADD8E6) kada se meni otvori
    MainMenu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
}


    }
}
