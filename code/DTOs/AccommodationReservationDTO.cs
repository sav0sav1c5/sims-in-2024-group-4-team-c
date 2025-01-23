using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.WPF.ViewModels;

namespace BookingApp.DTOs
{
    public class AccommodationReservationDTO : ViewModelBase
    {
        #region Data
        private AccommodationReservation _accommodationReservation;
        //public AccommodationDTO? AccommodationDTO { get; set; }
        public int Id 
        {
            get => _accommodationReservation.Id;
            set
            {
                _accommodationReservation.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public int AccommodationId
        {
            get => _accommodationReservation.AccommodationId;
            set
            {
                _accommodationReservation.AccommodationId = value;
                OnPropertyChanged(nameof(AccommodationId));
            }
        }
        public int GuestId
        {
            get => _accommodationReservation.GuestId;
            set
            {
                _accommodationReservation.GuestId = value;
                OnPropertyChanged(nameof(GuestId));
            }
        }
        public int NumberOfGuests
        {
            get => _accommodationReservation.NumberOfGuests;
            set
            {
                _accommodationReservation.NumberOfGuests = value;
                OnPropertyChanged(nameof(NumberOfGuests));
            }
        }
        public int StayLength
        {
            get => _accommodationReservation.StayLength;
            set
            {
                _accommodationReservation.StayLength = value;
                OnPropertyChanged(nameof(StayLength));
            }
        }
        public DateTime StartDate 
        { 
            get => _accommodationReservation.StartDate;
            set
            {
                _accommodationReservation.StartDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        /// <summary>
        /// Only used on the front-end
        /// </summary>
        public string StartDateString
        {
            get
            {
                DateTime date = _accommodationReservation.StartDate;
                return DateOnly.FromDateTime(date).ToString();
            }   
            private set
            {
                //_accommodationReservation.StartDate = value;
                OnPropertyChanged(nameof(StartDateString));
            }
        }
        /// <summary>
        /// Only used on the front-end
        /// </summary>
        public string EndDateString
        {
            get
            {
                DateTime date = _accommodationReservation.StartDate.AddDays(_accommodationReservation.StayLength);
                return DateOnly.FromDateTime(date).ToString();
            }
            private set
            {
                //_accommodationReservation.StartDate = value;
                OnPropertyChanged(nameof(EndDateString));
            }
        }
        public ReservationState.State ReservationState 
        {
            get => _accommodationReservation.ReservationState;
            set
            {
                _accommodationReservation.ReservationState = value;
                OnPropertyChanged(nameof(ReservationState));
            } 
        }
        public string AccommodationName
        {
            get => _accommodationReservation.Accommodation.Name;
            set 
            {
                _accommodationReservation.Accommodation.Name = value;
                OnPropertyChanged(nameof(AccommodationName));
            }
        }
        public string AccommodationCountry
        {
            get => _accommodationReservation.Accommodation.Location.Country;
            set
            {
                _accommodationReservation.Accommodation.Location.Country = value;
                OnPropertyChanged(nameof(AccommodationCountry));
            }
        }
        public string AccommodationCity
        {
            get => _accommodationReservation.Accommodation.Location.City;
            set
            {
                _accommodationReservation.Accommodation.Location.City = value;
                OnPropertyChanged(nameof(AccommodationCity));
            }
        }
        public string OwnerName
        {
            get => _accommodationReservation.Accommodation.Owner.FirstName;
            private set
            {
                //_accommodationReservation.Accommodation.Location.City = value;
                OnPropertyChanged(nameof(OwnerName));
            }
        }
        public AccommodationTypes.Type AccommodationType
        {
            get => _accommodationReservation.Accommodation.AccommodationType;
            set
            {
                _accommodationReservation.Accommodation.AccommodationType = value;
                OnPropertyChanged(nameof(AccommodationType));
            }
        }
        public string AccommodationImages
        {
            get => _accommodationReservation.Accommodation.Images;
            set
            {
                _accommodationReservation.Accommodation.Images = value;
                OnPropertyChanged(nameof(AccommodationImages));
            }
        }
        public string AccommodationImage
        {
            get
            {
                string[] images = _accommodationReservation.Accommodation.Images.Split(',');
                if (images.Count() > 0)
                    return images[0];
                else
                {
                    return "";
                }
            }
            private set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        public AccommodationReservationDTO()
        {
        }

        //public AccommodationReservationDTO(AccommodationReservation reservation)
        //{
        //    Id = reservation.Id;
        //    AccommodationId = reservation.AccommodationId;
        //    GuestId = reservation.GuestId;
        //    StartDate = reservation.StartDate;
        //    NumberOfGuests = reservation.NumberOfGuests;
        //    StayLength = reservation.StayLength;
        //    this.ReservationState = reservation.ReservationState;
        //    if (reservation.Accommodation != null)
        //    {
        //        AccommodationDTO = new AccommodationDTO(reservation.Accommodation);
        //    }
        //}

        public AccommodationReservationDTO(AccommodationReservation reservation)
        {
            _accommodationReservation = reservation;
        }
    }
}
