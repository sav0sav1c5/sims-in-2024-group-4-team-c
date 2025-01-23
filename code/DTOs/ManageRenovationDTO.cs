using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class ManageRenovationDTO:ViewModelBase
    {
        private Renovation _renovation;
        private Accommodation _accommodation;
        public int Id
        {
            get => _renovation.Id;
            set
            {
                _renovation.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => _accommodation.Name;
            set
            {
                _accommodation.Name = value;
                OnPropertyChanged(nameof(Name));
            }

        }

        public DateOnly StartDate
        {
            get => _renovation.StartDate;
            set
            {
                _renovation.StartDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        private IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();

        public DateOnly EndDate
        {
            get => _renovation.EndDate;
            set
            {
                _renovation.EndDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public bool State
        {
            get => _renovation.State;
            set
            {
                _renovation.State = value;
                OnPropertyChanged(nameof(State));
            }
        }

        public bool isCanceled
        {
            get => _renovation.isCanceled;
            set
            {
                _renovation.isCanceled = value;
                OnPropertyChanged(nameof(isCanceled));
            }
        }

        public ManageRenovationDTO(int _id,int _accommodationId, DateOnly _startDate, DateOnly _endDate, bool state, bool _isCanceled)
        {
            _renovation = new Renovation();
            _accommodation = new Accommodation();
            Id = _id;
            Name = _accommodationRepository.GetAll().Where(accomodation => accomodation.Id == _accommodationId).First().Name;
            StartDate = _startDate;
            EndDate = _endDate;
            State = state;
            isCanceled = _isCanceled;
        }

    }
}
