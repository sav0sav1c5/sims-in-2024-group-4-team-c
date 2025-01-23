using System.Collections.ObjectModel;
using System.Windows.Controls;
using BookingApp.Repository;
using System.Collections.Generic;
using BookingApp.Resources.DBAcces;
using System.Linq;
using System.Windows;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels.GuideViewModel;
using System.Windows.Navigation;
using BookingApp.DTOs;

namespace BookingApp.View.GuideView
{
    public partial class TourDetails : Page
    {
        public NavigationService navigationService;
        public TourReservationDTO SelectedTour { get; set; }
        public static Guide LoggedGuide { get; set; }

        public TourDetails(Guide guide, TourReservationDTO tour, NavigationService _navigationService)
        {
            InitializeComponent();
            navigationService = _navigationService;
            LoggedGuide = guide;
            SelectedTour = tour; // Initialize SelectedTour here
            var viewModel = new TourDetailsViewModel(LoggedGuide, SelectedTour, navigationService);
            DataContext = viewModel; 
        }
    }
}