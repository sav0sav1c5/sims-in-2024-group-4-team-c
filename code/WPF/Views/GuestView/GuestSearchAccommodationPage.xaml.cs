using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.ViewModels.GuestViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Linq;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuestSearchAccommodation.xaml
    /// </summary>
    public partial class GuestSearchAccommodationPage : Page
    {
        public GuestSearchAccommodationPage(GuestDTO loggedGuest, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new GuestSearchAccommodationViewModel(loggedGuest, navigationService);
        }

    }
}
