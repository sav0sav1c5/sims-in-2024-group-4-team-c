using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Services.OwnerServices;
using BookingApp.WPF.ViewModels.OwnerViewModel;
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

namespace BookingApp.View.OwnerView
{
    /// <summary>
    /// Interaction logic for AccommodationRegister.xaml
    /// </summary>
    public partial class AccommodationRegister : Page
    {



        public NavigationService navigationService;

        public AccommodationRegister(NavigationService _navigationService)
        {
            navigationService = _navigationService;
            InitializeComponent();
            var viewModel = new AccommodationRegisterViewModel(navigationService);
            this.DataContext = viewModel;


        }

        public void PopulateComboBoxAccommodationTypes(ObservableCollection<string> comboBoxAccommodationTypes)
        {
            comboBoxAccommodationTypes.Add(AccommodationTypes.TypeToString(AccommodationTypes.Type.Apartment));
            comboBoxAccommodationTypes.Add(AccommodationTypes.TypeToString(AccommodationTypes.Type.House));
            comboBoxAccommodationTypes.Add(AccommodationTypes.TypeToString(AccommodationTypes.Type.Cabin));

        }

        private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
