﻿using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels.TouristViewModel;
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

namespace BookingApp.WPF.Views.TouristView
{
    /// <summary>
    /// Interaction logic for RequestsView.xaml
    /// </summary>
    public partial class RequestsView : Page
    {
        public static Tourist LoggedTourist { get; set; }
        public RequestsView(Tourist tourist, NavigationService navigationService)
        {
            InitializeComponent();
            LoggedTourist = tourist;
            var viewModel = new RequestsViewModel(LoggedTourist, navigationService);
            this.DataContext = viewModel;
        }
    }
}
