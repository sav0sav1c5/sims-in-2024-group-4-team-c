using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class SchedulingRenovationDTO:ViewModelBase
    {
        private Renovation _renovation;
        private Location _location;
        private Accommodation _accommodation;



        public int Id
        {
            get => _renovation.Id;
            set
            {
                _renovation.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int AccommodationId
        {
            get => _renovation.AccommodationId;
            set
            {
                _renovation.AccommodationId = value;
                OnPropertyChanged(nameof(AccommodationId));
            }
        }

        public DateOnly StartDate
        {
            get => _renovation.StartDate;
            set
            {
                _renovation.StartDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateOnly EndDate
        {
            get => _renovation.EndDate;
            set
            {
                _renovation.EndDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public int EstimatedDuration
        {
            get => _renovation.EstimatedDuration;
            set
            {
                _renovation.EstimatedDuration = value;
                OnPropertyChanged(nameof(EstimatedDuration));
            }
        }

        public bool State
        {
            get => _renovation.State;
            set
            {
                _renovation.State = value;
                OnPropertyChanged(nameof(State));
            }
        }

        public string AccommodationName
        {
            get => _accommodation.Name;
            set
            {
                _accommodation.Name = value;
                OnPropertyChanged(nameof(AccommodationName));
            }
        }

        public string LocationCity
        {
            get => _location.City;
            set
            {
                _location.City = value;
                OnPropertyChanged(nameof(LocationCity));
            }
        }

        public string LocationCountry
        {
            get => _location.Country;
            set
            {
                _location.Country = value;
                OnPropertyChanged(nameof(LocationCountry));
            }
        }

        public string AccommodationType
        {
            get
            {
                switch (_accommodation.AccommodationType)
                {
                    case AccommodationTypes.Type.Apartment:
                        return "Apartment";
                    case AccommodationTypes.Type.House:
                        return "House";
                    case AccommodationTypes.Type.Cabin:
                        return "Cabin";
                    default:
                        return "Unknown";

                }
            }
            set
            {
                _location.Country = value;
                OnPropertyChanged(nameof(LocationCountry));
            }
        }

        public SchedulingRenovationDTO(int accommodationId, DateOnly startDate, DateOnly endDate, int estimatedDuration,bool state)
        {
            _renovation = new Renovation(); // Inicijalizacija objekta Renovation
            _location = new Location(); // Inicijalizacija objekta Location
            _accommodation = new Accommodation();

            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            EstimatedDuration = estimatedDuration;
            State = state;
        }

        public static explicit operator SchedulingRenovationDTO(Renovation v)
        {
            throw new NotImplementedException();
        }
    }
}
