using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLVT;
using DTO_QLVT;

namespace BLL_QLVT
{
    public class BUSVoucher
    {
        DALVoucher dalVoucher = new DALVoucher();
        public string InsertVoucher(Voucher vc)
        {
            try
            {
                vc.KhuyenMaiID = dalVoucher.generateKhuyenMaiID();
                if (string.IsNullOrEmpty(vc.KhuyenMaiID))
                {
                    return "Mã voucher không hợp lệ.";
                }
                dalVoucher.insertVoucher(vc);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Thêm mới không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
        public List<Voucher> GetVoucherList()
        {
            return dalVoucher.selectAll();
        }

        public string DeleteVoucher(string maKhuyenMai)
        {
            try
            {
                if (string.IsNullOrEmpty(maKhuyenMai))
                {
                    return "Mã voucher không hợp lệ.";
                }

                dalVoucher.deleteVoucher(maKhuyenMai);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Xóa không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateVoucher(Voucher vc)
        {
            try
            {
                if (string.IsNullOrEmpty(vc.KhuyenMaiID))
                {
                    return "Mã voucher không hợp lệ.";
                }

                dalVoucher.updateVoucher(vc);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Cập nhật không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
        public List<Voucher> TimKiemVoucher(string tuKhoa)
        {
            return dalVoucher.GetAllVoucher()
                .Where(vc => vc.TenKhuyenMai.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                    vc.KhuyenMaiID.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
