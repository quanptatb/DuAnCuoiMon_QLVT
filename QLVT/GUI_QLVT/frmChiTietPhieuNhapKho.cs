using BLL_QLVT;
using DTO_QLVT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTIL_QLVT;

namespace GUI_QLVT
{
    public partial class frmChiTietPhieuNhapKho : Form
    {
        // Khai báo các lớp BLL cần dùng
        private BUSVatTu busVatTu = new BUSVatTu();
        private BUSPhieuNhapKho busPhieuNhap = new BUSPhieuNhapKho();
        private BUSChiTietPhieuNhap busChiTiet = new BUSChiTietPhieuNhap();

        private PhieuNhapKho phieuNhap;
        // Danh sách các chi tiết của phiếu nhập hiện tại (giỏ hàng)
        private List<ChiTietPhieuNhap> chiTietList = new List<ChiTietPhieuNhap>();

        // Constructor nhận vào một đối tượng PhieuNhapKho
        // Điều này cho phép form được tái sử dụng để xem/sửa phiếu nhập cũ
        public frmChiTietPhieuNhapKho(PhieuNhapKho pn)
        {
            InitializeComponent();
            this.phieuNhap = pn;
            // Gán sự kiện cho form và các button
            this.Load += frmChiTietPhieuNhapKho_Load;
            btnThemChiTiet.Click += btnThemChiTiet_Click;
            btnXoaChiTiet.Click += btnXoaChiTiet_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnTimKiem.Click += btnTimKiem_Click;
            btnThanhToan.Click += btnThanhToan_Click;
            btnHuy.Click += btnHuy_Click;
            // Sự kiện cho ô giảm giá để tính lại tiền
            txtPhanTram.TextChanged += (s, e) => UpdateThanhToan();
        }

