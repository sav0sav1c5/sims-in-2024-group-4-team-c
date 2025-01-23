using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.View.GuideView;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using BookingApp.Services.GuideServices;
using BookingApp.DependencyInjection;
using BookingApp.Services;
using System.Windows.Media.Imaging;
using System.IO;

namespace BookingApp.WPF.ViewModels.GuideViewModel
{
    public class CreateTourViewModel : ViewModelBase
    {
        private NavigationService navigationService;

        private readonly CheckpointService checkPointService = DependencyContainer.GetInstance<CheckpointService>();
        private readonly TourService tourService = DependencyContainer.GetInstance<TourService>();
        private readonly LocationService locationService = DependencyContainer.GetInstance<LocationService>();
        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();
        private readonly TourRequestService tourRequestService = DependencyContainer.GetInstance<TourRequestService>();
        public static Guide LoggedGuide { get; set; }
        private RequestStatsItem _mostRequestedLocation;
        public RequestStatsItem MostRequestedLocation
        {
            get { return _mostRequestedLocation; }
            set
            {
                _mostRequestedLocation = value;
                OnPropertyChanged(nameof(MostRequestedLocation));
            }
        }
        private RequestStatsItem _mostRequestedLanguage;
        public RequestStatsItem MostRequestedLanguage
        {
            get { return _mostRequestedLanguage; }
            set
            {
                _mostRequestedLanguage = value;
                OnPropertyChanged(nameof(MostRequestedLanguage));
            }
        }
        private ObservableCollection<BitmapImage> _photos;

        public ObservableCollection<BitmapImage> Photos
        {
            get => _photos;
            set
            {
                _photos = value;
                OnPropertyChanged(nameof(Photos));
            }
        }
        private void AddPhoto(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFile = openFileDialog.FileName;
                BitmapImage bitmap = new BitmapImage(new Uri(selectedFile));
                Photos.Add(bitmap);
            }
        }
        public RelayCommand RemovePhotoCommand { get; private set; }
        private void RemovePhoto(object parameter)
        {
            if (parameter is BitmapImage photo)
            {
                Photos.Remove(photo);
            }
        }


