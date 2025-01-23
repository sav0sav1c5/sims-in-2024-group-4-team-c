using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface IVoucherUseRepository : IBaseRepository<VoucherUse>
    {
        List<VoucherUse> GetAll();
        VoucherUse Save(VoucherUse voucherUse);
        void Delete(VoucherUse voucherUse);
        VoucherUse GetByVoucherId(int id);
    }
}
