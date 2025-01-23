using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.Services.GuideServices;
using BookingApp.View;
using BookingApp.View.TouristView;
using BookingApp.WPF.Views.TouristView;
using BookingApp.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.Reflection.Metadata;


namespace BookingApp.WPF.ViewModels.TouristViewModel
{
    public class TouristProfileViewModel : ViewModelBase
    {
        private readonly VoucherService voucherService = DependencyContainer.GetInstance<VoucherService>();
        private readonly VoucherUseService voucherUseService = DependencyContainer.GetInstance<VoucherUseService>();
        private readonly TourAttendanceService tourAttendanceService = DependencyContainer.GetInstance<TourAttendanceService>();
        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();

        public int NumberOfVisitedTours { get; set; } = 0;

        public ObservableCollection<Voucher> Vouchers { get; set; } = new ObservableCollection<Voucher>();
        public List<string> StarIcons { get; set; } = new List<string>();


        public RelayCommand ToursCommand { get; private set; }
        public RelayCommand RequestsCommand { get; private set; }
        public RelayCommand StatisticsCommand { get; private set; }
        public RelayCommand RateToursCommand { get; private set; }
        public RelayCommand LogOutCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }
        public RelayCommand GeneratePdfReportCommand { get; private set; }
        public RelayCommand VoucherInfoCommand { get; private set; }
        public NavigationService NavigationService { get; set; }
        private readonly Tourist LoggedTourist;

        public TouristProfileViewModel(Tourist tourist, NavigationService _navigationService)
        {
            ToursCommand = new RelayCommand(ToursHome);
            RequestsCommand = new RelayCommand(Requests);
            StatisticsCommand = new RelayCommand(Statistics);
            RateToursCommand = new RelayCommand(RateTours);
            LogOutCommand = new RelayCommand(_ => LogOut());
            ExitCommand = new RelayCommand(_ => Exit());
            GeneratePdfReportCommand = new RelayCommand(GeneratePdfReport);
            VoucherInfoCommand = new RelayCommand(VoucherInfo);
            NavigationService = _navigationService;
            LoggedTourist = tourist;
            FillInformations();
            LoadVouchers();
            NewVoucher();
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _role;
        public string Role
        {
            get { return _role; }
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
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
        private void Exit()
        {
            NavigationService.GoBack();
        }

        private void VoucherInfo(object parameter)
        {
            string message = "Voucher informations\n\n" +
                             "1# How to earn them - The voucher is obtained if you go on any 5 tours in a period of one year.\n\n" +
                             "2# How to use them - It can be used when creating a tour reservation.\n\n" +
                             "3# Expiry date - Each voucher is valid for 6 months.";

            MessageBox.Show(message, "Voucher Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void FillInformations()
        {
            Username = LoggedTourist.Username;
            Role = "Tourist";
            FirstName = LoggedTourist.FirstName;
            LastName = LoggedTourist.LastName;
            Email = LoggedTourist.Email;
        }

        private void NewVoucher()
        {
            List<TourAttendance> tourAttendances = tourAttendanceService.GetAllTourAttendances().Where(att => att.TouristId == LoggedTourist.Id).ToList();
            List<TourReservation> reservations = tourReservationService.GetAllTourReservations();
            DateTime lastVoucherExpiration = GetLastVoucherDate();
            DateTime sixMonthsAgo = lastVoucherExpiration.AddMonths(-6);// Default to one year ago if no vouchers found
            DateTime checkDate = sixMonthsAgo.AddYears(1);

            foreach (var attendance in tourAttendances)
            {
                if (IsAttendanceConfirmed(attendance) && IsTouristMatch(attendance))
                {
                    if(attendance.IsConfirmed == true && attendance.IsPresent == true)
                    {
                        CheckReservationDates(attendance, reservations, sixMonthsAgo, checkDate);
                    }
                }
            }

            GenerateStarIcons();
        }

        private bool IsAttendanceConfirmed(TourAttendance attendance)
        {
            return attendance.IsConfirmed;
        }

        private bool IsTouristMatch(TourAttendance attendance)
        {
            return LoggedTourist.Id == attendance.TouristId;
        }
        private void CheckReservationDates(TourAttendance attendance, List<TourReservation> reservations , DateTime sixMonthsAgo, DateTime oneYear)
        {
            foreach (var reservation in reservations)
            {
                if (reservation.Id == attendance.TourReservationId && reservation.DateAndTime >= sixMonthsAgo && reservation.DateAndTime <= oneYear)
                {
                    NumberOfVisitedTours++;
                }
            }
        }

        private void GenerateStarIcons()
        {
            for (int i = 0; i < 5; i++)
            {
                if (i < NumberOfVisitedTours)
                {
                    StarIcons.Add("C:\\Users\\ws232\\OneDrive\\Documentos\\ProjectSIMS\\sims-in-2024-group-4-team-c\\Resources\\Images\\fullstar.png");
                }
                else
                {
                    StarIcons.Add("C:\\Users\\ws232\\OneDrive\\Documentos\\ProjectSIMS\\sims-in-2024-group-4-team-c\\Resources\\Images\\star.png");
                }
            }
        }

        private void LoadVouchers()
        {
            List<Voucher> allVouchers = voucherService.GetAllVouchers();
            List<VoucherUse> voucherUses = voucherUseService.GetAllVoucherUses();

            foreach (var voucher in allVouchers)
            {
                if (!IsVoucherUsed(voucher, voucherUses) && IsTouristVoucher(voucher))
                {
                    Vouchers.Add(voucher);
                }
            }
        }

        private DateTime GetLastVoucherDate()
        {
            Voucher lastVoucher = Vouchers.OrderByDescending(v => v.ExpirationDate).FirstOrDefault();
            return lastVoucher.ExpirationDate;
        }

        private void GeneratePdfReport(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF file (*.pdf)|*.pdf",
                FileName = "VouchersReport.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                CreateAndOpenPdf(filePath);
            }
        }

        private void CreateAndOpenPdf(string filePath)
        {
            CreatePdf(filePath);
            Process.Start(new ProcessStartInfo
            {
                FileName = "chrome.exe",
                Arguments = filePath,
                UseShellExecute = true
            });
        }

        private void CreatePdf(string filePath)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Vouchers Report";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyleEx.Bold);

            gfx.DrawString("Vouchers Report", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height),
                XStringFormats.TopCenter);

            DrawVoucherDetails(document, gfx, page);
            document.Save(filePath);
        }

        private void DrawVoucherDetails(PdfDocument document, XGraphics gfx, PdfPage page)
        {
            XFont fontDetails = new XFont("Verdana", 12, XFontStyleEx.Regular);
            int yPoint = 40;

            foreach (var voucher in Vouchers)
            {
                gfx.DrawString($"Name: {voucher.Name}", fontDetails, XBrushes.Black,
                    new XRect(40, yPoint, page.Width - 80, page.Height), XStringFormats.TopLeft);
                yPoint += 20;
                gfx.DrawString($"Expiration Date: {voucher.ExpirationDate.ToShortDateString()}", fontDetails, XBrushes.Black,
                    new XRect(40, yPoint, page.Width - 80, page.Height), XStringFormats.TopLeft);
                yPoint += 40;

                if (yPoint > page.Height - 40)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPoint = 40;
                }
            }
        }

        private bool IsVoucherUsed(Voucher voucher, List<VoucherUse> voucherUses)
        {
            return voucherUses.Any(vu => vu.VoucherId == voucher.Id);
        }

        private bool IsTouristVoucher(Voucher voucher)
        {
            return voucher.TouristId == LoggedTourist.Id;
        }
    }
}