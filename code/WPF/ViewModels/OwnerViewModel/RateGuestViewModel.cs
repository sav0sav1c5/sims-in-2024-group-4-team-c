using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.OwnerServices;
using BookingApp.View;
using BookingApp.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModel
{
    public class RateGuestViewModel:ViewModelBase
    {
        private NavigationService navigationService;
        public ObservableCollection<RateGuestDTO> MyData { get; set; }
        public RateGuestService _rateGuestService = DependencyContainer.GetInstance<RateGuestService>();
        private ObservableCollection<RateGuestDTO> _dataGridRateGuestItemSource { get; set; }
        public ObservableCollection<RateGuestDTO> DataGridRateGuestItemSource
        {
            get { return _dataGridRateGuestItemSource; }
            set { _dataGridRateGuestItemSource = value;  OnPropertyChanged(nameof(DataGridRateGuestItemSource)); }
        }
        public RelayCommand RateGuestCommand { get; private set; }
        public void LoadData()
        {



            MyData = new ObservableCollection<RateGuestDTO>();

            List<AccommodationReservation> accommodationReservations = _rateGuestService.GetAllReservations().ToList();
            foreach (var accommodationReservation in accommodationReservations)
            {
                _rateGuestService.UpdateStates(accommodationReservation);
                if (accommodationReservation.ReservationState == ReservationState.State.Ended)
                {
                    var _accommodationName = _rateGuestService.GetAccommodationName(accommodationReservation.AccommodationId);
                    var _guestName = _rateGuestService.GetGuestFullNameByGuestId(accommodationReservation.GuestId);
                    var _ownerId = UserSession.Instance.Id;
                    var _accommodationId = _rateGuestService.GetAccommodationId();
                    var _guestId = accommodationReservation.GuestId;
                    var _accommodationReservationId = accommodationReservation.Id;
                    var _stardDate = DateOnly.FromDateTime(accommodationReservation.StartDate);
                    var _endData = _stardDate.AddDays(accommodationReservation.StayLength);
          
                    bool _verify = true;
                    var accommodationReservationData = _rateGuestService.GetAccommodationReservationReviewById(_accommodationReservationId,out _verify);
                    if(_verify == true)
                    { 
                        if (accommodationReservationData == null || accommodationReservationData.Direction != false)
                        {
                             RateGuestDTO rateGuestViewModel = new RateGuestDTO(_accommodationName,
                                                                            _guestName,
                                                                            _ownerId,
                                                                            _guestId,
                                                                            _accommodationId,
                                                                            _accommodationReservationId,
                                                                            _stardDate,
                                                                            _endData);

                              MyData.Add(rateGuestViewModel);
                        }
                    }

                }
            }

            DataGridRateGuestItemSource = MyData;



        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        public void RateGuestC(object sender)
        {
            var button = sender as Button;
            var dataContext = button.DataContext as RateGuestDTO;

            // Otvorite novi prozor i prenesite podatke
            RateView rateView = new RateView(dataContext, navigationService);
            navigationService.Navigate(rateView);
        }

        public RateGuestViewModel(NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoadData();

            RateGuestCommand = new RelayCommand(RateGuestC, CanExecute_Command);

        }

       



    }
}
