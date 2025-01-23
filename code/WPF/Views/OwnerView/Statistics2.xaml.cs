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
    /// Interaction logic for Statistics2.xaml
    /// </summary>
    public partial class Statistics2 : Page
    {
        private NavigationService navigationService;
        private AccommodationDTO accommodation;
        public Statistics2(NavigationService navigationService,AccommodationDTO accommodationDTO)
        {
            InitializeComponent();
            this.navigationService = navigationService;
            accommodation = accommodationDTO;
            var viewModel = new Statistics2ViewModel(navigationService,accommodationDTO.Id);
            this.DataContext = viewModel;
        }

        private void CaptureSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            // Provjeri da li je neki element selektovan
            if (comboBoxMonths.SelectedItem != null)
            {
                // Uhvati selektovani element iz ComboBoxa
                int selectedMonth = (int)comboBoxMonths.SelectedItem;
                Statistics3 viewStatistics3 = new Statistics3(navigationService,accommodation.Id,selectedMonth);
                navigationService.Navigate(viewStatistics3);
                
                
            }
            
        }
    }
}
