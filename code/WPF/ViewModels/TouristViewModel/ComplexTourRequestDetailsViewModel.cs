using BookingApp.Domain.Model;
using BookingApp.DTOs;
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
using BookingApp.Services.GuideServices;
using BookingApp.DependencyInjection;
using System.Collections.ObjectModel;
using BookingApp.Services;

namespace BookingApp.WPF.ViewModels.TouristViewModel
{
    public class ComplexTourRequestDetailsViewModel : ViewModelBase
    {
        private readonly ComplexTourPartService complexTourPartService = DependencyContainer.GetInstance<ComplexTourPartService>();
        public ObservableCollection<ComplexTourPartDTO> ComplexTourParts { get; set; } = new ObservableCollection<ComplexTourPartDTO>();
        public ComplexTourRequestDTO ComplexTourRequestDTO { get; set; }
        public RelayCommand ToursCommand { get; private set; }
        public RelayCommand RequestsCommand { get; private set; }
        public RelayCommand StatisticsCommand { get; private set; }
        public RelayCommand RateToursCommand { get; private set; }
        public RelayCommand TutorialCommand { get; private set; }
        public RelayCommand NotificationsCommand { get; private set; }
        public RelayCommand ProfileCommand { get; private set; }
        public RelayCommand LogOutCommand { get; private set; }
        public RelayCommand BackToHomeCommand { get; private set; }
        public RelayCommand ShowDetailsCommand { get; private set; }
        public NavigationService NavigationService { get; set; }
        private readonly Tourist LoggedTourist;
        public string ProfileButtonContent { get; set; }
        public ComplexTourRequestDetailsViewModel(Tourist tourist, NavigationService _navigationService, ComplexTourRequestDTO selectedRequest)
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
            ShowDetailsCommand = new RelayCommand(ShowDetails);
            NavigationService = _navigationService;
            LoggedTourist = tourist;
            LoggedTouristInfo(tourist);
            ComplexTourRequestDTO = selectedRequest;
            LoadComplexTourParts();
            UpdateComplexTourStatus();
        }

        private string _complexTourStatus;
        public string ComplexTourStatus
        {
            get { return _complexTourStatus; }
            set
            {
                _complexTourStatus = value;
                OnPropertyChanged(nameof(ComplexTourStatus));
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

        private void ShowDetails(object parameter)
        {
            if (parameter is ComplexTourPartDTO tourPart)
            {
                if (tourPart.TourRequestState == TourRequestStates.Type.Declined)
                {
                    MessageBox.Show("Tour status: DECLINED.", "Tour Part Details", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (tourPart.TourRequestState == TourRequestStates.Type.Accepted ||
                         tourPart.TourRequestState == TourRequestStates.Type.Pending)
                {
                    string approvedDateText = tourPart.ApprovedDate == tourPart.EndDate ? "/" : tourPart.ApprovedDate.ToString();

                    MessageBox.Show(
                        $"Tour Part Details:\n\n" +
                        $"Name: {tourPart.TourName}\n" +
                        $"Country: {tourPart.Country}\n" +
                        $"City: {tourPart.City}\n" +
                        $"Start Date: {tourPart.StartDate}\n" +
                        $"End Date: {tourPart.EndDate}\n" +
                        $"Approved Date: {approvedDateText}\n" +
                        $"Description: {tourPart.Description}\n" +
                        $"Status: {tourPart.TourRequestState}\n",
                        "Tour Part Details",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                }
            }
        }


        private void LoadComplexTourParts()
        {
            var allComplexTourRequestParts = complexTourPartService.GetComplexTourPartsByComplexTourRequestId(ComplexTourRequestDTO.Id).Select(part => new ComplexTourPartDTO(part)).ToList();

            foreach (var part in allComplexTourRequestParts)
            {
                ComplexTourParts.Add(part);
            }
        }

        private void LoggedTouristInfo(Tourist tourist)
        {
            TouristDTO LoggedTouristDTO = new TouristDTO(tourist);
            ProfileButtonContent = $"{LoggedTouristDTO.FirstName} {LoggedTouristDTO.LastName}";

        }

        private void UpdateComplexTourStatus()
        {
            if (ComplexTourParts.Any(p => p.TourRequestState == TourRequestStates.Type.Declined))
            {
                ComplexTourStatus = " DECLINED";
            }
            else
            {
                ComplexTourStatus = "";
            }
        }
    }
}
