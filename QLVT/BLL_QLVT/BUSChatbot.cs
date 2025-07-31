using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL_QLVT;

namespace BLL_QLVT
{
    public class BUSChatbot
    {
        public static DataTable LastResult = null;

        public static string HandleMessage(string input)
        {
            input = input.ToLower().Trim();
            if (string.IsNullOrEmpty(input)) return "Bạn chưa nhập nội dung. Gõ 'help' để xem hướng dẫn.";

            if (input.Contains("chào") || input.Contains("hello"))
                return "Chào bạn! Mình có thể giúp gì?";

            if (input == "help" || input == "hướng dẫn")
            {
                return
                "🤖 Mình có thể giúp bạn với các chức năng sau:\n\n" +
                "🔍 **Tìm kiếm vật tư**:\n" +
                "   • theo mã: `tìm vật tư có mã là 001`\n" +
                "   • theo tên: `tìm vật tư tên xi măng`\n" +
                "   • theo giá: `tìm vật tư có giá 50000`\n" +
                "   • theo số lượng: `tìm vật tư có số lượng 100`\n" +
                "   • theo ngày tạo: `tìm vật tư có ngày tạo 25/07/2025`\n" +
                "   • theo mã loại: `tìm vật tư mã loại 003`\n" +
                "   • theo mã nhà cung cấp: `tìm vật tư mã nhà cung cấp 001`\n" +
                "   • theo mã trạng thái: `tìm vật tư trạng thái 001`\n" +
                "   • theo mã khuyến mãi: `tìm vật tư mã khuyến mãi 002`\n\n" +
                "👨‍💼 **Tìm kiếm nhân viên**:\n" +
                "   • `tìm nhân viên tên Hùng`, `tìm nhân viên có vai trò quản lý`\n" +
                "   • `tìm nhân viên tạm ngưng`, `tìm nhân viên có email abc@xyz`\n\n" +
                "📊 **Thống kê**:\n" +
                "   • Tồn kho thấp: `thống kê tồn kho thấp 100`\n" +
                "   • Doanh thu khách hàng: `thống kê doanh thu khách hàng KH001 từ 01/06/2025 đến 30/06/2025`\n" +
                "   • Đơn hàng chưa hoàn tất: `thống kê đơn hàng chưa hoàn tất nhân viên NV002`\n\n" +
                "💡 Gõ `help` bất cứ lúc nào để xem lại hướng dẫn.";
            }


            // ======= VẬT TƯ =======
            if (input.Contains("tìm vật tư") || Regex.IsMatch(input, @"\bvt\d{1,}", RegexOptions.IgnoreCase) || Regex.IsMatch(input, @"\bncc\d{1,}", RegexOptions.IgnoreCase) || Regex.IsMatch(input, @"\blvt\d{1,}", RegexOptions.IgnoreCase) || Regex.IsMatch(input, @"\btt\d{1,}", RegexOptions.IgnoreCase))
            {
                return XuLyTimVatTu(input);
            }
            if (input.Contains("tìm khách hàng") || Regex.IsMatch(input, @"kh\d{1,}", RegexOptions.IgnoreCase))
            {
                return XuLyTimKhachHang(input);
            }
            if (input.Contains("tìm hóa đơn") || Regex.IsMatch(input, @"hd\d{1,}", RegexOptions.IgnoreCase) || Regex.IsMatch(input, @"dh\d{1,}", RegexOptions.IgnoreCase))
            {
                return XuLyTimHoaDon(input);
            }

            // ======= NHÂN VIÊN =======
            if (input.Contains("tìm nhân viên"))
            {
                string tuKhoa = input;
                string[] xoa = { "tìm", "nhân viên", "có mã", "có tên", "mã", "tên", "email", "số điện thoại", "vai trò", "tình trạng", "là", "cần tìm", "với", "có" };
                foreach (var s in xoa) tuKhoa = tuKhoa.Replace(s, "");
                tuKhoa = tuKhoa.Trim();

                if (string.IsNullOrEmpty(tuKhoa))
                    return "Bạn cần nhập mã, tên, sđt, email, vai trò hoặc tình trạng.";

                LastResult = DALChat.TimNhanVien(tuKhoa);
                return $"Kết quả tìm nhân viên với từ khóa '{tuKhoa}':";
            }

            // ======= TỒN KHO THẤP =======
            if (input.Contains("thống kê tồn kho thấp"))
            {
                Match match = Regex.Match(input, @"thống kê tồn kho thấp (\d+)");
                if (match.Success)
                {
                    int soLuongToiThieu = int.Parse(match.Groups[1].Value);
                    LastResult = DALChat.ThongKeVatTuTonKhoThap(soLuongToiThieu);
                    return $"Danh sách vật tư tồn kho dưới {soLuongToiThieu}:";
                }
                return "Bạn cần nhập: thống kê tồn kho thấp [số lượng]";
            }

            // ======= DOANH THU KHÁCH HÀNG =======
            if (input.Contains("thống kê doanh thu khách hàng"))
            {
                Match match = Regex.Match(input, @"khách hàng (\w+) từ (\d{1,2}/\d{1,2}/\d{4}) đến (\d{1,2}/\d{1,2}/\d{4})");
                if (match.Success)
                {
                    string khID = match.Groups[1].Value;
                    DateTime ngayBD = DateTime.ParseExact(match.Groups[2].Value, "d/M/yyyy", null);
                    DateTime ngayKT = DateTime.ParseExact(match.Groups[3].Value, "d/M/yyyy", null);

                    if (ngayBD > ngayKT)
                        return "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.";

                    LastResult = DALChat.ThongKeDoanhThuTheoKhachHang(khID, ngayBD, ngayKT);
                    return $"Thống kê doanh thu khách hàng {khID} từ {ngayBD:dd/MM/yyyy} đến {ngayKT:dd/MM/yyyy}:";
                }
                return "Cú pháp: thống kê doanh thu khách hàng KHxxx từ dd/MM/yyyy đến dd/MM/yyyy";
            }

            // ======= ĐƠN HÀNG CHƯA HOÀN TẤT =======
            if (input.Contains("thống kê đơn hàng chưa hoàn tất"))
            {
                Match match = Regex.Match(input, @"nhân viên (\w+)");
                if (match.Success)
                {
                    string nvID = match.Groups[1].Value;
                    LastResult = DALChat.ThongKeDonHangChuaHoanTat(nvID);
                    return $"Thống kê đơn hàng chưa hoàn tất của nhân viên {nvID}:";
                }
                return "Bạn cần nhập: thống kê đơn hàng chưa hoàn tất nhân viên NVxxx";
            }

            return "Mình không hiểu yêu cầu của bạn. Gõ 'help' để xem hướng dẫn.";
        }

