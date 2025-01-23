using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.OwnerView
{
    /// <summary>
    /// Interaction logic for ViewGuestReviews.xaml
    /// </summary>
    public partial class ViewGuestReviews : Page
    {

        private readonly AccommodationReservationReviewRepository accommodationReservationReviewRepository = new AccommodationReservationReviewRepository();
        private readonly GuestRepository guestRepository = new GuestRepository();
        private readonly AccommodationRepository accommodationRepository = new AccommodationRepository();
        public ObservableCollection<AccommodationReservationReviewDTO> MyData { get; set; }


        public ViewGuestReviews()
        {
            InitializeComponent();
            LoadData();
        }
        /*
        public void LoadData()
        {
            MyData = new ObservableCollection<AccommodationReservationReviewDTO>();

            List<AccommodationReservationReview> accommodationReservationReviews = accommodationReservationReviewRepository.GetAll();
            foreach(var accommodationReservationReview in accommodationReservationReviews)
            {
                if(accommodationReservationReview.OwnerId == UserSession.Instance.Id)
                {
                    foreach(var accommodationReservationReviewGuest in accommodationReservationReviews)
                    {
                        if(accommodationReservationReview.AccommodationReservationId == accommodationReservationReviewGuest.AccommodationReservationId   && accommodationReservationReviewGuest.Direction == false)
                        {
                               AccommodationReservationReviewDTO accommodationReservationReviewDTO = new AccommodationReservationReviewDTO();
                               accommodationReservationReviewDTO.AccommodationName = accommodationRepository.GetById(accommodationReservationReview.AccommodationId).Name;
                               accommodationReservationReviewDTO.GuestName = guestRepository.GetById(accommodationReservationReviewGuest.GuestId).FirstName + " " + guestRepository.GetById(accommodationReservationReviewGuest.GuestId).LastName;
                               accommodationReservationReviewDTO.Cleanliness = accommodationReservationReviewGuest.Cleanliness;
                               accommodationReservationReviewDTO.Correctness = accommodationReservationReviewGuest.Correctness;
                               accommodationReservationReviewDTO.Comment = accommodationReservationReviewGuest.Comment;

                               MyData.Add(accommodationReservationReviewDTO);
                        }
                    }
                }
            }

            DataGridViewGuestReviews.ItemsSource = MyData;
        }*/

        public void LoadData()
        {
            MyData = new ObservableCollection<AccommodationReservationReviewDTO>();

            // HashSet za praćenje već dodanih rezervacija
            HashSet<int> addedReservationIds = new HashSet<int>();

            var allReviews = accommodationReservationReviewRepository.GetAll();
            var currentOwnerReviews = allReviews.Where(review => review.OwnerId == UserSession.Instance.Id);

            foreach (var userReview in currentOwnerReviews)
            {
                //poklapa se owner i 
                var matchingReview = currentOwnerReviews.FirstOrDefault(review => review.AccommodationReservationId == userReview.AccommodationReservationId && review.Direction == false && userReview.Direction == true);
                if (matchingReview != null && !addedReservationIds.Contains(matchingReview.AccommodationReservationId))
                {

                    

                        addedReservationIds.Add(matchingReview.AccommodationReservationId);

                        var accommodationName = accommodationRepository.GetById(userReview.AccommodationId).Name;
                        var guest = guestRepository.GetById(matchingReview.GuestId);
                        var guestName = $"{guest.FirstName} {guest.LastName}";

                        var reviewDTO = new AccommodationReservationReviewDTO
                        {
                            AccommodationName = accommodationName,
                            GuestName = guestName,
                            Cleanliness = matchingReview.Cleanliness.ToString(),
                            Correctness = matchingReview.Correctness.ToString(),
                            Comment = matchingReview.Comment
                        };

                        MyData.Add(reviewDTO);
                    


                   
                }
            }

            DataGridViewGuestReviews.ItemsSource = MyData;
        }
    }

    
}
