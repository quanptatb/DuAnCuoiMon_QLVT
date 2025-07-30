using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLVT;
using DTO_QLVT;

namespace BLL_QLVT
{
    public class BLLDonHang
    {
        DALDonHang dalDonHang = new DALDonHang();

        public List<DonHang> GetListDonHang(string donHangID)
        {
            return dalDonHang.selectAll(donHangID);
        }

        public string InsertDonHang(DonHang dh)
        {
            try
            {
                dh.DonHangID = dalDonHang.gerenateDonHang();
                if (string.IsNullOrEmpty(dh.DonHangID))
                {
                    return "Mã đơn bán hàng không hợp lệ.";
                }

                dalDonHang.insertDonHang(dh);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Thêm mới không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateDonHang(DonHang pbh)
        {
            try
            {
                if (string.IsNullOrEmpty(pbh.DonHangID))
                {
                    return "Mã đơn không hợp lệ.";
                }

                dalDonHang.updateDonHang(pbh);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Cập nhật không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteDonHang(string DonHangID)
        {
            try
            {
                if (string.IsNullOrEmpty(DonHangID))
                {
                    return "Mã đơn bán hàng không hợp lệ.";
                }

                dalDonHang.deleteDonHang(DonHangID);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Xóa không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
        public List<DonHang> SearchDonHang(string maDH)
        {
            if (string.IsNullOrEmpty(maDH))
            {
                return new List<DonHang>();
            }

            else
            {
                return dalDonHang.TimKiem(maDH);
            }
        }
    }
}
