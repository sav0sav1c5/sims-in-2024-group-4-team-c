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
    /// Interaction logic for ComplexTourRequestView.xaml
    /// </summary>
    public partial class ComplexTourRequestView : Page
    {
        public static Tourist LoggedTourist { get; set; }
        public string ComplexTourName { get; set; }
        public ComplexTourRequestView(Tourist tourist, NavigationService navigationService, string complexTourName)
        {
            InitializeComponent();
            LoggedTourist = tourist;
            ComplexTourName = complexTourName;
            var viewModel = new ComplexTourRequestViewModel(LoggedTourist, navigationService, ComplexTourName);
            this.DataContext = viewModel;
        }
    }
}
