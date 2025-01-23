using BookingApp.Domain.Model;
using BookingApp.WPF;
using BookingApp.WPF.ViewModels.GuideViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingApp.View.GuideView
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class GuideHomeView : Page
    {
        public static Guide LoggedGuide { get; set; }

        public GuideHomeView(Guide guide, NavigationService navigationService)
        {
            InitializeComponent();
            LoggedGuide = guide;
            var viewModel = new GuideHomeViewModel(LoggedGuide, navigationService);
            this.DataContext = viewModel;
        }
    }
}
