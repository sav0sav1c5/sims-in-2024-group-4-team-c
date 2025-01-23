using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    public class CurrentDateDataHandler:IDataHandler<CurrentDate>
    {
        //private DataContext dataContext = new DataContext();
        private DataContext dataContext = GlobalDataContext.DataContext;


        public void Delete(CurrentDate currentDate)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int currentDateId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CurrentDate> GetAll()
        {
            //throw new NotImplementedException();
            return dataContext.CurrentDates.ToList();
        }

        public void Save(IEnumerable<CurrentDate> entities)
        {
            throw new NotImplementedException();
        }

        public CurrentDate SaveOneEntity(CurrentDate currentDate)
        {
            dataContext.CurrentDates.Add(currentDate);
            dataContext.SaveChanges();
            return currentDate;
        }

        public void Update(CurrentDate currentDate)
        {
            throw new NotImplementedException();
        }

        public CurrentDate UpdateEntity(CurrentDate entity)
        {
            throw new NotImplementedException();
        }
    }
}
