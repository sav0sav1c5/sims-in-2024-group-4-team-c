using BookingApp.DependencyInjection;
using BookingApp.Services.OwnerServices;
using BookingApp.WPF.Views.OwnerView;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModel
{
    public class SuperOwnerViewModel:ViewModelBase
    {
        private NavigationService _navigationService;
        private SuperOwnerService _superOwnerService = DependencyContainer.GetInstance<SuperOwnerService>();

        public string SuperOwner
        {
            get => _superOwner;
            set
            {
                if (_superOwner != value)
                {
                    _superOwner = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _superOwner;

        public SuperOwnerViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            LoadData();
        }

        public void LoadData()
        {
            if(_superOwnerService.isSuperOwner())
            {
                SuperOwner = "You are super owner";
            }else
            {
                SuperOwner = "You are not super owner";
            }
        }


    }
}
