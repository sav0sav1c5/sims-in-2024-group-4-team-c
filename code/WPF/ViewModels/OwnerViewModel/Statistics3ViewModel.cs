using BookingApp.DependencyInjection;
using BookingApp.DTOs;
using BookingApp.Services.OwnerServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModel
{
    public class Statistics3ViewModel:ViewModelBase
    {
        private NavigationService navigationService;
        private StatisticMonthService _statisticMonthService = DependencyContainer.GetInstance<StatisticMonthService>();

        public ObservableCollection<StatisticDTO> MyData { get; set; }

        public ObservableCollection<int> comboBox { get; set; }
        private ObservableCollection<int> _comboBoxYearItemSource;
        public ObservableCollection<int> ComboBoxYearItemSource
        {
            get { return _comboBoxYearItemSource; }
            set
            {
                _comboBoxYearItemSource = value;
                OnPropertyChanged(nameof(ComboBoxYearItemSource));
            }
        }

        private ObservableCollection<int> _comboBoxMonths;
        public ObservableCollection<int> comboBoxMonths
        {
            get { return _comboBoxMonths; }
            set
            {
                _comboBoxMonths = value;
                OnPropertyChanged(nameof(comboBoxMonths));
            }
        }
        private ObservableCollection<StatisticDTO> _dataGridStatistics2ItemSource { get; set; }
        public ObservableCollection<StatisticDTO> DataGridStatistics2ItemSource
        {
            get { return _dataGridStatistics2ItemSource; }
            set
            {
                _dataGridStatistics2ItemSource = value;
                OnPropertyChanged(nameof(DataGridStatistics2ItemSource));
            }
        }

        public Statistics3ViewModel(NavigationService _navigationService, int accommodationId, int year)
        {
            navigationService = _navigationService;
            LoadData(accommodationId,year);
            
        }

        public void LoadData(int accommodationId, int year)
        {
            MyData = new ObservableCollection<StatisticDTO>();
            var months = _statisticMonthService.getAllMonths();
            foreach (var month in months)
            {
                int AccommodationReservations = _statisticMonthService.GetAccommodationReservationByYearAndMonth(year, month, accommodationId);
                int ModifiedReservations = _statisticMonthService.GetModifiedReservationByYearAndMonth(year, month, accommodationId);
                int CanceledReservations = _statisticMonthService.GetCanceledReservationByYearAndMonth(year, month, accommodationId);
                int RenovationRequests = _statisticMonthService.GetRenovationRequestsByYearAndMonth(year, month, accommodationId);
                StatisticDTO statisticDTO = new StatisticDTO(year, month,AccommodationReservations, CanceledReservations, ModifiedReservations, RenovationRequests);
                
                
                
                MyData.Add(statisticDTO);
            }

            DataGridStatistics2ItemSource = MyData;
        }
    }
}
