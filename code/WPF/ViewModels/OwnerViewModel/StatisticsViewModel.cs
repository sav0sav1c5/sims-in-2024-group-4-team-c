using BookingApp.DependencyInjection;
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
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModel
{
    public class StatisticsViewModel:ViewModelBase
    {
        private NavigationService navigationService;
        private StatisticService _statisticService = DependencyContainer.GetInstance<StatisticService>();

        public ObservableCollection<AccommodationDTO> MyData { get; set; }
        private ObservableCollection<AccommodationDTO> _dataGridStatisticsItemSource { get; set; }
        public ObservableCollection<AccommodationDTO> DataGridStatisticsItemSource
        { get { return _dataGridStatisticsItemSource; }
          set
            {
                _dataGridStatisticsItemSource = value;
                OnPropertyChanged(nameof(DataGridStatisticsItemSource));
            }
        }

        public RelayCommand ButtonAcceptCommand { get; private set; }
        public StatisticsViewModel(NavigationService _navigationService)
        {
            navigationService = _navigationService;
            LoadData();
            ButtonAcceptCommand = new RelayCommand(AcceptCommand);
        }

        public void LoadData()
        {
            MyData = new ObservableCollection<AccommodationDTO>();
            var data = _statisticService.GetAllOwnerAccommodations(UserSession.Instance.Id);
            foreach(var element in data)
            {
                AccommodationDTO dto = new AccommodationDTO(element);
                MyData.Add(dto);
            }

            DataGridStatisticsItemSource = MyData;
        }

        private void AcceptCommand(object parameter)
        {
            if(parameter != null)
            {
                var next = (AccommodationDTO)parameter;
                Statistics2 viewStatistics2 = new Statistics2(navigationService, next);
                navigationService.Navigate(viewStatistics2);

            }
        }

    }
}
