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
    public partial class frmPhieuNhapKho : Form
    {
        BUSPhieuNhapKho busPhieuNhapKho = new BUSPhieuNhapKho();
        BUSNhanVien busNhanVien = new BUSNhanVien();
        BUSNhaCungCap busNhaCungCap = new BUSNhaCungCap();
        public frmPhieuNhapKho()
        {
            InitializeComponent();
        }
        #region PHƯƠNG THỨC HỖ TRỢ
        //loadphieunhapkho
        private void LoadPhieuNhapKho()
        {
            try
            {
                dgvDSPhieuNhapKho.DataSource = busPhieuNhapKho.GetAllPhieuNhapKho();
                // Thiết lập các cột hiển thị
                dgvDSPhieuNhapKho.Columns["PhieuNhapID"].HeaderText = "Mã Phiếu Nhập";
                dgvDSPhieuNhapKho.Columns["NhaCungCapID"].HeaderText = "Nhà Cung Cấp";
                dgvDSPhieuNhapKho.Columns["NhanVienID"].HeaderText = "Nhân Viên Nhập";
                dgvDSPhieuNhapKho.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
                dgvDSPhieuNhapKho.Columns["GhiChu"].HeaderText = "Ghi Chú";
                // Cập nhật trạng thái nút
                UpdateButtonsState(isRowSelected: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phiếu nhập kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxes()
        {
            // Tải Nhà Cung Cấp
            cbxNhaCungCap.DataSource = busNhaCungCap.GetNhaCungCapList();
            cbxNhaCungCap.DisplayMember = "TenNhaCungCap";
            cbxNhaCungCap.ValueMember = "NhaCungCapID";

            // Tải Nhân Viên
            cbxNhanVien.DataSource = busNhanVien.GetNhanVienList();
            cbxNhanVien.DisplayMember = "HoTen";
            cbxNhanVien.ValueMember = "NhanVienID";
        }

        private void LamMoi()
        {
            txtPhieuNhapID.Clear();
            cbxNhaCungCap.SelectedIndex = -1;
            // Tự động chọn nhân viên đang đăng nhập
            if (AuthUtil.IsLogin())
            {
                cbxNhanVien.SelectedValue = AuthUtil.user.NhanVienID;
            }
            else
            {
                cbxNhanVien.SelectedIndex = -1;
            }
            dtpNgayNhap.Value = DateTime.Now;
            txtGhiChu.Clear();
            txtTimKiem.Clear();
            UpdateButtonsState(isRowSelected: false);
        }

        /// <summary>
        /// Cập nhật trạng thái (Enabled/Disabled) của các nút chức năng
        /// </summary>
        private void UpdateButtonsState(bool isRowSelected)
        {
            btnThem.Enabled = !isRowSelected;
            btnSua.Enabled = isRowSelected;
            btnXoa.Enabled = isRowSelected;
            // Giả sử bạn đã thêm nút này vào designer
            if (this.Controls.Find("btnXemChiTiet", true).FirstOrDefault() is Button btnView)
            {
                btnView.Enabled = isRowSelected;
            }
            btnMoi.Enabled = isRowSelected;
            txtPhieuNhapID.Enabled = false; // Mã phiếu luôn không được sửa
        }
        #endregion
        #region SỰ KIỆN
        private void frmPhieuNhapKho_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadPhieuNhapKho();
            LamMoi(); // Đặt trạng thái ban đầu cho form
        }

        private void dgvDSPhieuNhapKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSPhieuNhapKho.Rows.Count)
            {
                DataGridViewRow row = dgvDSPhieuNhapKho.Rows[e.RowIndex];
                txtPhieuNhapID.Text = row.Cells["PhieuNhapID"].Value.ToString();
                cbxNhaCungCap.SelectedValue = row.Cells["NhaCungCapID"].Value;
                cbxNhanVien.SelectedValue = row.Cells["NhanVienID"].Value;
                dtpNgayNhap.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
                txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString();

                UpdateButtonsState(isRowSelected: true);
            }
        }
        /// <summary>
        /// SỰ KIỆN THÊM MỚI (WORKFLOW ĐÃ THAY ĐỔI)
        /// Mở form chi tiết để thêm vật tư, thay vì lưu ngay lập tức.
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbxNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbxNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên nhập kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo một đối tượng PhieuNhapKho mới trong bộ nhớ
            PhieuNhapKho newPhieuNhap = new PhieuNhapKho
            {
                PhieuNhapID = busPhieuNhapKho.GenerateMaPN(),
                NhaCungCapID = cbxNhaCungCap.SelectedValue.ToString(),
                NhanVienID = cbxNhanVien.SelectedValue.ToString(),
                NgayNhap = dtpNgayNhap.Value,
                GhiChu = txtGhiChu.Text
            };

            // Mở form chi tiết và truyền đối tượng phiếu nhập vào
            frmChiTietPhieuNhapKho frmChiTiet = new frmChiTietPhieuNhapKho(newPhieuNhap);
            var dialogResult = frmChiTiet.ShowDialog();

            // Nếu người dùng lưu thành công bên form chi tiết, tải lại lưới
            if (dialogResult == DialogResult.OK)
            {
                LoadPhieuNhapKho();
                LamMoi();
            }
        }
        /// <summary>
        /// SỰ KIỆN XEM CHI TIẾT (CHỨC NĂNG MỚI)
        /// </summary>
        private void dgvDSPhieuNhapKho_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDSPhieuNhapKho.SelectedRows.Count > 0)
            {
                // Lấy thông tin phiếu nhập từ dòng đã chọn
                string phieuNhapID = dgvDSPhieuNhapKho.SelectedRows[0].Cells["PhieuNhapID"].Value.ToString();
                PhieuNhapKho selectedPhieuNhap = busPhieuNhapKho.selectById(phieuNhapID);

                if (selectedPhieuNhap != null)
                {
                    // Mở form chi tiết để xem (form chi tiết cần được lập trình để xử lý việc này)
                    frmChiTietPhieuNhapKho frmChiTiet = new frmChiTietPhieuNhapKho(selectedPhieuNhap);
                    frmChiTiet.Text = "Chi Tiết Phiếu Nhập Kho - " + phieuNhapID; // Đổi tiêu đề form
                    // Trong form chi tiết, bạn sẽ cần thêm logic để tải các chi tiết đã có
                    // và vô hiệu hóa các nút lưu nếu chỉ ở chế độ xem.
                    frmChiTiet.ShowDialog();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPhieuNhapID.Text))
                {
                    MessageBox.Show("Vui lòng chọn phiếu nhập kho để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cbxNhaCungCap.SelectedValue == null || cbxNhanVien.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ Nhà cung cấp và Nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                PhieuNhapKho phieuNhap = new PhieuNhapKho
                {
                    PhieuNhapID = txtPhieuNhapID.Text,
                    NhaCungCapID = cbxNhaCungCap.SelectedValue.ToString(),
                    NhanVienID = cbxNhanVien.SelectedValue.ToString(),
                    NgayNhap = dtpNgayNhap.Value,
                    GhiChu = txtGhiChu.Text
                };
                busPhieuNhapKho.UpdatePhieuNhapKho(phieuNhap);
                LoadPhieuNhapKho();
                LamMoi();
                MessageBox.Show("Cập nhật phiếu nhập kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật phiếu nhập kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhieuNhapID.Text))
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập kho để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu nhập này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    busPhieuNhapKho.DeletePhieuNhapKho(txtPhieuNhapID.Text);
                    LoadPhieuNhapKho();
                    LamMoi();
                    MessageBox.Show("Xóa phiếu nhập kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Cung cấp thông báo lỗi thân thiện hơn
                    string errorMessage = ex.Message.Contains("ChiTietPhieuNhap")
                        ? "Không thể xóa phiếu nhập đã có chi tiết vật tư."
                        : ex.Message;
                    MessageBox.Show("Lỗi khi xóa phiếu nhập kho: " + errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // **Khuyến nghị**: Nên tạo một phương thức tìm kiếm ở BLL/DAL để truy vấn từ CSDL
            // thay vì lọc dữ liệu đã tải về bộ nhớ, sẽ hiệu quả hơn với dữ liệu lớn.
            // Đoạn code dưới đây giữ nguyên logic cũ của bạn nhưng vẫn hoạt động được.
            try
            {
                string searchTerm = txtTimKiem.Text.Trim().ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    LoadPhieuNhapKho(); // Nếu ô tìm kiếm trống thì tải lại toàn bộ
                    return;
                }
                var results = busPhieuNhapKho.GetAllPhieuNhapKho()
                    .Where(p => (p.PhieuNhapID?.ToLower().Contains(searchTerm) ?? false) ||
                                (p.NhaCungCapID?.ToLower().Contains(searchTerm) ?? false) ||
                                (p.NhanVienID?.ToLower().Contains(searchTerm) ?? false) ||
                                (p.GhiChu?.ToLower().Contains(searchTerm) ?? false)).ToList();
                dgvDSPhieuNhapKho.DataSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm phiếu nhập kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
