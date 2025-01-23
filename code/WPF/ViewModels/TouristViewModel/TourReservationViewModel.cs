using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Navigation;
using BookingApp.DependencyInjection;
using BookingApp.Services.GuideServices;
using BookingApp.View.TouristView;
using System.Windows.Input;
using System.Collections.ObjectModel;
using BookingApp.Resources.DBAccess;
using BookingApp.WPF.Views.TouristView;
using System.Windows.Controls;
using BookingApp.DTOs;
using BookingApp.View;
using System.Diagnostics.Metrics;

namespace BookingApp.WPF.ViewModels.TouristViewModel
{
    public class TourReservationViewModel : ViewModelBase
    {
        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();
        private readonly VoucherUseService voucherUseService = DependencyContainer.GetInstance<VoucherUseService>();
        private readonly VoucherService voucherService = DependencyContainer.GetInstance<VoucherService>();
        public ObservableCollection<DateTime> ReservationDates { get; set; } = new ObservableCollection<DateTime>();
        public ObservableCollection<Voucher> Vouchers { get; set; } = new ObservableCollection<Voucher>();
        public List<TourParticipant> Participants { get; set; } = new List<TourParticipant>();
        public int RemainingParticipantsNumber { get; set; }
        public string ProfileButtonContent { get; set; }
        public DateTime SelectedDate { get; set; }
        public Voucher SelectedVoucher { get; set; }
        public Tour SelectedTour { get; set; }
        public RelayCommand BackToToursCommand { get; set; }
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
        public RelayCommand CheckDetailsCommand { get; private set; }
        public RelayCommand CreateReservationCommand { get; private set; }
        public NavigationService NavigationService { get; set; }
        private readonly Tourist LoggedTourist;

        public TourReservationViewModel(Tourist tourist, NavigationService _navigationService, Tour _selectedTour)
        {
            ToursCommand = new RelayCommand(ToursHome);
            RequestsCommand = new RelayCommand(Requests);
            StatisticsCommand = new RelayCommand(Statistics);
            RateToursCommand = new RelayCommand(RateTours);
            TutorialCommand = new RelayCommand(Tutorial);
            NotificationsCommand = new RelayCommand(Notifications);
            ProfileCommand = new RelayCommand(ViewProfile);
            LogOutCommand = new RelayCommand(_ => LogOut());
            BackToToursCommand = new RelayCommand(BackToTours);
            ConfirmCommand = new RelayCommand(Confirm);
            AddParticipantCommand = new RelayCommand(AddParticipants);
            CheckDetailsCommand = new RelayCommand(CheckDetails);
            CreateReservationCommand = new RelayCommand(CreateReservation);
            NavigationService = _navigationService;
            LoggedTourist = tourist;
            LoggedTouristInfo(tourist);
            SelectedTour = _selectedTour ?? throw new ArgumentNullException(nameof(_selectedTour));
            TourImageUrl = _selectedTour.Images;
            LoadReservationDates(SelectedTour.Id);
            LoadUserVouchers(tourist.Id);
        }

        private string tourImageUrl;
        public string TourImageUrl
        {
            get { return tourImageUrl; }
            private set
            {
                tourImageUrl = value;
                OnPropertyChanged(nameof(TourImageUrl));
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

        private void BackToTours(object obj)
        {
            NavigationService.GoBack();
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

        private void CheckDetails(object parameter)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine("Participants:");

            foreach (var participant in Participants)
            {
                message.AppendLine($"Name: {participant.FirstName} {participant.LastName}, Email: {participant.Email}");
            }

            MessageBox.Show(message.ToString(), "Participants Details", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void CreateReservation(object parametar)
        {
            if (SelectedDate == default(DateTime))
            {
                MessageBox.Show("Please select date for reservation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TourReservation reservation = new TourReservation(SelectedTour.Id, SelectedTour.GuideId, ParticipantsNumber, Participants);
            reservation.DateAndTime = SelectedDate;
            TourReservation newReservation = tourReservationService.SaveTourReservation(reservation);

            if (SelectedVoucher != null)
            {
                VoucherUse use = new VoucherUse(SelectedVoucher.Id, newReservation.Id);
                voucherUseService.SaveVoucherUse(use);
            }

            MessageBox.Show("Your tour reservation is successfully created.", "Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }

        private void LoggedTouristInfo(Tourist tourist)
        {
            TouristDTO LoggedTouristDTO = new TouristDTO(tourist);
            ProfileButtonContent = $"{LoggedTouristDTO.FirstName} {LoggedTouristDTO.LastName}";

        }

        private void LoadReservationDates(int tourId)
        {
            List<DateTime> reservationDates = tourReservationService.GetReservationDatesForTour(tourId);

            foreach(var date in reservationDates)
            {
                ReservationDates.Add(date);
            }
        }

        private void LoadUserVouchers(int touristId)
        {
            List<Voucher> allVouchers = voucherService.GetAllVouchers();
            List<VoucherUse> voucherUses = voucherUseService.GetAllVoucherUses();

            foreach (var voucher in allVouchers)
            {
                bool isUsed = false;

                foreach (var voucherUse in voucherUses)
                {
                    if (voucherUse.VoucherId == voucher.Id)
                    {
                        isUsed = true;
                        break;
                    }
                }

                if (!isUsed && voucher.TouristId == LoggedTourist.Id)
                {
                    Vouchers.Add(voucher);
                }
            }
        }
    }
}