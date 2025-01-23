using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.GuideView
{
    /// <summary>
    /// Interaction logic for DatePickerDialog.xaml
    /// </summary>
    public partial class DatePickerDialog : Window
    {
        public CalendarDateRange BlackoutDates { get; set; }
        public DateTime SelectedDate { get; set; }
        public DateTime DisplayDateStart { get; set; }
        public DateTime DisplayDateEnd { get; set; }

        public DatePickerDialog()
        {
            InitializeComponent();
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
