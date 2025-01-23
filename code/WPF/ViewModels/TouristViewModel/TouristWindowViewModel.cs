using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.TouristViewModel
{
    public class TouristWindowViewModel
    {
        public NavigationService Navigation { get; set; }
        public TouristWindowViewModel(NavigationService navigation)
        {
            Navigation = navigation;
        }
    }
}
