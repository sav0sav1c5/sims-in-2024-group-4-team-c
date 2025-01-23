using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.Views.Validation
{
    public class StringToIntValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var inputString = value as string;
                int result;
                if (int.TryParse(inputString, out result))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Please enter a valid integer value.");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
}
