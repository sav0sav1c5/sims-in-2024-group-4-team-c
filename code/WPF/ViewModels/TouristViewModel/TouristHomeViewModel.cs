using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BookingApp.DependencyInjection;
using BookingApp.Services.GuideServices;
using BookingApp.View.TouristView;
using BookingApp.View;
using System.Windows.Navigation;
using System.Diagnostics.Metrics;
using System.Linq;
using BookingApp.WPF.Views.TouristView;
using BookingApp.DTOs;

namespace BookingApp.WPF.ViewModels.TouristViewModel
{
    public class TouristHomeViewModel : ViewModelBase
    {
        private readonly TourService tourService = DependencyContainer.GetInstance<TourService>();
        private readonly LocationService locationService = DependencyContainer.GetInstance<LocationService>();
        public ObservableCollection<Tour> Tours { get; set; } = new ObservableCollection<Tour>();
        public string ProfileButtonContent { get; set; }

        public RelayCommand RefreshCommand { get; private set; }
        public RelayCommand ToursCommand { get; private set; }
        public RelayCommand RequestsCommand { get; private set; }
        public RelayCommand StatisticsCommand { get; private set; }
        public RelayCommand RateToursCommand { get; private set; }
        public RelayCommand ProfileCommand { get; private set; }
        public RelayCommand NotificationsCommand { get; private set; }
        public RelayCommand TutorialCommand { get; private set; }
        public RelayCommand LogOutCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand DetailsCommand { get; private set; }
        public RelayCommand ReservationCommand { get; private set; }
        public RelayCommand RefreshButtonClickCommand { get; private set; }
        public NavigationService NavigationService { get; set; }
        private readonly Tourist LoggedTourist;

        public TouristHomeViewModel(Tourist tourist, NavigationService _navigationService)
        {
            ToursCommand = new RelayCommand(ToursHome);
            RequestsCommand = new RelayCommand(Requests);
            StatisticsCommand = new RelayCommand(Statistics);
            RateToursCommand = new RelayCommand(RateTours);
            ProfileCommand = new RelayCommand(ViewProfile);
            NotificationsCommand = new RelayCommand(Notifications);
            TutorialCommand = new RelayCommand(Tutorial);
            LogOutCommand = new RelayCommand(_ => LogOut());
            SearchCommand = new RelayCommand(Search);
            DetailsCommand = new RelayCommand(Details);
            ReservationCommand = new RelayCommand(Reservation);
            RefreshCommand = new RelayCommand(_ => RefreshTourList());
            NavigationService = _navigationService;
            LoggedTourist = tourist;
            LoggedTouristInfo(tourist);

            RefreshTourList();
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

        private void Notifications(object parameter)
        {
            Page notificationsView = new NotificationsView(LoggedTourist, NavigationService);
            NavigationService.Navigate(notificationsView);
        }

        private void Tutorial(object parameter)
        {
            Page tutorialView = new TutorialView(LoggedTourist, NavigationService);
            NavigationService.Navigate(tutorialView);
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

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
            }
        }

        private string _state;
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        private string _duration;
        public string Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        private string _maxTouristNumber;
        public string MaxTouristNumber
        {
            get { return _maxTouristNumber; }
            set
            {
                _maxTouristNumber = value;
                OnPropertyChanged(nameof(MaxTouristNumber));
            }
        }

        private string _searchParametersInfo;
        public string SearchParametersInfo
        {
            get { return _searchParametersInfo; }
            set
            {
                _searchParametersInfo = value;
                OnPropertyChanged(nameof(SearchParametersInfo));
            }
        }

        private void Details(object parameter)
        {
            if (parameter is Tour selectedTour)
            {
                Page tourDetailsView = new TourDetailsView(LoggedTourist, NavigationService, selectedTour);
                NavigationService.Navigate(tourDetailsView);
            }
        }

        private void Reservation(object parameter)
        {
            if (parameter is Tour selectedTour)
            {
                Page tourReservationView = new TourReservationView(LoggedTourist, NavigationService, selectedTour);
                NavigationService.Navigate(tourReservationView);
            }
        }

        private void RefreshTourList()
        {
            List<Tour> allTours = tourService.GetAllTours();
            Tours.Clear();
            foreach (var tour in allTours)
            {
                Location location = locationService.GetLocationById(tour.Id);
                tour.Location = location;
                Tours.Add(tour);
            }
            ClearSearchParameters();
            SearchParametersInfo = "";
        }

        private void LoggedTouristInfo(Tourist tourist)
        {
            TouristDTO LoggedTouristDTO = new TouristDTO(tourist);
            ProfileButtonContent = $"{LoggedTouristDTO.FirstName} {LoggedTouristDTO.LastName}";

        }

        private bool MaxTouristNumberCheck(Tour tour)
        {
            if (string.IsNullOrEmpty(MaxTouristNumber))
            {
                return true;
            }
            else
            {
                if (!int.TryParse(MaxTouristNumber, out int number))
                {
                    MessageBox.Show("Max Tourist Number must be a valid number.");
                    return false; 
                }
                return number > 0 && tour.MaxTouristNumber == number;
            }
        }

        private List<Tour> FindMatchingTours()
        {
            List<Tour> foundTours = new List<Tour>();

            foreach (var tour in tourService.GetAllTours())
            {
                Location tourLocation = locationService.GetLocationById(tour.LocationId);
                bool stateMatches = string.IsNullOrEmpty(State) || tourLocation.Country == State;
                bool cityMatches = string.IsNullOrEmpty(City) || tourLocation.City == City;
                bool languageMatches = string.IsNullOrEmpty(Language) || tour.Language.Equals(Language, StringComparison.OrdinalIgnoreCase);
                bool durationMatches = string.IsNullOrEmpty(Duration) || tour.Duration.ToString() == Duration;
                bool maxTouristNumberMatches = MaxTouristNumberCheck(tour);

                if (stateMatches && cityMatches && languageMatches && durationMatches && maxTouristNumberMatches)
                {
                    foundTours.Add(tour);
                }
            }

            return foundTours;
        }

        private string GenerateSearchParametersInfo()
        {
            StringBuilder searchInfo = new StringBuilder();
            searchInfo.Append("Results for: ");
            List<string> parameters = new List<string>();

            if (!string.IsNullOrEmpty(State))
                parameters.Add($"{State}");
            if (!string.IsNullOrEmpty(City))
                parameters.Add($"{City}");
            if (!string.IsNullOrEmpty(Language))
                parameters.Add($"{Language}");
            if (!string.IsNullOrEmpty(Duration))
                parameters.Add($"{Duration}");
            if (!string.IsNullOrEmpty(MaxTouristNumber))
                parameters.Add($"{MaxTouristNumber}");

            searchInfo.Append(string.Join(", ", parameters));
            return searchInfo.ToString();
        }

        private bool ValidateMaxTouristNumberInput()
        {
            if (!string.IsNullOrEmpty(MaxTouristNumber) && !int.TryParse(MaxTouristNumber, out _))
            {
                MessageBox.Show("Max Tourist Number must be a valid integer.");
                return false;
            }

            return true;
        }

        private void Search(object parameter)
        {
            if (!ValidateMaxTouristNumberInput())
            {
                return;
            }

            List<Tour> foundTours = FindMatchingTours();
            SearchParametersInfo = GenerateSearchParametersInfo();

            Tours.Clear();
            foreach (var tour in foundTours)
            {
                Tours.Add(tour);
            }

            ClearSearchParameters();
        }

        private void ClearSearchParameters()
        {
            State = "";
            City = "";
            Language = "";
            Duration = "";
            MaxTouristNumber = "";
        }
    }
}