        // ============================== HÀM TÁCH RIÊNG: TÌM VẬT TƯ ==============================
        private static string XuLyTimVatTu(string input)
        {
            string raw = input.ToLower();
            string tuKhoa = input.Replace("tìm vật tư", "", StringComparison.OrdinalIgnoreCase).Trim();

            // Ưu tiên bắt các biểu thức cụ thể (giá, số lượng, ngày tạo)
            Match matchGia = Regex.Match(raw, @"giá\s*(\d+)");
            Match matchSoLuong = Regex.Match(raw, @"số lượng\s*(\d+)");
            Match matchSoLuongLHHB = Regex.Match(raw, @"số lượng lớn hơn\s*(\d+)");
            Match matchSoLuongBHHB = Regex.Match(raw, @"số lượng bé hơn\s*(\d+)");
            Match matchNgay = Regex.Match(raw, @"ngày tạo\s*(\d{1,2}/\d{1,2}/\d{4})");

            if (matchGia.Success && decimal.TryParse(matchGia.Groups[1].Value, out decimal gia))
            {
                LastResult = DALChat.TimVatTuTheoDonGia(gia);
                return $"Kết quả tìm vật tư theo giá: {gia}";
            }

            if (matchSoLuong.Success && int.TryParse(matchSoLuong.Groups[1].Value, out int sl))
            {
                LastResult = DALChat.TimVatTuTheoSoLuong(sl);
                return $"Kết quả tìm vật tư theo số lượng: {sl}";
            }
            if (matchSoLuongLHHB.Success && int.TryParse(matchSoLuongLHHB.Groups[1].Value, out int sllh))
            {
                LastResult = DALChat.TimVatTuTheoLonHonHoacBangSoLuong(sllh);
                return $"Kết quả tìm vật tư theo số lượng lớn hơn: {sllh}";
            }
            if (matchSoLuongBHHB.Success && int.TryParse(matchSoLuongBHHB.Groups[1].Value, out int slbh))
            {
                LastResult = DALChat.TimVatTuTheoBeHonHoacBangSoLuong(slbh);
                return $"Kết quả tìm vật tư theo số lượng bé hơn: {slbh}";
            }
            if (matchNgay.Success && DateTime.TryParseExact(matchNgay.Groups[1].Value, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ngay))
            {
                LastResult = DALChat.TimVatTuTheoNgayTao(ngay);
                return $"Kết quả tìm vật tư theo ngày tạo: {ngay:dd/MM/yyyy}";
            }

            // Tiền xử lý tuKhoa
            string[] xoa = { "có", "là", "cần", "tìm", "với", "của", "mã", "vật tư", "mã vật tư", "mã nhà cung cấp", "mã khuyến mãi", "tên", "loại", "trạng thái" };
            foreach (var s in xoa)
            {
                tuKhoa = tuKhoa.Replace(s, "", StringComparison.OrdinalIgnoreCase);
            }
            tuKhoa = tuKhoa.Trim();

            // Nếu chỉ nhập số → đoán định dạng
            if (Regex.IsMatch(tuKhoa, @"^\d+$"))
            {
                if (raw.Contains("mã"))
                {
                    tuKhoa = "VT" + tuKhoa.PadLeft(3, '0');
                }
                else if (raw.Contains("mã nhà cung cấp"))
                {
                    tuKhoa = "NCC" + tuKhoa.PadLeft(3, '0');
                }
                else if (raw.Contains("mã khuyến mãi"))
                {
                    tuKhoa = "KM" + tuKhoa.PadLeft(3, '0');
                }
                else if (raw.Contains("mã loại") || raw.Contains("loại"))
                {
                    tuKhoa = "LVT" + tuKhoa.PadLeft(3, '0');
                }
                else if (raw.Contains("mã trạng thái") || raw.Contains("trạng thái"))
                {
                    tuKhoa = "TT" + tuKhoa.PadLeft(3, '0');
                }
            }

            // Gọi đúng hàm DAL theo từ khóa
            if (raw.Contains("mã") || Regex.IsMatch(raw, @"\bvt\d{1,}", RegexOptions.IgnoreCase))
            {
                LastResult = DALChat.TimVatTuTheoMa(tuKhoa);
            }
            else if (raw.Contains("tên"))
            {
                LastResult = DALChat.TimVatTuTheoTen(tuKhoa);
            }
            else if (raw.Contains("mã nhà cung cấp") || Regex.IsMatch(raw, @"\bncc\d{1,}", RegexOptions.IgnoreCase))
            {
                LastResult = DALChat.TimVatTuTheoNCC(tuKhoa);
            }
            else if (raw.Contains("mã loại") || raw.Contains("loại") || Regex.IsMatch(raw, @"\blvt\d{1,}", RegexOptions.IgnoreCase))
            {
                LastResult = DALChat.TimVatTuTheoLoai(tuKhoa);
            }
            else if (raw.Contains("mã trạng thái") || raw.Contains("trạng thái") || Regex.IsMatch(raw, @"\btt\d{1,}", RegexOptions.IgnoreCase))
            {
                LastResult = DALChat.TimVatTuTheoTrangThai(tuKhoa);
            }
            else
            {
                LastResult = DALChat.TimVatTuTheoTen(tuKhoa); // fallback
            }


            return $"Kết quả tìm kiếm vật tư với từ khóa '{tuKhoa}':";
        }


