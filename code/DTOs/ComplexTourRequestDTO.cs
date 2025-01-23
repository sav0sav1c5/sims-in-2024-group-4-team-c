using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;

namespace BookingApp.DTOs
{
    public class ComplexTourRequestDTO : ViewModelBase
    {
        private ComplexTourRequest _complexTourRequest;

        public int Id
        {
            get => _complexTourRequest.Id;
            set
            {
                _complexTourRequest.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => _complexTourRequest.Name;
            set
            {
                _complexTourRequest.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int TouristId
        {
            get => _complexTourRequest.TouristId;
            set
            {
                _complexTourRequest.TouristId = value;
                OnPropertyChanged(nameof(TouristId));
            }
        }

        public Tourist Tourist
        {
            get => _complexTourRequest.Tourist;
            set
            {
                _complexTourRequest.Tourist = value;
                OnPropertyChanged(nameof(Tourist));
            }
        }

        public ICollection<ComplexTourPartDTO> Parts { get; set; }

        public ComplexTourRequestDTO(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequest = complexTourRequest;
            Parts = new List<ComplexTourPartDTO>();
            foreach (var part in complexTourRequest.Parts)
            {
                Parts.Add(new ComplexTourPartDTO(part));
            }
        }
    }

    public class ComplexTourPartDTO : ViewModelBase
    {
        private ComplexTourPart _complexTourPart;

        public int Id
        {
            get => _complexTourPart.Id;
            set
            {
                _complexTourPart.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string TourName
        {
            get => _complexTourPart.TourName;
            set
            {
                _complexTourPart.TourName = value;
                OnPropertyChanged(nameof(TourName));
            }
        }

        public DateTime StartDate
        {
            get => _complexTourPart.StartDate;
            set
            {
                _complexTourPart.StartDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get => _complexTourPart.EndDate;
            set
            {
                _complexTourPart.EndDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public DateTime ApprovedDate
        {
            get => _complexTourPart.ApprovedDate;
            set
            {
                _complexTourPart.ApprovedDate = value;
                OnPropertyChanged(nameof(ApprovedDate));
            }
        }

        public string Country
        {
            get => _complexTourPart.Country;
            set
            {
                _complexTourPart.Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public string City
        {
            get => _complexTourPart.City;
            set
            {
                _complexTourPart.City = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public string Language
        {
            get => _complexTourPart.Language;
            set
            {
                _complexTourPart.Language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        public string Description
        {
            get => _complexTourPart.Description;
            set
            {
                _complexTourPart.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public TourRequestStates.Type TourRequestState
        {
            get => _complexTourPart.TourRequestState;
            set
            {
                _complexTourPart.TourRequestState = value;
                OnPropertyChanged(nameof(TourRequestState));
            }
        }

        public int ComplexTourRequestId
        {
            get => _complexTourPart.ComplexTourRequestId;
            set
            {
                _complexTourPart.ComplexTourRequestId = value;
                OnPropertyChanged(nameof(ComplexTourRequestId));
            }
        }

        public ICollection<TourParticipantDTO> TourParticipants { get; set; }

        public ComplexTourPartDTO(ComplexTourPart complexTourPart)
        {
            _complexTourPart = complexTourPart;
            TourParticipants = new List<TourParticipantDTO>();
            foreach (var participant in complexTourPart.TourParticipants)
            {
                TourParticipants.Add(new TourParticipantDTO(participant));
            }
        }
    }
}
