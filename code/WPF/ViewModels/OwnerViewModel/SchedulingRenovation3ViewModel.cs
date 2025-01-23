using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Services.OwnerServices;
using BookingApp.View.OwnerView;
using BookingApp.WPF.Views.OwnerView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModel
{
    
    public class SchedulingRenovation3ViewModel:ViewModelBase
    {
        public ObservableCollection<Renovation> MyData { get; set; }
        private SchedulingRenovationService schedulingRenovationService = DependencyContainer.GetInstance<SchedulingRenovationService>();
        private NavigationService navigationService;
        private ObservableCollection<Renovation> _dataGridSchedulingReservationItemSource3 { get; set; }
        public ObservableCollection<Renovation> DataGridSchedulingReservationItemSource3
        {
            get { return _dataGridSchedulingReservationItemSource3; }
            set { _dataGridSchedulingReservationItemSource3 = value; OnPropertyChanged(nameof(DataGridSchedulingReservationItemSource3)); }
        }
        public RelayCommand ButtonAcceptCommand { get; private set; }

        public SchedulingRenovation3ViewModel(NavigationService _navigationService,SchedulingRenovationDTO schedulingRenovationDTO)
        {
            navigationService = _navigationService;
            LoadData(schedulingRenovationDTO);
            ButtonAcceptCommand = new RelayCommand(AcceptCommand);
        }

        public void LoadData(SchedulingRenovationDTO schedulingRenovationDTO)
        {
            MyData = new ObservableCollection<Renovation>();
            var Data = schedulingRenovationService.AvailableTermins(schedulingRenovationDTO);
            foreach(var element in Data)
            {
                Renovation renovation = new Renovation(element);
                MyData.Add(renovation);
            }
            DataGridSchedulingReservationItemSource3 = MyData;
        }

        private void AcceptCommand(object parameter)
        {
            if (parameter != null)
            {
                Renovation next = (Renovation)parameter;
                schedulingRenovationService.SaveRenovation(next);
                SchedulingRenovation viewScheaduleRenovation = new SchedulingRenovation(navigationService);
                navigationService.Navigate(viewScheaduleRenovation); ;
            }
            
        }
    }


}
