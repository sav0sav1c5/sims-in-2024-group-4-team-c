using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace BookingApp.DTOs
{
    public class TourDTO : ViewModelBase
    {
        private Tour _tour;

        public int Id
        {
            get => _tour.Id;
            set
            {
                _tour.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => _tour.Name;
            set
            {
                _tour.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int LocationId
        {
            get => _tour.LocationId;
            set
            {
                _tour.LocationId = value;
                OnPropertyChanged(nameof(LocationId));
            }
        }

        public string Description
        {
            get => _tour.Description;
            set
            {
                _tour.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Language
        {
            get => _tour.Language;
            set
            {
                _tour.Language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        public int MaxTouristNumber
        {
            get => _tour.MaxTouristNumber;
            set
            {
                _tour.MaxTouristNumber = value;
                OnPropertyChanged(nameof(MaxTouristNumber));
            }
        }

        public int AvailableSeats
        {
            get => _tour.AvailableSeats;
            set
            {
                _tour.AvailableSeats = value;
                OnPropertyChanged(nameof(AvailableSeats));
            }
        }

        public int GuideId
        {
            get => _tour.GuideId;
            set
            {
                _tour.GuideId = value;
                OnPropertyChanged(nameof(GuideId));
            }
        }

        public int Duration
        {
            get => _tour.Duration;
            set
            {
                _tour.Duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        public string Images
        {
            get => _tour.Images;
            set
            {
                _tour.Images = value;
                OnPropertyChanged(nameof(Images));
            }
        }
        public ICollection<Checkpoint> Checkpoints
        {
            get => _tour.Checkpoints;
            set
            {
                _tour.Checkpoints = value;
                OnPropertyChanged(nameof(Checkpoints));
            }
        }

        public TourDTO(Tour tour)
        {
            _tour = tour;
        }
    }
}
