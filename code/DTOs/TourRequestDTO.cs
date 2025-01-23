using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
using System.Collections.Generic;
using System;

namespace BookingApp.DTOs
{
    public class TourRequestDTO : ViewModelBase
    {
        private TourRequest _tourRequest;

        public int Id
        {
            get => _tourRequest.Id;
            set
            {
                _tourRequest.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int TouristId
        {
            get => _tourRequest.TouristId;
            set
            {
                _tourRequest.TouristId = value;
                OnPropertyChanged(nameof(TouristId));
            }
        }

        public Tourist Tourist { get; set; }

        public string TourName
        {
            get => _tourRequest.TourName;
            set
            {
                _tourRequest.TourName = value;
                OnPropertyChanged(nameof(TourName));
            }
        }

        public DateTime StartDate
        {
            get => _tourRequest.StartDate;
            set
            {
                _tourRequest.StartDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get => _tourRequest.EndDate;
            set
            {
                _tourRequest.EndDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public string Country
        {
            get => _tourRequest.Country;
            set
            {
                _tourRequest.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public string City
        {
            get => _tourRequest.City;
            set
            {
                _tourRequest.City = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public string Language
        {
            get => _tourRequest.Language;
            set
            {
                _tourRequest.Language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        public string Description
        {
            get => _tourRequest.Description;
            set
            {
                _tourRequest.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public int NumberOfParticipants
        {
            get => _tourRequest.NumberOfParticipants;
            set
            {
                _tourRequest.NumberOfParticipants = value;
                OnPropertyChanged(nameof(NumberOfParticipants));
            }
        }
        public bool NotifyTourist
        {
            get => _tourRequest.NotifyTourist;
            set
            {
                _tourRequest.NotifyTourist = value;
                OnPropertyChanged(nameof(NotifyTourist));
            }
        }

        public TourRequestStates.Type TourRequestState
        {
            get => _tourRequest.TourRequestState;
            set
            {
                _tourRequest.TourRequestState = value;
                OnPropertyChanged(nameof(TourRequestState));
            }
        }

        public ICollection<TourParticipantDTO> TourParticipants { get; set; }

        public TourRequestDTO(TourRequest tourRequest)
        {
            _tourRequest = tourRequest;
        }
    }
}
