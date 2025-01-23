using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.GuestServices;
using BookingApp.WPF.Views.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuestViewModel
{
    public class GuestPostForumCommentViewModel : ViewModelBase
    {
        #region Data
        public GuestDTO LoggedGuest { get; private set; }
        public NavigationService NavigationService { get; private set; }
        public ForumDTO ForumDTO { get; private set; }
        //public ObservableCollection<CommentDTO> ForumComments { get; private set; } = new ObservableCollection<CommentDTO>();
        public CommentDTO CommentDTO { get; set; } 
        private string _commentText = string.Empty;
        public string CommentText 
        {
            get => _commentText;
            set
            {
                _commentText = value;
                OnPropertyChanged(nameof(CommentText));
            }
        }
        #endregion

        #region Services
        //private readonly GuestService _guestService = DependencyContainer.GetInstance<GuestService>();
        private readonly GuestForumService _guestForumService = DependencyContainer.GetInstance<GuestForumService>();
        #endregion

        #region RelayCommands
        public RelayCommand CancelPostCMD { get; private set; }
        public RelayCommand PostCommentCMD { get; private set; }
        #endregion

        public GuestPostForumCommentViewModel(GuestDTO loggedGuest, NavigationService navigationService, ForumDTO forumDTO)
        {
            LoggedGuest = loggedGuest;
            NavigationService  = navigationService ;
            ForumDTO = forumDTO;
            CommentDTO = new CommentDTO(new Comment());
            CancelPostCMD = new RelayCommand(CancelPostCMD_func);
            PostCommentCMD = new RelayCommand(PostCommentCMD_func);
        }

        public void PostCommentCMD_func(object paramater)
        {
            CommentDTO.CommentText = CommentText;
            string errorMessage = _guestForumService.PostForumComment(ForumDTO, CommentDTO, LoggedGuest);
            if (errorMessage == string.Empty)
            {
                MessageBox.Show("Comment successfully posted");
            }
            else
            {
                MessageBox.Show(errorMessage);
            }
            this.NavigationService.Navigate(new GuestViewForum(LoggedGuest, NavigationService, ForumDTO));
        }

        public void CancelPostCMD_func(object paramater)
        {
            this.NavigationService.Navigate(new GuestViewForum(LoggedGuest, NavigationService,ForumDTO));
        }


    }
}
