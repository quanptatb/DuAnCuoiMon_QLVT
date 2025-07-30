using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLVT
{
    public class HoaDon
    {
        public string HoaDonID { get; set; }
        public string DonHangID { get; set; }
        public decimal TongTien { get; set; }
        public DateTime NgayThanhToan { get; set; } = new DateTime();
        public string PhuongThucThanhToan { get; set; }
        public bool TrangThai { get; set; }

    }
}
