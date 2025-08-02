using DAL_QLVT;
using DTO_QLVT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLVT
{
    public class BUSPhieuNhapKho
    {
        DALPhieuNhapKho DALPhieuNhapKho = new DALPhieuNhapKho();
        public List<PhieuNhapKho> GetAllPhieuNhapKho()
        {
            try
            {
                return DALPhieuNhapKho.selectAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách phiếu nhập kho: " + ex.Message);
            }
        }
        //selectById
        public PhieuNhapKho selectById(string phieuNhapID)
        {
            try
            {
                return DALPhieuNhapKho.selectById(phieuNhapID);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy phiếu nhập kho theo ID: " + ex.Message);
            }
        }
        public string GenerateMaPN()
        {
            try
            {
                return DALPhieuNhapKho.generateMaPN();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tạo mã phiếu nhập: " + ex.Message);
            }
        }
        // Trong file BLL_QLVT/BUSPhieuNhapKho.cs

        public void InsertPhieuNhapKho(PhieuNhapKho phieuNhap)
        {
            try
            {
                // Gọi phương thức DAL, phương thức này sẽ trả về mã mới
                string newId = DALPhieuNhapKho.Insert(phieuNhap);
                if (string.IsNullOrEmpty(newId))
                {
                    throw new Exception("Không thể tạo phiếu nhập mới từ CSDL.");
                }
                // Gán lại mã mới cho đối tượng, phòng trường hợp cần dùng sau này
                phieuNhap.PhieuNhapID = newId;
            }
            catch (Exception ex)
            {
                // Ném lại lỗi để lớp GUI có thể bắt và hiển thị
                throw new Exception("Lỗi khi thêm phiếu nhập kho: " + ex.Message);
            }
        }
        public void UpdatePhieuNhapKho(PhieuNhapKho phieuNhap)
        {
            try
            {
                DALPhieuNhapKho.Update(phieuNhap);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật phiếu nhập kho: " + ex.Message);
            }
        }
        public void DeletePhieuNhapKho(string phieuNhapID)
        {
            try
            {
                DALPhieuNhapKho.Delete(phieuNhapID);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa phiếu nhập kho: " + ex.Message);
            }
        }
        public List<PhieuNhapKho> SearchPhieuNhapKho(string keyword)
        {
            try
            {
                return DALPhieuNhapKho.Search(keyword);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm phiếu nhập kho: " + ex.Message);
            }
        }
    }
}
