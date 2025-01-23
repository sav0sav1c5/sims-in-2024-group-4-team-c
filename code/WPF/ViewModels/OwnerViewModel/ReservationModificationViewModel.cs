using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services;
using BookingApp.Services.OwnerServices;
using BookingApp.Services.GuestServices;
using BookingApp.View;
using BookingApp.WPF.Views.OwnerView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModel
{
    public class ReservationModificationViewModel:ViewModelBase
    {
        private NavigationService navigationService;
        private ReservationModificationService _reservationModificationService = DependencyContainer.GetInstance<ReservationModificationService>();
        private GuestService _guestService = DependencyContainer.GetInstance<GuestService>();
        public ObservableCollection<ReservationModificationDTO> MyData { get; set; }
        private ObservableCollection<ReservationModificationDTO> _dataGridReservationModification { get; set; }
        public ObservableCollection<ReservationModificationDTO> DataGridReservationModificationItemSource
        {
            get { return _dataGridReservationModification; }
            set { _dataGridReservationModification = value; OnPropertyChanged(nameof(DataGridReservationModificationItemSource)); }
        }

        public RelayCommand ButtonDeclineCommand { get; private set; }
        public RelayCommand ButtonAcceptCommand { get; private set; }
        public ReservationModificationViewModel(NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoadData();

            ButtonDeclineCommand = new RelayCommand(DeclineCommand);

            ButtonAcceptCommand = new RelayCommand(AcceptCommand);
        }

        private void AcceptCommand(object parameter)
        {
            if (parameter != null) 
            {
                var selectedReservation = (ReservationModificationDTO)parameter;
                var _modificationReservation = _reservationModificationService.getModificationRequest(selectedReservation.ReservationModificationId);
                if (_modificationReservation != null)
                {
                    _modificationReservation.RequestState = ReservationModificationRequestState.State.Approved;
                    var modifyReservation = _reservationModificationService.getReservationById(selectedReservation.AccommodationReservationId);
                    modifyReservation.StartDate = _modificationReservation.StartDate;
                    modifyReservation.StayLength = _modificationReservation.StayLength;

                    _reservationModificationService.updateModification(_modificationReservation);
                    _reservationModificationService.updateReservation(modifyReservation);
                }
            }
            LoadData();
        }

        private void DeclineCommand(object parameter)
        {
            if(parameter != null)
            {
                var selectedReservation = (ReservationModificationDTO)parameter;
                var _modificationReservation = _reservationModificationService.getModificationRequest(selectedReservation.ReservationModificationId);

                if (_modificationReservation != null)
                {
                    var commentDialog = new CommentDialog();
                    if (commentDialog.ShowDialog() == true)
                    {
                        // Korisnik je kliknuo na "Ok" dugme, tako da možete dobiti komentar iz pop-up prozora
                        string comment = commentDialog.CommentText;
                        _modificationReservation.OwnerComment = comment;

                        // Ovde možete dalje postupati sa komentarom
                    }
                    
                    _modificationReservation.RequestState = ReservationModificationRequestState.State.Denied;
                    _reservationModificationService.updateModification(_modificationReservation);
                }
                
            }
            LoadData();
        }


        


        public void LoadData()
        {
            MyData = new ObservableCollection<ReservationModificationDTO>();

            List<AccommodationReservationModificationRequest> _accommodationReservationModificationRequests = _reservationModificationService.getModificationRequests();

            foreach(var request in _accommodationReservationModificationRequests)
            {
                if(request.RequestState == ReservationModificationRequestState.State.Pending)
                {
                    var _accommodationReservation = _reservationModificationService.getReservationById(request.AccommodationReservationId);
                    var _accommodation = _reservationModificationService.getAccommodationById(_accommodationReservation.AccommodationId);
                    var _guest = _reservationModificationService.GetGuest(_accommodationReservation.GuestId);
                    if(_accommodation.OwnerId == UserSession.Instance.Id)
                    {
                        var _accommodationName = _accommodation.Name;
                        var _guestName = _guest.FirstName + " " + _guest.LastName;
                        DateTime _startDate = request.StartDate;
                        DateTime _endDate = request.StartDate.AddDays(request.StayLength);

                        CalendarDateRange calendarDateRange = new CalendarDateRange(_startDate,_endDate);
                        var _reservations = _reservationModificationService.getReservations();
                        bool _isAvalible = false;
                        foreach ( var reservation in _reservations)
                        {
                            //bool isOverlaping = _guestService.IsOverLappingWithDateRange(reservation,calendarDateRange);
                            bool isOverlaping = reservation.IsOverLappingWithDateRange(calendarDateRange);
                            if (!isOverlaping)
                            {
                                _isAvalible = true;
                            }
                        }

                        
                        //_guestService.IsOverLappingWithDateRange();
                        ReservationModificationDTO newReservation = new ReservationModificationDTO
                                                                        (_accommodationName,
                                                                        _guestName,
                                                                        _startDate,
                                                                        _isAvalible,
                                                                        _accommodationReservation.Id,
                                                                        request.Id);

                        MyData.Add(newReservation);
                    }
                }
            }

            DataGridReservationModificationItemSource = MyData;
        }

    }
}
