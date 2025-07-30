using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLVT
{
    public class Voucher
    {
        public string KhuyenMaiID { get; set; }
        public string TenKhuyenMai { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int GiamGiaPhanTram { get; set; }
        public string GhiChu { get; set; }
    }
}
