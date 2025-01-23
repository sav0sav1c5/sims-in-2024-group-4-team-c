using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuestServices;
using BookingApp.WPF.Views.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuestViewModel
{
    public class GuestSearchForumsViewModel : ViewModelBase
    {
        #region Data
        public GuestDTO LoggedGuest { get; private set; }
        public NavigationService NavigationService { get; private set; }
        public ObservableCollection<ForumDTO> FoundForumsDTO { get; private set; } = new ObservableCollection<ForumDTO>();   
        #endregion

        #region Services
        private readonly GuestService _guestService = DependencyContainer.GetInstance<GuestService>();
        private readonly GuestForumService _guestForumService = DependencyContainer.GetInstance<GuestForumService>();
        #endregion

        #region FrontEndMappedData
        private string _locationCountry = string.Empty;
        public string LocationCountry 
        {
            get => _locationCountry;
            set
            {
                _locationCountry = value;
                OnPropertyChanged(nameof(LocationCountry));
            }
        }
        private string _locationCity = string.Empty;
        public string LocationCity
        {
            get => _locationCity;
            set
            {
                _locationCity = value;
                OnPropertyChanged(nameof(LocationCity));
            }
        }
        public ObservableCollection<string> ComboBoxLocationContry { get; private set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ComboBoxLocationCity { get; private set; } = new ObservableCollection<string>();
        #endregion

        #region RelayCommands
        public RelayCommand SearchForumsCMD { get; private set; }
        public RelayCommand ViewForumCMD { get; private set; }
        public RelayCommand CreateNewForumCMD { get; private set; }
        #endregion

        public GuestSearchForumsViewModel(GuestDTO loggedGuest, NavigationService navigationService)
        {
            LoggedGuest = loggedGuest;
            NavigationService = navigationService;
            PopulateComboBoxLocationCountry();
            PopulateComboBoxLocationCity();
            SearchForumsCMD = new RelayCommand(SearchForumsCMD_func);
            ViewForumCMD = new RelayCommand(ViewForumCMD_func);
            CreateNewForumCMD = new RelayCommand(CreateNewForumCMD_func);
            AddForumDTOsListToObservableCollection(FoundForumsDTO, _guestForumService.GetAllForumDTOs());
        }

        private void AddForumDTOsListToObservableCollection(ObservableCollection<ForumDTO> target, List<ForumDTO> source)
        {
            foreach (ForumDTO forumDTO in source)
            {
                target.Add(forumDTO);
            }
        }

        public void SearchForumsCMD_func(object paramater)
        {
            FoundForumsDTO.Clear();
            bool IsLocationCountryNone = (LocationCountry == string.Empty) || LocationCountry.Equals("any");
            bool IsLocationCityNone = (LocationCity == string.Empty) || LocationCity.Equals("any");
            if (IsLocationCountryNone && IsLocationCityNone)
            {
                AddForumDTOsListToObservableCollection(FoundForumsDTO, _guestForumService.GetAllForumDTOs());
            }
            else if (IsLocationCountryNone)
            {
                AddForumDTOsListToObservableCollection(FoundForumsDTO, _guestForumService.GetForumDTOsByCity(LocationCity) );
            }
            else if (IsLocationCityNone)
            {
                AddForumDTOsListToObservableCollection(FoundForumsDTO, _guestForumService.GetForumDTOsByCountry(LocationCountry) );
            }
            else
            {
                AddForumDTOsListToObservableCollection(FoundForumsDTO, _guestForumService.GetForumDTOsByCountryAndCity(LocationCountry, LocationCity));
            }
        }

        public void ViewForumCMD_func(object paramater)
        {
            if (paramater is not ForumDTO)  //Never happens
            {
                return;
            }
            NavigationService.Navigate(new GuestViewForum(LoggedGuest, this.NavigationService, (ForumDTO)paramater));
        }

        public void CreateNewForumCMD_func(object paramater)
        {
            NavigationService.Navigate(new GuestCreateForumPage(LoggedGuest, NavigationService));

        }

        private void PopulateComboBoxLocationCountry()
        {
            List<string> allCountries = _guestService.GetAllLocationDistinctCountries();
            //AddRange does not exist for some reason
            foreach (string country in allCountries)
            {
                ComboBoxLocationContry.Add(country);
            }
            ComboBoxLocationContry.Add("any");
        }

        private void PopulateComboBoxLocationCity()
        {
            List<string> allCities = _guestService.GetAllLocationDistinctCities();
            //AddRange does not exist for some reason
            foreach (string city in allCities)
            {
                ComboBoxLocationCity.Add(city);
            }
            ComboBoxLocationCity.Add("any");
        }

    }
}
