using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.WPF.ViewModels;
using System.Collections.ObjectModel;

namespace BookingApp.DTOs
{
    public class AccommodationDTO : ViewModelBase
    {
        #region Data
        private Accommodation _acccommodation;
        public int Id 
        {
            get => _acccommodation.Id;
            set
            {
                _acccommodation.Id = value;
                OnPropertyChanged(nameof(Id));
            } 
        }
        public int LocationId 
        { 
            get => _acccommodation.LocationId; 
            set
            {
                _acccommodation.LocationId = value;
                OnPropertyChanged(nameof(LocationId));
            }
        }
        public string Name 
        {
            get => _acccommodation.Name;
            set
            {
                _acccommodation.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string  AccommodationType 
        {
            get => AccommodationTypes.TypeToString(_acccommodation.AccommodationType);
            set
            {
                AccommodationTypes.Type? castingResult = AccommodationTypes.StringToAccommodationType(value);
                if (castingResult != null)
                {
                    _acccommodation.AccommodationType = (AccommodationTypes.Type)castingResult;
                    OnPropertyChanged(nameof(AccommodationType));
                }
            }
        }
        public int MaxGuests 
        { 
            get => _acccommodation.MaxGuests;
            set
            {
                _acccommodation.MaxGuests = value;
                OnPropertyChanged(nameof(MaxGuests));
            }
        }
        public int MinReservationDays 
        {
            get => _acccommodation.MinReservationDays;
            set
            {
                _acccommodation.MinReservationDays = value;
                OnPropertyChanged(nameof(MinReservationDays));
            }
        }
        public int CancelationDeadline 
        { 
            get => _acccommodation.CancelationDeadline; 
            set
            {
                _acccommodation.CancelationDeadline = value;
                OnPropertyChanged(nameof(CancelationDeadline));
            }
        }
        public string Images 
        {
            get => _acccommodation.Images;
            set
            {
                _acccommodation.Images = value;
                OnPropertyChanged(nameof(Images));
            }
        }
        /// <summary>
        /// First image in 'Images'
        /// </summary>
        public string Image
        {
            get
            {
                string[] images = Images.Split(',');
                if (images.Count() > 0)
                    return images[0];
                else
                {
                    return "";
                }
            }
            private set
            {
                Images = value;
            }
        }
        public int OwnerId 
        {
            get => _acccommodation.OwnerId;
            set
            {
                _acccommodation.OwnerId = value;
                OnPropertyChanged(nameof(OwnerId));
            }
        }
        public string Country 
        {
            get => _acccommodation.Location.Country;
            set
            {
                _acccommodation.Location.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        } 
        public string City 
        {
            get => _acccommodation.Location.City;
            set
            {
                _acccommodation.Location.City = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public string OwnerName
        {
            get => _acccommodation.Owner.FirstName;
            set
            {
                _acccommodation.Owner.FirstName = value;
                OnPropertyChanged(nameof(OwnerName));
            }
        }
        #endregion
        
        private AccommodationDTO()
        {
        }
        public AccommodationDTO(Accommodation accommodation)
        {
            _acccommodation = accommodation;
        }

        public static implicit operator AccommodationDTO(ObservableCollection<AccommodationDTO> v)
        {
            throw new NotImplementedException();
        }

    }
}
