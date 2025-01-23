using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Resources.DBAccess;
using BookingApp.Services;
using BookingApp.Services.GuideServices;
using BookingApp.View.GuideView;
using BookingApp.WPF.Views.GuideView;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuideViewModel
{
    public class TourReviewsViewModel : ViewModelBase
    {
        public NavigationService navigationService { get; set; }
        private readonly TourReviewService tourReviewService = DependencyContainer.GetInstance<TourReviewService>();
        private readonly TourAttendanceService tourAttendanceService = DependencyContainer.GetInstance<TourAttendanceService>();
        private readonly TouristService touristService = DependencyContainer.GetInstance<TouristService>();
        private readonly CheckpointService checkpointService = DependencyContainer.GetInstance<CheckpointService>();
        private readonly TourReservationService tourReservationService = DependencyContainer.GetInstance<TourReservationService>();
        private readonly TourService tourService = DependencyContainer.GetInstance<TourService>();
        public static Guide LoggedGuide { get; set; }
        public ObservableCollection<TourReviewDTO> TourReviews { get; set; } = new ObservableCollection<TourReviewDTO>();
        private TourReviewDTO _selectedTourReview { get; set; }
        public TourReviewDTO SelectedTourReview
        {
            get { return _selectedTourReview; }
            set
            {
                _selectedTourReview = value;
                OnPropertyChanged(nameof(SelectedTourReview));
            }
        }
        private bool _isReportButtonVisible = true;
        public bool IsReportButtonVisible
        {
            get { return _isReportButtonVisible; }
            set
            {
                _isReportButtonVisible = value;
                OnPropertyChanged(nameof(IsReportButtonVisible));
            }
        }
        public RelayCommand ReportCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public TourReviewsViewModel(Guide guide, NavigationService _navigationService)
        {
            LoggedGuide = guide;
            navigationService = _navigationService;

            ReportCommand = new RelayCommand(_=>Report(), _=>CanExectuteReport());
            BackCommand = new RelayCommand(_ => NavigateBack());
            TourReviews = LoadTourReviews();
        }

        private bool CanExectuteReport()
        {
            return IsReportButtonVisible && SelectedTourReview != null && SelectedTourReview.IsValid;
        }
        public ObservableCollection<TourReviewDTO> LoadTourReviews()
        {
            var tourReviews = tourReviewService.GetTourReviewsByGuideId(LoggedGuide.Id);
            var tourReviewDTOs = new ObservableCollection<TourReviewDTO>();

            foreach (var review in tourReviews)
            {
                review.TourAttendance = tourAttendanceService.GetTourAttendanceById(review.TourAttendanceId);
                review.TourAttendance.Tourist = touristService.GetTouristById(review.TourAttendance.TouristId);
                if (review.TourAttendance.CheckpointJoinedId.HasValue)
                {
                    review.TourAttendance.CheckpointJoined = checkpointService.GetCheckpointById((int)review.TourAttendance.CheckpointJoinedId);
                }
                review.TourReservation = tourReservationService.GetTourReservationById(review.TourReservationId);
                review.TourReservation.Tour = tourService.GetTourById(review.TourReservation.TourId);
                var dto = new TourReviewDTO(review);

                tourReviewDTOs.Add(dto);
            }

            return tourReviewDTOs;
        }

        private void Report()
        {
            if (SelectedTourReview.IsValid)
            {
                // Update logic for updating TourReviewDTO
                SelectedTourReview.IsValid = false; // Assuming you set IsValid to false to indicate it's reported
                OnPropertyChanged(nameof(SelectedTourReview));

                // Update the database or service here if needed

                tourReviewService.UpdateTourReview(new TourReview
                {
                    Id = SelectedTourReview.Id, // Assuming SelectedTourReview has an Id property
                    Knowledge = SelectedTourReview.Knowledge,
                    Language = SelectedTourReview.Language,
                    Interestingness = SelectedTourReview.Interestingness,
                    Comment = SelectedTourReview.Comment,
                    IsValid = SelectedTourReview.IsValid,
                    TourAttendance = SelectedTourReview.TourAttendance // Assuming TourAttendance is set appropriately
                });

                TourReviews = LoadTourReviews(); // Refresh the tour reviews list
                OnPropertyChanged(nameof(TourReviews));

                SelectedTourReview = null;
                IsReportButtonVisible = false;
            }
        }


        private void NavigateBack()
        {
            Page guideProfile = new GuideProfileView(LoggedGuide, navigationService);
            navigationService.Navigate(guideProfile);
        }
    }
}
