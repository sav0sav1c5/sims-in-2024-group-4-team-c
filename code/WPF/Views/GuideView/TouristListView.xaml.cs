using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.WPF.ViewModels.GuideViewModel;
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

namespace BookingApp.WPF.Views.GuideView
{
    /// <summary>
    /// Interaction logic for TouristListView.xaml
    /// </summary>
    public partial class TouristListView : Page
    {
        public NavigationService navigationService;
        public TourReservationDTO SelectedTour { get; set; }
        public static Guide LoggedGuide { get; set; }

        public TouristListView(Guide guide, TourReservationDTO tour, NavigationService _navigationService)
        {
            InitializeComponent();
            navigationService = _navigationService;
            LoggedGuide = guide;
            SelectedTour = tour; // Initialize SelectedTour here
            var viewModel = new TouristListViewModel(LoggedGuide, SelectedTour, navigationService);
            DataContext = viewModel;
        }
    }
}
