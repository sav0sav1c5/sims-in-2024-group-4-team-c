using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface ICurrentDateRepository : IBaseRepository<CurrentDate>
    {
        public CurrentDate? Get();
    }
}
