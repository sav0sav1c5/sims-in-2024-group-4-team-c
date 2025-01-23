using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.View.GuideView;

using System.Windows.Controls;
using System.Windows.Navigation;
using BookingApp.WPF.Views.GuideView;

namespace BookingApp.WPF.ViewModels.GuideViewModel
{
    public class GuideWindowViewModel
    {
        public static Guide LoggedGuide { get; set; }

        public NavigationService navigationService { get; set; }
        public RelayCommand CreateTourCommand { get; private set; }
        public RelayCommand ProfileCommand { get; private set; }
        public RelayCommand HomeCommand { get; private set; }
        public GuideWindowViewModel(Guide guide, NavigationService navigation)
        {
            LoggedGuide = guide;
            navigationService = navigation;

            CreateTourCommand = new RelayCommand(Create);
            ProfileCommand = new RelayCommand(Profile);
            HomeCommand = new RelayCommand(Home);
        }
        private void Create(object sender)
        {
            Page CreateTour = new CreateTour(LoggedGuide, navigationService);
            navigationService.Navigate(CreateTour);
        }
        private void Profile(object sender)
        {
            Page Profile = new GuideProfileView(LoggedGuide, navigationService);
            navigationService.Navigate(Profile);
        }
        private void Home(object sender)
        {
            Page Home = new GuideHomeView(LoggedGuide, navigationService);
            navigationService.Navigate(Home);
        }
    }
}
