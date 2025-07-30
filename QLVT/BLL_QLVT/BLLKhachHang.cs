using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLVT;
using DTO_QLVT;

namespace BLL_QLVT
{
    public class BLLKhachHang
    {
        DALKhachHang dalKhachHang = new DALKhachHang();

        public List<KhachHang> GetKhachHangList()
        {
            return dalKhachHang.selectAll();
        }
        public string InsertKhachHang(KhachHang kh)
        {
            try
            {
                kh.KhachHangID = dalKhachHang.generateMaKhachHang();
                if (string.IsNullOrEmpty(kh.KhachHangID))
                {
                    return "Mã Khách hàng không hợp lệ.";
                }
                if (dalKhachHang.checkEmailExists(kh.Email))
                {
                    return "Email đã tồn tại.";
                }
                dalKhachHang.InsertKhachHang(kh);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Thêm mới không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
        public string UpdateKhachHang(KhachHang kh)
        {
            try
            {
                if (string.IsNullOrEmpty(kh.KhachHangID))
                {
                    return "Mã khách hàng không hợp lệ.";
                }

                dalKhachHang.updateKhachHang(kh);
                return string.Empty;
            }
            catch (Exception ex)
            {
         
                return "Lỗi: " + ex.Message;
            }
        }
        public string DeleteKhachHang(string maKH)
        {
            try
            {
                if (string.IsNullOrEmpty(maKH))
                {
                    return "Mã nhân viên không hợp lệ.";
                }

                dalKhachHang.deleteKhachHang(maKH);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Xóa không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
        public List<KhachHang> SearchKhachHang(string maKH)
        {
            if (string.IsNullOrEmpty(maKH))
                return new List<KhachHang>();

            return dalKhachHang.SearchKhachHang(maKH);
        }

    }
}
