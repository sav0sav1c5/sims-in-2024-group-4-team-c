using BookingApp.Domain.Model;
using BookingApp.DTOs;
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
    /// Interaction logic for ComplexTourRequestDetailsView.xaml
    /// </summary>
    public partial class ComplexTourRequestDetailsView : Page
    {
        public static Tourist LoggedTourist { get; set; }
        public ComplexTourRequestDTO ComplexTourRequestDTO { get; set; }
        public ComplexTourRequestDetailsView(Tourist tourist, NavigationService navigationService, ComplexTourRequestDTO selectedRequest)
        {
            InitializeComponent();
            LoggedTourist = tourist;
            ComplexTourRequestDTO = selectedRequest;
            var viewModel = new ComplexTourRequestDetailsViewModel(LoggedTourist, navigationService, ComplexTourRequestDTO);
            this.DataContext = viewModel;
        }
    }
}
