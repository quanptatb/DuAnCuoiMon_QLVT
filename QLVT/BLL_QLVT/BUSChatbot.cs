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
        public static DataSet LastDataSet = null;

        public static string HandleMessage(string input)
        {
            input = input.ToLower().Trim();
            if (string.IsNullOrEmpty(input)) return "Bạn chưa nhập nội dung. Gõ 'help' để xem hướng dẫn.";

            if (input.Contains("chào") || input.Contains("hello"))
                return "Chào bạn! Mình có thể giúp gì?";

            if (input == "help" || input == "hướng dẫn")
            {
                return "Bạn có thể hỏi mình về các vấn đề:\n" +
                       "✅ Vật tư còn hàng\n" +
                       "✅ Tìm vật tư theo mã, tên, giá, số lượng, loại, NCC, trạng thái, ngày tạo, khuyến mãi\n" +
                       "✅ Tìm nhân viên theo mã, tên, sđt, email, vai trò hoặc tình trạng\n" +
                       "✅ Thống kê tồn kho thấp [số lượng]\n" +
                       "✅ Doanh thu khách hàng KHxxx từ dd/MM/yyyy đến dd/MM/yyyy\n" +
                       "✅ Đơn hàng chưa hoàn tất của nhân viên NVxxx";
            }

            // ======= VẬT TƯ =======
            if (input.Contains("tìm vật tư") || Regex.IsMatch(input, @"\bvt\d{1,}", RegexOptions.IgnoreCase))
            {
                return XuLyTimVatTu(input);
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
                return $"Kết quả tìm vật tư theo số lượng: {sllh}";
            }
            if (matchNgay.Success && DateTime.TryParseExact(matchNgay.Groups[1].Value, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ngay))
            {
                LastResult = DALChat.TimVatTuTheoNgayTao(ngay);
                return $"Kết quả tìm vật tư theo ngày tạo: {ngay:dd/MM/yyyy}";
            }

            // Tiền xử lý tuKhoa
            string[] xoa = { "có", "là", "cần", "tìm", "với", "của", "mã", "vật tư", "mã vật tư", "mã nhà cung cấp", "mã khuyến mãi", "tên", "loại", "trạng thái" };
            foreach (var s in xoa)
                tuKhoa = tuKhoa.Replace(s, "", StringComparison.OrdinalIgnoreCase);
            tuKhoa = tuKhoa.Trim();

            // Nếu chỉ nhập số → đoán định dạng
            if (Regex.IsMatch(tuKhoa, @"^\d+$"))
            {
                if (raw.Contains("mã"))
                    tuKhoa = "VT" + tuKhoa.PadLeft(3, '0');
                else if (raw.Contains("mã nhà cung cấp"))
                    tuKhoa = "NCC" + tuKhoa.PadLeft(3, '0');
                else if (raw.Contains("mã khuyến mãi"))
                    tuKhoa = "KM" + tuKhoa.PadLeft(3, '0');
                else if (raw.Contains("mã loại") || raw.Contains("loại"))
                    tuKhoa = "LVT" + tuKhoa.PadLeft(3, '0');
                else if (raw.Contains("mã trạng thái") || raw.Contains("trạng thái"))
                    tuKhoa = "TT" + tuKhoa.PadLeft(3, '0');
            }

            // Gọi đúng hàm DAL theo từ khóa
            if (raw.Contains("mã"))
                LastResult = DALChat.TimVatTuTheoMa(tuKhoa);
            else if (raw.Contains("tên"))
                LastResult = DALChat.TimVatTuTheoTen(tuKhoa);
            else if (raw.Contains("mã nhà cung cấp"))
                LastResult = DALChat.TimVatTuTheoNCC(tuKhoa);


            else if (raw.Contains("mã loại") || raw.Contains("loại"))
                LastResult = DALChat.TimVatTuTheoLoai(tuKhoa);
            else if (raw.Contains("trạng thái"))
                LastResult = DALChat.TimVatTuTheoTrangThai(tuKhoa);
            else
                LastResult = DALChat.TimVatTuTheoTen(tuKhoa); // fallback

            return $"Kết quả tìm kiếm vật tư với từ khóa '{tuKhoa}':";
        }
    }
}
