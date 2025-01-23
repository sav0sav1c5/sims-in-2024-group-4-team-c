using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuideServices;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using BookingApp.WPF.Views.GuideView;
using System.Windows.Controls;
using System.Windows;
using BookingApp.View;
using BookingApp.View.GuideView;
using BookingApp.WPF.Views.TouristView;

namespace BookingApp.WPF.ViewModels.GuideViewModel
{
    public class GuideProfileViewModel : ViewModelBase
    {
        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();
        private readonly VoucherService voucherService = DependencyContainer.GetInstance<VoucherService>();
        private readonly VoucherUseService voucherUseService = DependencyContainer.GetInstance<VoucherUseService>();
        private readonly TourAttendanceService tourAttendanceService = DependencyContainer.GetInstance<TourAttendanceService>();
        private readonly TourService tourService = DependencyContainer.GetInstance<TourService>();
        private readonly TourReviewService tourReviewService = DependencyContainer.GetInstance<TourReviewService>();

        private ObservableCollection<string> _availableLanguages;
        public ObservableCollection<string> AvailableLanguages
        {
            get { return _availableLanguages; }
            set { _availableLanguages = value; OnPropertyChanged(nameof(AvailableLanguages)); }
        }
        public string StarImageSource => IsSuperGuide() ? "/Resources/Images/goldStar.png" : "/Resources/Images/blackStar.png";

        public NavigationService navigationService { get; set; }
        public static Guide LoggedGuide { get; set; }
        public string FirstAndLastName => LoggedGuide.FirstName + " " + LoggedGuide.LastName;
        public RelayCommand TourStatisticsCommand { get; private set; }
        public RelayCommand TourReviewsCommand { get; private set; }
        public RelayCommand TourRequestsCommand { get; private set; }
        public RelayCommand ComplexTourRequestsCommand { get; private set; }
        public RelayCommand TourRequestStatisticsCommand { get; private set; }
        public RelayCommand LogOutCommand { get; private set; }
        public RelayCommand QuitCommand { get; private set; }

