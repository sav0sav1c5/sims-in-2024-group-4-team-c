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
    /// Interaction logic for GuideProfileView.xaml
    /// </summary>
    public partial class GuideProfileView : Page
    {
        public NavigationService navigationService;
        public static Guide LoggedGuide { get; set; }

        public GuideProfileView(Guide guide, NavigationService _navigationService)
        {
            navigationService = _navigationService;
            InitializeComponent();
            LoggedGuide = guide;
            var viewModel = new GuideProfileViewModel(LoggedGuide, navigationService);
            this.DataContext = viewModel;
        }
    }
}
