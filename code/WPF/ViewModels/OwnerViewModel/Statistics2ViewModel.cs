using BookingApp.DependencyInjection;
using BookingApp.DTOs;
using BookingApp.Services.OwnerServices;
using BookingApp.WPF.Views.OwnerView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModel
{
    public class Statistics2ViewModel:ViewModelBase
    {
        private NavigationService navigationService;
        private StatisticService _statisticService = DependencyContainer.GetInstance<StatisticService>();

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
        public RelayCommand ButtonAcceptCommand { get; private set; }
        public Statistics2ViewModel(NavigationService _navigationService, int accommodationId)
        {
            navigationService = _navigationService;
            LoadData(accommodationId);
            LoadComboBox(accommodationId);
        }

        public void LoadComboBox(int accommodationId)
        {
            comboBox = new ObservableCollection<int>();
            var years = _statisticService.getAllYears(accommodationId);
            foreach ( var year in years )
            {
                comboBox.Add(year);
            }
            ComboBoxYearItemSource = comboBox;
        }

        public void LoadData(int accommodationId)
        {
            MyData = new ObservableCollection<StatisticDTO>();
            var years = _statisticService.getAllYears(accommodationId);
            foreach(var year in years)
            {
                StatisticDTO statisticDTO = new StatisticDTO();
                statisticDTO.Year = year;
                statisticDTO.AccommodationReservations = _statisticService.GetAccommodationReservationByYear(year,accommodationId);
                statisticDTO.CanceledReservations = _statisticService.GetCanceledReservationByYear(year,accommodationId);
                statisticDTO.ModifiedReservations = _statisticService.GetModifiedReservationByYear(year,accommodationId);
                statisticDTO.RenovationRequests = _statisticService.GetRenovationRequestsByYear(year,accommodationId);
                MyData.Add(statisticDTO);
            }

            DataGridStatistics2ItemSource = MyData;
        }

        
    }
}
