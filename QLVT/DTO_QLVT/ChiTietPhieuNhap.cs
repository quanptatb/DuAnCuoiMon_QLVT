using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLVT
{
    public class ChiTietPhieuNhap
    {
        public string ChiTietNhapID { get; set; }
        public string PhieuNhapID { get; set; }
        public string VatTuID { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
    }
}
