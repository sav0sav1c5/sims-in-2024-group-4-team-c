using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.WPF.ViewModels.TouristViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TourRequestView.xaml
    /// </summary>
    public partial class TourRequestView : Page
    {
        public static Tourist LoggedTourist { get; set; }
        public static ObservableCollection<TourRequestDTO> TourRequests { get; set; }   
        public TourRequestView(Tourist tourist, NavigationService navigationService, ObservableCollection<TourRequestDTO> tourRequests)
        {
            InitializeComponent();
            LoggedTourist = tourist;
            TourRequests = tourRequests;
            var viewModel = new TourRequestViewModel(LoggedTourist, navigationService, TourRequests);
            this.DataContext = viewModel;
        }
    }
}