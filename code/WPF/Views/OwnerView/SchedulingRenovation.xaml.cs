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
    /// Interaction logic for SchedulingRenovation.xaml
    /// </summary>
    public partial class SchedulingRenovation : Page
    {
        public NavigationService navigationService;
        public SchedulingRenovation(NavigationService _navigationService)
        {
            navigationService = _navigationService;
            InitializeComponent();
            var viewModel = new SchedulingRenovationViewModel(navigationService);
            this.DataContext = viewModel;
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Your event handling logic here
            // For example:
            // AccommodationDTO selectedItem = (AccommodationDTO)DataGridSchedulingReservation.SelectedItem;
            // if (selectedItem != null)
            // {
            //     // Do something with the selected item
            // }
        }



    }
}
