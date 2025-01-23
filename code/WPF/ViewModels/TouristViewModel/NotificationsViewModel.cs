using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuideServices;
using BookingApp.View.TouristView;
using BookingApp.WPF.Views.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;
using BookingApp.View;
using System.Security.Cryptography.X509Certificates;
using BookingApp.Resources.DBAccess;

namespace BookingApp.WPF.ViewModels.TouristViewModel
{
    public class NotificationsViewModel : ViewModelBase
    {
        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();
        private readonly TourAttendanceService tourAttendanceService = DependencyContainer.GetInstance<TourAttendanceService>();
        private readonly TourRequestService tourRequestService = DependencyContainer.GetInstance<TourRequestService>();
        private readonly LocationService locationService = DependencyContainer.GetInstance<LocationService>();
        private readonly TourService tourService = DependencyContainer.GetInstance<TourService>();
        public ObservableCollection<TourAttendanceDTO> TourAttendances { get; set; } = new ObservableCollection<TourAttendanceDTO>();
        public ObservableCollection<Tour> NewTours { get; set; } = new ObservableCollection<Tour>();
        public List<Tour> Tours { get; set; } = new List<Tour>();
        public List<TourRequest> TourRequests { get; set; } = new List<TourRequest>();
        public RelayCommand ToursCommand { get; private set; }
        public RelayCommand RequestsCommand { get; private set; }
        public RelayCommand StatisticsCommand { get; private set; }
        public RelayCommand RateToursCommand { get; private set; }
        public RelayCommand ProfileCommand { get; private set; }
        public RelayCommand LogOutCommand { get; private set; }
        public RelayCommand BackToHomeCommand { get; private set; }
        public RelayCommand ConfirmAttendanceCommand { get; private set; }
        public RelayCommand DeclineAttendanceCommand { get; private set; }
        public RelayCommand DetailsCommand { get; private set; }
        public NavigationService NavigationService { get; set; }
        private readonly Tourist LoggedTourist;

        public NotificationsViewModel(Tourist tourist, NavigationService _navigationService)
        {
            ToursCommand = new RelayCommand(ToursHome);
            RequestsCommand = new RelayCommand(Requests);
            StatisticsCommand = new RelayCommand(Statistics);
            RateToursCommand = new RelayCommand(RateTours);
            ProfileCommand = new RelayCommand(ViewProfile);
            LogOutCommand = new RelayCommand(_ => LogOut());
            BackToHomeCommand = new RelayCommand(BackToHome);
            ConfirmAttendanceCommand = new RelayCommand(ConfirmAttendance);
            DeclineAttendanceCommand = new RelayCommand(DeclineAttendance);
            DetailsCommand = new RelayCommand(Details);
            NavigationService = _navigationService; 
            LoggedTourist = tourist;
            LoadTourAttendances();
            LoadInformations();
            LoadNewTours();
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

        private void ConfirmAttendance(object parameter)
        {
            if (parameter is TourAttendanceDTO tourAttendance)
            {
                tourAttendance.IsConfirmed = true;
                TourAttendance originalAttendance = tourAttendanceService.GetTourAttendanceById(tourAttendance.Id);
                originalAttendance.IsConfirmed = true;
                
                using (var context = new DataContext())
                {
                    context.Update(originalAttendance);
                    context.SaveChanges();
                }

                MessageBox.Show("Tour attendance accepted.");
                TourAttendances.Remove(tourAttendance);
            }
        }

        private void DeclineAttendance(object parameter)
        {
            if (parameter is TourAttendanceDTO tourAttendance)
            {
                tourAttendance.IsConfirmed = false;
                TourAttendance originalAttendance = tourAttendanceService.GetTourAttendanceById(tourAttendance.Id);
                originalAttendance.IsConfirmed = false;

                using (var context = new DataContext())
                {
                    context.Update(originalAttendance);
                    context.SaveChanges();
                }

                MessageBox.Show("Tour attendance declined.");
                TourAttendances.Remove(tourAttendance);
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

        private void LoadAttendanceDTOs(List<TourAttendanceDTO> tourAttendanceDTOs, List<TourReservation> tourReservations, List<Tour> tours)
        {
            foreach (var attendance in tourAttendanceDTOs)
            {
                if (attendance.IsPresent == true && attendance.IsConfirmed == false && attendance.TouristId == LoggedTourist.Id)
                {
                    foreach (var reservation in tourReservations)
                    {
                        if (reservation.Id == attendance.TourReservationId)
                        {
                            attendance.TourReservation = reservation;

                            foreach (var tour in tours)
                            {
                                if (reservation.TourId == tour.Id)
                                {
                                    reservation.Tour = tour;
                                }
                            }
                        }
                    }
                    TourAttendances.Add(attendance);
                }
            }
        }

        private void LoadTourAttendances()
        {
            List<TourAttendanceDTO> tourAttendanceDTOs = tourAttendanceService.GetAllTourAttendances().Select(ta => new TourAttendanceDTO(ta)).ToList();
            List<TourReservation> tourReservations = tourReservationService.GetAllTourReservations();
            List<Tour> tours = tourService.GetAllTours();

            LoadAttendanceDTOs(tourAttendanceDTOs, tourReservations, tours);
        }

        private void LoadInformations()
        {
            TourRequests = tourRequestService.GetDeclinedTourRequestsByTouristId(LoggedTourist.Id);
            Tours = tourService.GetAllTours();

            foreach (var tour in Tours)
            {
                Location location = locationService.GetLocationById(tour.LocationId);
                tour.Location = location;
            }
        }

        private void LoadNewTours()
        {
            foreach (var tour in Tours)
            {
                foreach (var request in TourRequests)
                {
                    var firstReservation = tourReservationService.FindFirstReservation(tour.Id);
                    if (firstReservation != null && firstReservation.DateAndTime.Date > request.EndDate.AddHours(-48))
                    {
                        if ((request.Language != null && tour.Language != null && request.Language.Equals(tour.Language)))
                        {

                            NewTours.Add(tour);
                            break;
                        }
                        if((request.City != null && request.Country != null && tour.Location != null && request.City.Equals(tour.Location.City) && request.Country.Equals(tour.Location.Country)))
                        {
                            NewTours.Add(tour);
                            break;
                        }
                    }
                }
            }
        }
    }
}
