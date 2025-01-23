using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class AccommodationReservationReviewDTO : ViewModelBase
    {

        #region Data
        private AccommodationReservationReview _accommodationReservationReview;
        public int Id 
        {
            get => _accommodationReservationReview.Id;
            set
            {
                _accommodationReservationReview.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string AccommodationName 
        {
            get => _accommodationReservationReview.Accommodation.Name;
            set
            {
                _accommodationReservationReview.Accommodation.Name = value;
                OnPropertyChanged(nameof(AccommodationName));
            }
        }
        public string AccommodationCountry
        {
            get => _accommodationReservationReview.Accommodation.Location.Country;
            set
            {
                _accommodationReservationReview.Accommodation.Location.Country = value;
                OnPropertyChanged(nameof(AccommodationCountry));
            }
        }
        public string AccommodationCity
        {
            get => _accommodationReservationReview.Accommodation.Location.City;
            set
            {
                _accommodationReservationReview.Accommodation.Location.City = value;
                OnPropertyChanged(nameof(AccommodationCity));
            }
        }
        public string AccommodationImages
        {
            get => _accommodationReservationReview.Accommodation.Images;
            set
            {
                _accommodationReservationReview.Accommodation.Images = value;
                OnPropertyChanged(nameof(AccommodationImages));
            }
        }
        public string GuestName 
        {
            get => _accommodationReservationReview.Guest.FirstName;
            set
            {
                _accommodationReservationReview.Guest.FirstName = value;
                OnPropertyChanged(nameof(GuestName));
            }
        }
        public int? CleanlinessInt 
        {
            get => _accommodationReservationReview.Cleanliness;
            set
            {
                _accommodationReservationReview.Cleanliness = value;
                OnPropertyChanged(nameof(CleanlinessInt));
            }
        }
        public int? CorrectnessInt 
        {
            get => _accommodationReservationReview.Correctness;
            set
            {
                _accommodationReservationReview.Correctness = value;
                OnPropertyChanged(nameof(CorrectnessInt));
            }
        }

        //WTF!??? why is this a string
        public string Cleanliness { get; set; }
        public string Correctness { get; set; }
        //-------------
        public string Comment 
        {
            get => _accommodationReservationReview.Comment;
            set
            {
                _accommodationReservationReview.Comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        } 
        //Guest required fields:
        public int AccommodationId 
        {
            get => _accommodationReservationReview.AccommodationId;
            set
            {
                _accommodationReservationReview.AccommodationId = value;
                OnPropertyChanged(nameof(AccommodationId));
            }
        }
        public int GuestId 
        {
            get => _accommodationReservationReview.GuestId;
            set
            {
                _accommodationReservationReview.GuestId = value;
                OnPropertyChanged(nameof(GuestId));
            }
        }
        public int OwnerId 
        {  
            get => _accommodationReservationReview.OwnerId;
            set
            {
                _accommodationReservationReview.OwnerId = value;
                OnPropertyChanged(nameof(OwnerId));
            }
        }
        public string OwnerName
        {
            get => _accommodationReservationReview.Owner.FirstName;
            set
            {
                _accommodationReservationReview.Owner.FirstName= value;
                OnPropertyChanged(nameof(OwnerId));
            }
        }
        public int AccommodationReservationId 
        {
            get => _accommodationReservationReview.AccommodationReservationId;
            set
            {
                _accommodationReservationReview.AccommodationReservationId = value;
                OnPropertyChanged(nameof(AccommodationReservationId));
            }
        }
        public string AccommodationReservationStartDate
        {
            get
            {
                DateTime date = _accommodationReservationReview.AccommodationReservation.StartDate;
                return DateOnly.FromDateTime(date).ToString();
            }
                
            private set
            {
                //_accommodationReservationReview.AccommodationReservation.StartDate = value;
                OnPropertyChanged(nameof(AccommodationReservationStartDate));
            }
        }
        public string AccommodationReservationEndDate
        {
            get 
            {
                DateTime date = _accommodationReservationReview.AccommodationReservation.StartDate.AddDays(_accommodationReservationReview.AccommodationReservation.StayLength);
                return DateOnly.FromDateTime(date).ToString();
            }
            private set
            {
                //_accommodationReservationReview.AccommodationReservation.StartDate = value;
                OnPropertyChanged(nameof(AccommodationReservationEndDate));
            }
        }
        public int AccommodationReservationNumberOfGuests
        {
            get => _accommodationReservationReview.AccommodationReservation.NumberOfGuests;
            set
            {
                _accommodationReservationReview.AccommodationReservation.NumberOfGuests = value;
                OnPropertyChanged(nameof(AccommodationReservationNumberOfGuests));
            }
        }
        public int AccommodationReservationStayLength
        {
            get => _accommodationReservationReview.AccommodationReservation.StayLength;
            set
            {
                _accommodationReservationReview.AccommodationReservation.StayLength = value;
                OnPropertyChanged(nameof(AccommodationReservationStayLength));
            }
        }
        public int AccommodationReservationReviewId 
        {
            get => _accommodationReservationReview.Id;
            set
            {
                _accommodationReservationReview.Id = value;
                OnPropertyChanged(nameof(AccommodationReservationId));
            }
        }
        public bool? IsInNeedOfRenovation 
        {
            get => _accommodationReservationReview.IsInNeedOfRenovation;
            set
            {
                _accommodationReservationReview.IsInNeedOfRenovation = value;
                OnPropertyChanged(nameof(IsInNeedOfRenovation));
            }
        }
        public int? RenovationNeed 
        {
            get => _accommodationReservationReview.RenovationNeed;
            set
            {
                _accommodationReservationReview.RenovationNeed = value;
                OnPropertyChanged(nameof(RenovationNeed));
            }
        }
        public int? RuleCompliance 
        {
            get => _accommodationReservationReview.RuleCompliance;
            set
            {
                _accommodationReservationReview.RuleCompliance = value;
                OnPropertyChanged(nameof(RuleCompliance));
            }
        }
        public string? ReviewImages 
        {
            get => _accommodationReservationReview.Images;
            set
            {
                _accommodationReservationReview.Images = value;
                OnPropertyChanged(nameof(ReviewImages));
            }
        }
        #endregion

        public AccommodationReservationReviewDTO()
        {
        }
        public AccommodationReservationReviewDTO(AccommodationReservationReview review)
        {
            _accommodationReservationReview = review;
        }
        //public AccommodationReservationReviewDTO(AccommodationReservationReview review)
        //{
        //    Id = review.Id;
        //    Cleanliness = review.Cleanliness;
        //    Correctness = review.Correctness;
        //    Comment = review.Comment!;
        //    AccommodationId = review.AccommodationId;
        //    GuestId = review.GuestId;
        //    OwnerId = review.OwnerId;
        //    AccommodationReservationId = review.AccommodationReservationId;
        //    AccommodationReservationReviewId = review.Id;
        //    IsInNeedOfRenovation = review.IsInNeedOfRenovation;
        //    RenovationNeed = review.RenovationNeed;
        //    RuleCompliance = review.RuleCompliance;
        //    ReviewImages = review.ReviewImages;
        //}





        public AccommodationReservationReviewDTO(string accommodationName, string guestName, string cleanliness, string correctness, string comment)
        {
            _accommodationReservationReview = new AccommodationReservationReview();
            AccommodationName = accommodationName;
            GuestName = guestName;
            Cleanliness = cleanliness;
            Correctness = correctness;
            Comment = comment;
        }



    }
}
