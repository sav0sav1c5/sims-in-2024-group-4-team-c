using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Resources.DBAccess;
using BookingApp.Services.GuideServices;
using BookingApp.View;
using BookingApp.View.TouristView;
using BookingApp.WPF.Views.TouristView;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
    public class TourRequestViewModel : ViewModelBase
    {
        private readonly TourRequestService tourRequestService = DependencyContainer.GetInstance<TourRequestService>();
        public ObservableCollection<TourRequestDTO> RefreshedRequests { get; set; } = new ObservableCollection<TourRequestDTO>();
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; } = new ObservableCollection<TourRequestDTO>();
        public int RemainingParticipantsNumber { get; set; }
        public string ProfileButtonContent { get; set; }
        public List<TourParticipant> Participants { get; set; } = new List<TourParticipant>();
        public RelayCommand ToursCommand { get; private set; }
        public RelayCommand RequestsCommand { get; private set; }
        public RelayCommand StatisticsCommand { get; private set; }
        public RelayCommand RateToursCommand { get; private set; }
        public RelayCommand TutorialCommand { get; private set; }
        public RelayCommand NotificationsCommand { get; private set; }
        public RelayCommand ProfileCommand { get; private set; }
        public RelayCommand LogOutCommand { get; private set; }
        public RelayCommand ConfirmCommand { get; private set; }
        public RelayCommand AddParticipantCommand { get; private set; }
        public RelayCommand ShowDetailsCommand { get; private set; }

        public RelayCommand CreateRequestCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public NavigationService NavigationService { get; set; }
        private readonly Tourist LoggedTourist;

        public TourRequestViewModel(Tourist tourist, NavigationService _navigationService, ObservableCollection<TourRequestDTO> tourRequests)
        {
            ToursCommand = new RelayCommand(ToursHome);
            RequestsCommand = new RelayCommand(Requests);
            StatisticsCommand = new RelayCommand(Statistics);
            RateToursCommand = new RelayCommand(RateTours);
            TutorialCommand = new RelayCommand(Tutorial);
            NotificationsCommand = new RelayCommand(Notifications);
            ProfileCommand = new RelayCommand(ViewProfile);
            LogOutCommand = new RelayCommand(_ => LogOut());
            ConfirmCommand = new RelayCommand(Confirm);
            AddParticipantCommand = new RelayCommand(AddParticipants);
            ShowDetailsCommand = new RelayCommand(ShowDetails);
            CreateRequestCommand = new RelayCommand(CreateStandardRequest);
            CancelCommand = new RelayCommand(Cancel);
            NavigationService = _navigationService;
            LoggedTourist = tourist;
            LoggedTouristInfo(tourist);
            RefreshedRequests = tourRequests;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        private string _tourName;
        public string TourName
        {
            get { return _tourName; }
            set
            {
                _tourName = value;
                OnPropertyChanged(nameof(TourName));
            }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
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

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
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

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private bool _isParticipantsNumberEnabled = true;
        public bool IsParticipantsNumberEnabled
        {
            get { return _isParticipantsNumberEnabled; }
            set
            {
                _isParticipantsNumberEnabled = value;
                OnPropertyChanged(nameof(IsParticipantsNumberEnabled));
            }
        }

        private int _participantsNumber;
        public int ParticipantsNumber
        {
            get { return _participantsNumber; }
            set
            {
                _participantsNumber = value;
                OnPropertyChanged(nameof(ParticipantsNumber));
            }
        }

        private bool _isParticipantInfoEnabled = false;
        public bool IsParticipantInfoEnabled
        {
            get { return _isParticipantInfoEnabled; }
            set
            {
                _isParticipantInfoEnabled = value;
                OnPropertyChanged(nameof(IsParticipantInfoEnabled));
            }
        }

        private string _participantName;
        public string ParticipantName
        {
            get { return _participantName; }
            set
            {
                _participantName = value;
                OnPropertyChanged(nameof(ParticipantName));
            }
        }

        private string _participantLastName;
        public string ParticipantLastName
        {
            get { return _participantLastName; }
            set
            {
                _participantLastName = value;
                OnPropertyChanged(nameof(ParticipantLastName));
            }
        }

        private string _participantEmail;
        public string ParticipantEmail
        {
            get { return _participantEmail; }
            set
            {
                _participantEmail = value;
                OnPropertyChanged(nameof(ParticipantEmail));
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

        private bool ValidateTourRequestFields()
        {
            if (string.IsNullOrWhiteSpace(TourName) || string.IsNullOrWhiteSpace(Country) ||
                string.IsNullOrWhiteSpace(City) || string.IsNullOrWhiteSpace(Language) ||
                string.IsNullOrWhiteSpace(Description) || ParticipantsNumber <= 0)
            {
                MessageBox.Show("Please fill in all tour request fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (StartDate < DateTime.Now || EndDate < StartDate)
            {
                MessageBox.Show("Please enter valid dates.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool ValidateParticipantInfo()
        {
            if (string.IsNullOrWhiteSpace(ParticipantName) || string.IsNullOrWhiteSpace(ParticipantLastName) ||
                string.IsNullOrWhiteSpace(ParticipantEmail) || !IsValidEmail(ParticipantEmail))
            {
                MessageBox.Show("Please fill in all participant information correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        private void Confirm(object parameter)
        {
            if (!ValidateTourRequestFields()) return;

            RemainingParticipantsNumber = ParticipantsNumber;
            IsParticipantsNumberEnabled = false;
            IsParticipantInfoEnabled = true;

            if (ParticipantsNumber == 1)
            {
                HandleLastParticipantEntry();
                return;
            }

            MessageBox.Show("Please enter participant information.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            OnPropertyChanged(nameof(RemainingParticipantsNumber));
        }

        private void AddParticipants(object parameter)
        {
            if (!ValidateParticipantInfo()) return;

            TourParticipant participant = CreateParticipant();
            AddParticipantAndUpdateRemaining(participant);

            ParticipantName = "";
            ParticipantLastName = "";
            ParticipantEmail = "";

            if (RemainingParticipantsNumber == 1)
            {
                HandleLastParticipantEntry();
                return;
            }

            if (RemainingParticipantsNumber == 0)
            {
                MessageBox.Show("All participant informations added. After checking all informations you can finish creating of request");
            }
        }

        private TourParticipant CreateParticipant()
        {
            return new TourParticipant(ParticipantName, ParticipantLastName, ParticipantEmail);
        }

        private void HandleLastParticipantEntry()
        {
            MessageBox.Show("Your informations will be added automatically.");
            TourParticipant lastParticipant = new TourParticipant(LoggedTourist.FirstName, LoggedTourist.LastName, LoggedTourist.Email);
            Participants.Add(lastParticipant);
            RemainingParticipantsNumber--;
            OnPropertyChanged(nameof(RemainingParticipantsNumber));
            IsParticipantInfoEnabled = false;
        }

        private void AddParticipantAndUpdateRemaining(TourParticipant participant)
        {
            Participants.Add(participant);
            RemainingParticipantsNumber--;
            OnPropertyChanged(nameof(RemainingParticipantsNumber));
        }

        private void ShowDetails(object parameter)
        {
            MessageBox.Show($"Tour: {TourName}\n" +
                            $"Country: {Country}\n" +
                            $"City: {City}\n" +
                            $"Start Date: {StartDate}\n" +
                            $"End Date: {EndDate}\n" +
                            $"Language: {Language}\n" +
                            $"Description: {Description}\n\n" +
                            $"Participants:\n{string.Join("\n", Participants.Select(p => $"{p.FirstName} {p.LastName} - {p.Email}"))}",
                            "Tour Details", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void CreateStandardRequest(object parametar)
        {
            if (!ValidateTourRequestFields()) return;

            using (var context = new DataContext())
            {
                TourRequest tourRequest = new TourRequest(TourName, StartDate, EndDate, Country, City, Language, Description, Participants);
                tourRequest.TouristId = LoggedTourist.Id;
                context.TourRequests.Add(tourRequest);
                context.SaveChanges();
            }
            MessageBox.Show("Your tour request is successfully created.");
            TourRequests.Add(new TourRequestDTO(new TourRequest(TourName, StartDate, EndDate, Country, City, Language, Description, Participants)));
            RefreshedRequests.Add(new TourRequestDTO(new TourRequest(TourName, StartDate, EndDate, Country, City, Language, Description, Participants)));
            NavigationService.GoBack();
        }

        private void Cancel(object parameter)
        {
            NavigationService.GoBack();
        }

        private void LoggedTouristInfo(Tourist tourist)
        {
            TouristDTO LoggedTouristDTO = new TouristDTO(tourist);
            ProfileButtonContent = $"{LoggedTouristDTO.FirstName} {LoggedTouristDTO.LastName}";

        }
    }
}
