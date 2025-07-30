using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLVT;
using DTO_QLVT;

namespace BLL_QLVT
{
    public class BUSNhaCungCap
    {
        DALNhaCungCap dalNhaCungCap = new DALNhaCungCap();

        public string InsertNhaCungCap(NhaCungCap ncc)
        {
            try
            {
                ncc.NhaCungCapID = dalNhaCungCap.generateNhaCungCapID();
                if (string.IsNullOrEmpty(ncc.NhaCungCapID))
                {
                    return "Mã nhà cung cấp không hợp lệ.";
                }
                if (dalNhaCungCap.checkEmailExists(ncc.Email))
                {
                    return "Email đã tồn tại.";
                }
                dalNhaCungCap.insertNhaCungCap(ncc);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Thêm mới không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
        public List<NhaCungCap> GetNhaCungCapList()
        {
            return dalNhaCungCap.selectAll();
        }

        public string DeleteNhaCungCap(string maNhaCungCap)
        {
            try
            {
                if (string.IsNullOrEmpty(maNhaCungCap))
                {
                    return "Mã nhà cung cấp không hợp lệ.";
                }

                dalNhaCungCap.deleteNhaCungCap(maNhaCungCap);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Xóa không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateNhaCungCap(NhaCungCap ncc)
        {
            try
            {
                if (string.IsNullOrEmpty(ncc.NhaCungCapID))
                {
                    return "Mã nhà cung cấp không hợp lệ.";
                }

                dalNhaCungCap.updateNhaCungCap(ncc);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Cập nhật không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
        public List<NhaCungCap> TimKiemNhaCungCap(string tuKhoa)
        {
            return dalNhaCungCap.GetAllNhaCungCap()
                .Where(ncc => ncc.TenNhaCungCap.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                    ncc.Email.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                    ncc.NhaCungCapID.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
