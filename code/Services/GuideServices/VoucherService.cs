using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System.Collections.Generic;

namespace BookingApp.Services.GuideServices
{
    public class VoucherService
    {
        private readonly IVoucherRepository _voucherRepository = DependencyContainer.GetInstance<IVoucherRepository>();

        public VoucherService()
        {
        }

        public List<Voucher> GetAllVouchers()
        {
            return _voucherRepository.GetAll();
        }
        public List<Voucher> GetAllVouchersByTouristId(int id)
        {
            return _voucherRepository.GetByTouristId(id);
        }

        public Voucher SaveVoucher(Voucher voucher)
        {
            return _voucherRepository.Save(voucher);
        }

        public void DeleteVoucher(Voucher voucher)
        {
            _voucherRepository.Delete(voucher);
        }

        public void DeleteVoucherById(int voucherId)
        {
            _voucherRepository.DeleteById(voucherId);
        }
    }
}
