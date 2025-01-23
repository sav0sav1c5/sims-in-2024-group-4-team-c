using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.View.GuestView;
using BookingApp.WPF.ViewModels.GuestViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.GuestView
{
    /// <summary>
    /// Interaction logic for GuestReviewAccommodationReservationPage.xaml
    /// </summary>
    public partial class GuestReviewAccommodationReservationPage : Page
    {
        private readonly GuestReviewAccommodationReservationViewModel _guestReviewAccommodationReservationViewModel;
        public GuestReviewAccommodationReservationPage(GuestDTO loggedGuest, AccommodationReservationDTO accommodationReservationDTO ,NavigationService navigationService)
        {
            InitializeComponent();
            _guestReviewAccommodationReservationViewModel = new GuestReviewAccommodationReservationViewModel(loggedGuest, accommodationReservationDTO, navigationService);
            this.DataContext = _guestReviewAccommodationReservationViewModel;
            _guestReviewAccommodationReservationViewModel.AccommodationImageGallery.RefreshFrontImage(AccommodationImageGallery);

        }


        /// <summary>
        /// Workaroung since you are unable to pass a paramater to a LostFocus Relay function
        /// </summary>
        private void RefreshPreviewReviewImage(object sender, RoutedEventArgs e)
        {
            //Testing values:
            //C:\Users\LopD\Desktop\online nastava\semestar 6\SimsProjekatV3\Resources\Images\house.jpg
            //C:\Users\LopD\Desktop\online nastava\semestar 6\SimsProjekatV3\Resources\Images\house2.jpg

            //Can't pass a paramater to a lost focus event handler
            System.Windows.Controls.Image targetImage = PreviewReviewImage;

            //Any events come before value binding so 
            //"string newImagePath = _guestReviewAccommodationReservationViewModel.NewImagePath;"
            //will not contain the needed string
            string newImagePath = NewImagePathTextBox.Text;       ////////////////////////////////////////////

            try
            {   //The OS can throw an exception if the file can't be loaded as a bitmap    
                var uploadedBitMapImage = new BitmapImage(new Uri(
                    newImagePath
                    , UriKind.RelativeOrAbsolute
                    )
                );
                targetImage.Source = uploadedBitMapImage;
            }
            catch (Exception exception)
            {
                targetImage.Source = null;
                MessageBox.Show("Exception occured:" + exception.Message);
            }

        }

    }
}
