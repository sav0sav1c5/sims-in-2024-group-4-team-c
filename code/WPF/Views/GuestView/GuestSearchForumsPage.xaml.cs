﻿using BookingApp.DTOs;
using BookingApp.WPF.ViewModels.GuestViewModel;
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

namespace BookingApp.WPF.Views.GuestView
{
    /// <summary>
    /// Interaction logic for GuestSearchForumsPage.xaml
    /// </summary>
    public partial class GuestSearchForumsPage : Page
    {
        #region Data
        private GuestSearchForumsViewModel _guestSearchForumsViewModel;
        #endregion
        public GuestSearchForumsPage(GuestDTO loggedGuest, NavigationService navigationService)
        {
            InitializeComponent();
            _guestSearchForumsViewModel = new GuestSearchForumsViewModel(loggedGuest, navigationService);
            this.DataContext = _guestSearchForumsViewModel;
        }
    }
}
