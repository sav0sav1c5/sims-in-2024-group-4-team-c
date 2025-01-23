﻿using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels.GuideViewModel;
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

namespace BookingApp.WPF.Views.GuideView
{
    /// <summary>
    /// Interaction logic for ComplexToursView.xaml
    /// </summary>
    public partial class ComplexTourRequestsView : Page
    {
        public NavigationService navigationService;
        public static Guide LoggedGuide { get; set; }

        public ComplexTourRequestsView(Guide guide, NavigationService _navigationService)
        {
            navigationService = _navigationService;
            InitializeComponent();
            LoggedGuide = guide;
            var viewModel = new ComplexTourRequestsViewModel(LoggedGuide, navigationService);
            this.DataContext = viewModel;
        }
    }
}
