using BookingApp.DependencyInjection;
using BookingApp.DTOs;
using BookingApp.Repository;
using BookingApp.Services.OwnerServices;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModel
{
    public class ViewGuestReviewsViewModel : ViewModelBase
    {
        private ViewGuestReviewsService _viewGuestReviewService = DependencyContainer.GetInstance<ViewGuestReviewsService>();

        private NavigationService navigationService;

        private ObservableCollection<AccommodationReservationReviewDTO> _dataGridViewGuestReviewsItemSource { get; set; }
        public ObservableCollection<AccommodationReservationReviewDTO> DataGridViewGuestReviewsItemSource
        {
            get { return _dataGridViewGuestReviewsItemSource; }
            set { _dataGridViewGuestReviewsItemSource = value; OnPropertyChanged(nameof(DataGridViewGuestReviewsItemSource)); }
        }


        public ObservableCollection<AccommodationReservationReviewDTO> MyData { get; set; }

        public ViewGuestReviewsViewModel(NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoadData();
        }

        public string NumberToStars(int number)
        {
            string res = "";
            if(number == 1)
                res = "★☆☆☆☆";
            else if(number == 2)
                res = "★★☆☆☆";
            else if (number == 3)
                res = "★★★☆☆";
            else if (number == 4)
                res = "★★★★☆";
            else if (number == 5)
                res = "★★★★★";


            return res;
        }

        public void LoadData()
        {
            MyData = new ObservableCollection<AccommodationReservationReviewDTO>();

            // HashSet za praćenje već dodanih rezervacija
            HashSet<int> addedReservationIds = new HashSet<int>();

            var allReviews = _viewGuestReviewService.GetAllReservationReviews();
            var currentOwnerReviews = allReviews.Where(review => review.OwnerId == UserSession.Instance.Id);

            foreach (var userReview in currentOwnerReviews)
            {
                //poklapa se owner i 
                var matchingReview = currentOwnerReviews.FirstOrDefault(review => review.AccommodationReservationId == userReview.AccommodationReservationId && review.Direction == false && userReview.Direction == true);
                if (matchingReview != null && !addedReservationIds.Contains(matchingReview.AccommodationReservationId))
                {



                    addedReservationIds.Add(matchingReview.AccommodationReservationId);

                    var accommodationName = _viewGuestReviewService.GetAccommodationName(userReview.AccommodationId);
                    var guest = _viewGuestReviewService.GetGuest(matchingReview.GuestId);
                    var guestName = $"{guest.FirstName} {guest.LastName}";
                    var _cleanlines = NumberToStars((int)matchingReview.Cleanliness);
                    var _correctnes = NumberToStars((int)matchingReview.Correctness);
                    
                    AccommodationReservationReviewDTO reviewDTO = new AccommodationReservationReviewDTO(
                                                                        accommodationName,
                                                                        guestName,

                                                                        _cleanlines,
                                                                        _correctnes,
                      matchingReview.Comment);


                    MyData.Add(reviewDTO);




                }
            }


            DataGridViewGuestReviewsItemSource = MyData;
        }

        
    }


}

