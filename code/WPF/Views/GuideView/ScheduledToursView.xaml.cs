using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels.GuideViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.View.GuideView
{
    public partial class ScheduledToursView : Page
    {
        public NavigationService navigationService;
        public static Guide LoggedGuide { get; set; }

        public ScheduledToursView(Guide guide, NavigationService _navigationService)
        {
            navigationService = _navigationService;
            InitializeComponent();
            LoggedGuide = guide;
            var viewModel = new ScheduledToursViewModel(LoggedGuide, navigationService);
            this.DataContext = viewModel;
        }
    }
}
