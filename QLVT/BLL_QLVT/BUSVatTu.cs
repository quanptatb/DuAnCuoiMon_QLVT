using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLVT;
using DTO_QLVT;
using Microsoft.Data.SqlClient;
using DAL_QLVT;
using DTO_QLVT;

namespace BLL_QLVT
{
    public class BUSVatTu
    {
        DALVatTu dalVatTu = new DALVatTu();

        public List<VatTu> GetVatTuList(string trangThai = null)
        {
            return dalVatTu.selectAll(trangThai);
        }
        //selectById
        public VatTu selectById(string vatTuID)
        {
            if (string.IsNullOrEmpty(vatTuID))
            {
                return null;
            }
            return dalVatTu.selectById(vatTuID);
        }
        public string InsertVatTu(VatTu vt)
        {
            try
            {
                vt.VatTuID = dalVatTu.generateVatTuID();
                if (string.IsNullOrEmpty(vt.VatTuID))
                {
                    return "Mã vật tư không hợp lệ.";
                }

                dalVatTu.insertVatTu(vt);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Thêm mới không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateVatTu(VatTu vt)
        {
            try
            {
                if (string.IsNullOrEmpty(vt.VatTuID))
                {
                    return "Mã vật tư không hợp lệ.";
                }

                dalVatTu.updateVatTu(vt);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Cập nhật không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteVatTu(string maVT)
        {
            try
            {
                if (string.IsNullOrEmpty(maVT))
                {
                    return "Mã vật tư không hợp lệ.";
                }

                dalVatTu.deleteVatTu(maVT);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Xóa không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
        public List<VatTu> SearchVatTu(string maVT)
        {
            if (string.IsNullOrEmpty(maVT))
            {
                return new List<VatTu>();
            }

            else
            {
                return dalVatTu.TimKiem(maVT);
            }
        }
    }

}
