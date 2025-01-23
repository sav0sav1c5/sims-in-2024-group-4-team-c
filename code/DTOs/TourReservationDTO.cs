using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;

namespace BookingApp.DTOs
{
    public class TourReservationDTO : ViewModelBase
    {
        private TourReservation _tourReservation;

        public int Id
        {
            get => _tourReservation.Id;
            set
            {
                _tourReservation.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int TourId
        {
            get => _tourReservation.TourId;
            set
            {
                _tourReservation.TourId = value;
                OnPropertyChanged(nameof(TourId));
            }
        }

        public TourDTO Tour { get; set; }

        public int GuideId
        {
            get => _tourReservation.GuideId;
            set
            {
                _tourReservation.GuideId = value;
                OnPropertyChanged(nameof(GuideId));
            }
        }

        public GuideDTO Guide { get; set; }

        public int TouristsNumber
        {
            get => _tourReservation.TouristsNumber;
            set
            {
                _tourReservation.TouristsNumber = value;
                OnPropertyChanged(nameof(TouristsNumber));
            }
        }

        public TourStates.Type TourState
        {
            get => _tourReservation.TourState;
            set
            {
                _tourReservation.TourState = value;
                OnPropertyChanged(nameof(TourState));
            }
        }

        public string DateAndTime
        {
            get => _tourReservation.DateAndTime.ToString("MM/dd/yyyy HH:mm");
            set
            {
                DateTime parsedDate;
                if (DateTime.TryParse(value, out parsedDate))
                {
                    _tourReservation.DateAndTime = parsedDate;
                }
                else
                {
                    // Handle the error
                    // ...
                }
                OnPropertyChanged(nameof(DateAndTime));
            }
        }

        public ICollection<TourParticipantDTO> TourParticipants { get; set; }

        public TourReservationDTO(TourReservation tourReservation)
        {
            _tourReservation = tourReservation;
            Tour = new TourDTO(tourReservation.Tour);
            Guide = new GuideDTO(tourReservation.Guide);
            TourParticipants = new List<TourParticipantDTO>();
            foreach (var participant in tourReservation.TourParticipants)
            {
                TourParticipants.Add(new TourParticipantDTO(participant));
            }
        }
    }
}
