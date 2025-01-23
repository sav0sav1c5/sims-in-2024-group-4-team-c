using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using BookingApp.Services.GuideServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class VoucherUseRepository : IVoucherUseRepository
    {
        private List<VoucherUse> voucherUses;
        private readonly IDataHandler<VoucherUse> voucherUseDataHandler;

        public VoucherUseRepository()
        {
            voucherUseDataHandler = new VoucherUseDataHandler();
            voucherUses = voucherUseDataHandler.GetAll().ToList();
        }

        public void Delete(VoucherUse voucherUse)
        {
            voucherUseDataHandler.Delete(voucherUse);
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<VoucherUse> GetAll()
        {
            return voucherUses;
        }

        public VoucherUse? GetById(int id)
        {
            throw new NotImplementedException();
        }
        public VoucherUse GetByVoucherId(int id)
        {
            return voucherUses.Where(vu => vu.VoucherId == id).FirstOrDefault();
        }

        public VoucherUse Save(VoucherUse voucherUse)
        {
            voucherUses.Add(voucherUse);
            return voucherUseDataHandler.SaveOneEntity(voucherUse);
        }

        public void Update(VoucherUse entity)
        {
            throw new NotImplementedException();
        }

        void IBaseRepository<VoucherUse>.Save(VoucherUse entity)
        {
            throw new NotImplementedException();
        }
    }
}
