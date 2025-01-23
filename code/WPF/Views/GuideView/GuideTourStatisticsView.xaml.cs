using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels.GuideViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
namespace BookingApp.WPF.Views.GuideView
{
    /// <summary>
    /// Interaction logic for TourStatisticsView.xaml
    /// </summary>
    public partial class GuideTourStatisticsView : Page
    {
        public NavigationService navigationService;
        public static Guide LoggedGuide { get; set; }

        public GuideTourStatisticsView(Guide guide, NavigationService _navigationService)
        {
            InitializeComponent();
            navigationService = _navigationService;
            LoggedGuide = guide;
            var viewModel = new GuideTourStatisticsViewModel(LoggedGuide, navigationService);
            this.DataContext = viewModel;
        }
    }
}
