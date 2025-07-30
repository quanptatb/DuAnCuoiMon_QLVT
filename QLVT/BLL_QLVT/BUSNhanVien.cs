using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLVT;
using DTO_QLVT;

namespace BLL_QLVT
{
    public class BUSNhanVien
    {
        DALNhanVien dalNhanVien = new DALNhanVien();
        public NhanVien DangNhap(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            return dalNhanVien.getNhanVien1(email, password);
        }
        public string InsertNhanVien(NhanVien nv)
        {
            try
            {
                nv.NhanVienID = dalNhanVien.generateMaNhanVien();
                if (string.IsNullOrEmpty(nv.NhanVienID))
                {
                    return "Mã nhân viên không hợp lệ.";
                }
                if (dalNhanVien.checkEmailExists(nv.email))
                {
                    return "Email đã tồn tại.";
                }
                dalNhanVien.insertNhanVien(nv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Thêm mới không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public List<NhanVien> TimKiemNhanVien(string tuKhoa)
        {
            return dalNhanVien.GetAllNhanVien()
                .Where(nv => nv.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                    nv.NhanVienID.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                    nv.ChucVu.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                    nv.email.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        public bool ResetMatKhau(string email, string mk)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(mk))
                {
                    return false;
                }
                dalNhanVien.ResetMatKhau(mk, email);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<NhanVien> GetNhanVienList()
        {
            return dalNhanVien.selectAll();
        }
        public string UpdateNhanVien(NhanVien nv)
        {
            try
            {
                if (string.IsNullOrEmpty(nv.NhanVienID))
                {
                    return "Mã nhân viên không hợp lệ.";
                }

                dalNhanVien.updateNhanVien(nv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Cập nhật không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
        public string DeleteNhanVien(string maNV)
        {
            try
            {
                if (string.IsNullOrEmpty(maNV))
                {
                    return "Mã nhân viên không hợp lệ.";
                }

                dalNhanVien.deleteNhanVien(maNV);
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
