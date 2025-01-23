using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels.TouristViewModel;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.Views.TouristView
{
    public partial class TutorialView : Page
    {
        public static Tourist LoggedTourist { get; set; }

        public TutorialView(Tourist tourist, NavigationService navigationService)
        {
            InitializeComponent();
            LoggedTourist = tourist;
            var viewModel = new TutorialViewModel(LoggedTourist, navigationService, VideoPlayer);
            this.DataContext = viewModel;
        }
    }
}
