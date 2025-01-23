using BookingApp.DTOs;
using BookingApp.View.OwnerView;
using BookingApp.WPF.ViewModels.OwnerViewModel;
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

namespace BookingApp.WPF.Views.OwnerView
{
    /// <summary>
    /// Interaction logic for ReservationModification.xaml
    /// </summary>
    public partial class ReservationModification : Page
    {
        public NavigationService navigationService;
        public ReservationModification(NavigationService _navigationService)
        {
            navigationService = _navigationService;
            InitializeComponent();
            var viewModel = new ReservationModificationViewModel(navigationService);
            this.DataContext = viewModel;

        }

       

        
    }
}
