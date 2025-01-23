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
    /// Interaction logic for Statistics3.xaml
    /// </summary>
    public partial class Statistics3 : Page
    {
        private NavigationService navigationService;
        public Statistics3(NavigationService _navigationService,int accommodationId, int year)
        {
            InitializeComponent();
            this.navigationService = navigationService;
            var viewModel = new Statistics3ViewModel(_navigationService, accommodationId, year);
            this.DataContext = viewModel;
        }
    }
}
