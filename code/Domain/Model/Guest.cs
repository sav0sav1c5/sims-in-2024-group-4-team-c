using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Guest : User
    {
        /// <summary>
        /// The number of discount coupons the guest receives when he becomes a super guest
        /// </summary>
        public const int MaxSuperGuestDiscountCouponCount = 5;
        /// <summary>
        /// The minimum number of reservations needed in the previous year for a guest to become a super guest
        /// </summary>
        public const int MinReservationsNeededForSuperGuest = 10;
        [Required]
        public bool IsSuperGuest { get; set; } = false;
        [Required]
        public int SuperGuestDiscountCouponCount { get; set; } = 0;


        //public virtual ICollection<AccommodationReservation> AccommodationReservations { get; } = new List<AccommodationReservation>();
        public Guest()
        {
            UserType = UserType.Guest;
        }
        public Guest(string username, string password, string firstName, string lastName, string email)
        {
            UserType = UserType.Guest;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void Copy(Guest guest)
        {
            Username = guest.Username;
            Password = guest.Password;
            FirstName = guest.FirstName;
            LastName = guest.LastName;
            Email = guest.Email;
            IsSuperGuest = guest.IsSuperGuest;
            SuperGuestDiscountCouponCount = guest.SuperGuestDiscountCouponCount;
        }

        public void SetToSuperGuest()
        {
            IsSuperGuest = true;
            SuperGuestDiscountCouponCount = MaxSuperGuestDiscountCouponCount;
        }
        public void SetToNormalGuest()
        {
            IsSuperGuest = false;
            SuperGuestDiscountCouponCount = 0;
        }
    }
}
