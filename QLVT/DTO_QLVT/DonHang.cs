using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLVT
{
    public class DonHang
    {
        public string DonHangID { get; set; }
        public string KhachHangID { get; set; }
        public string NhanVienID { get; set; }
        public DateTime NgayDat { get; set; }
        public string GhiChu{ get; set; }
        public string TrangThai { get; set; } = "Đã giao";
    }
}