        private void frmChiTietPhieuNhapKho_Load(object sender, EventArgs e)
        {
            SetupDataGridViews();
            LoadVatTuGrid();
            UpdatePhieuNhapInfo();
            UpdateThanhToan(); // Cập nhật các ô tiền ban đầu
        }
        /// <summary>
        /// Cài đặt cấu hình ban đầu cho các DataGridView
        /// </summary>
        private void SetupDataGridViews()
        {
            // --- Cấu hình dgvSanPham ---
            dgvSanPham.AutoGenerateColumns = false;
            dgvSanPham.Columns.Clear();
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "VatTuID", HeaderText = "Mã VT" });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenVatTu", HeaderText = "Tên Vật Tư", Width = 250 });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn Giá", DefaultCellStyle = { Format = "N0" } });
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuongTon", HeaderText = "SL Tồn" });
            dgvSanPham.ReadOnly = true;
            dgvSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // --- Cấu hình dgvChiTiet (giỏ hàng) ---
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.Columns.Clear();
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "VatTuID", HeaderText = "Mã VT" });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenVatTu", HeaderText = "Tên Vật Tư", Width = 200 });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuong", HeaderText = "Số Lượng" });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn Giá", DefaultCellStyle = { Format = "N0" } });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn { Name = "ThanhTien", HeaderText = "Thành Tiền", DefaultCellStyle = { Format = "N0" } });
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// Tải và hiển thị danh sách vật tư lên dgvSanPham
        /// </summary>
        private void LoadVatTuGrid()
        {
            dgvSanPham.DataSource = busVatTu.GetVatTuList();
        }

        /// <summary>
        /// Cập nhật thông tin phiếu nhập trên giao diện (mã, nhân viên, ngày)
        /// </summary>
        private void UpdatePhieuNhapInfo()
        {
            lbMaDon.Text = phieuNhap.PhieuNhapID;
            lbNgayDat.Text = phieuNhap.NgayNhap.ToString("dd/MM/yyyy");
            // Lấy tên nhân viên đang đăng nhập
            if (AuthUtil.IsLogin())
            {
                lbChuSoHuu.Text = AuthUtil.user.HoTen;
                phieuNhap.NhanVienID = AuthUtil.user.NhanVienID; // Gán NhanVienID cho phiếu nhập
            }
        }

        /// <summary>
        /// Tải lại và hiển thị các chi tiết trong giỏ hàng (chiTietList)
        /// </summary>
        private void RefreshChiTietGrid()
        {
            dgvChiTiet.DataSource = null;
            if (chiTietList.Any())
            {
                // Sử dụng một trick để hiển thị cả các thuộc tính không có trong DTO (Tên vật tư, Thành tiền)
                var displayList = chiTietList.Select(ct => new
                {
                    ct.VatTuID,
                    TenVatTu = busVatTu.selectById(ct.VatTuID)?.TenVatTu, // Lấy tên vật tư từ mã
                    ct.SoLuong,
                    ct.DonGia,
                    ThanhTien = ct.SoLuong * ct.DonGia
                }).ToList();
                dgvChiTiet.DataSource = displayList;
            }
        }

        /// <summary>
        /// Tính toán và cập nhật các ô tiền trong groupbox thanh toán
        /// </summary>
        private void UpdateThanhToan()
        {
            decimal tongTien = chiTietList.Sum(ct => ct.SoLuong * ct.DonGia);

            // Giao diện có các ô Giảm giá, Dịch vụ nhưng không rõ logic
            // Ở đây tạm tính giảm giá theo % và bỏ qua dịch vụ
            decimal.TryParse(txtPhanTram.Text, out decimal phanTramGiam);
            decimal giamGia = tongTien * (phanTramGiam / 100);
            decimal thanhTien = tongTien - giamGia;

            txtTong.Text = tongTien.ToString("N0");
            txtGiamGia.Text = giamGia.ToString("N0");
            txtThanhTien.Text = thanhTien.ToString("N0");
            txtDichVu.Text = "0"; // Tạm thời
        }
        #region Button Event Handlers
        private void btnThemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một vật tư để thêm vào phiếu nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvSanPham.SelectedRows[0];
            string vatTuId = selectedRow.Cells[0].Value.ToString();
            decimal donGia = Convert.ToDecimal(selectedRow.Cells[2].Value);
            int soLuongTon = Convert.ToInt32(selectedRow.Cells[3].Value);

            string soLuongStr = "1"; // Giá trị mặc định
            if (InputBox.Show("Nhập số lượng", "Nhập số lượng nhập:", ref soLuongStr) == DialogResult.OK)
            {
                if (int.TryParse(soLuongStr, out int soLuong) && soLuong > 0)
                {
                    // Kiểm tra xem vật tư đã có trong giỏ hàng chưa
                    var existingItem = chiTietList.FirstOrDefault(ct => ct.VatTuID == vatTuId);
                    if (existingItem != null)
                    {
                        // Nếu có rồi thì cộng thêm số lượng
                        existingItem.SoLuong += soLuong;
                    }
                    else
                    {
                        // Nếu chưa có thì tạo mới
                        chiTietList.Add(new ChiTietPhieuNhap
                        {
                            VatTuID = vatTuId,
                            SoLuong = soLuong,
                            DonGia = donGia
                        });
                    }

                    RefreshChiTietGrid();
                    UpdateThanhToan();
                }
                else
                {
                    MessageBox.Show("Số lượng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoaChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một chi tiết để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string vatTuId = dgvChiTiet.SelectedRows[0].Cells[0].Value.ToString();
            var itemToRemove = chiTietList.FirstOrDefault(ct => ct.VatTuID == vatTuId);
            if (itemToRemove != null)
            {
                chiTietList.Remove(itemToRemove);
                RefreshChiTietGrid();
                UpdateThanhToan();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            dgvSanPham.DataSource = busVatTu.SearchVatTu(keyword);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadVatTuGrid();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn hủy phiếu nhập này? Mọi thông tin chưa lưu sẽ bị mất.", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (!chiTietList.Any())
            {
                MessageBox.Show("Phiếu nhập không có chi tiết nào. Vui lòng thêm vật tư.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Bạn có muốn xác nhận và lưu phiếu nhập này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.No)
            {
                return;
            }

            try
            {
                // Bước 1: Lưu phiếu nhập kho (header)
                busPhieuNhap.InsertPhieuNhapKho(this.phieuNhap);

                // Bước 2: Lưu các chi tiết phiếu nhập
                foreach (var chiTiet in chiTietList)
                {
                    chiTiet.ChiTietNhapID = busChiTiet.GenerateMaCTPN(); // Tạo mã chi tiết
                    chiTiet.PhieuNhapID = this.phieuNhap.PhieuNhapID; // Gán mã phiếu nhập
                    busChiTiet.InsertChiTietPhieuNhap(chiTiet);
                }

                MessageBox.Show("Lưu phiếu nhập kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lưu phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void btnGioHang_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem giỏ hàng có rỗng không
            if (!chiTietList.Any())
            {
                MessageBox.Show("Giỏ hàng hiện đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Hỏi xác nhận trước khi xóa
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa tất cả vật tư khỏi phiếu nhập này không?",
                "Xác nhận làm trống giỏ hàng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Xóa tất cả các mục trong danh sách chi tiết (giỏ hàng)
                chiTietList.Clear();

                // Cập nhật lại giao diện
                RefreshChiTietGrid(); // Làm mới lưới chi tiết
                UpdateThanhToan();  // Cập nhật lại các ô tính tiền

                MessageBox.Show("Giỏ hàng đã được làm trống.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
