using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Services.OwnerServices;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Navigation;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.ViewModels.OwnerViewModel
{
    public class AccommodationRegisterViewModel : ViewModelBase
    {
        private NavigationService navigationService;
        private AccommodationRegisterService _accommodationRegisterService = DependencyContainer.GetInstance<AccommodationRegisterService>();
        /*
            Name = name;
            MaxGuests = maxGuests;
            CancelationDeadline = cancelationDeadline;
            OwnerId = ownerId;
            LocationId = locationId;
            MinReservationDays = minReservationDays;
            AccommodationType = accommodationType;
            ReviewImages = images;
    */


        private string _name;
        public string Name { 
            get { return _name; } 
            set
            {
                _name = value; 
                OnPropertyChanged(nameof(Name));
            } 
        }
        private string _maxGuests;
        public string MaxGuests
        {
            get { return _maxGuests; }
            set
            {
                _maxGuests = value;
                OnPropertyChanged(nameof(MaxGuests));
            }
        }
        private string _cancelationDeadline;
        public string CancelationDeadline
        {
            get { return _cancelationDeadline; }
            set
            {
                _cancelationDeadline = value;
                OnPropertyChanged(nameof(CancelationDeadline));
            }
        }

        public int OwnerId { get; set; }
        public int LocationId { get; set; }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        private string _minimumReservationDays;
        public string MinimumReservationDays
        {
            get { return _minimumReservationDays; }
            set
            {
                _minimumReservationDays = value;
                OnPropertyChanged(nameof(MinimumReservationDays));
            }
        }
        public AccommodationTypes AccommodationType { get; set; }
        public string Images { get; set; }

        private bool _isCabinChecked;
        public bool IsCabinChecked
        {
            get { return _isCabinChecked; }
            set
            {
                _isCabinChecked = value;
                OnPropertyChanged(nameof(IsCabinChecked)); // Ovo osigurava ažuriranje prikaza kada se promeni vrednost
            }
        }

        private bool _isHouseChecked;
        public bool IsHouseChecked
        {
            get { return _isHouseChecked; }
            set
            {
                _isHouseChecked = value;
                OnPropertyChanged(nameof(IsHouseChecked)); // Ovo osigurava ažuriranje prikaza kada se promeni vrednost
            }
        }

        private bool _isApartmentChecked;
        public bool IsApartmentChecked
        {
            get { return _isApartmentChecked; }
            set
            {
                _isApartmentChecked = value;
                OnPropertyChanged(nameof(IsApartmentChecked)); // Ovo osigurava ažuriranje prikaza kada se promeni vrednost
            }
        }




        private ObservableCollection<string> _loadLocations { get; set; }
        public ObservableCollection<string> LoadLocations
        {
            get { return _loadLocations; }
            set { _loadLocations = value; OnPropertyChanged(nameof(LoadLocations)); }
        }

        private string _accommodationTypeSelected { get; set; }
        public string AccommodationTypeSelected
        {
            get { return _accommodationTypeSelected; }
            set { _accommodationTypeSelected = value; OnPropertyChanged(nameof(AccommodationTypeSelected)); }
        }

        private string _locationSelected { get; set; }
        public string LocationSelected
        {
            get { return _locationSelected; }
            set { _locationSelected = value; OnPropertyChanged(nameof(LocationSelected)); }
        }

        public RelayCommand NewAccommodationCommand { get; private set; }


        public AccommodationRegisterViewModel(NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoadLocations = new ObservableCollection<string>();
            LoadDataIntoComboBox();

            NewAccommodationCommand = new RelayCommand(NewAccommodation, CanExecute_Command);

        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        public void PopulateComboBoxAccommodationTypes(ObservableCollection<string> comboBoxAccommodationTypes)
        {
            comboBoxAccommodationTypes.Add(AccommodationTypes.TypeToString(AccommodationTypes.Type.Apartment));
            comboBoxAccommodationTypes.Add(AccommodationTypes.TypeToString(AccommodationTypes.Type.House));
            comboBoxAccommodationTypes.Add(AccommodationTypes.TypeToString(AccommodationTypes.Type.Cabin));

        }



        private void LoadDataIntoComboBox()
        {
            List<Location> locations = _accommodationRegisterService.GetAllLocations();

            LoadLocations.Clear();

            foreach (Location location in locations)
            {
                string CityAndCountry = location.City + ", " + location.Country;
                LoadLocations.Add(CityAndCountry);
            }
        }

        private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void NewAccommodation(object sender)
        {
            try
            {

                var selectedItem = AccommodationTypeSelected;
                
                if (IsCabinChecked | IsApartmentChecked | IsHouseChecked)
                {
                    AccommodationTypes.Type accommodationType = AccommodationTypes.Type.Apartment;
                    if (IsApartmentChecked)
                        accommodationType = AccommodationTypes.Type.Apartment;
                    else if (IsHouseChecked)
                        accommodationType = AccommodationTypes.Type.House;
                    else if (IsCabinChecked)
                        accommodationType = AccommodationTypes.Type.Cabin;
                    string _city = City;
                    string _country = Country;
                    if(_accommodationRegisterService.GetByCityAndCountry(_city, _country)==null)
                    {
                        _accommodationRegisterService.SaveLocation(new Location(_city, _country));
                    }
                    Location _location = _accommodationRegisterService.GetByCityAndCountry(_country, _city);
                    /*
                    string locationText = LocationSelected;
                    string[] parts = locationText.Split(',');
                    Location location = _accommodationRegisterService.GetByCity(parts[0].Trim());
                    */
                    Accommodation accommodation = new Accommodation(
                        Name,
                        int.Parse(MaxGuests),
                        int.Parse(CancelationDeadline),
                        UserSession.Instance.Id,
                        _location.Id,
                        int.Parse(MinimumReservationDays),
                        accommodationType,
                        "/img/src/jednaslika.jpg"
                    );

                    _accommodationRegisterService.SaveAccommodation(accommodation);

                    Name = "";
                    MaxGuests = "";
                    CancelationDeadline = "";
                    MinimumReservationDays = "";
                    IsCabinChecked = false;
                    IsHouseChecked = false;
                    IsApartmentChecked = false;
                    City = "";
                    Country = "";

                }







            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do pogreške prilikom spremanja podataka: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
