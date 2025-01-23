using System;
using System.Globalization;
using System.Windows.Data;

namespace BookingApp.WPF.Converters
{
    public class IsRatedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Proveravamo da li je IsConfirmed true, ali je TourReview već kreiran
            if (values.Length == 2 && (bool)values[0] && values[1] != null)
            {
                // Ako jeste, vraćamo vrednost true, što će aktivirati DataTrigger u XAML-u
                return true;
            }
            // Inače, vraćamo false
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
