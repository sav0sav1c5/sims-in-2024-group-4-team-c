using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class GuestDTO : ViewModelBase
    {
        #region Data
        public Guest _guest { get; private set; }

        public int Id
        {
            get => _guest.Id;
            set
            {
                _guest.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Username
        {
            get => _guest.Username;
            set
            {
                _guest.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string FirstName
        {
            get => _guest.FirstName;
            set
            {
                _guest.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => _guest.LastName;
            set
            {
                _guest.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Email
        {
            get => _guest.Email;
            set
            {
                _guest.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public bool IsSuperGuest
        {
            get => _guest.IsSuperGuest;
            set
            {
                _guest.IsSuperGuest = value;
                OnPropertyChanged(nameof(IsSuperGuest));
            }
        }
        public int SuperGuestDiscountCouponCount
        {
            get => _guest.SuperGuestDiscountCouponCount;
            set
            {
                _guest.SuperGuestDiscountCouponCount = value;
                OnPropertyChanged(nameof(SuperGuestDiscountCouponCount));
            }
        }

        #endregion

        public GuestDTO(Guest guest) 
        {
            _guest = guest;
        }
    }
}
