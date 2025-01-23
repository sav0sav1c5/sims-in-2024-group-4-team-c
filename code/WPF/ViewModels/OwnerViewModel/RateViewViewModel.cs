using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Navigation;
using BookingApp.Services.OwnerServices;
using BookingApp.DependencyInjection;
using BookingApp.View.OwnerView;

namespace BookingApp.WPF.ViewModels.OwnerViewModel
{
    public class RateViewViewModel:ViewModelBase
    {
        private NavigationService _navigationService;
        private RateGuestDTO _rateGuestDTO;
        private RateViewService _rateViewService = DependencyContainer.GetInstance<RateViewService>();

        public int cleanliness;
        public int ruleCompliance;
        public string comment;
        public int OwnerId;
        public int GuestId;
        public int AccommodationId;
        public bool Direction;
        public int AccommodationReservationId;
        private string _accommodationName;
        public string AccommodationName
        {
            get { return _accommodationName; }
            set
            {
                if (_accommodationName != value)
                {
                    _accommodationName = value;
                    OnPropertyChanged(nameof(AccommodationName));
                }
            }
        }

        private string _guestName;
        public string GuestName
        {
            get { return _guestName; }
            set
            {
                if (_guestName != value)
                {
                    _guestName = value;
                    OnPropertyChanged(nameof(GuestName));
                }
            }
        }

        private DateOnly _startDate;
        public DateOnly StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        private DateOnly _endDate;
        public DateOnly EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        public int SelectedCleanlinessRating
        {
            get => _selectedCleanlinessRating;
            set
            {
                if (_selectedCleanlinessRating != value)
                {
                    _selectedCleanlinessRating = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _selectedCleanlinessRating = 1;

        public int SelectedRuleComplianceRating
        {
            get => _selectedRuleComplianceRating;
            set
            {
                if (_selectedRuleComplianceRating != value)
                {
                    _selectedRuleComplianceRating = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _selectedRuleComplianceRating = 1;

        public string CommentText
        {
            get => _commentText;
            set
            {
                if (_commentText != value)
                {
                    _commentText = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _commentText;

        public RelayCommand SaveGuestRatingCommand { get; private set; }
        




        public RateViewViewModel(RateGuestDTO rateGuestDTO)
        {

            
            SelectedCleanlinessRating = 1;
            SelectedRuleComplianceRating = 1;

            AccommodationName = rateGuestDTO.AccommodationName;
            GuestName = rateGuestDTO.GuestName;
            OwnerId = rateGuestDTO.OwnerId;
            GuestId = rateGuestDTO.GuestId;
            AccommodationId = rateGuestDTO.AccommodationId;
            StartDate = rateGuestDTO.StartDate;
            EndDate = rateGuestDTO.EndDate;
            Direction = true;
            AccommodationReservationId = rateGuestDTO.AccommodationReservationId;

            OnPropertyChanged(nameof(SelectedCleanlinessRating)); // Promenjeno - Dodato osvežavanje GUI-a za čistoću
            OnPropertyChanged(nameof(SelectedRuleComplianceRating)); // Promenjeno - Dodato osvežavanje GUI-a za pridržavanje pravila

            SaveGuestRatingCommand = new RelayCommand(SaveGuestRating, CanExecute_Command);
            
            
            SaveGuestRatingCommand = new RelayCommand(SaveGuestRating, CanSaveGuestRating);

        }


        private bool CanSaveGuestRating(object obj)
        {
            // Implement your logic here to determine if the rating can be saved
            return true;
        }



        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        
        // Dodajte event handler za klik na dugme
        
        private void SaveGuestRating(object sender)
        {
            var radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.IsChecked == true)
            {
                int rating;
                if (int.TryParse(radioButton.Tag?.ToString(), out rating))
                {
                    if (radioButton.GroupName == "CleanlinessGroup")
                    {
                        SelectedCleanlinessRating = rating;
                    }
                    else if (radioButton.GroupName == "RuleComplianceGroup")
                    {
                        SelectedRuleComplianceRating = rating;
                    }
                }
            }
            // Čuvanje ocene čistoće
            int cleanliness = SelectedCleanlinessRating;

            // Čuvanje ocene poštovanja pravila
            int ruleCompliance = SelectedRuleComplianceRating;

            // Čuvanje komentara
            comment = CommentText;

            AccommodationReservationReview rateGuest = new AccommodationReservationReview(AccommodationId,
                                                GuestId,
                                                OwnerId,
                                                cleanliness,
                                                ruleCompliance,
                                                comment,
                                                Direction,
                                                AccommodationReservationId);
            _rateViewService.SaveAccommodationReservationReview(rateGuest);
        }
    }
}
