using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class ReservationModificationDTO : ViewModelBase
    {
        #region Data
        private AccommodationReservationModificationRequest _reservationModificationRequest;
        public int Id
        {
            get => _reservationModificationRequest.Id;
            set
            {
                _reservationModificationRequest.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public int AccommodationId 
        {
            get => _reservationModificationRequest.AccommodationReservation.AccommodationId;
            set
            {
                _reservationModificationRequest.AccommodationReservation.AccommodationId = value;
                OnPropertyChanged(nameof(AccommodationId));
            }
        }
        public int AccommodationReservationId 
        {
            get => _reservationModificationRequest.AccommodationReservationId;
            set
            {
                _reservationModificationRequest.AccommodationReservationId = value;
                OnPropertyChanged(nameof(AccommodationReservationId));
            }
        }
        public int ReservationModificationId 
        {
            get => _reservationModificationRequest.Id;
            set
            {
                _reservationModificationRequest.Id = value;
                OnPropertyChanged(nameof(ReservationModificationId));
            }
        }
        public string AccommodationName 
        {
            get => _reservationModificationRequest.AccommodationReservation.Accommodation.Name;
            set
            {
                _reservationModificationRequest.AccommodationReservation.Accommodation.Name = value;
                OnPropertyChanged(nameof(AccommodationName));
            }
        }
        public string AccommodationCountry
        {
            get => _reservationModificationRequest.AccommodationReservation.Accommodation.Location.Country;
            set
            {
                _reservationModificationRequest.AccommodationReservation.Accommodation.Location.Country = value;
                OnPropertyChanged(nameof(AccommodationCountry));
            }
        }
        public string AccommodationCity
        {
            get => _reservationModificationRequest.AccommodationReservation.Accommodation.Location.City;
            set
            {
                _reservationModificationRequest.AccommodationReservation.Accommodation.Location.City = value;
                OnPropertyChanged(nameof(AccommodationCity));
            }
        }
        public string AccommodationType
        {
            get => _reservationModificationRequest.AccommodationReservation.Accommodation.AccommodationType.ToString();
            private set
            {
                OnPropertyChanged(nameof(AccommodationType));
            }
        }
        /// <summary>
        /// Shows the first Image of 'Accommodation.Images' or string.Empty if it doesn't exist
        /// </summary>
        public string AccommodationImage
        {
            get
            {
                string[] images = _reservationModificationRequest.AccommodationReservation.Accommodation.Images.Split(',');
                if (images.Count() > 0)
                    return images[0];
                return string.Empty;
            }
            private set
            {
                OnPropertyChanged(nameof(AccommodationImage));
            }
        }
        public string AccommodationImages
        {
            get
            {
                return _reservationModificationRequest.AccommodationReservation.Accommodation.Images;
            }
            private set
            {
                OnPropertyChanged(nameof(AccommodationImages));
            }
        }
        public string GuestName 
        {
            get => _reservationModificationRequest.AccommodationReservation.Guest.FirstName;
            set
            {
                _reservationModificationRequest.AccommodationReservation.Guest.FirstName = value;
                OnPropertyChanged(nameof(GuestName));
            }
        }
        public string OwnerName
        {
            get => _reservationModificationRequest.AccommodationReservation.Accommodation.Owner.FirstName;
            set
            {
                _reservationModificationRequest.AccommodationReservation.Accommodation.Owner.FirstName = value;
                OnPropertyChanged(nameof(OwnerName));
            }
        }
        public DateTime NewDate
        {
            get => _reservationModificationRequest.StartDate;
            set
            {
                _reservationModificationRequest.StartDate = value;
                //OnPropertyChanged(nameof(NewStartDate));
                OnPropertyChanged(nameof(NewDate));
            }
        }
        /// <summary>
        /// NewStartDate just parsed as a string for use on the front-end
        /// </summary>
        public string NewStartDateString
        {
            get
            {
                return DateOnly.FromDateTime( _reservationModificationRequest.StartDate).ToString();
            }
            private set
            {
                //_reservationModificationRequest.StartDate = value;
                OnPropertyChanged(nameof(NewStartDateString));
            }
        }
        /// <summary>
        /// Only used on the front-end
        /// </summary>
        public string NewEndDateString
        {
            get
            {
                return DateOnly.FromDateTime(_reservationModificationRequest.StartDate.AddDays(_reservationModificationRequest.StayLength)).ToString(); //TODO: fix this
            }
            private set
            {
                OnPropertyChanged(nameof(NewEndDateString));
            }
        }
        /// <summary>
        /// Only used on the front-end
        /// </summary>
        public string OldStartDateString
        {
            get
            {
                return DateOnly.FromDateTime(_reservationModificationRequest.AccommodationReservation.StartDate).ToString(); //TODO: fix this
            }
            private set
            {
                OnPropertyChanged(nameof(OldStartDateString));
            }
        }
        /// <summary>
        /// Only used on the front-end
        /// </summary>
        public string OldEndDateString
        {
            get
            {
                return DateOnly.FromDateTime(_reservationModificationRequest.AccommodationReservation
                    .StartDate.AddDays(_reservationModificationRequest.AccommodationReservation.StayLength)
                    ).ToString(); 
            }
            private set
            {
                OnPropertyChanged(nameof(OldEndDateString));
            }
        }
        public bool IsAvalible {get;set; }      //! I do not know how to map this one
        //Guest fields
        public string RequestState
        {
            get => ReservationModificationRequestState.StateToString(_reservationModificationRequest.RequestState);
            set
            {
                if (value.Length > 0)
                {
                    _reservationModificationRequest.RequestState = ReservationModificationRequestState.StringToState(value);
                    OnPropertyChanged(nameof(RequestState));
                }
            }
        }
        public string OwnerComment
        {
            get => _reservationModificationRequest.OwnerComment;
            set
            {
                _reservationModificationRequest.OwnerComment = value;
                OnPropertyChanged(nameof(OwnerComment));
            }
        }
        public int NewStayLength
        {
            get => _reservationModificationRequest.StayLength;
            set
            {
                _reservationModificationRequest.StayLength = value;
                OnPropertyChanged(nameof(NewStayLength));
            }
        }
        public int NewNumberOfGuests
        {
            get => _reservationModificationRequest.NumberOfGuests;
            set
            {
                _reservationModificationRequest.NumberOfGuests = value;
                OnPropertyChanged(nameof(NewNumberOfGuests));
            }
        }
        /// <summary>
        /// Only used on the front-end
        /// </summary>
        public int OldStayLength
        {
            get => _reservationModificationRequest.AccommodationReservation.StayLength;
            private set
            {
                //_reservationModificationRequest.StayLength = value;
                OnPropertyChanged(nameof(OldStayLength));
            }
        }
        /// <summary>
        /// Only used on the front-end
        /// </summary>
        public int OldNumberOfGuests
        {
            get => _reservationModificationRequest.AccommodationReservation.NumberOfGuests;
            private set
            {
                //_reservationModificationRequest.NumberOfGuests = value;
                OnPropertyChanged(nameof(OldNumberOfGuests));
            }
        }
        #endregion


        public ReservationModificationDTO(string accommodationName, string guestName, DateTime newDate, bool isAvalible, int accommodationReservationId, int reservationModificationId)
        {
            _reservationModificationRequest = new AccommodationReservationModificationRequest();    //added
            AccommodationName = accommodationName;
            GuestName = guestName;
            //NewStartDate = newDate;   //FAULTY MERGE 
            NewDate = newDate;
            IsAvalible = isAvalible;
            AccommodationReservationId = accommodationReservationId;
            ReservationModificationId = reservationModificationId;
        }
        public ReservationModificationDTO(AccommodationReservationModificationRequest request)
        {
            _reservationModificationRequest = request;
        }
    }
}
