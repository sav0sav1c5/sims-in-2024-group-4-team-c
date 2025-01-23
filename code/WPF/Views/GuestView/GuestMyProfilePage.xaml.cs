using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.View.GuestView;
using BookingApp.WPF.ViewModels.GuestViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for GuestMyProfile.xaml
    /// </summary>
    public partial class GuestMyProfilePage : Page
    {
        #region Data
        //private GuestDTO LoggedGuest { get; set; }
        #endregion

        public GuestMyProfilePage(GuestDTO guestDTO, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new GuestMyProfileViewModel(guestDTO, navigationService);
            //LoggedGuest = guest;
            //LoggedGuest = guestDTO;
        }
    }
}
