using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private List<Voucher> vouchers;
        private readonly IDataHandler<Voucher> voucherDataHandler;

        public VoucherRepository()
        {
            voucherDataHandler = new VoucherDataHandler();
            vouchers = voucherDataHandler.GetAll().ToList();
        }

        public List<Voucher> GetAll()
        {
            return vouchers;
        }
        public List<Voucher> GetByTouristId(int id)
        {
            return vouchers.FindAll(tourReservation => tourReservation.TouristId == id);
        }

        public Voucher Save(Voucher voucher)
        {
            vouchers.Add(voucher);
            return voucherDataHandler.SaveOneEntity(voucher);
        }

        public void Delete(Voucher voucher)
        {
            voucherDataHandler.Delete(voucher);
        }

        public void DeleteById(int voucherId)
        {
            var voucher = vouchers.Find(v => v.Id == voucherId);
            if (voucher != null)
            {
                voucherDataHandler.DeleteById(voucherId);
                vouchers.Remove(voucher);
            }
        }

        void IBaseRepository<Voucher>.Save(Voucher entity)
        {
            throw new NotImplementedException();
        }

        public Voucher? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Voucher entity)
        {
            throw new NotImplementedException();
        }
    }
}
