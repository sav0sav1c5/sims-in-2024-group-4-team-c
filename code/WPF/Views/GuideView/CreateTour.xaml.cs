using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF;
using BookingApp.WPF.ViewModels.GuideViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.View.GuideView
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Page
    {
        public NavigationService navigationService;
        public static Guide LoggedGuide { get; set; }

        public CreateTour(Guide guide, NavigationService _navigationService)
        {
            navigationService = _navigationService;
            InitializeComponent();
            LoggedGuide = guide;
            var viewModel = new CreateTourViewModel(LoggedGuide, navigationService);
            this.DataContext = viewModel;
        }
        private void TextBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (int.TryParse(textBox.Text, out int currentValue))
                {
                    // Increment or decrement based on mouse wheel direction
                    int increment = e.Delta > 0 ? 1 : -1;
                    int newValue = currentValue + increment;

                    // Update the text box with the new value
                    textBox.Text = newValue.ToString();

                    // Set the cursor position to the end of the text
                    textBox.CaretIndex = textBox.Text.Length;
                }
            }

            // Mark the event as handled to prevent scrolling the parent control
            e.Handled = true;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (e.Key == Key.Up || e.Key == Key.Down)
                {
                    if (int.TryParse(textBox.Text, out int currentValue))
                    {
                        // Increment or decrement based on arrow key
                        int increment = e.Key == Key.Up ? 1 : -1;
                        int newValue = currentValue + increment;

                        // Update the text box with the new value
                        textBox.Text = newValue.ToString();

                        // Set the cursor position to the end of the text
                        textBox.CaretIndex = textBox.Text.Length;

                        // Mark the event as handled to prevent default behavior
                        e.Handled = true;
                    }
                }
            }
        }






    }
}
