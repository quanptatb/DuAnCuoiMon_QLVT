using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DAL_QLVT
{
    public class DALChat
    {
        private static string connectionString = @"Data Source=DESKTOP-NPO91IS\SQLEXPRESS;Initial Catalog=QuanLyVatTuXayDung;Integrated Security=True;Trust Server Certificate=True";

        public static DataTable TimNhanVien(string tuKhoa)
        {

            string query = @"
                SELECT *
                FROM NhanVien
                WHERE 
                    NhanVienID LIKE @TuKhoa
                    OR HoTen LIKE @TuKhoa
                    OR SoDienThoai LIKE @TuKhoa
                    OR Email LIKE @TuKhoa
                    OR 
                    (
                        (@TuKhoaLower = N'quản lý' AND VaiTro = 1) 
                        OR (@TuKhoaLower = N'nhân viên' AND VaiTro = 0)
                    )
                    OR 
                    (
                        (@TuKhoaLower = N'đang hoạt động' AND TinhTrang = 1) 
                        OR (@TuKhoaLower = N'tạm ngưng' AND TinhTrang = 0)
                    )
            ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");
                da.SelectCommand.Parameters.AddWithValue("@TuKhoaLower", tuKhoa.Trim().ToLower());
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable ThongKeVatTuTonKhoThap(int soLuongToiThieu)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ThongKeVatTuTonKhoThap", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SoLuongToiThieu", soLuongToiThieu);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable ThongKeDoanhThuTheoKhachHang(string khachHangID, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ThongKeDoanhThuTheoKhachHang", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@KhachHangID", khachHangID);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                cmd.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable ThongKeDonHangChuaHoanTat(string nhanVienID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ThongKeDonHangChuaHoanTat", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        // Tìm theo Mã vật tư (chính xác hoặc theo đuôi)
        public static DataTable TimVatTuTheoMa(string ma)
        {
            string query = @"SELECT * FROM VatTu WHERE VatTuID = @ma OR RIGHT(VatTuID, 3) = @duoi";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@ma", ma);
                da.SelectCommand.Parameters.AddWithValue("@duoi", ma.Length > 3 ? ma.Substring(ma.Length - 3) : ma);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }


        // Tìm theo Tên vật tư
        public static DataTable TimVatTuTheoTen(string ten)
        {
            string query = @"SELECT * FROM VatTu WHERE TenVatTu LIKE @kw";
            return TimKiemDonGian(query, ten);
        }

        // Tìm theo Đơn giá
        public static DataTable TimVatTuTheoDonGia(decimal gia)
        {
            string query = @"SELECT * FROM VatTu WHERE DonGia = @gia";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@gia", gia);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Tìm theo Số lượng tồn
        public static DataTable TimVatTuTheoSoLuong(int soLuong)
        {
            string query = @"SELECT * FROM VatTu WHERE SoLuongTon = @sl";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@sl", soLuong);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable TimVatTuTheoLonHonHoacBangSoLuong(int soLuong)
        {
            string query = @"SELECT * FROM VatTu WHERE SoLuongTon >= @sl";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@sl", soLuong);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Tìm theo Mã loại vật tư
        public static DataTable TimVatTuTheoLoai(string loaiID)
        {
            string query = @"SELECT * FROM VatTu WHERE LoaiVatTuID = @kw";
            return TimKiemDonGian(query, loaiID);
        }

        // Tìm theo Mã nhà cung cấp
        public static DataTable TimVatTuTheoNCC(string nccID)
        {
            string query = @"SELECT * FROM VatTu WHERE NhaCungCapID = @kw";
            return TimKiemDonGian(query, nccID);
        }

        // Tìm theo Mã trạng thái
        public static DataTable TimVatTuTheoTrangThai(string ttID)
        {
            string query = @"SELECT * FROM VatTu WHERE TrangThaiID = @kw";
            return TimKiemDonGian(query, ttID);
        }

        // Tìm theo Ngày tạo (định dạng chuỗi)
        public static DataTable TimVatTuTheoNgayTao(DateTime ngay)
        {
            string query = @"SELECT * FROM VatTu WHERE CAST(NgayTao AS DATE) = @ngay";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@ngay", ngay);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }


        // Tìm theo Mã khuyến mãi
        public static DataTable TimVatTuTheoMaKhuyenMai(string kmID)
        {
            string query = @"SELECT * FROM VatTu WHERE KhuyenMaiID = @kw";
            return TimKiemDonGian(query, kmID);
        }


        private static DataTable TimKiemDonGian(string query, string tuKhoa)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@kw", "%" + tuKhoa.ToUpper() + "%"); // ép về in hoa
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }


    }
}
