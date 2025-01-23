using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuestServices;
using BookingApp.Utilities;
using BookingApp.View;
using BookingApp.View.TouristView;
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
    public class GuestSearchAccommodationViewModel : ViewModelBase
    {
        #region Data
        public GuestDTO LoggedGuest { get; private set; }
        public NavigationService NavigationService { get; private set; }
        public ObservableCollection<AccommodationDTO> FoundAccommodationsDTO { get; private set; } = new ObservableCollection<AccommodationDTO>();
        #endregion

        #region Services
        private readonly GuestService _guestService = DependencyContainer.GetInstance<GuestService>();
        #endregion

        #region FrontEndMappedData
        public DateTime AccommodationReservationSearchFromDate { get; set; } 
        public DateTime AccommodationReservationSearchToDate { get; set; } 
        private string _accommodationCountry = string.Empty;
        public string AccommodationCountry  
        {
            get => _accommodationCountry;
            set
            {
                _accommodationCountry = value;
                OnPropertyChanged("AccommodationCountry");
            }
            
        }
        private string _accommodationCity = string.Empty;
        public string AccommodationCity
        {
            get => _accommodationCity;
            set
            {
                _accommodationCity = value;
                OnPropertyChanged("AccommodationCity");
            }

        }
        private string _accommodationType = string.Empty;
        public string AccommodationType 
        {
            get => _accommodationType;
            set
            {
                _accommodationType = value;
                OnPropertyChanged("AccommodationType");
            }
        }
        private string _accommodationName = string.Empty;
        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                _accommodationName = value;
                OnPropertyChanged("AccommodationName");
            }

        }
        private int _accommodationNumberOfGuests;
        public string AccommodationNumberOfGuests
        {
            get => _accommodationNumberOfGuests.ToString();
            set
            {
                int newNumberOfGuests;
                if (!int.TryParse(value, out newNumberOfGuests) || newNumberOfGuests <= 0)
                {
                    MessageBox.Show("Number of guests must be a positive round above zero!");
                }
                else
                {
                    _accommodationNumberOfGuests = newNumberOfGuests;
                }
                OnPropertyChanged("AccommodationCountry");
            }
        }
        private int _accommodationStayingPeriod;
        public string AccommodationStayingPeriod
        {
            get => _accommodationStayingPeriod.ToString();
            set
            {
                int newStayingPeriod;
                if (!int.TryParse(value, out newStayingPeriod) || newStayingPeriod <= 0)
                {
                    MessageBox.Show("Staying period must be a positive round number above zero!");
                }
                else
                {
                    _accommodationStayingPeriod = newStayingPeriod;
                }
                OnPropertyChanged("StayingPeriod");
            }
        }
        public ObservableCollection<string> ComboBoxAccommodationContry { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ComboBoxAccommodationCity { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ComboBoxAccommodationTypes { get; set; } = new ObservableCollection<string>();
        #endregion


        #region RelayCommands
        public RelayCommand SearchAccommodationsCMD { get; private set; }
        public RelayCommand ViewAccommodationCMD { get; private set; }
        #endregion

        public GuestSearchAccommodationViewModel(GuestDTO loggedGuest, NavigationService navigationService)
        {
            LoggedGuest = loggedGuest;
            NavigationService = navigationService;
            AccommodationStayingPeriod = "1";
            AccommodationNumberOfGuests = "1";
            AccommodationReservationSearchFromDate = _guestService.GetCurrentDate().Date;
            AccommodationReservationSearchToDate = _guestService.GetCurrentDate().Date;
            PopulateComboBoxAccommodationTypes(ComboBoxAccommodationTypes);
            PopulateComboBoxAccommodationContry(ComboBoxAccommodationContry);
            PopulateComboBoxAccommodationCity(ComboBoxAccommodationCity);
            List<AccommodationDTO> allAccommodations = _guestService.GetAllAccommodationDTOs();
            foreach (AccommodationDTO accommodationDTO in allAccommodations)
            {
                FoundAccommodationsDTO.Add(accommodationDTO);
            }

            SearchAccommodationsCMD = new RelayCommand(SearchAccommodationsCMD_func);
            ViewAccommodationCMD = new RelayCommand(ViewAccommodationCMD_func);
        }

        public void PopulateComboBoxAccommodationContry(ObservableCollection<string> comboBoxAccommodationContry)
        {
            List<string> allCountries = _guestService.GetAllLocationDistinctCountries();
            //AddRange does not exist for some reason
            foreach (string country in allCountries)
            {
                comboBoxAccommodationContry.Add(country);
            }
        }

        public void PopulateComboBoxAccommodationCity(ObservableCollection<string> comboBoxAccommodationCity)
        {
            List<string> allCities = _guestService.GetAllLocationDistinctCities();
            //AddRange does not exist for some reason
            foreach (string city in allCities)
            {
                comboBoxAccommodationCity.Add(city);
            }
        }

        public void PopulateComboBoxAccommodationTypes(ObservableCollection<string> comboBoxAccommodationTypes)
        {
            comboBoxAccommodationTypes.Add(AccommodationTypes.TypeToString(AccommodationTypes.Type.Apartment));
            comboBoxAccommodationTypes.Add(AccommodationTypes.TypeToString(AccommodationTypes.Type.House));
            comboBoxAccommodationTypes.Add(AccommodationTypes.TypeToString(AccommodationTypes.Type.Cabin));
            comboBoxAccommodationTypes.Add("Any");
        }

        public void SearchAccommodationsCMD_func(object paramater)
        {
            FoundAccommodationsDTO.Clear();
            List<Accommodation> accommodations = _guestService.GetAccommodationsByNameLikeAndCountryAndCityAndNumberOfGuestsAndStayingPeriodAndType(
                AccommodationName
                , AccommodationCountry
                , AccommodationCity
                , _accommodationNumberOfGuests
                , _accommodationStayingPeriod
                , AccommodationTypes.StringToAccommodationType(AccommodationType)
                );

            foreach (var accommodation in accommodations)
            {
                //Let's say that the user didn't select anything or made a bad selection
                if (AccommodationReservationSearchFromDate <=  AccommodationReservationSearchToDate)
                {
                    FoundAccommodationsDTO.Add(new AccommodationDTO(accommodation));
                    continue;
                }
                Accommodation? targetAccommodation = _guestService.GetAccommodationById(accommodation.Id);
                if (targetAccommodation == null)
                {
                    continue;
                }
                if (DateRangeUtility.FindFreeReservationDateRanges(
                    targetAccommodation.GetAccommodationReservedDateRanges()
                    , AccommodationReservationSearchFromDate < _guestService.GetCurrentDate().Date ? _guestService.GetCurrentDate().Date : AccommodationReservationSearchFromDate
                    , AccommodationReservationSearchToDate < _guestService.GetCurrentDate().Date ? _guestService.GetCurrentDate().Date : AccommodationReservationSearchToDate
                    , _accommodationStayingPeriod
                    ).Count > 0)
                {
                    FoundAccommodationsDTO.Add(new AccommodationDTO(accommodation));
                }
            }
        }

        public void ViewAccommodationCMD_func(object paramater)
        {
            if (paramater is not AccommodationDTO)
            {
                return;
            }

            AccommodationDTO selectedAccommodationDTO = (AccommodationDTO)paramater;

            NavigationService.Navigate(
                new GuestReservationPage(
                    LoggedGuest
                    , (AccommodationDTO)paramater
                    , int.Parse(AccommodationStayingPeriod)
                    , int.Parse(AccommodationNumberOfGuests)
                    , NavigationService
                    , AccommodationReservationSearchFromDate
                    , AccommodationReservationSearchToDate
                    )
            );
        }

    }
}
