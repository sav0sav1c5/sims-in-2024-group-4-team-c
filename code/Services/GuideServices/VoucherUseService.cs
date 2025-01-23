using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.GuideServices
{
    public class VoucherUseService
    {
        private readonly IVoucherUseRepository _voucherUseRepository = DependencyContainer.GetInstance<IVoucherUseRepository>();

        public VoucherUseService() { }

        public List<VoucherUse> GetAllVoucherUses()
        {
            return _voucherUseRepository.GetAll();
        }
        public VoucherUse GetVoucherUseByVoucherId(int id)
        {
            return _voucherUseRepository.GetByVoucherId(id);
        }

        public void DeleteVoucherUse(VoucherUse voucherUse)
        {
            _voucherUseRepository.Delete(voucherUse);
        }

        public VoucherUse SaveVoucherUse(VoucherUse voucherUse)
        {
            return _voucherUseRepository.Save(voucherUse);
        }
    }
}
