using BookingApp.Domain.Model;
using BookingApp.DependencyInjection;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.ViewModels.GuestViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using BookingApp.DTOs;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuestReservationPage.xaml
    /// </summary>
    public partial class GuestReservationPage : Page
    {
        #region Data
        private GuestReservationViewModel _guestReservationViewModel { get; set; }
        #endregion


        public GuestReservationPage(GuestDTO loggedGuest, AccommodationDTO accommodationDTO, int stayingPeriod, int numberOfGuests, NavigationService navigationService, DateTime accommodationReservationSearchFromDate, DateTime accommodationReservationSearchToDate)
        {
            InitializeComponent();
            _guestReservationViewModel = new GuestReservationViewModel(
                loggedGuest
                , accommodationDTO
                , stayingPeriod
                , numberOfGuests
                , navigationService
                , accommodationReservationSearchFromDate
                , accommodationReservationSearchToDate);
            this.DataContext = _guestReservationViewModel;
            _guestReservationViewModel.AccommodationImageGallery.RefreshFrontImage(AccommodationImageGallery);    
        }

    }
}
