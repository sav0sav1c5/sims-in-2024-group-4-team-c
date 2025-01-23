using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface IVoucherRepository : IBaseRepository<Voucher>
    {
        List<Voucher> GetAll();
        Voucher Save(Voucher voucher);
        void Delete(Voucher voucher);
        void DeleteById(int voucherId);
        List<Voucher> GetByTouristId(int id);
    }
}
