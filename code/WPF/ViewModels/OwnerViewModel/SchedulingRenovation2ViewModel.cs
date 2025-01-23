using BookingApp.DTOs;
using BookingApp.WPF.Views.OwnerView;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModel
{
    public class SchedulingRenovation2ViewModel:ViewModelBase
    {
        private NavigationService navigationService;
        private string _startDate;
        public string StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        private string _endDate;
        public string EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private int _estimatedTime;
        public int EstimatedTime
        {
            get { return _estimatedTime; }
            set
            {
                _estimatedTime = value;
                OnPropertyChanged(nameof(EstimatedTime));
            }
        }

        public RelayCommand searchRenovationCommand { get; private set; }
        public SchedulingRenovation2ViewModel(NavigationService _navigationService, AccommodationDTO accommodation)
        {
            navigationService = _navigationService;

            searchRenovationCommand = new RelayCommand((obj) => NewRenovation(obj, accommodation), CanExecute_Command);
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        private void NewRenovation(object sender,AccommodationDTO accommodation)
        {
            DateOnly startDate = DateOnly.ParseExact(StartDate,"dd-MM-yyyy",null);
            DateOnly endDate = DateOnly.ParseExact(EndDate, "dd-MM-yyyy", null);
            SchedulingRenovationDTO renovation = new SchedulingRenovationDTO(accommodation.Id, startDate, endDate, EstimatedTime, true);
            ScheadulingRenovation3 viewScheadulingRenovation3 = new ScheadulingRenovation3(navigationService, renovation);
            navigationService.Navigate(viewScheadulingRenovation3);

        }
    }
}
