using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class CurrentDateRepository : ICurrentDateRepository
    {
        private CurrentDate? _currentDate;
        private readonly IDataHandler<CurrentDate>? _currentDateHandler;

        public CurrentDate? Get() => _currentDate;

        public CurrentDateRepository()
        {
            _currentDateHandler = new CurrentDateDataHandler();
            _currentDate = _currentDateHandler.GetAll().FirstOrDefault();
        }

        public void Update(CurrentDate newDate) { _currentDate = newDate; }

        public void Save(CurrentDate entity)
        {
            throw new NotImplementedException();
        }

        public CurrentDate? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<CurrentDate> GetAll()
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            return;
        }

        public void Delete(CurrentDate entity)
        {
            return;
        }


    }
}