        // ============================== HÀM TÁCH RIÊNG: TÌM KHÁCH HÀNG ==============================
        private static string XuLyTimKhachHang(string input)
        {
            string raw = input.ToLower();
            string tuKhoa = input.Replace("tìm khách hàng", "", StringComparison.OrdinalIgnoreCase).Trim();
            // Ưu tiên tìm theo ngày
            Match matchNgay = Regex.Match(raw, @"ngày tạo\s*(\d{1,2}/\d{1,2}/\d{4})");
            if (matchNgay.Success && DateTime.TryParseExact(matchNgay.Groups[1].Value, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ngay))
            {
                LastResult = DALChat.TimKhachHangTheoNgayTao(ngay);
                return $"Kết quả tìm khách hàng theo ngày tạo: {ngay:dd/MM/yyyy}";
            }

            // Tiền xử lý từ khóa
            string[] xoa = { "có", "mã", "tên", "email", "số điện thoại", "sđt", "ngày tạo", "địa chỉ", "là", "cần", "tìm", "với", "khách hàng" };
            foreach (var s in xoa)
            {
                tuKhoa = tuKhoa.Replace(s, "", StringComparison.OrdinalIgnoreCase);
            }
            tuKhoa = tuKhoa.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                return "❌ Bạn cần nhập mã, tên, email, sđt hoặc ngày tạo của khách hàng.";
            }
            if (Regex.IsMatch(tuKhoa, @"^\d+$"))
            {
                // Nếu chỉ nhập số → đoán định dạng
                tuKhoa = "KH" + tuKhoa.PadLeft(3, '0');
            }

