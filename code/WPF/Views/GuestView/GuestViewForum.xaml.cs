using BookingApp.DTOs;
using BookingApp.WPF.ViewModels.GuestViewModel;
using System;
using System.Collections.Generic;
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

namespace BookingApp.WPF.Views.GuestView
{
    /// <summary>
    /// Interaction logic for GuestViewForum.xaml
    /// </summary>
    public partial class GuestViewForum : Page
    {
        #region Data
        private GuestViewForumViewModel _guestViewForumViewModel;
        #endregion

        public GuestViewForum(GuestDTO loggedGuest, NavigationService navigationService, ForumDTO forumDTO)
        {
            InitializeComponent();
            _guestViewForumViewModel = new GuestViewForumViewModel(loggedGuest, navigationService, forumDTO);
            this.DataContext = _guestViewForumViewModel;
        }
    }
}
