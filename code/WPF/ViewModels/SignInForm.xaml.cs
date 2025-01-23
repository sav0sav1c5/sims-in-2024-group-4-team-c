using BookingApp.Repository;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using BookingApp.View;
using BookingApp.View.GuideView;
using BookingApp.View.GuestView;

using BookingApp.View.OwnerView;

using BookingApp.Domain.Model;
using BookingApp.WPF;
using BookingApp.WPF.Views.TouristView;
//using BookingApp.View.OwnerView;



namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window, INotifyPropertyChanged
    {
        private readonly UserRepository _repository;

        private string? _username;

        public string? Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User? user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    OpenUserHome(user);
                    UserSession.Instance.Username = user.Username;
                    UserSession.Instance.Id = user.Id;
                    Close();
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
        }

        protected void OpenUserHome(User user)
        {
            switch (user.UserType)
            {
                case UserType.Owner:
                    OwnerHomeView ownerHome = new OwnerHomeView((Owner)user);
                    ownerHome.Show();
                    break;
                case UserType.Guest:
                    GuestHomeView guestHome = new GuestHomeView((Guest)user);
                    guestHome.Show();
                    break;
                case UserType.Guide:
                    GuideWindow guideWindow = new GuideWindow((Guide)user);
                    guideWindow.Show(); 
                    break;
                case UserType.Tourist:
                    TouristWindow touristWindow = new TouristWindow((Tourist)user);
                    touristWindow.Show();
                    break;
                default:
                    MessageBox.Show("Invalid user type!");
                    break;
            }
        }
    }

    public class UserSession
    {
        private static UserSession _instance;

        public static UserSession Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserSession();
                return _instance;
            }
        }

        public string Username { get; set; }
        public int Id { get; set; }

        private UserSession() { }
    }

}