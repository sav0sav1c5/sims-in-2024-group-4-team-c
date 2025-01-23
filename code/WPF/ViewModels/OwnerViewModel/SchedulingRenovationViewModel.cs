using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.OwnerServices;
using BookingApp.View;
using BookingApp.WPF.Views.OwnerView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModel
{
    public class SchedulingRenovationViewModel:ViewModelBase
    {
        public ObservableCollection<AccommodationDTO> MyData { get; set; }
        private SchedulingRenovationService schedulingRenovationService = DependencyContainer.GetInstance<SchedulingRenovationService>();
        private NavigationService navigationService;
        private ObservableCollection<AccommodationDTO> _dataGridSchedulingReservationItemSource { get; set; }
        public ObservableCollection<AccommodationDTO> DataGridSchedulingReservationItemSource
        {
            get { return _dataGridSchedulingReservationItemSource; }
            set { _dataGridSchedulingReservationItemSource = value; OnPropertyChanged(nameof(DataGridSchedulingReservationItemSource)); }
        }

        private ObservableCollection<AccommodationDTO> _dataGridSchedulingReservation { get; set; }
        public ObservableCollection<AccommodationDTO> DataGridSchedulingReservation
        {
            get { return _dataGridSchedulingReservation; }
            set { _dataGridSchedulingReservation= value; OnPropertyChanged(nameof(DataGridSchedulingReservation)); }
        }
        public RelayCommand ButtonAcceptCommand { get; private set; }
        public SchedulingRenovationViewModel(NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoadData();
            ButtonAcceptCommand = new RelayCommand(AcceptCommand);
        }

        public void LoadData()
        {
            MyData = new ObservableCollection<AccommodationDTO>();
            var Data = schedulingRenovationService.LoadData(UserSession.Instance.Id);
            foreach(var element in Data)
            {
                AccommodationDTO dto = new AccommodationDTO(element);

                MyData.Add(dto);
            }
            DataGridSchedulingReservationItemSource = MyData;
        }

        private void AcceptCommand(object parameter)
        {
            if (parameter != null)
            {
                AccommodationDTO next = (AccommodationDTO)parameter;
                ScheadulingRenovation2 viewScheadulingRenovation2 = new ScheadulingRenovation2(navigationService,next);
                navigationService.Navigate(viewScheadulingRenovation2);
            }
            LoadData();
        }
    }
}
