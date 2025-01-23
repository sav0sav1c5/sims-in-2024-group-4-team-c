using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuideServices;
using BookingApp.WPF.ViewModels;
using BookingApp.WPF.Views.GuideView;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.Windows.Navigation;
using Microsoft.Win32;

public class GuideTourStatisticsViewModel : ViewModelBase
{
    public NavigationService navigationService { get; set; }

    private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();
    private readonly TourParticipantService tourParticipantService = DependencyContainer.GetInstance<TourParticipantService>();
    private readonly TourAttendanceService tourAttendanceService = DependencyContainer.GetInstance<TourAttendanceService>();
    private readonly TourService tourService = DependencyContainer.GetInstance<TourService>();

    public ObservableCollection<TourDTO> GuideTours { get; set; } = new ObservableCollection<TourDTO>();
    public double AverageTouristsPerTour { get; set; }
    public ObservableCollection<TourStatsItem> TourStats { get; set; }
    public static Guide LoggedGuide { get; set; }
    private string _year;

    public string YearText
    {
        get { return _year; }
        set
        {
            _year = value;
            OnPropertyChanged(nameof(YearText));
        }
    }

    public RelayCommand GoBackCommand { get; }
    public RelayCommand CancelTourCommand { get; }
    public RelayCommand TourSelectedCommand { get; }
    public RelayCommand CalculateStatisticsCommand { get; }
    public RelayCommand GenerateReportCommand { get; }

    public GuideTourStatisticsViewModel(Guide guide, NavigationService _navigationService)
    {
        navigationService = _navigationService;
        LoggedGuide = guide;

        GuideTours = GetGuideTours();
        TourStats = new ObservableCollection<TourStatsItem>();
        CalculateTourStatistics();

        GoBackCommand = new RelayCommand(NavigateBack);
        TourSelectedCommand = new RelayCommand(TourSelected);
        CalculateStatisticsCommand = new RelayCommand(_ => CalculateStatistics());
        GenerateReportCommand = new RelayCommand(_ => GenerateReport());
    }

    private void CalculateStatistics()
    {
        TourStats.Clear();
        int year = int.Parse(YearText);
        if (year < 0 || string.IsNullOrEmpty(YearText))
        {
            CalculateTourStatistics();
            return;
        }

        var toursForYear = GuideTours.Where(tour => tour.Duration == 3).ToList();

        foreach (var tour in toursForYear)
        {
            GetStatisticsForSelectedTour(tour);
        }

        TourStats.OrderByDescending(x => x.AverageTourists).ToList();

        if (TourStats.Count > 0)
        {
            TourStats[0].IsHighestAverage = true;
        }
    }

    private void TourSelected(object obj)
    {
        if (obj is TourDTO selectedTour)
        {
            SelectedTour = selectedTour;
            LoadSelectedTourStatistics();
            NavigateToTourStatistics(selectedTour);
        }
    }

    private TourStatsItem _mostVisitedTour;
    public TourStatsItem MostVisitedTour
    {
        get { return _mostVisitedTour; }
        set
        {
            _mostVisitedTour = value;
            OnPropertyChanged(nameof(MostVisitedTour));
        }
    }
    private void CalculateTourStatistics()
    {
        // Clear the existing stats
        TourStats.Clear();

        // Calculate stats for each tour
        foreach (var tour in GuideTours)
        {
            GetStatisticsForSelectedTour(tour);
        }

        // Sort and find the most visited tour
        var sortedStats = TourStats.OrderByDescending(x => x.AverageTourists).ToList();
        if (sortedStats.Count > 0)
        {
            sortedStats[0].IsHighestAverage = true;
            MostVisitedTour = sortedStats[0];
        }
        else
        {
            MostVisitedTour = null;
        }

        // Update the TourStats collection
        TourStats = new ObservableCollection<TourStatsItem>(sortedStats);
    }

