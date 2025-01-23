using BookingApp.Domain.Model;
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
    /// Interaction logic for TourRequestStatisticsView.xaml
    /// </summary>
    public partial class TourRequestStatisticsView : Page
    {
        public NavigationService navigationService;
        public static Guide LoggedGuide { get; set; }

        public TourRequestStatisticsView(Guide guide, NavigationService _navigationService)
        {
            InitializeComponent();
            navigationService = _navigationService;
            LoggedGuide = guide;
            var viewModel = new TourRequestStatisticsViewModel(LoggedGuide, navigationService);
            this.DataContext = viewModel;
        }
    }
}
