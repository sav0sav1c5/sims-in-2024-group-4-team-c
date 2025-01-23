using BookingApp.Domain.Model;
using BookingApp.View.TouristView;
using BookingApp.View;
using BookingApp.WPF.Views.OwnerView;
using BookingApp.WPF.Views.TouristView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Navigation;
using BookingApp.DTOs;
using System.Threading.Channels;

namespace BookingApp.WPF.ViewModels.TouristViewModel
{
    public class ComplexTourRequestNameViewModel : ViewModelBase
    {
        public RelayCommand ToursCommand { get; private set; }
        public RelayCommand RequestsCommand { get; private set; }
        public RelayCommand StatisticsCommand { get; private set; }
        public RelayCommand RateToursCommand { get; private set; }
        public RelayCommand TutorialCommand { get; private set; }
        public RelayCommand NotificationsCommand { get; private set; }
        public RelayCommand ProfileCommand { get; private set; }
        public RelayCommand LogOutCommand { get; private set; }
        public RelayCommand ConfirmNameCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public NavigationService NavigationService { get; set; }
        public string ProfileButtonContent { get; set; }
        private readonly Tourist LoggedTourist;
        public ComplexTourRequestNameViewModel(Tourist tourist, NavigationService _navigationService)
        {
            ToursCommand = new RelayCommand(ToursHome);
            RequestsCommand = new RelayCommand(Requests);
            StatisticsCommand = new RelayCommand(Statistics);
            RateToursCommand = new RelayCommand(RateTours); 
            TutorialCommand = new RelayCommand(Tutorial);
            NotificationsCommand = new RelayCommand(Notifications);
            ProfileCommand = new RelayCommand(ViewProfile);
            LogOutCommand = new RelayCommand(_ => LogOut());
            ConfirmNameCommand = new RelayCommand(ConfirmName); 
            CancelCommand = new RelayCommand(Cancel);
            NavigationService = _navigationService;
            LoggedTourist = tourist;
            LoggedTouristInfo(tourist);
        }

        private string complexTourName;
        public string ComplexTourName
        {
            get { return complexTourName; }
            set
            {
                complexTourName = value;
                OnPropertyChanged(nameof(ComplexTourName));
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

        private void LoggedTouristInfo(Tourist tourist)
        {
            TouristDTO LoggedTouristDTO = new TouristDTO(tourist);
            ProfileButtonContent = $"{LoggedTouristDTO.FirstName} {LoggedTouristDTO.LastName}";

        }

        private void ConfirmName(object parameter)
        {
            if (string.IsNullOrEmpty(ComplexTourName) || ComplexTourName.All(char.IsDigit))
            {
                MessageBox.Show("Please enter a valid tour name (letters and/or numbers).", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Page tourRequestView = new ComplexTourRequestView(LoggedTourist, NavigationService, ComplexTourName);
                NavigationService.Navigate(tourRequestView);
            }
        }

        private void Cancel(object parameter)
        {
            NavigationService.GoBack();
        }
    }
}
