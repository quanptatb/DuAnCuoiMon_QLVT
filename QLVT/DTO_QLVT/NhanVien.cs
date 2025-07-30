using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLVT
{
    public class NhanVien
    {
        public string NhanVienID { get; set; }
        public string HoTen { get; set; }
        public string ChucVu { get; set; }
        public string SoDienThoai { get; set; }
        public string email { get; set; }
        public string MatKhau {  get; set; }
        public string GhiChu {  get; set; }
        public bool VaiTro { get; set; }
        public bool TinhTrang { get; set; }

        public string VaiTroText => VaiTro ? "Quản Lý" : "Nhân Viên";
        public string TinhTrangText => TinhTrang ? "Đang Hoạt Động" : "Tạm Ngưng";
    }
}
