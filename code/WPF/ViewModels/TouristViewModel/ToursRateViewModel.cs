using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Resources.DBAccess;
using BookingApp.Services.GuideServices;
using BookingApp.View;
using BookingApp.View.TouristView;
using BookingApp.WPF.Converter;
using BookingApp.WPF.Views.TouristView;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.TouristViewModel
{
    public class ToursRateViewModel : ViewModelBase
    {
        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();
        private readonly TourAttendanceService tourAttendanceService = DependencyContainer.GetInstance<TourAttendanceService>();
        private readonly TourReviewService tourReviewService = DependencyContainer.GetInstance<TourReviewService>();
        private readonly LocationService locationService = DependencyContainer.GetInstance<LocationService>();
        private readonly TourService tourService = DependencyContainer.GetInstance<TourService>();
        public ObservableCollection<TourReservation> TourReservations { get; set; } = new ObservableCollection<TourReservation>();
        public ObservableCollection<TourAttendance> TourAttendances { get; set; } = new ObservableCollection<TourAttendance>();
        public ObservableCollection<int> Grades { get; set; } = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
        public ObservableCollection<Location> Locations { get; set; } = new ObservableCollection<Location>();
        public ObservableCollection<TourDTO> Tours { get; set; } = new ObservableCollection<TourDTO>();
        public List<TourReview> TourReviews = new List<TourReview>();
        public TourAttendance SelectedAttendance { get; set; } = new TourAttendance();
        public string TourName { get; set; }
        public string TourDescription { get; set; }
        public int KnowledgeRate { get; set; }
        public int LanguageRate { get; set; }
        public int InterestingnessRate { get; set; }
        public string ImagePath { get; set; }
        public string ProfileButtonContent { get; set; }
        public RelayCommand ToursCommand { get; private set; }
        public RelayCommand RequestsCommand { get; private set; }
        public RelayCommand StatisticsCommand { get; private set; }
        public RelayCommand RateToursCommand { get; private set; }
        public RelayCommand TutorialCommand { get; private set; }
        public RelayCommand NotificationsCommand { get; private set; }
        public RelayCommand ProfileCommand { get; private set; }
        public RelayCommand LogOutCommand { get; private set; }
        public RelayCommand RateCommand { get; private set; }
        public RelayCommand UploadImageCommand { get; private set; }
        public RelayCommand RateTourCommand { get; private set; }
        public NavigationService NavigationService { get; set; }
        private readonly Tourist LoggedTourist;


        public ToursRateViewModel(Tourist tourist, NavigationService _navigationService)
        {
            ToursCommand = new RelayCommand(ToursHome);
            RequestsCommand = new RelayCommand(Requests);
            StatisticsCommand = new RelayCommand(Statistics);
            RateToursCommand = new RelayCommand(RateTours);
            TutorialCommand = new RelayCommand(Tutorial);
            NotificationsCommand = new RelayCommand(Notifications);
            ProfileCommand = new RelayCommand(ViewProfile);
            LogOutCommand = new RelayCommand(_ => LogOut());
            RateCommand = new RelayCommand(Rate);
            UploadImageCommand = new RelayCommand(UploadImage);
            RateTourCommand = new RelayCommand(RateTour);
            NavigationService = _navigationService;
            LoggedTourist = tourist;
            LoggedTouristInfo(tourist);
            TourReviews = tourReviewService.GetAllTourReviews();
            LoadReservationsAndTours();
            LoadInformations();
            KnowledgeRating = 1;
            LanguageRating = 1;
            InterestingnessRating = 1;
        }

        private string descriptionBox;
        public string DescriptionBox
        {
            get { return descriptionBox; }
            set
            {
                descriptionBox = value;
                OnPropertyChanged(nameof(DescriptionBox));
            }
        }

        private int _knowledgeRating;
        public int KnowledgeRating
        {
            get { return _knowledgeRating; }
            set
            {
                _knowledgeRating = value;
                OnPropertyChanged(nameof(KnowledgeRating));
            }
        }

        private int _languageRating;
        public int LanguageRating
        {
            get { return _languageRating; }
            set
            {
                _languageRating = value;
                OnPropertyChanged(nameof(LanguageRating));
            }
        }

        private int _interestingnessRating;
        public int InterestingnessRating
        {
            get { return _interestingnessRating; }
            set
            {
                _interestingnessRating = value;
                OnPropertyChanged(nameof(InterestingnessRating));
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

        private void Rate(object parameter)
        {
            if (parameter is TourAttendance selectedAttendance)
            {
                string tourName = selectedAttendance.TourReservation?.Tour?.Name;
                TourName = tourName;
                SelectedAttendance = tourAttendanceService.GetTourAttendanceById(selectedAttendance.Id);

                if (!string.IsNullOrEmpty(TourName))
                {
                    if (IsAlreadyRated(SelectedAttendance))
                    {
                        MessageBox.Show($"Review already created for tour {tourName}.\n\nDetails:\n" +
                                        $"Tour Description: {TourDescription}\n" +
                                        $"Knowledge Rate: {KnowledgeRate}\n" +
                                        $"Language Rate: {LanguageRate}\n" +
                                        $"Interestingness Rate: {InterestingnessRate}",
                                        "Review already created");
                    }
                    else
                    {
                        MessageBox.Show($"You can insert review information for tour: {tourName}.");
                    }
                }
                else
                {
                    MessageBox.Show("Error: Selected tour name is not available.");
                }
            }
            else
            {
                MessageBox.Show("Error: Unable to retrieve selected tour information.");
            }
        }

        private bool IsAlreadyRated(TourAttendance attendance)
        {
            List<TourReview> tourReviews = tourReviewService.GetAllTourReviews();

            foreach (var review in tourReviews)
            {
                if (review.TourAttendanceId == attendance.Id)
                {
                    TourDescription = review.Comment;
                    KnowledgeRate = review.Knowledge;
                    LanguageRate = review.Language;
                    InterestingnessRate = review.Interestingness;
                    return true;
                }
            }

            return false;
        }


        private void UploadImage(object parameter)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    string selectedImagePath = openFileDialog.FileName;
                    ImagePath = selectedImagePath;
                    MessageBox.Show("Image added successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding image: {ex.Message}");
            }
        }

        private void RateTour(object parameter)
        {
            using (var context = new DataContext())
            {
                TourReview tourReview = new TourReview(KnowledgeRating, LanguageRating, InterestingnessRating, descriptionBox, ImagePath, SelectedAttendance.TourReservation.GuideId, SelectedAttendance.TourReservationId, SelectedAttendance.Id);
                context.TourReviews.Add(tourReview);
                context.SaveChanges();
                TourReviews.Add(tourReview);
            }

            MessageBox.Show($"Review for tour {TourName} is successfully created.");
            DescriptionBox = "";
            ImagePath = "";
            KnowledgeRating = 1;
            LanguageRating = 1;
            InterestingnessRating = 1;
        }

        private void LoggedTouristInfo(Tourist tourist)
        {
            TouristDTO LoggedTouristDTO = new TouristDTO(tourist);
            ProfileButtonContent = $"{LoggedTouristDTO.FirstName} {LoggedTouristDTO.LastName}";

        }

        private void LoadReservationsAndTours()
        {
            var allTourReservations = tourReservationService.GetAllTourReservations().Select(tr => new TourReservationDTO(tr)).ToList();
            var allTours = tourService.GetAllTours().Select(t => new TourDTO(t)).ToList();
            List<TourReservation> tourReservations = tourReservationService.GetAllTourReservations();
            List<Tour> tours = tourService.GetAllTours();

            foreach (var reservation in tourReservations)
            {
                foreach (var tour in tours)
                {
                    if (tour.Id == reservation.TourId)
                    {
                        reservation.Tour = tour;
                        Location location = locationService.GetLocationById(tour.Id);
                        tour.Location = location;
                    }
                }
                TourReservations.Add(reservation);
            }
        }

        private void LoadTourAttendances()
        {
            List<TourAttendance> touristAttendances = tourAttendanceService.GetAllTourAttendances();

            foreach (var attendance in touristAttendances)
            {
                if (attendance.TouristId == LoggedTourist.Id)
                {
                    TourAttendances.Add(attendance);
                }
            }

            LoadReservationsAndTours();
        }

        private void LoadInformations()
        {
            LoadTourAttendances();

            foreach (var attendance in TourAttendances)
            {
                foreach (var reservation in TourReservations)
                {
                    if (attendance.TourReservationId == reservation.Id)
                    {
                        attendance.TourReservation = reservation;
                    }
                }
            }
        }
    }
}
