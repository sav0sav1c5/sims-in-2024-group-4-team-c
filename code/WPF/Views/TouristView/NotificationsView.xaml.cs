using BookingApp.Domain.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.TouristView
{
    /// <summary>
    /// Interaction logic for NotificationsView.xaml
    /// </summary>
    public partial class NotificationsView : Page
    {
        public static Tourist LoggedTourist { get; set; }   
        public NotificationsView(Tourist tourist, NavigationService _navigationService)
        {
            InitializeComponent();
            LoggedTourist = tourist;
            var viewModel = new NotificationsViewModel(LoggedTourist, _navigationService);
            this.DataContext = viewModel;
        }
    }
}
