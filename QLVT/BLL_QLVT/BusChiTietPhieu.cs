using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLVT;
using DTO_QLVT;

namespace BLL_QLVT
{
    public class BusChiTietPhieu
    {
        DALChiTietPhieu dalChiTietPhieu = new DALChiTietPhieu();

        public List<ChiTietDonHang> GetChiTietPhieuList(string maPhieu)
        {
            return dalChiTietPhieu.selectChiTietByDonHangID(maPhieu);
        }

        public string InsertChiTietDonHang(ChiTietDonHang ct)
        {
            try
            {
                ct.ChiTietDonHangID = dalChiTietPhieu.generateChiTietID();
                if (string.IsNullOrEmpty(ct.ChiTietDonHangID))
                {
                    return "Mã chi tiết đơn không hợp lệ.";
                }

                dalChiTietPhieu.insertChiTiet(ct);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Thêm mới không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateSoLuong(ChiTietDonHang pbh)
        {
            try
            {
                if (string.IsNullOrEmpty(pbh.ChiTietDonHangID))
                {
                    return "Mã chi tiết đơn không hợp lệ.";
                }

                dalChiTietPhieu.updateSoluong(pbh);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Cập nhật không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteChiTiet(string maPhieu)
        {
            try
            {
                if (string.IsNullOrEmpty(maPhieu))
                {
                    return "Mã đơn bán hàng không hợp lệ.";
                }

                dalChiTietPhieu.deleteChiTietPhieu(maPhieu);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Xóa không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
    }
}
