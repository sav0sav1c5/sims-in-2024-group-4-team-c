using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using BookingApp.Resources.DBAcces;
using BookingApp.WPF.ViewModels.GuideViewModel;
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

namespace BookingApp.View.GuideView
{
    /// <summary>
    /// Interaction logic for LiveTourView.xaml
    /// </summary>
    public partial class LiveTourView : Page
    {
        public static Guide LoggedGuide { get; set; }
        public static TourReservationDTO SelectedTour { get; set; }
        public LiveTourView(Guide guide, TourReservationDTO _tour, NavigationService navigationService)
        {
            InitializeComponent();
            LoggedGuide = guide;
            SelectedTour = _tour;
            var viewModel = new LiveTourViewModel(LoggedGuide, SelectedTour, navigationService);
            this.DataContext = viewModel;
        }
        public void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Pass the event handling to the ViewModel
            if (DataContext is LiveTourViewModel viewModel)
            {
                viewModel.CheckboxChecked(sender);
            }
        }
    }
}
