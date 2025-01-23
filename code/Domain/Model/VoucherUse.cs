using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    [Table("VoucherUse")]
    public class VoucherUse
    {
        [Key, Column(Order = 0)]
        public int VoucherId { get; set; }

        [Key, Column(Order = 1)]
        public int TourReservationId { get; set; }

        [ForeignKey("VoucherId")]
        public virtual Voucher? Voucher { get; set; }

        [ForeignKey("TourReservationId")]
        public virtual TourReservation? TourReservation { get; set; }

        public VoucherUse() { }

        public VoucherUse(int voucherId, int tourReservationId)
        {
            VoucherId = voucherId;
            TourReservationId = tourReservationId;
        }
    }
}
