using BookingApp.Domain.Model;
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
using System.Windows.Shapes;
using System.Windows.Navigation;
using BookingApp.Domain.Model;
using BookingApp.WPF;
using BookingApp.WPF.ViewModels.GuideViewModel;

namespace BookingApp.View.GuideView
{
    public partial class GuideWindow : Window
    {
        public GuideWindowViewModel guideWindowViewModel { get; set; }
        public Guide LoggedGuide { get; set; }

        public GuideWindow(Guide guide)
        {
            InitializeComponent();
            guideWindowViewModel = new GuideWindowViewModel(guide, this.GuideContentFrame.NavigationService);
            LoggedGuide = guide;
            GuideHomeView guideHomeView = new GuideHomeView(guide, GuideContentFrame.NavigationService);
            GuideContentFrame.Navigate(guideHomeView);

            this.DataContext = guideWindowViewModel;
        }
    }
}

