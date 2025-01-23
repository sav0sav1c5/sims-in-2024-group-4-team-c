using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuideServices;
using BookingApp.View;
using BookingApp.View.TouristView;
using BookingApp.WPF.Views.TouristView;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.TouristViewModel
{
    public class TourStatisticsViewModel : ViewModelBase
    {
        private readonly TourRequestService tourRequestService = DependencyContainer.GetInstance<TourRequestService>();
        public ObservableCollection<TourRequest> TourRequests { get; set; } = new ObservableCollection<TourRequest>();
        public Dictionary<(string city, string country), int> RequestsLocations { get; set; } = new Dictionary<(string city, string country), int>();
        public Dictionary<string, int> RequestsLanguages { get; set; } = new Dictionary<string, int>();
        public SeriesCollection LocationsCollection { get; set; } = new SeriesCollection();
        public SeriesCollection LanguagesCollection { get; set; } = new SeriesCollection();
        public List<string> Years { get; set; } = new List<string> { "General", "2020", "2021", "2022", "2023", "2024" };
        public Func<double, string> YAxisLabelFormatter => value => value.ToString("0.##");
        public Location[] LocationLabels { get; set; }
        public string[] LanguageLabels { get; set; }
        public int PercentageOfAccepted { get; set; } = 0;
        public int PercentageOfDeclined { get; set; } = 0;
        public int NumberOfRequests { get; set; } = 0;
        public int NumberOfAccepted { get; set; } = 0;
        public int NumberOfDeclined { get; set; } = 0; 
        public string ProfileButtonContent { get; set; }
        public string SelectedYear { get; set; }
        public RelayCommand RefreshCommand { get; private set; }
        public RelayCommand ToursCommand { get; private set; }
        public RelayCommand RequestsCommand { get; private set; }
        public RelayCommand StatisticsCommand { get; private set; }
        public RelayCommand RateToursCommand { get; private set; }
        public RelayCommand TutorialCommand { get; private set; }
        public RelayCommand NotificationsCommand { get; private set; }
        public RelayCommand ProfileCommand { get; private set; }
        public RelayCommand LogOutCommand { get; private set; }
        public RelayCommand ConfirmCommand { get; private set; }
        public NavigationService NavigationService { get; set; }
        private readonly Tourist LoggedTourist;

        public TourStatisticsViewModel(Tourist tourist, NavigationService _navigationService)
        {
            ToursCommand = new RelayCommand(ToursHome);
            RequestsCommand = new RelayCommand(Requests);
            StatisticsCommand = new RelayCommand(Statistics);
            RateToursCommand = new RelayCommand(RateTours);
            NotificationsCommand = new RelayCommand(Notifications);
            TutorialCommand = new RelayCommand(Tutorial);
            ProfileCommand = new RelayCommand(ViewProfile);
            LogOutCommand = new RelayCommand(_ => LogOut());
            ConfirmCommand = new RelayCommand(ConfirmYear);
            NavigationService = _navigationService;
            LoggedTourist = tourist;
            LoggedTouristInfo(tourist);
            LoadTourRequests(0);
            UpdatePercentage();
            UpdateLanguageStatistics();
            UpdateLocationStatistics();
        }

        private void ConfirmYear(object parameter)
        {
            int selectedYear;
            if (SelectedYear == "General")
            {
                selectedYear = 0; // Postavljamo 0 ako je izabrana opcija "General"
            }
            else
            {
                if (!int.TryParse(SelectedYear, out selectedYear))
                {
                    // Neuspešno parsiranje, možete izvršiti odgovarajuće korake za grešku
                    return;
                }
            }

            TourRequests.Clear();
            NumberOfRequests = 0;
            NumberOfAccepted = 0;
            NumberOfDeclined = 0;
            PercentageOfAccepted = 0;
            PercentageOfDeclined = 0;
            LoadTourRequests(selectedYear);
            UpdatePercentage();
            RequestsLanguages.Clear();
            RequestsLocations.Clear();
            LanguagesCollection.Clear();
            LocationsCollection.Clear();
            UpdateLanguageStatistics();
            UpdateLocationStatistics();
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

        private void LoadTourRequests(int selectedYear)
        {
            List<TourRequest> tourRequests = tourRequestService.GetAllTourRequests();

            if (selectedYear != 0)
            {
                tourRequests = tourRequestService.GetTourRequestsByYear(selectedYear);
            }
            else
            {
                tourRequests = tourRequestService.GetAllTourRequests();
            }

            foreach (var request in tourRequests)
            {
                TourRequests.Add(request);
                NumberOfRequests++;

                if (request.TourRequestState == TourRequestStates.Type.Accepted)
                {
                    NumberOfAccepted++;
                }
                else if (request.TourRequestState == TourRequestStates.Type.Declined)
                {
                    NumberOfDeclined++;
                }
            }

            UpdatePercentage();
        }

        private void UpdatePercentage()
        {
            if (NumberOfRequests > 0)
            {
                PercentageOfAccepted = (int)(((double)NumberOfAccepted / NumberOfRequests) * 100);
                PercentageOfDeclined = (int)(((double)NumberOfDeclined / NumberOfRequests) * 100);
                OnPropertyChanged(nameof(PercentageOfAccepted));
                OnPropertyChanged(nameof(PercentageOfDeclined));
            }
            else
            {
                PercentageOfAccepted = 0;
                PercentageOfDeclined = 0;
                OnPropertyChanged(nameof(PercentageOfAccepted));
                OnPropertyChanged(nameof(PercentageOfDeclined));
            }
        }

        private void UpdateLanguageStatistics()
        {
            RequestsLanguages = GetRequestsByLanguage();

            foreach (KeyValuePair<string, int> pair in RequestsLanguages)
            {
                ColumnSeries series = new ColumnSeries();
                series.Values = new ChartValues<int> { pair.Value };
                series.Title = pair.Key;
                LanguagesCollection.Add(series);
            }

            LanguageLabels = RequestsLanguages.Keys.ToArray();
        }

        private void UpdateLocationStatistics()
        {
            RequestsLocations = GetRequestsByLocation();

            foreach (var pair in RequestsLocations)
            {
                ColumnSeries series = new ColumnSeries();
                series.Values = new ChartValues<int> { pair.Value };
                series.Title = $"{pair.Key.city}, {pair.Key.country}";
                LocationsCollection.Add(series);
            }

            LocationLabels = RequestsLocations.Keys.Select(key => new Location { City = key.city, Country = key.country }).ToArray();
        }

        private Dictionary<string, int> GetRequestsByLanguage()
        {
            Dictionary<string, int> requestsByLanguage = new Dictionary<string, int>();

            foreach (var request in TourRequests)
            {
                if (requestsByLanguage.ContainsKey(request.Language))
                {
                    requestsByLanguage[request.Language]++;
                }
                else
                {
                    requestsByLanguage[request.Language] = 1;
                }
            }

            return requestsByLanguage;
        }

        private Dictionary<(string city, string country), int> GetRequestsByLocation()
        {
            Dictionary<(string city, string country), int> requestsByLocation = new Dictionary<(string city, string country), int>();

            foreach (var request in TourRequests)
            {
                var locationKey = (request.City, request.Country);

                if (requestsByLocation.ContainsKey(locationKey))
                {
                    requestsByLocation[locationKey]++;
                }
                else
                {
                    requestsByLocation[locationKey] = 1;
                }
            }

            return requestsByLocation;
        }
    }
}
