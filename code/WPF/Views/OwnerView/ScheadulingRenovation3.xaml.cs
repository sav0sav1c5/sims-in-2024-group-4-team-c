using BookingApp.DTOs;
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
    /// Interaction logic for ScheadulingRenovation3.xaml
    /// </summary>
    public partial class ScheadulingRenovation3 : Page
    {
        private NavigationService navigationService;
        public ScheadulingRenovation3(NavigationService _navigationService, SchedulingRenovationDTO schedulingRenovationDTO)
        {
            navigationService = _navigationService;
            InitializeComponent();
            var viewModel = new SchedulingRenovation3ViewModel(navigationService, schedulingRenovationDTO);
            this.DataContext = viewModel;
        }
    }
}