        public string ImagePath { get; set; }

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
                LoadCities();
                LoadCheckpoints();
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
                LoadCheckpoints();
            }
        }

        private ObservableCollection<string> _availableCountries;
        public ObservableCollection<string> AvailableCountries
        {
            get { return _availableCountries; }
            set
            {
                _availableCountries = value;
                OnPropertyChanged(nameof(AvailableCountries));
            }
        }

        private ObservableCollection<string> _availableCities;
        public ObservableCollection<string> AvailableCities
        {
            get { return _availableCities; }
            set
            {
                _availableCities = value;
                OnPropertyChanged(nameof(AvailableCities));
            }
        }

        public string TourName { get; set; }
        public string Description { get; set; }

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

        public int Duration { get; set; }
        public int MaxTouristNumber { get; set; }
        private DateTime _dateAndTime;
        public DateTime DateAndTime
        {
            get => _dateAndTime;
            set
            {
                _dateAndTime = value;
                OnPropertyChanged(nameof(DateAndTime));
            }
        }

        private ObservableCollection<Checkpoint> _selectedCheckpoints { get; set; }
        public ObservableCollection<Checkpoint> SelectedCheckpoints
        {
            get { return _selectedCheckpoints; }
            set
            {
                _selectedCheckpoints = value;
                OnPropertyChanged(nameof(SelectedCheckpoints));
            }
        }

        public ObservableCollection<string> Languages { get; } = new ObservableCollection<string>
        {
            "English",
            "Spanish",
            "Serbian",
            "German"
        };

        public RelayCommand AddImageCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }
        public RelayCommand CreateTourCommand { get; private set; }
        public RelayCommand CreateTourBasedOnStatisticsCommand { get; private set; }
        public RelayCommand AddSelectedCheckpointCommand { get; private set; }

        public CreateTourViewModel(Guide guide, NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoggedGuide = guide;
            SelectedCheckpoints = new ObservableCollection<Checkpoint>();
            Photos = new ObservableCollection<BitmapImage>();
            DateAndTime = DateTime.Now;

            // Initialize commands
            CreateTourCommand = new RelayCommand(CreateTour, CanExecute_Command);
            AddImageCommand = new RelayCommand(AddPhoto);
            BackCommand = new RelayCommand(Back);
            RemovePhotoCommand = new RelayCommand(RemovePhoto);
            CreateTourBasedOnStatisticsCommand = new RelayCommand(CreateTourBasedOnStatistics);
            AddSelectedCheckpointCommand = new RelayCommand(AddSelectedCheckpoint);

            LoadAvailableLocations();
        }

        private void AddSelectedCheckpoint(object obj)
        {
            var selectedCheckpoints = ((ListBox)obj).SelectedItems;
            foreach (var checkpoint in selectedCheckpoints)
            {
                // Add the logic to handle selected checkpoints as needed
                SelectedCheckpoints.Add((Checkpoint)checkpoint);
            }
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void LoadAvailableLocations()
        {
            var locations = locationService.GetAllLocations(); // Assuming GetAllLocations() returns a list of locations
            AvailableCountries = new ObservableCollection<string>(locations.Select(loc => loc.Country).Distinct());
        }

        private void LoadCities()
        {
            if (!string.IsNullOrEmpty(Country))
            {
                var locations = locationService.GetAllLocations(); // Assuming GetAllLocations() returns a list of locations
                AvailableCities = new ObservableCollection<string>(locations.Where(loc => loc.Country == Country).Select(loc => loc.City).Distinct());
            }
            else
            {
                AvailableCities = new ObservableCollection<string>();
            }
        }

        private void LoadCheckpoints()
        {
            if (string.IsNullOrEmpty(Country) || string.IsNullOrEmpty(City))
                return;

            Location? location = locationService.GetAllLocations()
                .FirstOrDefault(l => l.Country == Country && l.City == City);

            if (location != null)
            {
                List<Checkpoint> allCheckpoints = checkPointService.GetCheckpointsByLocationId(location.Id);

                // Filter checkpoints by name to avoid duplicates with different IDs
                List<Checkpoint> uniqueCheckpoints = GetUniqueCheckpoints(allCheckpoints);

                // Set the ItemsSource with the unique checkpoints
                SelectedCheckpoints.Clear();
                foreach (var checkpoint in uniqueCheckpoints)
                {
                    SelectedCheckpoints.Add(checkpoint);
                }
            }
        }

        private List<Checkpoint> GetUniqueCheckpoints(List<Checkpoint> checkpoints)
        {
            Dictionary<string, Checkpoint> uniqueCheckpoints = new Dictionary<string, Checkpoint>();
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (!uniqueCheckpoints.ContainsKey(checkpoint.Name))
                {
                    uniqueCheckpoints.Add(checkpoint.Name, checkpoint);
                }
            }
            return uniqueCheckpoints.Values.ToList();
        }
        private string ConvertImageToBase64(BitmapImage bitmapImage)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        private void CreateTour(object sender)
        {
            if (IsValidTourDetails())
            {
                Location location = GetOrCreateLocation(Country, City);

                List<string> base64Images = Photos.Select(photo => ConvertImageToBase64(photo)).ToList();

                // Join base64 strings into a single string with a delimiter if necessary
                string imagesString = string.Join(";", base64Images);

                Tour newTour = CreateNewTour(TourName, location.Id, Description, Language, Duration, MaxTouristNumber, imagesString);
                Tour savedTour = tourService.SaveTour(newTour);

                TourReservation tourReservation = new TourReservation(savedTour.Id, TourStates.Type.Pending, LoggedGuide.Id, MaxTouristNumber, DateAndTime);
                tourReservationService.SaveTourReservation(tourReservation);

                int tourId = savedTour.Id;
                CreateCheckpointsForTour(tourId);

                MessageBox.Show("Tour created successfully!");
            }
            else
            {
                MessageBox.Show("Please fill in all the details correctly.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool IsValidTourDetails()
        {
            return !string.IsNullOrEmpty(TourName) &&
                   !string.IsNullOrEmpty(Description) &&
                   !string.IsNullOrEmpty(Country) &&
                   !string.IsNullOrEmpty(City) &&
                   !string.IsNullOrEmpty(Language) &&
                   Duration > 0 &&
                   MaxTouristNumber > 0 &&
                   DateAndTime > DateTime.Now &&
                   SelectedCheckpoints.Any();
        }

        private void CreateTourBasedOnStatistics(object sender)
        {
            CalculateMostRequestedLocationAndLanguage();

            Country = MostRequestedLocation.Country;
            City = MostRequestedLocation.City;
            Language = MostRequestedLanguage.Language;

            OnPropertyChanged(nameof(Country));
            OnPropertyChanged(nameof(City));
            OnPropertyChanged(nameof(Language));
        }

        private Location GetOrCreateLocation(string country, string city)
        {
            Location? location = locationService.GetAllLocations()
                .FirstOrDefault(l => l.Country == country && l.City == city);

            if (location == null)
            {
                location = new Location(city, country);
                locationService.SaveLocation(location);
            }

            return location;
        }

        private Tour CreateNewTour(string name, int locationId, string description, string language, int duration, int maxTouristNumber, string imagesString)
        {
            return new Tour()
            {
                Name = name,
                LocationId = locationId,
                Description = description,
                Language = language,
                Duration = duration,
                MaxTouristNumber = maxTouristNumber,
                GuideId = LoggedGuide.Id,
                Images = imagesString
            };
        }

        private void CreateCheckpointsForTour(int tourId)
        {
            foreach (Checkpoint originalCheckpoint in SelectedCheckpoints)
            {
                Checkpoint newCheckpoint = new Checkpoint()
                {
                    Name = originalCheckpoint.Name,
                    LocationId = originalCheckpoint.LocationId,
                    TourId = tourId,
                    IsActive = false
                };
                checkPointService.SaveCheckpoint(newCheckpoint);
            }
        }

        private void AddImage(object sender)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    string selectedImagePath = openFileDialog.FileName;

                    // Save the selected image path to the ImagePath variable
                    ImagePath = selectedImagePath;

                    MessageBox.Show("Image added successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding image: {ex.Message}");
            }
        }

        private void CalculateMostRequestedLocationAndLanguage()
        {
            var today = DateTime.Today;
            var yearAgo = today.AddYears(-1);

            List<TourRequest> TourRequests = tourRequestService.GetAllTourRequests();
            var filteredRequests = TourRequests.Where(tr =>
                tr.StartDate >= yearAgo
            );

            var locationStats = filteredRequests
                .GroupBy(tr => new { tr.Country, tr.City })
                .Select(g => new RequestStatsItem
                {
                    Country = g.Key.Country,
                    City = g.Key.City,
                    Count = g.Count()
                })
                .OrderByDescending(stat => stat.Count)
                .FirstOrDefault();

            MostRequestedLocation = locationStats; // Update the property with the most requested location

            var languageStats = filteredRequests
                .GroupBy(tr => tr.Language)
                .Select(g => new RequestStatsItem
                {
                    Language = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(stat => stat.Count)
                .FirstOrDefault();

            MostRequestedLanguage = languageStats; // Update the property with the most requested language

            OnPropertyChanged(nameof(MostRequestedLocation));
            OnPropertyChanged(nameof(MostRequestedLanguage));
        }

        private void Back(object sender)
        {
            Page GuideHomeView = new GuideHomeView(LoggedGuide, navigationService);
            navigationService.Navigate(GuideHomeView);
        }
    }

    public class RequestStatsItem
    {
        public string TourName { get; set; }
        public string Country { get; internal set; }
        public string City { get; internal set; }
        public string Language { get; internal set; }
        public string Year { get; internal set; }
        public int Count { get; internal set; }
    }
}
