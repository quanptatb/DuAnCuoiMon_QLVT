using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_QLVT;
using DTO_QLVT;
using UTIL_QLVT;

namespace GUI_QLVT
{
    public partial class frmQLVatTu : Form
    {
        public frmQLVatTu()
        {
            InitializeComponent();
        }

        private void frmQLVatTu_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadLoaiVatTu();
            LoadDanhSachVatTu();
            LoadTrangThaiVatTu();
            LoadNhaCungCap();
        }
        private void ClearForm()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            txtMaVatTu.Clear();
            txtTenVatTu.Clear();
            txtDonGia.Clear();
            txtSoLuongTon.Clear();
            txtGhiChu.Clear();
            cboMaLoaiVatTu.SelectedIndex = -1;
            cboMaTrangThai.SelectedIndex = -1;
            cboMaNhaCungCap.SelectedIndex = -1;

        }

        private void LoadLoaiVatTu()
        {
            try
            {
                BUSLoaiVatTu bUSLoaiVatTu = new BUSLoaiVatTu();
                List<LoaiVatTu> dsLoai = bUSLoaiVatTu.GetLoaiVatTuList();
                cboMaLoaiVatTu.DataSource = dsLoai;
                cboMaLoaiVatTu.ValueMember = "LoaiVatTuID";
                cboMaLoaiVatTu.DisplayMember = "TenLoaiVatTu";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách vật tư" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachVatTu()
        {
            dgvDSQLVatTu.ColumnHeadersHeight = 40;
            dgvDSQLVatTu.RowTemplate.Height = 40;
            BUSVatTu busVatTu = new BUSVatTu();
            dgvDSQLVatTu.DataSource = null;
            List<VatTu> lstVT = busVatTu.GetVatTuList();
            dgvDSQLVatTu.DataSource = lstVT;

        }
        private void LoadTrangThaiVatTu()
        {
            try
            {
                BUSTTVatTu busTTVatTu = new BUSTTVatTu();
                List<TrangThaiVatTu> dsTT = busTTVatTu.GetTrangThaiVatTuList();
                cboMaTrangThai.DataSource = dsTT;
                cboMaTrangThai.ValueMember = "TrangThaiID";
                cboMaTrangThai.DisplayMember = "TenTrangThai";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách vật tư" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadNhaCungCap()
        {
            try
            {
                BUSNhaCungCap busNhaCungCap = new BUSNhaCungCap();
                List<NhaCungCap> dsNCC = busNhaCungCap.GetNhaCungCapList();
                cboMaNhaCungCap.DataSource = dsNCC;
                cboMaNhaCungCap.ValueMember = "NhaCungCapID";
                cboMaNhaCungCap.DisplayMember = "TenNhaCungCap";
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi khi tải danh sách nhà cung cấp" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string MaVatTu = txtMaVatTu.Text.Trim();
                string MaLoaiVatTu = cboMaLoaiVatTu.SelectedValue?.ToString();
                string tenVatTu = txtTenVatTu.Text.Trim();
                string DonGia = txtDonGia.Text.Trim();
                string SoLuongTon = txtSoLuongTon.Text.Trim();
                string MaNhaCungCap = cboMaNhaCungCap.SelectedValue?.ToString();
                DateTime NgayTao = dtpNgayTao.Value;
                string GhiChu = txtGhiChu.Text.Trim();
                string MaTrangThai = cboMaTrangThai.SelectedValue?.ToString();

                // Kiểm tra dữ liệu nhập vào
                if (string.IsNullOrEmpty(tenVatTu) || string.IsNullOrEmpty(DonGia) || string.IsNullOrEmpty(MaLoaiVatTu))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chuyển đổi đơn giá
                if (!decimal.TryParse(DonGia, out decimal donGia))
                {
                    MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!int.TryParse(SoLuongTon, out int soLuongTon))
                {
                    MessageBox.Show("Số lượng tồn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo đối tượng vật tư
                VatTu vt = new VatTu()
                {
                    VatTuID = MaVatTu,
                    LoaiVatTuID = MaLoaiVatTu,
                    TenVatTu = tenVatTu,
                    DonGia = donGia,
                    SoLuongTon = soLuongTon,
                    NhaCungCapID = MaNhaCungCap,
                    NgayTao = NgayTao,
                    GhiChu = GhiChu,
                    TrangThaiID = MaTrangThai
                };

                // Thêm vật tư vào cơ sở dữ liệu
                BUSVatTu busVatTu = new BUSVatTu();
                busVatTu.InsertVatTu(vt);

                MessageBox.Show("Thêm vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới form sau khi thêm
                ClearForm();
                LoadDanhSachVatTu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string subject = "Thông báo: Thêm vật tư mới";
            string body = $"Xin chào, nhân viên {AuthUtil.user.email} vừa thêm vật tư mới vào lúc {DateTime.Now:HH:mm dd/MM/yyyy}";
            string tenNhanVien = AuthUtil.user.HoTen;

            Email.GuiEmail(subject, body, tenNhanVien);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string MaVatTu = txtMaVatTu.Text.Trim();
                string MaLoaiVatTu = cboMaLoaiVatTu.SelectedValue?.ToString();
                string tenVatTu = txtTenVatTu.Text.Trim();
                string DonGia = txtDonGia.Text.Trim();
                string SoLuongTon = txtSoLuongTon.Text.Trim();
                string MaNhaCungCap = cboMaNhaCungCap.SelectedValue?.ToString();
                DateTime NgayTao = dtpNgayTao.Value;
                string GhiChu = txtGhiChu.Text.Trim();
                string MaTrangThai = cboMaTrangThai.SelectedValue?.ToString();

                // Kiểm tra dữ liệu nhập vào
                if (string.IsNullOrEmpty(tenVatTu) || string.IsNullOrEmpty(DonGia) || string.IsNullOrEmpty(MaLoaiVatTu))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chuyển đổi đơn giá
                if (!decimal.TryParse(DonGia, out decimal donGia))
                {
                    MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!int.TryParse(SoLuongTon, out int soLuongTon))
                {
                    MessageBox.Show("Số lượng tồn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo đối tượng vật tư
                VatTu vt = new VatTu()
                {
                    VatTuID = MaVatTu,
                    LoaiVatTuID = MaLoaiVatTu,
                    TenVatTu = tenVatTu,
                    DonGia = donGia,
                    SoLuongTon = soLuongTon,
                    NhaCungCapID = MaNhaCungCap,
                    NgayTao = NgayTao,
                    GhiChu = GhiChu,
                    TrangThaiID = MaTrangThai
                };

                // Thêm vật tư vào cơ sở dữ liệu
                BUSVatTu busVatTu = new BUSVatTu();
                string result = busVatTu.UpdateVatTu(vt);

                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("Cập nhật thông tin thành công");
                    ClearForm();
                    LoadDanhSachVatTu();
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string subject = "Thông báo: Xóa vật tư";
            string body = $"Xin chào, nhân viên {AuthUtil.user.email} vừa xóa vật tư vào lúc {DateTime.Now:HH:mm dd/MM/yyyy}";
            string tenNhanVien = AuthUtil.user.HoTen;

            Email.GuiEmail(subject, body, tenNhanVien);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaVatTu = txtMaVatTu.Text.Trim();
            string MaLoaiVatTu = cboMaLoaiVatTu.SelectedValue?.ToString();
            string tenVatTu = txtTenVatTu.Text.Trim();
            string DonGia = txtDonGia.Text.Trim();
            string SoLuongTon = txtSoLuongTon.Text.Trim();
            string MaNhaCungCap = cboMaNhaCungCap.SelectedValue?.ToString();
            DateTime NgayTao = dtpNgayTao.Value;
            string GhiChu = txtGhiChu.Text.Trim();
            string MaTrangThai = cboMaTrangThai.SelectedValue?.ToString();

            if (string.IsNullOrEmpty(MaVatTu))
            {
                if (dgvDSQLVatTu.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDSQLVatTu.SelectedRows[0];
                    MaVatTu = selectedRow.Cells["VatTuID"].Value.ToString();
                    MaLoaiVatTu = selectedRow.Cells["LoaiVatTuID"].Value.ToString();
                    tenVatTu = selectedRow.Cells["TenVatTu"].Value.ToString();
                    DonGia = selectedRow.Cells["DonGia"].Value.ToString();
                    SoLuongTon = selectedRow.Cells["SoLuongTon"].Value.ToString();
                    MaNhaCungCap = selectedRow.Cells["NhaCungCapID"].Value.ToString();
                    NgayTao = (DateTime)selectedRow.Cells["NgayTao"].Value;
                    GhiChu = selectedRow.Cells["GhiChu"].Value.ToString();
                    MaTrangThai = selectedRow.Cells["TrangThaiID"].Value.ToString();

                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một vật tư để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                tenVatTu = txtTenVatTu.Text.Trim();
            }

            if (string.IsNullOrEmpty(MaVatTu))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa vật tư {MaVatTu} - {tenVatTu}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BUSVatTu busVatTu = new BUSVatTu();
                string kq = busVatTu.DeleteVatTu(MaVatTu);

                if (string.IsNullOrEmpty(kq))
                {

                    MessageBox.Show($"Xóa thông tin vật tư {MaVatTu} - {tenVatTu} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhSachVatTu();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            string subject = "Thông báo: Xóa vật tư";
            string body = $"Xin chào, nhân viên {AuthUtil.user.email} vừa xóa vật tư vào lúc {DateTime.Now:HH:mm dd/MM/yyyy}";
            string tenNhanVien = AuthUtil.user.HoTen;

            Email.GuiEmail(subject, body, tenNhanVien);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadLoaiVatTu();
            LoadTrangThaiVatTu();
            LoadDanhSachVatTu();
        }

        private void dgvDSQLVatTu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDSQLVatTu.Rows[e.RowIndex];
            txtMaVatTu.Text = row.Cells["VatTuID"].Value.ToString();
            cboMaLoaiVatTu.SelectedValue = row.Cells["LoaiVatTuID"].Value.ToString();
            txtTenVatTu.Text = row.Cells["TenVatTu"].Value.ToString();
            txtDonGia.Text = row.Cells["DonGia"].Value.ToString();
            txtSoLuongTon.Text = row.Cells["SoLuongTon"].Value.ToString();
            cboMaNhaCungCap.SelectedValue = row.Cells["NhaCungCapID"].Value.ToString();
            dtpNgayTao.Value = (DateTime)row.Cells["NgayTao"].Value;
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();
            cboMaTrangThai.SelectedValue = row.Cells["TrangThaiID"].Value.ToString();
            // Bật nút "Sửa"
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            BUSVatTu busVatTu = new BUSVatTu();
            string maVT = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(maVT))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<VatTu> ketQua = busVatTu.SearchVatTu(maVT);
            dgvDSQLVatTu.DataSource = ketQua;
        }
    }
}