    private TourStatsItem GetStatisticsForSelectedTour(TourDTO tour)
    {
        var reservations = tourReservationService.GetTourReservationsByTourId(tour.Id);
        TourStatsItem item = new TourStatsItem();

        if (!reservations.Any(r => r.TourState == TourStates.Type.Finished))
        {
            return item;
        }

        var tourParticipantsAges = GetTourParticipantsAges(reservations);
        var tourAttendancesAges = GetTourAttendancesAges(reservations);

        double avgTourists = CalculateAverageTourists(reservations);

        TourStatsItem tourStats = new TourStatsItem
        {
            TourName = tour.Name,
            AverageTourists = avgTourists,
            TotalUnder18 = tourParticipantsAges.Count(a => a < 18) + tourAttendancesAges.Count(a => a < 18),
            TotalBetween18And50 = tourParticipantsAges.Count(a => a >= 18 && a <= 50) + tourAttendancesAges.Count(a => a >= 18 && a <= 50),
            TotalOver50 = tourParticipantsAges.Count(a => a > 50) + tourAttendancesAges.Count(a => a > 50)
        };

        TourStats.Add(tourStats);
        return tourStats;
    }

    private List<int> GetTourParticipantsAges(IEnumerable<TourReservation> reservations)
    {
        var participantAges = new List<int>();

        foreach (var reservation in reservations)
        {
            if (reservation.TourState == TourStates.Type.Finished)
            {
                var tourParticipants = tourParticipantService.GetTourParticipantsByTourReservationId(reservation.Id);
                participantAges.AddRange(tourParticipants.Select(p => p.Age));
            }
        }

        return participantAges;
    }

    private List<int> GetTourAttendancesAges(IEnumerable<TourReservation> reservations)
    {
        var attendanceAges = new List<int>();

        foreach (var reservation in reservations)
        {
            if (reservation.TourState == TourStates.Type.Finished)
            {
                var tourAttendances = tourAttendanceService.GetTourAttendanceByReservationId(reservation.Id);
                attendanceAges.AddRange(tourAttendances.Select(a => a.Age));
            }
        }

        return attendanceAges;
    }

    private double CalculateAverageTourists(IEnumerable<TourReservation> reservations)
    {
        int totalTourists = reservations.Where(r => r.TourState == TourStates.Type.Finished).Sum(r => r.TouristsNumber + 1);
        int totalTours = reservations.Count(r => r.TourState == TourStates.Type.Finished);

        return totalTours > 0 ? (double)totalTourists / totalTours : 0;
    }

    private TourStatsItem _selectedTourStats;
    public TourStatsItem SelectedTourStats
    {
        get { return _selectedTourStats; }
        set
        {
            _selectedTourStats = value;
            OnPropertyChanged(nameof(SelectedTourStats));
            LoadSelectedTourStatistics();
            UpdatePieChart();
        }
    }

    private TourDTO selectedTour;
    public TourDTO SelectedTour
    {
        get { return selectedTour; }
        set
        {
            if (selectedTour != value)
            {
                selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                LoadSelectedTourStatistics();
            }
        }
    }

    private string SelectedTourName;
    private void LoadSelectedTourStatistics()
    {
        if (SelectedTour != null)
        {
            SelectedTourName = SelectedTour.Name;
            SelectedTourStats = GetStatisticsForSelectedTour(SelectedTour);
        }
    }

    public void UpdateSelectedTourStatistics(Tour selectedTour)
    {
        SelectedTourStats = TourStats.FirstOrDefault(item => item.TourName == selectedTour.Name);
    }

    private ObservableCollection<TourDTO> GetGuideTours()
    {
        List<Tour> tours = tourService.GetToursByGuideId(LoggedGuide.Id).ToList();
        foreach (var tour in tours)
        {
            TourDTO tourDto = new TourDTO(tour);
            GuideTours.Add(tourDto);
        }
        return GuideTours;
    }

    private void NavigateBack(object sender)
    {
        Page Profile = new GuideProfileView(LoggedGuide, navigationService);
        navigationService.Navigate(Profile);
    }

    private void NavigateToTourStatistics(TourDTO selectedTour)
    {
       // Page tourStatisticsPage = new DetailedTourStatisticsView(selectedTour, navigationService);
        //navigationService.Navigate(tourStatisticsPage);
    }

