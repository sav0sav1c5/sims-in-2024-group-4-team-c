using BookingApp.Domain.Model;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface IAccommodationRepository : IBaseRepository<Accommodation>
    {
        public Accommodation? GetByName(string name);

        public List<Accommodation> GetByNameLikeAndCountryAndCityAndNumberOfGuestsAndStayingPeriodAndType(string? name, string? country, string? city, int numberOfGuests, int stayingPeriod, AccommodationTypes.Type? accommodationType);

        public List<Accommodation>? GetByMaxGuest(int maxGuest);

        public List<Accommodation>? GetByLocationId(int locationId);

        public Accommodation? GetByOwnerId(int id);
    }
}
