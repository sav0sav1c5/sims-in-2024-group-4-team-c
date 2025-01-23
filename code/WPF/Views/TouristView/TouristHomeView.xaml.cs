using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels.GuideViewModel;
using BookingApp.WPF.ViewModels.TouristViewModel;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.View.TouristView
{
    public partial class TouristHomeView : Page
    {
        public static Tourist LoggedTourist { get; set; }
        public TouristHomeView(Tourist tourist, NavigationService navigationService)
        {
            InitializeComponent();
            LoggedTourist = tourist;
            var viewModel = new TouristHomeViewModel(LoggedTourist, navigationService);
            this.DataContext = viewModel;
        }
    }
}