        public GuideProfileViewModel(Guide guide, NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoggedGuide = guide ?? throw new ArgumentNullException(nameof(guide));

            IsSuperGuide();

            TourStatisticsCommand = new RelayCommand(TourStatisticsClick);
            TourReviewsCommand = new RelayCommand(TourReviewsClick);
            TourRequestsCommand = new RelayCommand(TourRequestsClick);
            ComplexTourRequestsCommand = new RelayCommand(ComplexTourRequestsClick);
            TourRequestStatisticsCommand = new RelayCommand(TourRequestStatisticsClick);
            LogOutCommand = new RelayCommand(_ => LogOut());
            QuitCommand = new RelayCommand(_ => Quit());

        }
        private void TourStatisticsClick(object sender)
        {
            Page TourStatistics = new Views.GuideView.GuideTourStatisticsView(LoggedGuide, navigationService);
            navigationService.Navigate(TourStatistics);
        }
        private void TourRequestsClick(object sender)
        {
            Page TourRequests = new TourRequestsView(LoggedGuide, navigationService);
            navigationService.Navigate(TourRequests);
        }
        private void ComplexTourRequestsClick(object sender)
        {
            Page ComplexTourRequests = new ComplexTourRequestsView(LoggedGuide, navigationService);
            navigationService.Navigate(ComplexTourRequests);
        }
        private void TourReviewsClick(object sender)
        {
            Page TourReviews = new TourReviewsView(LoggedGuide, navigationService);
            navigationService.Navigate(TourReviews);
        }
        private void TourRequestStatisticsClick(object sender)
        {
            Page TourRequestStatistics = new TourRequestStatisticsView(LoggedGuide, navigationService);
            navigationService.Navigate(TourRequestStatistics);
        }
        private void LogOut()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Zatvori trenutni prozor
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(GuideWindow))
                    {
                        SignInForm signInForm = new SignInForm();
                        signInForm.Show();
                        window.Close();
                    }
                }
            }
        }
        private void Quit()
        {
            if (ConfirmQuit())
            {
                ProcessVouchersForAttendances();
                CancelAllReservations();
                NavigateToSignInPage();
            }
        }

        private bool ConfirmQuit()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to quit?", "Quit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        private void ProcessVouchersForAttendances()
        {
            var reservations = tourReservationService.GetTourReservationsByGuideId(LoggedGuide.Id);
            foreach (var reservation in reservations)
            {
                var attendances = tourAttendanceService.GetTourAttendanceByReservationId(reservation.Id);
                foreach (var attendance in attendances)
                {
                    ProcessVouchersForAttendance(reservation, attendance);
                }
            }
        }

        private void ProcessVouchersForAttendance(TourReservation reservation, TourAttendance attendance)
        {
            var vouchers = voucherService.GetAllVouchersByTouristId(attendance.TouristId);
            if (vouchers.Count > 0)
            {
                foreach (var voucher in vouchers)
                {
                    var voucherUse = voucherUseService.GetVoucherUseByVoucherId(voucher.Id);
                    if (voucherUse != null && voucherUse.TourReservationId == reservation.Id)
                    {
                        CreateVoucherForReservation(reservation, attendance.TouristId);
                    }
                }
            }
            else
            {
                CreateGlobalVoucher(attendance.TouristId);
            }
        }

        private void CreateVoucherForReservation(TourReservation reservation, int touristId)
        {
            var newVoucher = new Voucher($"Voucher for {reservation.Tour.Name}", DateTime.Now.AddYears(2), touristId);
            voucherService.SaveVoucher(newVoucher);
        }

        private void CreateGlobalVoucher(int touristId)
        {
            var newVoucher = new Voucher($"Voucher for any tour", DateTime.Now.AddYears(2), touristId) { IsGlobal = true };
            voucherService.SaveVoucher(newVoucher);
        }

        private void CancelAllReservations()
        {
            var reservations = tourReservationService.GetTourReservationsByGuideId(LoggedGuide.Id);
            foreach (var reservation in reservations)
            {
                reservation.TourState = TourStates.Type.Canceled;
                tourReservationService.UpdateTourReservation(reservation);
            }
        }

        private void NavigateToSignInPage()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is GuideWindow)
                {
                    SignInForm signInForm = new SignInForm();
                    signInForm.Show();
                    window.Close();
                }
            }
        }


        private bool IsSuperGuide()
        {
            var tourReservations = tourReservationService.GetTourReservationsByGuideId(LoggedGuide.Id);
            var languageReservationCounts = new Dictionary<string, int>();
            var languageRatingSums = new Dictionary<string, double>();
            var languageRatingCounts = new Dictionary<string, int>();

            PopulateLanguageStatistics(tourReservations, languageReservationCounts, languageRatingSums, languageRatingCounts);

            var mostFrequentLanguage = GetMostFrequentLanguage(languageReservationCounts);
            if (mostFrequentLanguage.Value < 20)
            {
                return false;
            }

            var averageRating = CalculateAverageRating(mostFrequentLanguage.Key, languageRatingSums, languageRatingCounts);
            return averageRating > 4.0;
        }

        private void PopulateLanguageStatistics(IEnumerable<TourReservation> tourReservations,
                                                IDictionary<string, int> languageReservationCounts,
                                                IDictionary<string, double> languageRatingSums,
                                                IDictionary<string, int> languageRatingCounts)
        {
            foreach (var reservation in tourReservations)
            {
                reservation.Tour = tourService.GetTourById(reservation.TourId);
                if (reservation.DateAndTime > DateTime.Now.AddYears(-1))
                {
                    var language = reservation.Tour.Language;

                    IncrementDictionaryValue(languageReservationCounts, language);

                    var reviews = tourReviewService.GetTourReviewsByTourReservationId(reservation.Id);
                    foreach (var review in reviews)
                    {
                        IncrementDictionaryValue(languageRatingSums, language, review.Language);
                        IncrementDictionaryValue(languageRatingCounts, language);
                    }
                }
            }
        }

        private KeyValuePair<string, int> GetMostFrequentLanguage(IDictionary<string, int> languageReservationCounts)
        {
            return languageReservationCounts.OrderByDescending(kv => kv.Value).FirstOrDefault();
        }

        private double CalculateAverageRating(string language,
                                              IDictionary<string, double> languageRatingSums,
                                              IDictionary<string, int> languageRatingCounts)
        {
            if (languageRatingSums.ContainsKey(language) && languageRatingCounts.ContainsKey(language))
            {
                return languageRatingSums[language] / languageRatingCounts[language];
            }
            return 0;
        }

        private void IncrementDictionaryValue(IDictionary<string, int> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key]++;
            }
            else
            {
                dictionary[key] = 1;
            }
        }

        private void IncrementDictionaryValue(IDictionary<string, double> dictionary, string key, double value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += value;
            }
            else
            {
                dictionary[key] = value;
            }
        }


        private void LoadAvailableLanguages()
        {
            var tours = tourService.GetAllTours();
            AvailableLanguages = new ObservableCollection<string>(tours.Select(tour => tour.Language).Distinct());
        }

    }
}
