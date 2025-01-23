using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;

namespace BookingApp.DTOs
{
    public class TourReviewDTO : ViewModelBase
    {
        private TourReview _tourReview;

        public int Id
        {
            get => _tourReview.Id;
            set
            {
                _tourReview.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int Knowledge
        {
            get => _tourReview.Knowledge;
            set
            {
                _tourReview.Knowledge = value;
                OnPropertyChanged(nameof(Knowledge));
            }
        }

        public int Language
        {
            get => _tourReview.Language;
            set
            {
                _tourReview.Language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        public int Interestingness
        {
            get => _tourReview.Interestingness;
            set
            {
                _tourReview.Interestingness = value;
                OnPropertyChanged(nameof(Interestingness));
            }
        }

        public string Comment
        {
            get => _tourReview.Comment;
            set
            {
                _tourReview.Comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public bool IsValid
        {
            get => _tourReview.IsValid;
            set
            {
                _tourReview.IsValid = value;
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public TourAttendance TourAttendance { get; set; }
        public TourReservation TourReservation { get; set; }

        public TourReviewDTO(TourReview tourReview)
        {
            _tourReview = tourReview;
            TourAttendance = tourReview.TourAttendance;
            TourReservation = tourReview.TourReservation;
        }
    }
}
