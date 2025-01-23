using BookingApp.Domain.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace BookingApp.WPF.ViewModels.TouristViewModel
{
    public class TutorialViewModel : ViewModelBase
    {
        private Uri currentVideoUri;
        public RelayCommand BackToHomeCommand { get; private set; }
        public RelayCommand ReservationTutorialCommand { get; private set; }
        public RelayCommand StandardTourTutorialCommand { get; private set; }
        public RelayCommand ComplexTourTutorialCommand { get; private set; }
        public RelayCommand RateToursTutorialCommand { get; private set; }
        public RelayCommand VoucherTutorialCommand { get; private set; }
        public RelayCommand PlayCommand { get; private set; }
        public RelayCommand PauseCommand { get; private set; }
        public RelayCommand Forward10SecondsCommand { get; private set; }
        public RelayCommand Backward10SecondsCommand { get; private set; }

        public NavigationService NavigationService { get; set; }
        private readonly Tourist LoggedTourist;
        private readonly MediaElement VideoPlayer;

        private bool isVideoPlaying = false;

        public TutorialViewModel(Tourist tourist, NavigationService navigationService, MediaElement videoPlayer)
        {
            LoggedTourist = tourist;
            NavigationService = navigationService;
            VideoPlayer = videoPlayer ?? throw new ArgumentNullException(nameof(videoPlayer));

            BackToHomeCommand = new RelayCommand(BackToHome);
            ReservationTutorialCommand = new RelayCommand(PlayReservationTutorial);
            StandardTourTutorialCommand = new RelayCommand(PlayStandardTourTutorial);
            ComplexTourTutorialCommand = new RelayCommand(PlayComplexTourTutorial);
            RateToursTutorialCommand = new RelayCommand(PlayRateToursTutorial);
            VoucherTutorialCommand = new RelayCommand(PlayVoucherTutorial);

            PlayCommand = new RelayCommand(PlayVideo, CanPlay);
            PauseCommand = new RelayCommand(PauseVideo, CanPause);
            Forward10SecondsCommand = new RelayCommand(Forward10Seconds, CanSeek);
            Backward10SecondsCommand = new RelayCommand(Backward10Seconds, CanSeek);
        }

        private void BackToHome(object obj)
        {
            NavigationService.GoBack();
        }

        private void PlayReservationTutorial(object obj)
        {
            Play("C:\\Users\\ws232\\Videos\\creating_tour_reservation_tutorial.mp4");
        }

        private void PlayStandardTourTutorial(object obj)
        {
            Play("C:\\Users\\ws232\\Videos\\creating_of_standard_tour_request_tutorial.mp4");
        }

        private void PlayComplexTourTutorial(object obj)
        {
            Play("C:\\Users\\ws232\\Videos\\creating_of_complex_tour_request_tutorial.mp4");
        }

        private void PlayRateToursTutorial(object obj)
        {
            Play("C:\\Users\\ws232\\Videos\\generating_voucher_report_tutorial.mp4");
        }

        private void PlayVoucherTutorial(object obj)
        {
            Play("C:\\Users\\ws232\\Videos\\using_of_vouchers_tutorial.mp4");
        }

        private void Play(object obj)
        {
            if (VideoPlayer != null)
            {
                VideoPlayer.Source = new Uri(obj.ToString());
                VideoPlayer.Play();
                isVideoPlaying = true;
            }
        }

        private void PlayVideo(object obj)
        {
            if (VideoPlayer != null)
            {
                VideoPlayer.Play();
                isVideoPlaying = true;
            }
        }

        private bool CanPlay(object obj)
        {
            return !isVideoPlaying && VideoPlayer != null;
        }

        private void PauseVideo(object obj)
        {
            if (VideoPlayer != null)
            {
                VideoPlayer.Pause();
                isVideoPlaying = false;
            }
        }

        private bool CanPause(object obj)
        {
            return isVideoPlaying && VideoPlayer != null;
        }

        private void Forward10Seconds(object obj)
        {
            if (VideoPlayer != null)
            {
                TimeSpan newPosition = VideoPlayer.Position + TimeSpan.FromSeconds(10);
                if (newPosition < VideoPlayer.NaturalDuration.TimeSpan)
                    VideoPlayer.Position = newPosition;
            }
        }

        private void Backward10Seconds(object obj)
        {
            if (VideoPlayer != null)
            {
                TimeSpan newPosition = VideoPlayer.Position - TimeSpan.FromSeconds(10);
                if (newPosition > TimeSpan.Zero)
                    VideoPlayer.Position = newPosition;
                else
                    VideoPlayer.Position = TimeSpan.Zero;
            }
        }

        private bool CanSeek(object obj)
        {
            return VideoPlayer != null;
        }
    }
}
