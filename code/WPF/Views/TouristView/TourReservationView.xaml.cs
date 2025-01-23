using System;
using System.Windows.Controls;
using BookingApp.Domain.Model;
using System.Windows.Navigation;
using BookingApp.WPF.ViewModels.TouristViewModel;

namespace BookingApp.View.TouristView
{
    public partial class TourReservationView : Page
    {
        public static Tourist LoggedTourist { get; set; }
        public static Tour SelectedTour { get; set; }

        public TourReservationView(Tourist loggedTourist, NavigationService navigationService, Tour selectedTour)
        {
            InitializeComponent();
            LoggedTourist = loggedTourist;
            SelectedTour = selectedTour;
            var viewModel = new TourReservationViewModel(LoggedTourist, navigationService, SelectedTour);
            DataContext = viewModel;
        }
    }
}
