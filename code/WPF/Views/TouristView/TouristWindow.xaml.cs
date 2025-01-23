using BookingApp.Domain.Model;
using BookingApp.View.GuideView;
using BookingApp.View.TouristView;
using BookingApp.WPF.ViewModels.GuideViewModel;
using BookingApp.WPF.ViewModels.TouristViewModel;
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

namespace BookingApp.WPF.Views.TouristView
{
    /// <summary>
    /// Interaction logic for TouristWindow.xaml
    /// </summary>
    public partial class TouristWindow : Window
    {
        public TouristWindowViewModel touristWindowViewModel { get; set; }
        public Tourist LoggedTourist { get; set; }

        public TouristWindow(Tourist tourist)
        {
            InitializeComponent();
            touristWindowViewModel = new TouristWindowViewModel(this.TouristContentFrame.NavigationService);
            LoggedTourist = tourist;
            TouristHomeView touristHomeView = new TouristHomeView(tourist, TouristContentFrame.NavigationService);
            TouristContentFrame.Navigate(touristHomeView);

            this.DataContext = touristWindowViewModel;
        }
    }
}