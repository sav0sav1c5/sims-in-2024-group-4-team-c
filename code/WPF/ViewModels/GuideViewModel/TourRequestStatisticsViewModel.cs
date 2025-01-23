using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuideServices;
using BookingApp.View.GuideView;
using BookingApp.WPF.Views.GuideView;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuideViewModel
{
    public class TourRequestStatisticsViewModel : ViewModelBase
    {
        public NavigationService navigationService { get; set; }

        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();
        private readonly TourParticipantService tourParticipantService = DependencyContainer.GetInstance<TourParticipantService>();
        private readonly TourAttendanceService tourAttendanceService = DependencyContainer.GetInstance<TourAttendanceService>();
        private readonly TourService tourService = DependencyContainer.GetInstance<TourService>();
        private readonly TourRequestService tourRequestService = DependencyContainer.GetInstance<TourRequestService>();
        private readonly LocationService locationService = DependencyContainer.GetInstance<LocationService>();
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; } = new ObservableCollection<TourRequestDTO>();
        public ObservableCollection<TourRequestStatsItem> LocationStats { get; set; }
        public ObservableCollection<TourRequestStatsItem> LanguageStats { get; set; }
        public ObservableCollection<TourRequestStatsItem> MonthlyTourRequestStats { get; set; }
        public ObservableCollection<TourRequestStatsItem> MonthlyLanguageStats { get; set; }
        public ObservableCollection<TourRequestStatsItem> MonthlyLocationStats { get; set; }

        public ObservableCollection<TourRequestStatsItem> YearlyTourRequestStats { get; set; }
        public ObservableCollection<TourRequestStatsItem> YearlyLocationStats { get; set; }
        public ObservableCollection<TourRequestStatsItem> YearlyLanguageStats { get; set; }
        public ObservableCollection<TourRequestStatsItem> YearlyStats { get; set; }
        // Properties for YearlySeriesCollection, YearlyLabels, and YearlyFormatter
        private SeriesCollection _monthlyLocationSeriesCollection;
        public SeriesCollection MonthlyLocationSeriesCollection
        {
            get { return _monthlyLocationSeriesCollection; }
            set
            {
                _monthlyLocationSeriesCollection = value;
                OnPropertyChanged(nameof(MonthlyLocationSeriesCollection));
            }
        }

        private string[] _monthlyLocationLabels;
        public string[] MonthlyLocationLabels
        {
            get { return _monthlyLocationLabels; }
            set
            {
                _monthlyLocationLabels = value;
                OnPropertyChanged(nameof(MonthlyLocationLabels));
            }
        }

        private Func<double, string> _monthlyLocationFormatter;
        public Func<double, string> MonthlyLocationFormatter
        {
            get { return _monthlyLocationFormatter; }
            set
            {
                _monthlyLocationFormatter = value;
                OnPropertyChanged(nameof(MonthlyLocationFormatter));
            }
        }
        private SeriesCollection _yearlySeriesCollection;
        public SeriesCollection YearlySeriesCollection
        {
            get { return _yearlySeriesCollection; }
            set
            {
                _yearlySeriesCollection = value;
                OnPropertyChanged(nameof(YearlySeriesCollection));
            }
        }

        private string[] _yearlyLabels;
        public string[] YearlyLabels
        {
            get { return _yearlyLabels; }
            set
            {
                _yearlyLabels = value;
                OnPropertyChanged(nameof(YearlyLabels));
            }
        }

        private Func<double, string> _yearlyFormatter;
        public Func<double, string> YearlyFormatter
        {
            get { return _yearlyFormatter; }
            set
            {
                _yearlyFormatter = value;
                OnPropertyChanged(nameof(YearlyFormatter));
            }
        }
        // Property for SeriesCollection
        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection = value;
                OnPropertyChanged(nameof(SeriesCollection));
            }
        }
        private string[] _labels;
        public string[] Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }

        private Func<double, string> _formatter;
        public Func<double, string> Formatter
        {
            get { return _formatter; }
            set
            {
                _formatter = value;
                OnPropertyChanged(nameof(Formatter));
            }
        }

        private Axis _yearlyStatsAxisX;
        public Axis YearlyStatsAxisX
        {
            get => _yearlyStatsAxisX;
            set
            {
                _yearlyStatsAxisX = value;
                OnPropertyChanged(nameof(YearlyStatsAxisX));
            }
        }

        private Axis _yearlyStatsAxisY;
        public Axis YearlyStatsAxisY
        {
            get => _yearlyStatsAxisY;
            set
            {
                _yearlyStatsAxisY = value;
                OnPropertyChanged(nameof(YearlyStatsAxisY));
            }
        }


        public static Guide LoggedGuide { get; set; }
        private int? _selectedYear;
        public int? SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                _selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
                CalculateStatistics();
            }
        }

        public ObservableCollection<int> AvailableYears { get; set; }

        private string selectedCountry;
        public string SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
                UpdateCities(selectedCountry);
                CalculateStatistics();
            }
        }
        private string selectedCity;
        public string SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
                CalculateStatistics();
            }
        }
        private string selectedLanguage;
        public string SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
                CalculateStatistics();
            }
        }
        private ObservableCollection<string> _availableCountries;
        public ObservableCollection<string> AvailableCountries
        {
            get { return _availableCountries; }
            set { _availableCountries = value; OnPropertyChanged(nameof(AvailableCountries)); }
        }

        private ObservableCollection<string> _availableCities;
        public ObservableCollection<string> AvailableCities
        {
            get { return _availableCities; }
            set { _availableCities = value; OnPropertyChanged(nameof(AvailableCities)); }
        }

        private ObservableCollection<string> _availableLanguages;
        public ObservableCollection<string> AvailableLanguages
        {
            get { return _availableLanguages; }
            set { _availableLanguages = value; OnPropertyChanged(nameof(AvailableLanguages)); }
        }
        public RelayCommand BackCommand { get; }
        public RelayCommand TourSelectedCommand { get; }
        public RelayCommand CalculateStatisticsCommand { get; set; }
        public ObservableCollection<TourRequestStatsItem> TourRequestStats { get; private set; }

        public TourRequestStatisticsViewModel(Guide guide, NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoggedGuide = guide;
            AvailableYears = new ObservableCollection<int>(Enumerable.Range(2017, 8));

            // Initialize collections
            MonthlyLanguageStats = new ObservableCollection<TourRequestStatsItem>();
            MonthlyLocationStats = new ObservableCollection<TourRequestStatsItem>();
            YearlyLanguageStats = new ObservableCollection<TourRequestStatsItem>();
            YearlyLocationStats = new ObservableCollection<TourRequestStatsItem>();

            InitializeAxes(); // Ensure InitializeAxes is called early

            TourRequests = GetGuideTourRequests();
            LoadAvailableLanguages();
            LoadAvailableLocations();
            CalculateStatistics();

            BackCommand = new RelayCommand(NavigateBack);
            CalculateStatisticsCommand = new RelayCommand(_ => CalculateStatistics());
        }

        private void InitializeAxes()
        {
            // Initialize your axes here
            YearlyStatsAxisX = new Axis
            {
                Title = "Year",
                Labels = YearlyLanguageStats.Select(stat => stat.Year).ToList()
            };

            YearlyStatsAxisY = new Axis
            {
                Title = "Request Count",
                LabelFormatter = value => value.ToString() // You can format the labels as needed
            };

            OnPropertyChanged(nameof(YearlyStatsAxisX));
            OnPropertyChanged(nameof(YearlyStatsAxisY));
        }
        private void CalculateStatistics()
        {
            if (SelectedYear.HasValue)
            {
                // Year is selected
                int year = SelectedYear.Value;
                if (year > 0) // Assuming 0 is not a valid year
                {
                    if(SelectedCity != null && SelectedCountry != null && SelectedLanguage != null)
                    {
                        CalculateMonthlyStatisticsForLanguage(year);
                        CalculateMonthlyStatisticsForLocation(year);
                    }
                    else if(SelectedCity != null || SelectedCountry != null)
                    {
                        CalculateMonthlyStatisticsForLocation(year);
                    }
                    else if(SelectedLanguage != null)
                    {
                        CalculateMonthlyStatisticsForLanguage(year);
                    }
                    else
                    {
                        CalculateMonthlyStatistics(year);
                    }
                }
                else
                {
                    CalculateLast5YearsStatistics();
                }
            }
            else
            {
                if (SelectedCity != null && SelectedCountry != null && SelectedLanguage != null)
                {
                    CalculateLast5YearsStatisticsForLanguage();
                    CalculateLast5YearsStatisticsForLocation();
                }
                else if (SelectedCity != null || SelectedCountry != null)
                {
                    CalculateLast5YearsStatisticsForLocation();
                }
                else if (SelectedLanguage != null)
                {
                    CalculateLast5YearsStatisticsForLanguage();
                }
                else
                {
                    CalculateLast5YearsStatistics();
                }
            }
        }
        private void CalculateMonthlyStatisticsForLanguage(int year)
        {
            var filteredRequests = TourRequests.Where(tr =>
                tr.StartDate.Year == year &&
                (string.IsNullOrEmpty(SelectedLanguage) || tr.Language == SelectedLanguage)
            );

            var monthlyStats = new List<TourRequestStatsItem>();

            // Loop through each month and calculate the statistics
            for (int month = 1; month <= 12; month++)
            { 
                var requestsInMonth = filteredRequests.Where(tr => tr.StartDate.Month == month);
                monthlyStats.Add(new TourRequestStatsItem
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month),
                    Count = requestsInMonth.Count()
                });
            }

            // Assign the calculated statistics to the TourRequestStats property
            MonthlyLanguageStats = new ObservableCollection<TourRequestStatsItem>(monthlyStats);
            UpdateColumnChart(monthlyStats);

            // Notify property change for binding
            OnPropertyChanged(nameof(MonthlyLanguageStats));
        }

        private void CalculateYearlyStatisticsForLanguage()
        {
            var filteredRequests = TourRequests.Where(tr =>
                (string.IsNullOrEmpty(SelectedLanguage) || tr.Language == SelectedLanguage)
            );

            var yearlyStats = new List<TourRequestStatsItem>();

            // Group requests by year and calculate the statistics
            var groupedByYear = filteredRequests.GroupBy(tr => tr.StartDate.Year);

            foreach (var group in groupedByYear)
            {
                yearlyStats.Add(new TourRequestStatsItem
                {
                    Year = group.Key.ToString(),
                    Count = group.Count()
                });
            }

            // Update the ColumnChart series for yearly statistics
            UpdateYearlyColumnChart(yearlyStats);
        }
        private void CalculateMonthlyStatisticsForLocation(int year)
        {
            var filteredRequests = TourRequests.Where(tr =>
                tr.StartDate.Year == year &&
                (string.IsNullOrEmpty(SelectedCountry) || tr.Country == SelectedCountry) &&
                (string.IsNullOrEmpty(SelectedCity) || tr.City == SelectedCity) 
            );

            var monthlyStats = new List<TourRequestStatsItem>();

            // Loop through each month and calculate the statistics
            for (int month = 1; month <= 12; month++)
            {
                var requestsInMonth = filteredRequests.Where(tr => tr.StartDate.Month == month);
                monthlyStats.Add(new TourRequestStatsItem
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month),
                    Count = requestsInMonth.Count()
                });
            }

            // Assign the calculated statistics to the TourRequestStats property
            MonthlyLocationStats = new ObservableCollection<TourRequestStatsItem>(monthlyStats);
            UpdateMonthlyLocationChart(monthlyStats);
            // Notify property change for binding
            OnPropertyChanged(nameof(MonthlyLocationStats));
        }

        private void CalculateMonthlyStatistics(int year)
        {
            var filteredRequests = TourRequests.Where(tr =>
                tr.StartDate.Year == year 
            );

            var monthlyStats = new List<TourRequestStatsItem>();

            // Loop through each month and calculate the statistics
            for (int month = 1; month <= 12; month++)
            {
                var requestsInMonth = filteredRequests.Where(tr => tr.StartDate.Month == month);
                monthlyStats.Add(new TourRequestStatsItem
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month),
                    Count = requestsInMonth.Count()
                });
            }

            // Assign the calculated statistics to the TourRequestStats property
            MonthlyTourRequestStats = new ObservableCollection<TourRequestStatsItem>(monthlyStats);
            UpdateColumnChart(monthlyStats);
            // Notify property change for binding
            OnPropertyChanged(nameof(MonthlyTourRequestStats));
        }


        private void CalculateLast5YearsStatistics()
        {
            var today = DateTime.Today;
            var fiveYearsAgo = today.AddYears(-5);

            var filteredRequests = TourRequests.Where(tr =>
                tr.StartDate >= fiveYearsAgo &&
                (string.IsNullOrEmpty(SelectedCountry) || tr.Country == SelectedCountry) &&
                (string.IsNullOrEmpty(SelectedCity) || tr.City == SelectedCity) &&
                (string.IsNullOrEmpty(SelectedLanguage) || tr.Language == SelectedLanguage) &&
                (tr.TourRequestState == TourRequestStates.Type.Pending)
            );

            var yearlyStats = filteredRequests
                .GroupBy(tr => tr.StartDate.Year)
                .Select(g => new TourRequestStatsItem
                {
                    Year = g.Key.ToString(),
                    Count = g.Count()
                })
                .ToList();

            // Ensure all years within the last 5 years are included, even if they have zero requests
            var last5Years = Enumerable.Range(today.Year - 5, 5);
            foreach (int year in last5Years)
            {
                var yearString = year.ToString();
                if (!yearlyStats.Any(stat => stat.Year == yearString))
                {
                    yearlyStats.Add(new TourRequestStatsItem
                    {
                        Year = yearString,
                        Count = 0 // Assume 0 count for missing stats
                    });
                }
            }

            // Sort the stats by year
            yearlyStats = yearlyStats.OrderBy(stat => int.Parse(stat.Year)).ToList();

            YearlyTourRequestStats = new ObservableCollection<TourRequestStatsItem>(yearlyStats);
            YearlyLocationStats = new ObservableCollection<TourRequestStatsItem>(yearlyStats);
            YearlyLanguageStats = new ObservableCollection<TourRequestStatsItem>(yearlyStats);

            UpdateYearlyColumnChart(yearlyStats);
            OnPropertyChanged(nameof(YearlyTourRequestStats));
        }
        private void CalculateLast5YearsStatisticsForLanguage()
        {
            var today = DateTime.Today;
            var fiveYearsAgo = today.AddYears(-5);

            var filteredRequests = TourRequests.Where(tr =>
                tr.StartDate >= fiveYearsAgo &&
                (string.IsNullOrEmpty(SelectedLanguage) || tr.Language == SelectedLanguage)
            );

            var yearlyStats = filteredRequests
                .GroupBy(tr => tr.StartDate.Year)
                .Select(g => new TourRequestStatsItem
                {
                    Year = g.Key.ToString(),
                    Count = g.Count()
                })
                .ToList();

            // Ensure all years within the last 5 years are included, even if they have zero requests
            var last5Years = Enumerable.Range(today.Year - 5, 5);
            foreach (int year in last5Years)
            {
                var yearString = year.ToString();
                if (!yearlyStats.Any(stat => stat.Year == yearString))
                {
                    yearlyStats.Add(new TourRequestStatsItem
                    {
                        Year = yearString,
                        Count = 0 // Assume 0 count for missing stats
                    });
                }
            }

            // Sort the stats by year
            yearlyStats = yearlyStats.OrderBy(stat => int.Parse(stat.Year)).ToList();

            YearlyLanguageStats = new ObservableCollection<TourRequestStatsItem>(yearlyStats);
            UpdateYearlyColumnChart(yearlyStats);
            OnPropertyChanged(nameof(YearlyLanguageStats));
        }
        private void CalculateLast5YearsStatisticsForLocation()
        {
            var today = DateTime.Today;
            var fiveYearsAgo = today.AddYears(-5);

            var filteredRequests = TourRequests.Where(tr =>
                tr.StartDate >= fiveYearsAgo &&
                (string.IsNullOrEmpty(SelectedCountry) || tr.Country == SelectedCountry) &&
                (string.IsNullOrEmpty(SelectedCity) || tr.City == SelectedCity)
            );

            var yearlyStats = filteredRequests
                .GroupBy(tr => tr.StartDate.Year)
                .Select(g => new TourRequestStatsItem
                {
                    Year = g.Key.ToString(),
                    Count = g.Count()
                })
                .ToList();

            // Ensure all years within the last 5 years are included, even if they have zero requests
            var last5Years = Enumerable.Range(today.Year - 5, 5);
            foreach (int year in last5Years)
            {
                var yearString = year.ToString();
                if (!yearlyStats.Any(stat => stat.Year == yearString))
                {
                    yearlyStats.Add(new TourRequestStatsItem
                    {
                        Year = yearString,
                        Count = 0 // Assume 0 count for missing stats
                    });
                }
            }

            // Sort the stats by year
            yearlyStats = yearlyStats.OrderBy(stat => int.Parse(stat.Year)).ToList();

            YearlyLocationStats = new ObservableCollection<TourRequestStatsItem>(yearlyStats);
            UpdateYearlyColumnChart(yearlyStats);
            OnPropertyChanged(nameof(YearlyLocationStats));
        }

        private void LoadAvailableLocations()
        {
            var locations = locationService.GetAllLocations(); // Assuming GetAllLocations() returns a list of locations
            AvailableCountries = new ObservableCollection<string>(locations.Select(loc => loc.Country).Distinct());
            AvailableCities = new ObservableCollection<string>(locations.Select(loc => loc.City).Distinct());
        }
        private void LoadAvailableLanguages()
        {
            var tours = tourService.GetAllTours();
            AvailableLanguages = new ObservableCollection<string>(tours.Select(tour => tour.Language).Distinct());
        }
        private void UpdateCities(string selectedCountry)
        {
            var locations = locationService.GetAllLocations(); // Assuming GetAllLocations() returns a list of locations
            AvailableCities = new ObservableCollection<string>(
                locations.Where(loc => loc.Country == selectedCountry).Select(loc => loc.City).Distinct()
            );
        }

        private ObservableCollection<TourRequestDTO> GetGuideTourRequests()
        {
            List<TourRequest> tourRequests = tourRequestService.GetAllTourRequests();
            foreach (var tourRequest in tourRequests)
            {
                TourRequestDTO tourRequestDTO = new TourRequestDTO(tourRequest);
                TourRequests.Add(tourRequestDTO);
            }
            return TourRequests;
        }
        private void NavigateBack(object sender)
        {
            GuideProfileView guideProfile = new GuideProfileView(LoggedGuide, navigationService);
            this.navigationService.Navigate(guideProfile);
        }
        private void UpdateColumnChart(List<TourRequestStatsItem> monthlyStats)
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Requests",
                    Values = new ChartValues<int>(monthlyStats.Select(ms => ms.Count)),
                    DataLabels = true
                }
            };

            Labels = monthlyStats.Select(ms => ms.Month).ToArray();
            Formatter = value => value.ToString("N");

            OnPropertyChanged(nameof(SeriesCollection));
            OnPropertyChanged(nameof(Labels));
            OnPropertyChanged(nameof(Formatter));
        }
        private void UpdateYearlyColumnChart(List<TourRequestStatsItem> yearlyStats)
        {
            YearlySeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Requests",
                    Values = new ChartValues<int>(yearlyStats.Select(ys => ys.Count)),
                    DataLabels = true
                }
            };

            YearlyLabels = yearlyStats.Select(ys => ys.Year).ToArray();
            YearlyFormatter = value => value.ToString("N");

            OnPropertyChanged(nameof(YearlySeriesCollection));
            OnPropertyChanged(nameof(YearlyLabels));
            OnPropertyChanged(nameof(YearlyFormatter));
        }
        private void UpdateMonthlyLocationChart(List<TourRequestStatsItem> monthlyLocationStats)
        {
            MonthlyLocationSeriesCollection = new SeriesCollection
    {
        new ColumnSeries
        {
            Title = "Requests",
            Values = new ChartValues<int>(monthlyLocationStats.Select(ms => ms.Count)),
            DataLabels = true
        }
    };

            MonthlyLocationLabels = monthlyLocationStats.Select(ms => ms.Month).ToArray();
            MonthlyLocationFormatter = value => value.ToString("N");

            OnPropertyChanged(nameof(MonthlyLocationSeriesCollection));
            OnPropertyChanged(nameof(MonthlyLocationLabels));
            OnPropertyChanged(nameof(MonthlyLocationFormatter));
        }

    }
    public class TourRequestStatsItem
    {
        public string TourName { get; set; }
        public string Month { get; internal set; }
        public int Count { get; internal set; }
        public string Year { get; internal set; }
        public string Language { get; internal set; }
    }
}

