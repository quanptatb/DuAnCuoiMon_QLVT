using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLVT;
using DTO_QLVT;
namespace BLL_QLVT
{
    public class BUSLoaiVatTu
    {
        DALLoaiVatTu dalLoaiVatTu = new DALLoaiVatTu();

        public List<LoaiVatTu> GetLoaiVatTuList()
        {
            return dalLoaiVatTu.selectAll();
        }


        public string InsertLoaiVatTu(LoaiVatTu loaiVT)
        {
            try
            {
                loaiVT.LoaiVatTuID = dalLoaiVatTu.generateMaLoaiVatTu();
                if (string.IsNullOrEmpty(loaiVT.LoaiVatTuID))
                {
                    return "Mã loại vật tư không hợp lệ.";
                }

                dalLoaiVatTu.insertLoaiVatTu(loaiVT);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Thêm mới không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateLoaiVatTu(LoaiVatTu loaiVT)
        {
            try
            {
                if (string.IsNullOrEmpty(loaiVT.LoaiVatTuID))
                {
                    return "Mã loại vật tư không hợp lệ.";
                }

                dalLoaiVatTu.updateLoaiVatTu(loaiVT);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Cập nhật không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteLoaiVatTu(string maloaiVT)
        {
            try
            {
                if (string.IsNullOrEmpty(maloaiVT))
                {
                    return "Mã loại vật tư không hợp lệ.";
                }

                dalLoaiVatTu.deleteLoaiVatTu(maloaiVT);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Xóa không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
        public List<LoaiVatTu> SearchLoaiVatTu(string maLVT)
        {
            if (string.IsNullOrEmpty(maLVT))
            {
                return new List<LoaiVatTu>();
            }

            else
            {
                return dalLoaiVatTu.TimKiem(maLVT);
            }
        }
    }
}
