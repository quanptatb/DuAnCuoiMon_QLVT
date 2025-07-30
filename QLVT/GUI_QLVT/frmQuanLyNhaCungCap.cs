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

namespace GUI_QLVT
{
    public partial class frmQuanLyNhaCungCap : Form
    {
        public frmQuanLyNhaCungCap()
        {
            InitializeComponent();
        }

        private void frmQuanLyNhaCungCap_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachNhaCungCap();
        }

        private void LoadDanhSachNhaCungCap()
        {
            BUSNhaCungCap bUSNhaCungCap = new BUSNhaCungCap();
            dgvDanhSachNhaCungCap.DataSource = null;
            dgvDanhSachNhaCungCap.DataSource = bUSNhaCungCap.GetNhaCungCapList();
            dgvDanhSachNhaCungCap.RowTemplate.Height = 40;
        }

        private void dgvDanhSachNhaCungCap_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDanhSachNhaCungCap.Rows[e.RowIndex];
            // Đổ dữ liệu vào các ô nhập liệu trên form
            txtMaNhaCungCap.Text = row.Cells["NhaCungCapID"].Value.ToString();
            txtTenNhaCungCap.Text = row.Cells["TenNhaCungCap"].Value.ToString();
            txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            dtpNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();

            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtMaNhaCungCap.Enabled = false;
        }

        private void ClearForm()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            txtMaNhaCungCap.Clear();
            txtTenNhaCungCap.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            dtpNgayTao.Value = DateTime.Now;
            txtGhiChu.Clear();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.");
                return;
            }

            BUSNhaCungCap bus = new BUSNhaCungCap();
            var ketQua = bus.TimKiemNhaCungCap(tuKhoa);

            if (ketQua != null && ketQua.Count > 0)
            {
                dgvDanhSachNhaCungCap.DataSource = null;
                dgvDanhSachNhaCungCap.DataSource = ketQua;
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên phù hợp.");
                dgvDanhSachNhaCungCap.DataSource = null;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNhaCungCap = txtMaNhaCungCap.Text.Trim();
            string tenNhaCungCap = txtTenNhaCungCap.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            DateTime ngayTao = dtpNgayTao.Value;
            string ghiChu = txtGhiChu.Text.Trim();
            if (string.IsNullOrEmpty(tenNhaCungCap) || string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(diaChi)
                || dtpNgayTao.Value > DateTime.Now || string.IsNullOrEmpty(ghiChu))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nhà cung cấp và kiểm tra ngày tạo.");
                return;
            }

            NhaCungCap ncc = new NhaCungCap
            {
                NhaCungCapID = maNhaCungCap,
                TenNhaCungCap = tenNhaCungCap,
                SoDienThoai = soDienThoai,
                Email = email,
                DiaChi = diaChi,
                NgayTao = ngayTao,
                GhiChu = ghiChu
            };
            BUSNhaCungCap bus = new BUSNhaCungCap();
            string result = bus.InsertNhaCungCap(ncc);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachNhaCungCap();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNhaCungCap = txtMaNhaCungCap.Text.Trim();
            string name = txtTenNhaCungCap.Text.Trim();
            if (string.IsNullOrEmpty(maNhaCungCap))
            {
                if (dgvDanhSachNhaCungCap.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDanhSachNhaCungCap.SelectedRows[0];
                    maNhaCungCap = selectedRow.Cells["NhaCungCapID"].Value.ToString();
                    name = selectedRow.Cells["TenNhaCungCap"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một nhà cung cấp để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(maNhaCungCap))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhà cung cấp {maNhaCungCap} - {name}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BUSNhaCungCap bus = new BUSNhaCungCap();
                string kq = bus.DeleteNhaCungCap(maNhaCungCap);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin nhà cung cấp {maNhaCungCap} - {name} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhSachNhaCungCap();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNhaCungCap = txtMaNhaCungCap.Text.Trim();
            string tenNhaCungCap = txtTenNhaCungCap.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            DateTime ngayTao = dtpNgayTao.Value;
            string ghiChu = txtGhiChu.Text.Trim();


            if (string.IsNullOrEmpty(tenNhaCungCap) || string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(diaChi)
               || dtpNgayTao.Value > DateTime.Now || string.IsNullOrEmpty(ghiChu))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nhà cung cấp và kiểm tra ngày tạo.");
                return;
            }
            NhaCungCap ncc = new NhaCungCap
            {
                NhaCungCapID = maNhaCungCap,
                TenNhaCungCap = tenNhaCungCap,
                SoDienThoai = soDienThoai,
                Email = email,
                DiaChi = diaChi,
                NgayTao = ngayTao,
                GhiChu = ghiChu
            };
            BUSNhaCungCap bus = new BUSNhaCungCap();
            string result = bus.UpdateNhaCungCap(ncc);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachNhaCungCap();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachNhaCungCap();
        }

        private void dgvDanhSachNhaCungCap_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDanhSachNhaCungCap.Rows[e.RowIndex];
            // Đổ dữ liệu vào các ô nhập liệu trên form
            txtMaNhaCungCap.Text = row.Cells["NhaCungCapID"].Value.ToString();
            txtTenNhaCungCap.Text = row.Cells["TenNhaCungCap"].Value.ToString();
            txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            dtpNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();

            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtMaNhaCungCap.Enabled = false;
        }
    }
}
