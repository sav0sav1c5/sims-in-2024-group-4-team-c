using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.DTOs
{
    public class ForumDTO : ViewModelBase
    {
        #region Data
        public Forum _forum { get; private set; }
        public int Id 
        {
            get => _forum.Id;
            set
            {
                _forum.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get => _forum.Name;
            set
            {
                _forum.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Description
        {
            get => _forum.Description;
            set
            {
                _forum.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public int AuthorId
        {
            get => _forum.AuthorId;
            set
            {
                _forum.AuthorId = value;
                OnPropertyChanged(nameof(AuthorId));
            }
        }
        public string AuthorFirstName
        {
            get => _forum.Author.FirstName;
            private set
            {
                _forum.Author.FirstName = value;
                OnPropertyChanged(nameof(AuthorFirstName));
            }
        }
        public int LocationId
        {
            get => _forum.LocationId;
            set
            {
                _forum.LocationId = value;
                OnPropertyChanged(nameof(LocationId));
            }
        }
        public string Country
        {
            get => _forum.Location.Country;
            private set
            {
                _forum.Location.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }
        public string City
        {
            get => _forum.Location.Country;
            private set
            {
                _forum.Location.City = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public List<Comment> Comments
        {
            get => _forum.Comments;
            set
            {
                _forum.Comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }
        public string LocationCountry
        {
            get => _forum.Location.Country;
            set
            {
                _forum.Location.Country = value;
                OnPropertyChanged(nameof(LocationCountry));
            }
        }
        public string LocationCity
        {
            get => _forum.Location.City;
            set
            {
                _forum.Location.City = value;
                OnPropertyChanged(nameof(LocationCity));
            }
        }
        public bool IsClosed 
        {
            get => _forum.IsClosed;
            set
            {
                _forum.IsClosed = value;
                OnPropertyChanged(nameof(IsClosed));
            }
        }
        public System.Windows.Visibility IsClosedIconVisibility
        {
            get => _forum.IsClosed ? Visibility.Visible : Visibility.Collapsed;
            private set { }
        }
        public bool IsUseful
        {
            get => _forum.IsUseful;
            set
            {
                _forum.IsUseful = value;
                OnPropertyChanged(nameof(IsUseful));
            }
        }
        public System.Windows.Visibility IsUsefulIconVisibility
        {
            get => _forum.IsUseful ? Visibility.Visible : Visibility.Collapsed;
            private set { }
        }
        #endregion
        public ForumDTO( Forum forum)
        {
            _forum = forum;
        }
    }
}
