using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services;
using BookingApp.Services.GuideServices;
using BookingApp.View;
using BookingApp.View.TouristView;
using BookingApp.WPF.Views.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.TouristViewModel
{
    public class RequestsViewModel : ViewModelBase
    {
        private readonly TourRequestService tourRequestService = DependencyContainer.GetInstance<TourRequestService>();
        private readonly ComplexTourRequestService complexTourRequestService = DependencyContainer.GetInstance<ComplexTourRequestService>();
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; } = new ObservableCollection<TourRequestDTO>(); 
        public ObservableCollection<ComplexTourRequestDTO> ComplexTourRequests { get; set; } = new ObservableCollection<ComplexTourRequestDTO>();
        public string ProfileButtonContent { get; set; }
        public RelayCommand ToursCommand { get; private set; }
        public RelayCommand RequestsCommand { get; private set; }
        public RelayCommand StatisticsCommand { get; private set; }
        public RelayCommand RateToursCommand { get; private set; }
        public RelayCommand TutorialCommand { get; private set; }
        public RelayCommand NotificationsCommand { get; private set; }
        public RelayCommand ProfileCommand { get; private set; }
        public RelayCommand LogOutCommand { get; private set; }
        public RelayCommand StandardRequestCommand { get; private set; }
        public RelayCommand ComplexRequestCommand { get; private set; }
        public RelayCommand DetailsCommand { get; private set; }
        public NavigationService NavigationService { get; set; }
        private readonly Tourist LoggedTourist;

        public RequestsViewModel(Tourist tourist, NavigationService _navigationService)
        {
            ToursCommand = new RelayCommand(ToursHome);
            RequestsCommand = new RelayCommand(Requests);
            StatisticsCommand = new RelayCommand(Statistics);
            RateToursCommand = new RelayCommand(RateTours); 
            TutorialCommand = new RelayCommand(Tutorial);
            NotificationsCommand = new RelayCommand(Notifications);
            ProfileCommand = new RelayCommand(ViewProfile);
            LogOutCommand = new RelayCommand(_ => LogOut());
            StandardRequestCommand = new RelayCommand(CreateRequest);
            ComplexRequestCommand = new RelayCommand(CreateComplexRequest);
            DetailsCommand = new RelayCommand(ViewDetails); 
            NavigationService = _navigationService;
            LoggedTourist = tourist;
            LoggedTouristInfo(tourist);
            LoadTourRequests();
            LoadComplexTourRequests();  
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

        private void CreateRequest(object parameter)
        {
            Page tourRequestView = new TourRequestView(LoggedTourist, NavigationService, TourRequests);
            NavigationService.Navigate(tourRequestView);
        }

        private void CreateComplexRequest(object parameter)
        {
            Page complexTourRequestNameView = new ComplexTourRequestNameView(LoggedTourist, NavigationService);
            NavigationService.Navigate(complexTourRequestNameView);
        }

        private void LoggedTouristInfo(Tourist tourist)
        {
            TouristDTO LoggedTouristDTO = new TouristDTO(tourist);
            ProfileButtonContent = $"{LoggedTouristDTO.FirstName} {LoggedTouristDTO.LastName}";

        }

        private void LoadTourRequests()
        {
            var allTourRequests = tourRequestService.GetAllTourRequests().Select(tr => new TourRequestDTO(tr)).ToList();
            var touristRequests = allTourRequests.Where(tr => tr.TouristId == LoggedTourist.Id);

            foreach (var request in touristRequests)
            {
                TourRequests.Add(request);
            }
        }

        private void LoadComplexTourRequests()
        {
            var allComplexTourRequests = complexTourRequestService.GetAllComplexTourRequests().Select(ctr => new ComplexTourRequestDTO(ctr)).ToList();
            var touristRequests = allComplexTourRequests.Where(ctr => ctr.TouristId == LoggedTourist.Id);

            foreach (var request in touristRequests)
            {
                ComplexTourRequests.Add(request);
            }
        }

        private void ViewDetails(object parameter)
        {
            if (parameter is ComplexTourRequestDTO selectedRequest)
            {
                Page complexTourDetailsView = new ComplexTourRequestDetailsView(LoggedTourist, NavigationService, selectedRequest);
                NavigationService.Navigate(complexTourDetailsView);
            }
        }
    }
}