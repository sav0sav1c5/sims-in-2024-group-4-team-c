using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BookingApp.WPF.ViewModels.GuestViewModel;
using BookingApp.View;
using BookingApp.Services.GuestServices;
using BookingApp.Utilities;
using BookingApp.DTOs;
using System.Collections.ObjectModel;
using BookingApp.View.GuestView;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;

namespace BookingApp.WPF.ViewModels.GuestViewModel
{
    public class GuestReservationViewModel : ViewModelBase
    {
        #region Data
        public GuestDTO LoggedGuest { get; private set; }
        public AccommodationDTO BookingAccommodationDTO { get; private set; }
        public NavigationService NavigationService { get; private set; }
        public DateTime AccommodationReservationSearchFromDate { get; set; }
        public DateTime AccommodationReservationSearchToDate { get; set; }
        public DateTime CurrentDate { get; private set; }
        private int _stayingPeriod;
        public string StayingPeriod
        {
            get => _stayingPeriod.ToString();
            set
            {
                if (!int.TryParse(value, out _stayingPeriod))
                {
                    MessageBox.Show("Number of guests must be a positive round above zero!");
                }

                OnPropertyChanged("StayingPeriod");
            }
        }
        private int _numberOfGuests;
        public string NumberOfGuests
        {
            get => _numberOfGuests.ToString();
            set
            {
                if (int.TryParse(value, out _numberOfGuests) == false)
                {
                    MessageBox.Show("Number of guests must be a positive round above zero!");
                }
                OnPropertyChanged("AccommodationCountry");
            }
        }
        List<CalendarDateRange> ReservedAccommodationDateRanges { get; set; }
        public ObservableCollection<CalendarDateRange> FreeReservationDateRanges { get; set; } = new ObservableCollection<CalendarDateRange>();
        public ImageGallery AccommodationImageGallery { get; set; }
        #endregion

        #region Services
        private readonly GuestService _guestService = DependencyContainer.GetInstance<GuestService>();
        //private readonly SuperGuestService _superGuestService = DependencyContainer.GetInstance<SuperGuestService>();
        private readonly GuestAccommodationReservationService _guestAccommodationReservationService = DependencyContainer.GetInstance<GuestAccommodationReservationService>();
        #endregion

        #region RelayCommands
        public RelayCommand ListFreeReservationDateRangesCMD { get; private set; }
        public RelayCommand BookAccommodationCMD { get; private set; }
        public RelayCommand ShowNextAccommodationImageCMD { get; private set; }
        public RelayCommand ShowPreviousAccommodationImageCMD { get; private set; }
        #endregion

        public GuestReservationViewModel(GuestDTO loggedGuest, AccommodationDTO accommodationDTO, int stayingPeriod, int numberOfGuests, NavigationService navigationService, DateTime accommodationReservationSearchFromDate, DateTime accommodationReservationSearchToDate)
        {
            LoggedGuest = loggedGuest;
            NavigationService = navigationService;
            BookingAccommodationDTO = accommodationDTO;
            StayingPeriod = stayingPeriod.ToString();
            NumberOfGuests = numberOfGuests.ToString();
            CurrentDate = _guestService.GetCurrentDate();
            AccommodationReservationSearchFromDate = accommodationReservationSearchFromDate;
            AccommodationReservationSearchToDate = accommodationReservationSearchToDate;
            
            Accommodation? targetAccommodation = _guestService.GetAccommodationById(BookingAccommodationDTO.Id);
            if (targetAccommodation == null)
            {
                MessageBox.Show("Could not find accommodation!");
                return;
            }
            ReservedAccommodationDateRanges = targetAccommodation.GetAccommodationReservedDateRanges();
            
            ListFreeReservationDateRangesCMD = new RelayCommand(ListFreeReservationDateRangesCMD_func);
            BookAccommodationCMD = new RelayCommand(BookAccommodationCMD_func);

            List<string> accommImages = [.. BookingAccommodationDTO.Images.Split(',')];
            AccommodationImageGallery = new ImageGallery(accommImages);
            ShowNextAccommodationImageCMD = new RelayCommand(AccommodationImageGallery.ShowNextImage);
            ShowPreviousAccommodationImageCMD = new RelayCommand(AccommodationImageGallery.ShowPreviousImage);

            //If we queued by free reservation dates then we print those reservation date ranges be default immediately
            if (DateOnly.FromDateTime( AccommodationReservationSearchFromDate) >= DateOnly.FromDateTime(_guestService.GetCurrentDate().Date)
                && DateOnly.FromDateTime(AccommodationReservationSearchToDate) >= DateOnly.FromDateTime(AccommodationReservationSearchFromDate ) )
            {
                ListFreeReservationDateRangesCMD_func(0);   //0 passed but is never used
            }
        }

        public void BookAccommodationCMD_func(object paramater) 
        {
            System.Windows.Controls.DataGrid? dataGrid = (DataGrid)paramater ;
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select a reservation date!");
                return;
            }

            CalendarDateRange selectedDateRange = (CalendarDateRange)dataGrid.SelectedItem;

            AccommodationReservation newReservation = new AccommodationReservation(
                selectedDateRange.Start,
                int.Parse(NumberOfGuests),
                int.Parse(StayingPeriod),
                LoggedGuest.Id,
                BookingAccommodationDTO.Id,
                ReservationState.State.Reserved
            );

            Accommodation? targetAccommodation = _guestService.GetAccommodationById(BookingAccommodationDTO.Id);            

            if (targetAccommodation == null
                || _guestAccommodationReservationService.BookAccommodation(targetAccommodation, newReservation, ReservedAccommodationDateRanges)  < 0 )
            {
                MessageBox.Show("Booking failed!");
                return;
            }

            if (LoggedGuest.IsSuperGuest && LoggedGuest.SuperGuestDiscountCouponCount > 0)
            {
                LoggedGuest.SuperGuestDiscountCouponCount -= 1;
            }
            MessageBox.Show("Booking successful!");
            NavigationService.Navigate(new GuestSearchAccommodationPage(LoggedGuest, NavigationService));
        }

        public void ListFreeReservationDateRangesCMD_func(object paramater)
        {
            FreeReservationDateRanges.Clear();

            if (DateOnly.FromDateTime(AccommodationReservationSearchFromDate) < DateOnly.FromDateTime(CurrentDate)
                || DateOnly.FromDateTime(AccommodationReservationSearchToDate) < DateOnly.FromDateTime(CurrentDate) )
            {
                MessageBox.Show("Can not select date past the current date!");
            }

            List<CalendarDateRange> FreeCalendarDateRanges = DateRangeUtility.FindFreeReservationDateRanges(
                    ReservedAccommodationDateRanges
                    , AccommodationReservationSearchFromDate
                    , AccommodationReservationSearchToDate
                    , _stayingPeriod
                    );

            foreach ( CalendarDateRange range in FreeCalendarDateRanges )
            {
                FreeReservationDateRanges.Add(range);
            }

        }

    }
}
