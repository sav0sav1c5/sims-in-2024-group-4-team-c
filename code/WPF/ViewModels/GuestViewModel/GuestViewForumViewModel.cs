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
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuestViewModel
{
    public class GuestViewForumViewModel : ViewModelBase
    {
        #region Data
        public GuestDTO LoggedGuest { get; private set; }
        public NavigationService NavigationService { get; private set; }
        public ForumDTO ForumDTO { get; private set; }
        public ObservableCollection<CommentDTO> ForumComments { get; private set; } = new ObservableCollection<CommentDTO>();
        #endregion

        #region Services
        //private readonly GuestService _guestService = DependencyContainer.GetInstance<GuestService>();
        private readonly GuestForumService _guestForumService = DependencyContainer.GetInstance<GuestForumService>();
        #endregion

        #region RelayCommands
        public RelayCommand LeaveCommentCMD { get; private set; }
        public RelayCommand CloseForumCMD { get; private set; }
        #endregion

        public GuestViewForumViewModel(GuestDTO loggedGuest, NavigationService navigationService, ForumDTO forumDTO)
        {
            LoggedGuest = loggedGuest;
            NavigationService = navigationService;
            ForumDTO = forumDTO;
            foreach (Comment comment in ForumDTO.Comments)
            {
                ForumComments.Add(new CommentDTO(comment));
            }
            LeaveCommentCMD = new RelayCommand(LeaveCommentCMD_func);
            CloseForumCMD = new RelayCommand(CloseForumCMD_func);
        }
        
        public void LeaveCommentCMD_func(object paramater)
        {
            NavigationService.Navigate(new GuestPostForumCommentPage(LoggedGuest, NavigationService, ForumDTO));
        }

        public void CloseForumCMD_func(object paramater)
        {
            string errorMessage = _guestForumService.CloseForum(ForumDTO, LoggedGuest);
            if (errorMessage == string.Empty)
            {
                MessageBox.Show("Forum successfully closed!");
            }
            else
            {
                MessageBox.Show(errorMessage);
            }

            NavigationService.Navigate(new GuestSearchForumsPage(LoggedGuest, NavigationService));
        }
    }
}
