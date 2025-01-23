using BookingApp.Domain.Model;
using BookingApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuestViewModel
{
    public class GuestMyProfileViewModel : ViewModelBase
    {
        #region Data
        public GuestDTO LoggedGuest { get; private set; }
        public NavigationService NavigationService { get; private set; }
        public System.Windows.Visibility SuperGuestIconVisibility
        {
            get => LoggedGuest.IsSuperGuest ? Visibility.Visible : Visibility.Collapsed;
            private set { }
        }
        #endregion

        public GuestMyProfileViewModel(GuestDTO loggedGuest, NavigationService navigationService)
        {
            LoggedGuest = loggedGuest;
            NavigationService = navigationService;
        }
    }
}