    private void GenerateReport()
    {
        // Create a SaveFileDialog to request a path and file name to save to.
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf";
        saveFileDialog.Title = "Save Tour Statistics Report";
        saveFileDialog.FileName = "TourStatisticsReport.pdf";

        // Show the dialog and get the result
        if (saveFileDialog.ShowDialog() == true)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Tour Statistics Report";

            // Create a new page
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Define fonts
            XFont titleFont = new XFont("Verdana", 24);
            XFont sectionFont = new XFont("Verdana", 20);
            XFont contentFont = new XFont("Verdana", 16);
            XFont footerFont = new XFont("Verdana", 12);

            // Title
            gfx.DrawString("Tour Statistics Report - Voyage", titleFont, XBrushes.Black,
                new XRect(0, 0, page.Width, 50), XStringFormats.TopCenter);

            // Section title
            gfx.DrawString("Most Visited Tour", sectionFont, XBrushes.Black,
                new XRect(20, 80, page.Width - 40, 40), XStringFormats.TopLeft);

            if (MostVisitedTour != null)
            {
                // Adding information about the most visited tour
                gfx.DrawString($"Tour Name: {MostVisitedTour.TourName}", contentFont, XBrushes.Black,
                    new XRect(20, 130, page.Width - 40, 30), XStringFormats.TopLeft);

                gfx.DrawString($"Average Tourists: {MostVisitedTour.AverageTourists}", contentFont, XBrushes.Black,
                    new XRect(20, 170, page.Width - 40, 30), XStringFormats.TopLeft);

                gfx.DrawString($"Total Under 18: {MostVisitedTour.TotalUnder18}", contentFont, XBrushes.Black,
                    new XRect(20, 210, page.Width - 40, 30), XStringFormats.TopLeft);

                gfx.DrawString($"Total Between 18 and 50: {MostVisitedTour.TotalBetween18And50}", contentFont, XBrushes.Black,
                    new XRect(20, 250, page.Width - 40, 30), XStringFormats.TopLeft);

                gfx.DrawString($"Total Over 50: {MostVisitedTour.TotalOver50}", contentFont, XBrushes.Black,
                    new XRect(20, 290, page.Width - 40, 30), XStringFormats.TopLeft);
            }

            // Draw a horizontal line at the bottom of the page
            gfx.DrawLine(XPens.Black, 20, page.Height - 50, page.Width - 20, page.Height - 50);

            // Footer text
            gfx.DrawString("IHP - Prahovo", footerFont, XBrushes.Black,
                new XRect(20, page.Height - 40, page.Width - 40, 20), XStringFormats.TopLeft);

            gfx.DrawString("Fax Number: 019-3542-816", footerFont, XBrushes.Black,
                new XRect(20, page.Height - 20, page.Width - 40, 20), XStringFormats.TopLeft);

            // Save the document to the chosen file
            string filename = saveFileDialog.FileName;
            document.Save(filename);

            // Open the document
            Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });
        }
    }



    // LiveCharts Properties
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

    private void UpdatePieChart()
    {
        if (SelectedTourStats != null)
        {
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Under 18",
                    Values = new ChartValues<int> { SelectedTourStats.TotalUnder18 },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "18 to 50",
                    Values = new ChartValues<int> { SelectedTourStats.TotalBetween18And50 },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Over 50",
                    Values = new ChartValues<int> { SelectedTourStats.TotalOver50 },
                    DataLabels = true
                }
            };
        }
    }
    public class TourStatsItem
    {
        public string TourName { get; set; }
        public double AverageTourists { get; set; }
        public bool IsHighestAverage { get; set; } // New property
        public int TotalUnder18 { get; set; } // Total attendees under 18 years old
        public int TotalBetween18And50 { get; set; } // Total attendees between 18 and 50 years old
        public int TotalOver50 { get; set; } // Total attendees over 50 years old
        public string Month { get; internal set; }
        public int Count { get; internal set; }
        public string Year { get; internal set; }
    }
}
