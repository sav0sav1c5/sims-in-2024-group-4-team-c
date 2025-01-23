using BookingApp.Repository;
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
using BookingApp.Repository;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels.TouristViewModel;
using System.Windows.Navigation;

namespace BookingApp.View.TouristView
{
    /// <summary>
    /// Interaction logic for TouristProfileView.xaml
    /// </summary>
    public partial class TouristProfileView : Page
    {
        public static Tourist LoggedTourist { get; set; }

        public TouristProfileView(Tourist tourist, NavigationService navigationService)
        {
            InitializeComponent();;
            LoggedTourist = tourist;
            var viewModel = new TouristProfileViewModel(LoggedTourist, navigationService);
            this.DataContext = viewModel;
        }
    }
}
