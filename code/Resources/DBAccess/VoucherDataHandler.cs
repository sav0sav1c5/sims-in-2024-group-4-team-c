using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    public class VoucherDataHandler : IDataHandler<Voucher>
    {
        private readonly DataContext dataContext = new DataContext();

        public IEnumerable<Voucher> GetAll()
        {
            return dataContext.Vouchers.ToList();
        }
        public void Save(IEnumerable<Voucher> entities)
        {
            dataContext.Vouchers.AddRange(entities);
            dataContext.SaveChanges();
        }

        public Voucher SaveOneEntity(Voucher entity)
        {
            dataContext.Vouchers.AddRange(entity);
            dataContext.SaveChanges();

            return entity;
        }

        public void Delete(Voucher entity)
        {
            dataContext.Vouchers.Remove(entity);
        }

        public void DeleteById(int entityId)
        {
            throw new NotImplementedException();
        }

        public void Update(Voucher entity)
        {
            throw new NotImplementedException();
        }

        public Voucher UpdateEntity(Voucher entity)
        {
            throw new NotImplementedException();
        }
    }
}
