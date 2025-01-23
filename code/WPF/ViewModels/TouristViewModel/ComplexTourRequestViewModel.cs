using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Resources.DBAccess;
using BookingApp.Services.GuideServices;
using BookingApp.View.TouristView;
using BookingApp.View;
using BookingApp.WPF.Views.OwnerView;
using BookingApp.WPF.Views.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Navigation;
using System.Diagnostics.Metrics;

namespace BookingApp.WPF.ViewModels.TouristViewModel
{
    public class ComplexTourRequestViewModel : ViewModelBase
    {
        private readonly TourRequestService tourRequestService = DependencyContainer.GetInstance<TourRequestService>();
        public ObservableCollection<TourRequestDTO> RefreshedRequests { get; set; } = new ObservableCollection<TourRequestDTO>();
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; } = new ObservableCollection<TourRequestDTO>();
        public int RemainingParticipantsNumber { get; set; }
        public string ProfileButtonContent { get; set; }
        public string ComplexTourName { get; set; }
        public List<TourParticipant> Participants { get; set; } = new List<TourParticipant>();
        public List<ComplexTourPart> TourParts { get; set; } = new List<ComplexTourPart>();
        public ComplexTourRequest ComplexTourRequest { get; set; }
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
        public RelayCommand AddTourCommand { get; private set; }
        public RelayCommand ShowDetailsCommand { get; private set; }
        public RelayCommand CreateRequestCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public NavigationService NavigationService { get; set; }
        private readonly Tourist LoggedTourist;

        public ComplexTourRequestViewModel(Tourist tourist, NavigationService _navigationService, string complexTourName)
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
            AddTourCommand = new RelayCommand(AddTours);
            ShowDetailsCommand = new RelayCommand(ShowDetails);
            CreateRequestCommand = new RelayCommand(CreateComplexRequest);
            CancelCommand = new RelayCommand(Cancel);
            NavigationService = _navigationService;
            LoggedTourist = tourist;
            ComplexTourName = complexTourName;
            LoggedTouristInfo(tourist);
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            ComplexTourRequest = new ComplexTourRequest();
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

        private void Confirm(object parameter)
        {
            if (!int.TryParse(ParticipantsNumber.ToString(), out int number) || number <= 0)
            {
                MessageBox.Show("Participant number must be a number greater than 0 and must not contain letters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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
            if (string.IsNullOrEmpty(ParticipantName) || string.IsNullOrEmpty(ParticipantLastName) || string.IsNullOrEmpty(ParticipantEmail))
            {
                MessageBox.Show("Please fill in all participant information.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

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
                MessageBox.Show("All participant informations added. After checking all informatrions you can finish creating of request");
            }
        }

        private TourParticipant CreateParticipant()
        {
            return new TourParticipant(ParticipantName, ParticipantLastName, ParticipantEmail);
        }

        private void HandleLastParticipantEntry()
        {
            MessageBox.Show("Your informations will be added automatically. You can check informations of tour, and then add it by pressing 'Add tour'");
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
            StringBuilder details = new StringBuilder();

            foreach (var part in ComplexTourRequest.Parts)
            {
                details.AppendLine($"Tour Name: {part.TourName}")
                       .AppendLine($"Country: {part.Country}")
                       .AppendLine($"City: {part.City}")
                       .AppendLine($"Start Date: {part.StartDate.ToShortDateString()}")
                       .AppendLine($"End Date: {part.EndDate.ToShortDateString()}")
                       .AppendLine($"Language: {part.Language}")
                       .AppendLine($"Description: {part.Description}")
                       .AppendLine($"Participants:");

                foreach (var participant in part.TourParticipants)
                {
                    details.AppendLine($"  {participant.FirstName} {participant.LastName} - {participant.Email}");
                }

                details.AppendLine();
            }

            MessageBox.Show(details.ToString(), "Complex Tour Details", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddTours(object parameter)
        {
            if (string.IsNullOrEmpty(TourName) || string.IsNullOrEmpty(Country) || string.IsNullOrEmpty(City) || string.IsNullOrEmpty(Language) || string.IsNullOrEmpty(Description))
            {
                MessageBox.Show("Please fill in all tour information.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ComplexTourPart complexTourPart = new ComplexTourPart(TourName, StartDate, EndDate, EndDate, Country, City, Language, Description, Participants, ComplexTourRequest.Id);
            ComplexTourRequest.Parts.Add(complexTourPart);
            MessageBox.Show("Your tour is added successfully. You can now create a complex tour request or continue adding tours.");
            ClearTourFields();
        }

        private void ClearTourFields()
        {
            TourName = "";
            Country = "";
            City = "";
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            Language = "";
            Description = "";
            ParticipantName = "";
            ParticipantLastName = "";
            ParticipantEmail = "";
            ParticipantsNumber = 0;
            Participants.Clear();
            IsParticipantsNumberEnabled = true;
        }

        private void CreateComplexRequest(object parametar)
        {
            if (string.IsNullOrEmpty(ComplexTourName) || ComplexTourRequest.Parts.Count == 0)
            {
                MessageBox.Show("Please enter a valid complex tour name and add at least one tour.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            using (var context = new DataContext())
            {
                ComplexTourRequest.Name = ComplexTourName;
                ComplexTourRequest.TouristId = LoggedTourist.Id;
                context.ComplexTourRequests.Add(ComplexTourRequest);
                context.SaveChanges();
            }
            MessageBox.Show("Your complex tour request is successfully created.");
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
