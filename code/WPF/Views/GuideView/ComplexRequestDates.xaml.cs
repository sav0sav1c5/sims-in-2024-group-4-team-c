using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.Views.GuideView
{
    public partial class ComplexRequestDates : Window
    {
        public DateTime? SelectedDate { get; set; }
        public DateTime DisplayDateStart { get; set; }
        public DateTime DisplayDateEnd { get; set; }
        private List<DateTime> AvailableDates { get; set; }

        public ComplexRequestDates(DateTime startDate, DateTime endDate, List<DateTime> availableDates)
        {
            InitializeComponent();

            DataContext = this;

            DisplayDateStart = startDate;
            DisplayDateEnd = endDate;
            AvailableDates = availableDates;

            //ConfigureBlackoutDates();
        }

        private void ConfigureBlackoutDates()
        {
            HashSet<DateTime> availableDatesSet = new HashSet<DateTime>(AvailableDates);

            DateTime currentDate = DisplayDateStart;
            while (currentDate <= DisplayDateEnd)
            {
                if (!availableDatesSet.Contains(currentDate))
                {
                    datePicker.BlackoutDates.Add(new CalendarDateRange(currentDate));
                }
                currentDate = currentDate.AddDays(1);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}