using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Navigation;
using BookingApp.DependencyInjection;
using BookingApp.Services.GuideServices;
using BookingApp.View.TouristView;
using System.Windows.Input;
using System.Windows.Controls;
using BookingApp.WPF.Views.TouristView;
using BookingApp.DTOs;
using BookingApp.View;

namespace BookingApp.WPF.ViewModels.TouristViewModel
{
    public class TourDetailsViewModel : ViewModelBase
    {
        public string ProfileButtonContent { get; set; }
        public Tour SelectedTour { get; set; }
        public RelayCommand ToursCommand { get; private set; }
        public RelayCommand RequestsCommand { get; private set; }
        public RelayCommand StatisticsCommand { get; private set; }
        public RelayCommand RateToursCommand { get; private set; }
        public RelayCommand TutorialCommand { get; private set; }
        public RelayCommand NotificationsCommand { get; private set; }
        public RelayCommand ProfileCommand { get; private set; }
        public RelayCommand LogOutCommand { get; private set; }
        public RelayCommand BackToHomeCommand { get; private set; }
        public RelayCommand TourReservationCommand { get; private set; }
        public NavigationService NavigationService { get; set; }
        private readonly Tourist LoggedTourist;

        public TourDetailsViewModel(Tourist tourist, NavigationService _navigationService, Tour _selectedTour)
        {
            ToursCommand = new RelayCommand(ToursHome);
            RequestsCommand = new RelayCommand(Requests);
            StatisticsCommand = new RelayCommand(Statistics);
            RateToursCommand = new RelayCommand(RateTours);
            TutorialCommand = new RelayCommand(Tutorial);
            NotificationsCommand = new RelayCommand(Notifications);
            ProfileCommand = new RelayCommand(ViewProfile);
            LogOutCommand = new RelayCommand(_ => LogOut());
            BackToHomeCommand = new RelayCommand(BackToHome);
            TourReservationCommand = new RelayCommand(Reservation); 
            NavigationService = _navigationService;
            LoggedTourist = tourist;
            LoggedTouristInfo(tourist);

            SelectedTour = _selectedTour ?? throw new ArgumentNullException(nameof(_selectedTour));
            TourImageUrl = _selectedTour.Images;
        }
        
        private string tourImageUrl;
        public string TourImageUrl
        {
            get { return tourImageUrl; }
            private set
            {
                tourImageUrl = value;
                OnPropertyChanged(nameof(TourImageUrl));
            }
        }

        private void ToursHome(object parameter)
        {
            Page toursView = new TouristHomeView(LoggedTourist, NavigationService);
            NavigationService.Navigate(toursView);
        }

        private void Requests(object parameter)
        {
            Page requestsView = new RequestsView(LoggedTourist, NavigationService);
            NavigationService.Navigate(requestsView);
        }

        private void Statistics(object parameter)
        {
            Page tourStatisticsView = new TourStatisticsView(LoggedTourist, NavigationService);
            NavigationService.Navigate(tourStatisticsView);
        }

        private void RateTours(object parameter)
        {
            Page toursRateView = new ToursRateView(LoggedTourist, NavigationService);
            NavigationService.Navigate(toursRateView);
        }

        private void Tutorial(object parameter)
        {
            Page tutorialView = new TutorialView(LoggedTourist, NavigationService);
            NavigationService.Navigate(tutorialView);
        }

        private void Notifications(object parameter)
        {
            Page notificationsView = new NotificationsView(LoggedTourist, NavigationService);
            NavigationService.Navigate(notificationsView);
        }

        private void ViewProfile(object parameter)
        {
            Page profileView = new TouristProfileView(LoggedTourist, NavigationService);
            NavigationService.Navigate(profileView);
        }

        private void LogOut()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(TouristWindow))
                    {
                        SignInForm signInForm = new SignInForm();
                        signInForm.Show();
                        window.Close();
                    }
                }
            }
        }

        private void BackToHome(object obj)
        {
            NavigationService.GoBack();
        }
        private void Reservation(object parameter)
        {
            Page tourReservationView = new TourReservationView(LoggedTourist, NavigationService, SelectedTour);
            NavigationService.Navigate(tourReservationView);
        }

        private void LoggedTouristInfo(Tourist tourist)
        {
            TouristDTO LoggedTouristDTO = new TouristDTO(tourist);
            ProfileButtonContent = $"{LoggedTouristDTO.FirstName} {LoggedTouristDTO.LastName}";

        }
    }
}