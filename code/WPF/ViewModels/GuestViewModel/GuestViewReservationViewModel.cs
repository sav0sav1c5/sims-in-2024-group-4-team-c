using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuestServices;
using BookingApp.View.GuestView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuestViewModel
{
    public class GuestViewReservationViewModel : ViewModelBase
    {
        #region Data
        public GuestDTO LoggedGuest { get; private set; }
        //TODO:remove
        //public AccommodationReservation AccommodationReservation { get; private set; }      
        public AccommodationReservationDTO AccommodationReservationDTO { get; private set; }
        public AccommodationDTO AccommodationDTO { get; private set; }
        public AccommodationReservationModificationRequest ModifictaionRequest { get; private set; }
        public NavigationService NavigationService { get; private set; }
        public string AccommodationReservationStartDate
        {
            //get => DateOnly.FromDateTime(AccommodationReservation.StartDate).ToString();
            get => DateOnly.FromDateTime(AccommodationReservationDTO.StartDate).ToString();
        }
        public string AccommodationReservationEndDate
        {
            //get => DateOnly.FromDateTime(AccommodationReservation.StartDate.AddDays(AccommodationReservation.StayLength)).ToString();
            get => DateOnly.FromDateTime(AccommodationReservationDTO.StartDate.AddDays(AccommodationReservationDTO.StayLength)).ToString();
        }
        #endregion

        #region Services
        private readonly GuestService _guestService = DependencyContainer.GetInstance<GuestService>();
        private readonly GuestAccommodationReservationService _guestAccommodationReservationService = DependencyContainer.GetInstance<GuestAccommodationReservationService>();
        private readonly GuestAccommodationReservationModificationRequestService _reservationModificationRequestService = DependencyContainer.GetInstance<GuestAccommodationReservationModificationRequestService>();
        #endregion

        #region RelayCommands
        public RelayCommand CancelReservationCMD { get; private set; }
        public RelayCommand SendAccommodationReservationModificationRequestCMD { get; private set; }
        #endregion

        public GuestViewReservationViewModel(GuestDTO loggedGuest, AccommodationReservationDTO accommodationReservationDTO, NavigationService navigationService)
        {
            LoggedGuest = loggedGuest;
            NavigationService = navigationService;
            AccommodationReservationDTO = accommodationReservationDTO;
            AccommodationDTO = new AccommodationDTO( _guestService.GetAccommodationById(AccommodationReservationDTO.AccommodationId)! );
            //old:
            //ModifictaionRequest = new AccommodationReservationModificationRequest(AccommodationReservation);
            //new:
            //Note: ctor with DTO will not set the 'AccommodationReservation' field, you must do it manually
            ModifictaionRequest = new AccommodationReservationModificationRequest(AccommodationReservationDTO);
            ModifictaionRequest.AccommodationReservation = _guestAccommodationReservationService.GetAccommodationReservationById(AccommodationReservationDTO.Id);
            
            CancelReservationCMD = new RelayCommand(CancelReservationCMD_func);
            SendAccommodationReservationModificationRequestCMD = new RelayCommand(SendAccommodationReservationModificationRequestCMD_func);
        }

        public void CancelReservationCMD_func(object paramater)
        {
            string errorMessage = _guestAccommodationReservationService.CancelReservation(AccommodationReservationDTO.Id);
            if (errorMessage != string.Empty)
            {
                MessageBox.Show(errorMessage);
                return;
            }

            MessageBox.Show("Reservation successfully canceled!");
            NavigationService.Navigate(new GuestBookingsViewPage(LoggedGuest, NavigationService));
        }

        

        public void SendAccommodationReservationModificationRequestCMD_func(object paramater)
        {
            //string message =  _guestService.SendAccommodationReservationModificationRequest(ModifictaionRequest);
            if (AccommodationReservationDTO.ReservationState != ReservationState.State.Reserved)
            {
                MessageBox.Show("Reservation is not in reserved state");
                return;
            }
            string message = _reservationModificationRequestService.SendAccommodationReservationModificationRequest(ModifictaionRequest);
            MessageBox.Show(message);
        }
    }
}
