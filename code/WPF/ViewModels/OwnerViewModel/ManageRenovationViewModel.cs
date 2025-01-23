using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using BookingApp.Services.OwnerServices;
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
    public class ManageRenovationViewModel:ViewModelBase
    {
        private NavigationService navigationService;
        private ManageRenovationService _manageRenovationService = DependencyContainer.GetInstance<ManageRenovationService>();
        private CurrentDateRepository currentDateRepository = new CurrentDateRepository(); 

        public ObservableCollection<ManageRenovationDTO> MyData { get; set; }
        private ObservableCollection<ManageRenovationDTO> _dataGridManageRenovationItemSource { get; set; }
        public ObservableCollection<ManageRenovationDTO> DataGridManageRenovationItemSource
        {
            get { return _dataGridManageRenovationItemSource; }
            set { _dataGridManageRenovationItemSource = value; OnPropertyChanged(nameof(DataGridManageRenovationItemSource)); }
        }
        public RelayCommand ButtonAcceptCommand { get; private set; }

        public ManageRenovationViewModel(NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoadData();
            ButtonAcceptCommand = new RelayCommand(AcceptCommand);
        }

        public void LoadData()
        {
            MyData = new ObservableCollection<ManageRenovationDTO>();
            var data = _manageRenovationService.getRenovations();
            foreach(var element in data)
            {
                ManageRenovationDTO dto = new ManageRenovationDTO(element.Id,element.AccommodationId,element.StartDate,element.EndDate,element.State,element.isCanceled);
                MyData.Add(dto);
            }
            DataGridManageRenovationItemSource = MyData;

        }

        private void AcceptCommand(object parameter)
        {
            if (parameter != null)
            {
                var data = (ManageRenovationDTO) parameter;

                Renovation renovation = _manageRenovationService.getRenovationById(data.Id);
                if(renovation.StartDate.AddDays(-5) >=  DateOnly.FromDateTime(currentDateRepository.Get().Date))
                renovation.isCanceled = true;
                _manageRenovationService.updateRenovation(renovation);
            }
            LoadData();
        }
    }
}