            // Phân loại tìm kiếm
            if (Regex.IsMatch(raw, @"^kh\d{1,}$", RegexOptions.IgnoreCase) || raw.Contains("mã"))
            {
                LastResult = DALChat.TimKhachHangTheoMa(tuKhoa);
            }
            else if (raw.Contains("email"))
            {
                // Kiểm tra đúng định dạng Gmail
                if (Regex.IsMatch(tuKhoa, @"^[\w\.-]+@gmail\.com$", RegexOptions.IgnoreCase))
                {
                    LastResult = DALChat.TimKhachHangTheoEmail(tuKhoa);
                }
                else
                {
                    return "❌ Email không hợp lệ. Chỉ chấp nhận địa chỉ Gmail.";
                }
            }

            else if (raw.Contains("điện thoại") || raw.Contains("số điện thoại") || raw.Contains("sđt"))
            {
                if (Regex.IsMatch(tuKhoa, @"^0\d{9}$"))
                {
                    LastResult = DALChat.TimKhachHangTheoSoDienThoai(tuKhoa);
                }
                else
                {
                    return "❌ Số điện thoại không hợp lệ. Phải gồm 10 chữ số và bắt đầu bằng số 0.";
                }
            }
            else if (raw.Contains("địa chỉ"))
            {
                LastResult = DALChat.TimKhachHangTheoDiaChi(tuKhoa);
            }
            else
                LastResult = DALChat.TimKhachHangTheoTen(tuKhoa); // fallback

            return $"Kết quả tìm kiếm khách hàng với từ khóa '{tuKhoa}':";
        }

        // ============================== HÀM TÁCH RIÊNG: TÌM HÓA ĐƠN ==============================

        private static string XuLyTimHoaDon(string input)
        {
            string raw = input.ToLower();
            string tuKhoa = input.Replace("tìm hóa đơn", "", StringComparison.OrdinalIgnoreCase).Trim();
            // Ưu tiên tìm theo ngày
            Match matchTongTien = Regex.Match(raw, @"tổng tiền\s*(\d+)");
            if (matchTongTien.Success && decimal.TryParse(matchTongTien.Groups[1].Value, out decimal tongTien))
            {
                LastResult = DALChat.TimHoaDonTheoTongTien(tongTien);
                return $"Kết quả tìm hóa đơn theo tổng tiền: {tongTien}";
            }
            Match matchNgay = Regex.Match(raw, @"ngày thanh toán\s*(\d{1,2}/\d{1,2}/\d{4})");
            if (matchNgay.Success && DateTime.TryParseExact(matchNgay.Groups[1].Value, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ngay))
            {
                LastResult = DALChat.TimHoaDonTheoNgayThanhToan(ngay);
                return $"Kết quả tìm hóa đơn theo ngày thanh toán: {ngay:dd/MM/yyyy}";
            }

            // Tiền xử lý từ khóa
            string[] xoa = { "có", "mã", "tổng tiền", "phương thức thanh toán", "là", "cần", "tìm", "với", "đơn hàng" };
            foreach (var s in xoa)
            {
                tuKhoa = tuKhoa.Replace(s, "", StringComparison.OrdinalIgnoreCase);
            }
            tuKhoa = tuKhoa.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                return "❌ Bạn cần nhập mã, tên, email, sđt hoặc ngày tạo của khách hàng.";
            }
            if (Regex.IsMatch(tuKhoa, @"^\d+$"))
            {
                if (raw.Contains("mã"))
                {
                    tuKhoa = "HD" + tuKhoa.PadLeft(3, '0');
                }
                else if (raw.Contains("mã đơn hàng"))
                {
                    tuKhoa = "HD" + tuKhoa.PadLeft(3, '0');
                }
            }

            // Phân loại tìm kiếm
            if (Regex.IsMatch(raw, @"^hd\d{1,}$", RegexOptions.IgnoreCase) || raw.Contains("mã"))
            {
                LastResult = DALChat.TimHoaDonTheoMa(tuKhoa);
            }
            else if (Regex.IsMatch(raw, @"^dh\d{1,}$", RegexOptions.IgnoreCase) || raw.Contains("mã đơn hàng"))
            {
                LastResult = DALChat.TimHoaDonTheoMaDonHang(tuKhoa);
            }
            else if (raw.Contains("phương thức thanh toán"))
            {
                LastResult = DALChat.TimHoaDonTheoPhuongThucThanhToan(tuKhoa);
            }

            return $"Kết quả tìm kiếm hóa đơn với từ khóa '{tuKhoa}':";
        }
        //==========================================================================================================KẾT THÚC======================================================================================================
    }
}
