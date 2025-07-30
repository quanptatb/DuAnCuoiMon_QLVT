using DAL_QLVT;
using DTO_QLVT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLVT
{
    public class BUSChiTietPhieuNhap
    {
        DALChiTietPhieuNhap DALChiTietPhieuNhap = new DALChiTietPhieuNhap();
        public List<ChiTietPhieuNhap> GetAllChiTietPhieuNhap()
        {
            try
            {
                return DALChiTietPhieuNhap.SelectAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách chi tiết phiếu nhập: " + ex.Message);
            }
        }
        public string GenerateMaCTPN()
        {
            try
            {
                return DALChiTietPhieuNhap.GenerateMaCTPN();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tạo mã chi tiết phiếu nhập: " + ex.Message);
            }
        }
        public void InsertChiTietPhieuNhap(ChiTietPhieuNhap chiTietPhieuNhap)
        {
            try
            {
                DALChiTietPhieuNhap.Insert(chiTietPhieuNhap);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chi tiết phiếu nhập: " + ex.Message);
            }
        }
        public void UpdateChiTietPhieuNhap(ChiTietPhieuNhap chiTietPhieuNhap)
        {
            try
            {
                DALChiTietPhieuNhap.Update(chiTietPhieuNhap);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật chi tiết phiếu nhập: " + ex.Message);
            }
        }
        public void DeleteChiTietPhieuNhap(string chiTietNhapID)
        {
            try
            {
                DALChiTietPhieuNhap.Delete(chiTietNhapID);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa chi tiết phiếu nhập: " + ex.Message);
            }
        }
        //search
        public List<ChiTietPhieuNhap> SearchChiTietPhieuNhap(string keyword)
        {
            try
            {
                string sql = "SELECT * FROM ChiTietPhieuNhap WHERE PhieuNhapID LIKE @0 OR VatTuID LIKE @0";
                List<object> args = new List<object> { "%" + keyword + "%" };
                return DALChiTietPhieuNhap.SelectBySql(sql, args);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm chi tiết phiếu nhập: " + ex.Message);
            }
        }
    }
}
