using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLVT;
using DAL_QLVT;


namespace BLL_QLVT
{
    public class BLLHoaDon
    {
        DALHoaDon dalHoaDon = new DALHoaDon();

        public List<HoaDon> GetListHoaDon(string maHD)
        {
            return dalHoaDon.selectAll(maHD);
        }
        public string InsertHoaDon(HoaDon hd)
        {
            try
            {
                hd.HoaDonID = dalHoaDon.generateMaHD();
                if (string.IsNullOrEmpty(hd.HoaDonID))
                {
                    return "Mã hóa đơn không hợp lệ.";
                }

                dalHoaDon.insertHoaDon(hd);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Thêm mới không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
        public string UpdateHoaDon(HoaDon hd)
        {
            try
            {
                if (string.IsNullOrEmpty(hd.HoaDonID))
                {
                    return "Mã hóa đơn không hợp lệ.";
                }

                dalHoaDon.updateHoaDon(hd);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Cập nhật không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
        public string DeleteHoaDon(string maHD)
        {
            try
            {
                if (string.IsNullOrEmpty(maHD))
                {
                    return "Mã hóa đơn không hợp lệ.";
                }

                dalHoaDon.deleteHoaDon(maHD);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Xóa không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
      
         public List<HoaDon> SearchHoaDon(string maHD)
        {
            if (string.IsNullOrWhiteSpace(maHD))
                return new List<HoaDon>();

            return dalHoaDon.SearchHoaDon(maHD);
        }

    }
}


