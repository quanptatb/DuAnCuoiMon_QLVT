using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace DAL_QLVT
{
    public class DALChat
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
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

        // Tìm theo Mã loại vật tư
        public static DataTable TimVatTuTheoLoai(string loaiID)
        {
            string query = @"SELECT * FROM VatTu WHERE LoaiVatTuID = @loaiID OR RIGHT(LoaiVatTuID, 3) = @duoi";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@loaiid", loaiID);
                da.SelectCommand.Parameters.AddWithValue("@duoi", loaiID.Length > 3 ? loaiID.Substring(loaiID.Length - 3) : loaiID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Tìm theo Mã nhà cung cấp
        public static DataTable TimVatTuTheoNCC(string nccID)
        {
            string query = @"SELECT * FROM VatTu WHERE NhaCungCapID = @nccID OR RIGHT(NhaCungCapID, 3) = @duoi";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@nccid", nccID);
                da.SelectCommand.Parameters.AddWithValue("@duoi", nccID.Length > 3 ? nccID.Substring(nccID.Length - 3) : nccID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Tìm theo Mã trạng thái
        public static DataTable TimVatTuTheoTrangThai(string ttID)
        {
            string query = @"SELECT * FROM VatTu WHERE TrangThaiID = @ttID OR RIGHT(TrangThaiID, 3) = @duoi";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@ttid", ttID);
                da.SelectCommand.Parameters.AddWithValue("@duoi", ttID.Length > 3 ? ttID.Substring(ttID.Length - 3) : ttID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
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
        public static DataTable TimVatTuTheoBeHonHoacBangSoLuong(int soLuong)
        {
            string query = @"SELECT * FROM VatTu WHERE SoLuongTon <= @sl";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@sl", soLuong);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        //========================================================================TÌM KHÁCH HÀNG=========================================================================================
        public static DataTable TimKhachHangTheoMa(string maKH)
        {
            string query = @"SELECT * FROM KhachHang WHERE KhachHangID = @ma OR RIGHT(KhachHangID, 3) = @duoi";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@ma", maKH);
                da.SelectCommand.Parameters.AddWithValue("@duoi", maKH.Length > 3 ? maKH.Substring(maKH.Length - 3) : maKH);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable TimKhachHangTheoTen(string ten)
        {
            return TimKiemDonGian("SELECT * FROM KhachHang WHERE HoTen LIKE @kw", ten);
        }

        public static DataTable TimKhachHangTheoSoDienThoai(string sdt)
        {
            return TimKiemDonGian("SELECT * FROM KhachHang WHERE SoDienThoai LIKE @kw", sdt);
        }

        public static DataTable TimKhachHangTheoEmail(string email)
        {
            return TimKiemDonGian("SELECT * FROM KhachHang WHERE Email LIKE @kw", email);
        }

        public static DataTable TimKhachHangTheoDiaChi(string dc)
        {
            return TimKiemDonGian("SELECT * FROM KhachHang WHERE DiaChi LIKE @kw", dc);
        }

        public static DataTable TimKhachHangTheoNgayTao(DateTime ngay)
        {
            string query = @"SELECT * FROM KhachHang WHERE CAST(NgayTao AS DATE) = @ngay";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@ngay", ngay);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        //=======================================================================TÌM HÓA ĐƠN=========================================================================================
        public static DataTable TimHoaDonTheoMa(string maHD)
        {
            string query = @"SELECT * FROM HoaDon WHERE HoaDonID = @ma OR RIGHT(HoaDonID, 3) = @duoi";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@ma", maHD);
                da.SelectCommand.Parameters.AddWithValue("@duoi", maHD.Length > 3 ? maHD.Substring(maHD.Length - 3) : maHD);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public static DataTable TimHoaDonTheoMaDonHang(string donHangID)
        {
            string query = @"SELECT * FROM HoaDon WHERE DonHangID = @donHangID OR RIGHT(DonHangID, 3) = @duoi";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@donHangID", donHangID);
                da.SelectCommand.Parameters.AddWithValue("@duoi", donHangID.Length > 3 ? donHangID.Substring(donHangID.Length - 3) : donHangID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public static DataTable TimHoaDonTheoTongTien(decimal tongTien)
        {
            string query = @"SELECT * FROM HoaDon WHERE TongTien = @tongTien";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@tongTien", tongTien);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public static DataTable TimHoaDonTheoNgayThanhToan(DateTime ngay)
        {
            string query = @"SELECT * FROM HoaDon WHERE CAST(NgayThanhToan AS DATE) = @ngay";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@ngay", ngay);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public static DataTable TimHoaDonTheoPhuongThucThanhToan(string phuongThuc)
        {
            string query = @"SELECT * FROM HoaDon WHERE PhuongThucThanhToan LIKE @kw";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@kw", "%" + phuongThuc + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }


        //=======================================================================HÀM TÁCH=========================================================================================
        private static DataTable TimKiemDonGian(string query, string tuKhoa)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@kw", "%" + tuKhoa + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }


    }
}
