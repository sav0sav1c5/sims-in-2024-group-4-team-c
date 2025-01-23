using System;
using System.Windows.Controls;
using BookingApp.Domain.Model;
using System.Windows.Navigation;
using BookingApp.WPF.ViewModels.TouristViewModel;
using BookingApp.DTOs;

namespace BookingApp.View.TouristView
{
    public partial class TourDetailsView : Page
    {
        public static Tourist LoggedTourist { get; set; }
        public static Tour SelectedTour { get; set; }

        public TourDetailsView(Tourist loggedTourist, NavigationService navigationService, Tour selectedTour)
        {
            InitializeComponent();
            LoggedTourist = loggedTourist;
            SelectedTour = selectedTour;
            var viewModel = new TourDetailsViewModel(LoggedTourist, navigationService, SelectedTour);
            DataContext = viewModel;
        }
    }
}