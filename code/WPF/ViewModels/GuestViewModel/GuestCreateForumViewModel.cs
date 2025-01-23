using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuestServices;
using BookingApp.Services.OwnerServices;
using BookingApp.WPF.Views.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuestViewModel
{
    public class GuestCreateForumViewModel : ViewModelBase
    {
        #region Data
        public GuestDTO LoggedGuest { get; private set; }
        public NavigationService NavigationService { get; private set; }
        #endregion

        #region Services
        private readonly GuestService _guestService = DependencyContainer.GetInstance<GuestService>();
        private readonly GuestForumService _guestForumService = DependencyContainer.GetInstance<GuestForumService>();
        private readonly ForumService _ownerForumService = DependencyContainer.GetInstance<ForumService>();
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
                PopulateComboBoxLocationCity();
                LocationCity = ComboBoxLocationCity.ElementAt(0);
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
        private string _initialCommentText = string.Empty;
        public string InitialCommentText
        {
            get => _initialCommentText;
            set
            {
                _initialCommentText = value;
                OnPropertyChanged(nameof(InitialCommentText));
            }
        }
        private string _forumName = string.Empty;
        public string ForumName
        {
            get => _forumName;
            set
            {
                _forumName = value;
                OnPropertyChanged(nameof(ForumName));
            }
        }
        #endregion

        #region RelayCommands
        public RelayCommand SaveForumCMD { get; private set; }
        public RelayCommand CancelCreationCMD { get; private set; }
        #endregion

        public GuestCreateForumViewModel(GuestDTO loggedGuest, NavigationService navigationService)
        {
            LoggedGuest = loggedGuest;
            NavigationService = navigationService;
            PopulateComboBoxLocationCountry();
            PopulateComboBoxLocationCity();
            LocationCountry = ComboBoxLocationContry.First();
            LocationCity = ComboBoxLocationCity.First();
            SaveForumCMD = new RelayCommand(SaveForumCMD_func);
            CancelCreationCMD = new RelayCommand(CancelCreationCMD_func);
        }

        public void CancelCreationCMD_func(object paramater)
        {
            NavigationService.Navigate(new GuestSearchForumsViewModel(LoggedGuest, NavigationService));
        }

        public void SaveForumCMD_func(object paramater)
        {
            Forum newForum = new Forum();
            newForum.AuthorId = LoggedGuest.Id;
            newForum.Author = LoggedGuest._guest;
            newForum.Name = ForumName;
            Location? foundLocation = _guestService.GetLocationByCountryAndCity(LocationCountry, LocationCity);
            if (foundLocation == null)
            {
                MessageBox.Show("We are sorry but we were unable to find the specified location!");
                return;
            }
            newForum.Location = foundLocation;
            newForum.LocationId = foundLocation.Id;
            newForum.Comments = new List<Comment>();

            _guestForumService.SaveForum(newForum); 
            if (InitialCommentText != string.Empty)
            {
                Comment initialForumComment = new Comment();
                initialForumComment.CommentText = InitialCommentText;
                _guestForumService.PostForumComment(new ForumDTO(newForum), new CommentDTO(initialForumComment), LoggedGuest);
            }
            MessageBox.Show("New forum successfully created");
            _ownerForumService.alertOwnerNewForum(newForum.Id);
            this.NavigationService.Navigate(new GuestViewForum(LoggedGuest, NavigationService, new ForumDTO( newForum)));
        }

        private void PopulateComboBoxLocationCountry()
        {
            List<string> allCountries = _guestService.GetAllLocationDistinctCountries();
            //AddRange does not exist for some reason
            foreach (string country in allCountries)
            {
                ComboBoxLocationContry.Add(country);
            }
            //ComboBoxLocationContry.Add("any");
        }

        private void PopulateComboBoxLocationCity()
        {
            ComboBoxLocationCity.Clear();
            List<string> allCities = _guestService.GetAllLocationDistinctCitiesByCountry(LocationCountry);
            //AddRange does not exist for some reason
            foreach (string city in allCities)
            {
                ComboBoxLocationCity.Add(city);
            }
            //ComboBoxLocationCity.Add("any");
        }

    }
}
