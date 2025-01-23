using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuestServices;
using BookingApp.View.GuestView;
using BookingApp.WPF.Views.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuestViewModel
{
    public class GuestBookingsViewModel : ViewModelBase
    {
        #region Data
        public GuestDTO LoggedGuest { get; private set; }
        public ObservableCollection<AccommodationReservationDTO> FoundReservationsDTO { get; private set; } = new ObservableCollection<AccommodationReservationDTO>();
        public NavigationService NavigationService { get; private set; }
        #endregion

        #region Services
        private readonly GuestService _guestService = DependencyContainer.GetInstance<GuestService>();
        private readonly GuestAccommodationReservationService _guestAccommodationReservationService = DependencyContainer.GetInstance<GuestAccommodationReservationService>();
        private readonly GuestAccommodationReservationReviewService _reservationReviewService = DependencyContainer.GetInstance<GuestAccommodationReservationReviewService>();
        #endregion

        #region RelayCommands
        public RelayCommand ViewSelectedReservationCMD { get; private set; } 
        public RelayCommand ReviewSelectedReservationCMD { get; private set; } 
        public RelayCommand ViewReceivedReviewCMD { get; private set; } 
        #endregion

        public GuestBookingsViewModel(GuestDTO loggedGuest, NavigationService navigationService )
        {
            LoggedGuest = loggedGuest;
            NavigationService = navigationService;
            List<AccommodationReservationDTO> accommodationReservationDTOs = _guestAccommodationReservationService.GetAllAccommodationReservationDTOsByGuestId(loggedGuest.Id);
            foreach (AccommodationReservationDTO accommodationReservationDTO in accommodationReservationDTOs)
            {
                FoundReservationsDTO.Add(accommodationReservationDTO);
            }
            ViewSelectedReservationCMD = new RelayCommand(ViewSelectedReservationCMD_func);
            ReviewSelectedReservationCMD = new RelayCommand(ReviewSelectedReservationCMD_func);
            ViewReceivedReviewCMD = new RelayCommand(ViewReceivedReviewCMD_func);
        }

        public void ViewSelectedReservationCMD_func(object paramater)
        {
            System.Windows.Controls.ListView? castedListView = paramater as ListView;
            if (castedListView == null)
                return;
            
            AccommodationReservationDTO selectedAccommodationReservationDTO = (AccommodationReservationDTO)castedListView.SelectedItem;
            if (selectedAccommodationReservationDTO == null)
                return;
            
            NavigationService.Navigate( new GuestViewReservationPage(LoggedGuest, selectedAccommodationReservationDTO, NavigationService));
        }

        public void ReviewSelectedReservationCMD_func(object paramater)
        {
            System.Windows.Controls.ListView? castedListView = paramater as ListView;
            if (castedListView == null)
                return;
            
            AccommodationReservationDTO selectedAccommodationReservationDTO = (AccommodationReservationDTO)castedListView.SelectedItem;
            if (selectedAccommodationReservationDTO == null)
                return;
            
            if (_reservationReviewService.GuestAccommodationReservationReviewExists(selectedAccommodationReservationDTO.Id) ) 
            {
                MessageBox.Show("Review for selected reservation already exists!");
                return;
            }
            if (selectedAccommodationReservationDTO.ReservationState != ReservationState.State.Ended)
            {
                MessageBox.Show("Reservation has not ended!");
                return;
            }
            if (_reservationReviewService.IsGuestReviewPostingPeriodOver(selectedAccommodationReservationDTO.Id))
            {
                MessageBox.Show("Review period for reservation is over!");
                return;
            }
            NavigationService.Navigate(new GuestReviewAccommodationReservationPage(LoggedGuest, selectedAccommodationReservationDTO, NavigationService) );
        }

        public void ViewReceivedReviewCMD_func(object paramater)
        {
            System.Windows.Controls.ListView? castedListView = paramater as ListView;
            if (castedListView == null)
                return;
            AccommodationReservationDTO selectedAccommodationReservationDTO = (AccommodationReservationDTO)castedListView.SelectedItem;
            if (selectedAccommodationReservationDTO == null)
                return;
            
            if (_reservationReviewService.OwnerAccommodationReservationReviewExists(selectedAccommodationReservationDTO.Id) )
            {
                NavigationService.Navigate(new GuestViewReceivedOwnerReviewPage(LoggedGuest, selectedAccommodationReservationDTO, NavigationService));
                return;
            }
            else
            {
                MessageBox.Show("Owner has not reviewed your stay!");
            }
        }

    }
}
