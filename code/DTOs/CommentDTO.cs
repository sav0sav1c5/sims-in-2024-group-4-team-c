using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.DTOs
{
    public class CommentDTO : ViewModelBase
    {
        #region Data
        public Comment _comment { get; private set; }
        public int Id
        {
            get => _comment.Id;
            set
            {
                _comment.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public int ForumId
        {
            get => _comment.ForumId;
            set
            {
                _comment.ForumId = value;
                OnPropertyChanged(nameof(ForumId));
            }
        }
        public DateTime PublishedDate 
        {
            get => _comment.PublishedDate;
            set
            {
                _comment.PublishedDate = value;
                OnPropertyChanged(nameof(PublishedDate));
            }
        }
        /// <summary>
        /// Only used on the front end 
        /// </summary>
        public string PublishedDateStr
        {
            get => DateOnly.FromDateTime( _comment.PublishedDate).ToString();
            private set
            {
                //_comment.PublishedDate = value;
                OnPropertyChanged(nameof(PublishedDateStr));
            }
        }
        public bool IsOwnerComment 
        {
            get => _comment.IsOwnerComment;
            set
            {
                _comment.IsOwnerComment = value;
                OnPropertyChanged(nameof(IsOwnerComment));
            }
        }
        public string CommentText 
        {
            get => _comment.CommentText;
            set
            {
                _comment.CommentText = value;
                OnPropertyChanged(nameof(CommentText));
            }
        } 
        public int NumOfReports 
        {
            get => _comment.NumOfReports;
            set
            {
                _comment.NumOfReports = value;
                OnPropertyChanged(nameof(NumOfReports));
            }
        }
        /// <summary>
        /// Only used on the front end
        /// </summary>
        public string UserFirstName
        {
            get => _comment.User.FirstName;
            private set
            {
                _comment.User.FirstName = value;
                OnPropertyChanged(nameof(UserFirstName));
            }
        }
        public bool IsUseful
        {
            get => _comment.IsUseful;
            set
            {
                _comment.IsUseful = value;
                OnPropertyChanged(nameof(IsUseful));
            }
        }
        public int UserId
        {
            get => _comment.UserId;
            set
            {
                _comment.UserId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }
        public User? User 
        {
            get => _comment.User;
            set
            {
                _comment.User = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public System.Windows.Visibility IsUsefulIconVisibility
        {
            get => _comment.IsUseful ? Visibility.Visible : Visibility.Collapsed;
            private set { }
        }
        #endregion

        public CommentDTO(Comment comment)
        {
            _comment = comment;
        }
    }
}
