using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.View.TouristView;
using System;
using System.Globalization;
using System.Windows.Data;

namespace BookingApp.WPF.Converter
{
    public class LocationIdToCountyConverter : IValueConverter
    {
        private readonly LocationRepository _locationRepository = new LocationRepository();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int locationId)
            {
                // Ovdje bi trebalo da pristupite repository-ju ili bazi podataka
                Location location = _locationRepository.GetById(locationId);
                return location?.Country ?? "";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
