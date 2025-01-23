using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
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

namespace BookingApp.View.OwnerView
{
    /// <summary>
    /// Interaction logic for RateView.xaml
    /// </summary>
    public partial class RateView : Page
    {

        public NavigationService navigationService;
        
        

        public RateView(RateGuestDTO rateGuestDTO, NavigationService _navigationService)
        {
            navigationService = _navigationService;
            InitializeComponent();
            var viewModel = new RateViewViewModel(rateGuestDTO);
            this.DataContext = viewModel;
            
        }

       



    }
}
