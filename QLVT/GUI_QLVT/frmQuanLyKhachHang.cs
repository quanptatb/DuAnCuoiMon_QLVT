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

namespace GUI_QLVT
{
    public partial class frmQuanLyKhachHang : Form
    {
        public frmQuanLyKhachHang()
        {
            InitializeComponent();
        }

        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachKhachHang();
        }
        private void LoadDanhSachKhachHang()
        {
            dgvDSKH.ColumnHeadersHeight = 40;
            dgvDSKH.RowTemplate.Height = 40;
            BLLKhachHang bLLKhachHang = new BLLKhachHang();
            dgvDSKH.DataSource = null;
            dgvDSKH.DataSource = bLLKhachHang.GetKhachHangList();

        }

        private void ClearForm()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtEmail.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtGhiChu.Clear();
            dtpNgayTao.Value = DateTime.Now;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            string maNV = txtMaKH.Text.Trim();
            string hoTen = txtTenKH.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string soDienThoai = txtSDT.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();
            DateTime ngayTao = dtpNgayTao.Value;
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(ghiChu))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng.");
                return;
            }
            KhachHang kh = new KhachHang
            {
                KhachHangID = maNV,
                HoTen = hoTen,
                Email = email,
                DiaChi = diaChi,
                SoDienThoai = soDienThoai,
                GhiChu = ghiChu,
                NgayTao = ngayTao
            };
            BLLKhachHang bLL = new BLLKhachHang();
            string result = bLL.InsertKhachHang(kh);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachKhachHang();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNV = txtMaKH.Text.Trim();
            string hoTen = txtTenKH.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string soDienThoai = txtSDT.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();
            DateTime ngayTao = dtpNgayTao.Value;
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(ghiChu))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng.");
                return;
            }
            KhachHang kh = new KhachHang
            {
                KhachHangID = maNV,
                HoTen = hoTen,
                Email = email,
                DiaChi = diaChi,
                SoDienThoai = soDienThoai,
                GhiChu = ghiChu,
                NgayTao = ngayTao
            };
            BLLKhachHang bLL = new BLLKhachHang();
            string result = bLL.UpdateKhachHang(kh);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachKhachHang();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            string name = txtTenKH.Text.Trim();
            if (string.IsNullOrEmpty(maKH))
            {
                if (dgvDSKH.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDSKH.SelectedRows[0];
                    maKH = selectedRow.Cells["KhachHangID"].Value.ToString();
                    name = selectedRow.Cells["HoTen"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một Khách hàng để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa Khách hàng {maKH} - {name}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BLLKhachHang bLL = new BLLKhachHang();
                string kq = bLL.DeleteKhachHang(maKH);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin khách hàng {maKH} - {name} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhSachKhachHang();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachKhachHang();
        }

        private void dgvDSKH_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDSKH.Rows[e.RowIndex];
            // Đổ dữ liệu vào các ô nhập liệu trên form
            txtMaKH.Text = row.Cells["KhachHangID"].Value.ToString();
            txtTenKH.Text = row.Cells["HoTen"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();
            dtpNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);
            // Bật nút "Sửa"
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            // Tắt chỉnh sửa mã nhân viên
            txtMaKH.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            string maKH = txtTimKiem.Text.Trim();

            if (string.IsNullOrWhiteSpace(maKH))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BLLKhachHang bLLKhachHang = new BLLKhachHang();
            List<KhachHang> result = bLLKhachHang.SearchKhachHang(maKH);

            dgvDSKH.DataSource = null;
            dgvDSKH.DataSource = result;

            if (result.Count == 0)
            {
                MessageBox.Show($"Không tìm thấy khách hàng nào với mã: {maKH}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvDSKH_DefaultCellStyleChanged(object sender, EventArgs e)
        {
            dgvDSKH.DefaultCellStyle.SelectionForeColor = Color.Black;

        }

        private void dgvDSKH_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDSKH.Rows[e.RowIndex];
            // Đổ dữ liệu vào các ô nhập liệu trên form
            txtMaKH.Text = row.Cells["KhachHangID"].Value.ToString();
            txtTenKH.Text = row.Cells["HoTen"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();
            dtpNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);
            // Bật nút "Sửa"
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            // Tắt chỉnh sửa mã nhân viên
            txtMaKH.Enabled = false;
        }
    }
}
