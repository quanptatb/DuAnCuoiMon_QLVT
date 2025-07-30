using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLVT;
using DTO_QLVT;

namespace BLL_QLVT
{
    public class BUSTTVatTu
    {
        DALTTVatTu dalTTVT = new DALTTVatTu();

        public List<TrangThaiVatTu> GetTrangThaiVatTuList()
        {
            return dalTTVT.selectAll();
        }
        public List<TrangThaiVatTu> SearchTrangThaiVatTu(string maTTVT)
        {
            if (string.IsNullOrEmpty(maTTVT))
            {
                return new List<TrangThaiVatTu>();
            }

            else
            {
                return dalTTVT.TimKiem(maTTVT);
            }
        }

        public string InsertTrangThaiVatTu(TrangThaiVatTu TTVT)
        {
            try
            {
                TTVT.TrangThaiID = dalTTVT.generateTrangThaiID();
                if (string.IsNullOrEmpty(TTVT.TrangThaiID))
                {
                    return "Mã trạng thái vật tư không hợp lệ.";
                }

                dalTTVT.insertTrangThaiVatTu(TTVT);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Thêm mới không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateTrangThaiVatTu(TrangThaiVatTu TTVT)
        {
            try
            {
                if (string.IsNullOrEmpty(TTVT.TrangThaiID))
                {
                    return "Mã trạng thái vật tư không hợp lệ.";
                }

                dalTTVT.updateTrangThaiVatTu(TTVT);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Cập nhật không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteTrangThaiVatTu(string TTVT)
        {
            try
            {
                if (string.IsNullOrEmpty(TTVT))
                {
                    return "Mã trạng thái vật tư không hợp lệ.";
                }

                dalTTVT.deleteTrangThaiVatTu(TTVT);
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
