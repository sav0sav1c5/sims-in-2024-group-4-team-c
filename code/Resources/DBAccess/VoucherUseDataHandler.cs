using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    public class VoucherUseDataHandler : IDataHandler<VoucherUse>
    {
        private readonly DataContext dataContext = new DataContext();
        public void Delete(VoucherUse entity)
        {
            dataContext.VoucherUses.Remove(entity);
        }

        public void DeleteById(int entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VoucherUse> GetAll()
        {
            return dataContext.VoucherUses.ToList();
        }

        public void Save(IEnumerable<VoucherUse> entities)
        {
            dataContext.VoucherUses.AddRange(entities);
            dataContext.SaveChanges();
        }

        public VoucherUse SaveOneEntity(VoucherUse entity)
        {
            dataContext.VoucherUses.AddRange(entity);
            dataContext.SaveChanges();

            return entity;
        }

        public void Update(VoucherUse entity)
        {
            throw new NotImplementedException();
        }

        public VoucherUse UpdateEntity(VoucherUse entity)
        {
            throw new NotImplementedException();
        }
    }
}
