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
using static System.Net.Mime.MediaTypeNames;

namespace GUI_QLVT
{
    public partial class frmQuanLyNhanVien : Form
    {
        public frmQuanLyNhanVien()
        {
            InitializeComponent();
        }

        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachNhanVien();
        }
        private void LoadDanhSachNhanVien()
        {
            dgvDanhSachNhanVien.RowTemplate.Height = 40;
            BUSNhanVien bUSNhanVien = new BUSNhanVien();
            dgvDanhSachNhanVien.DataSource = null;
            dgvDanhSachNhanVien.DataSource = bUSNhanVien.GetNhanVienList();
        }
        private void ClearForm()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            txtMaNhanVien.Clear();
            txtHoTen.Clear();
            txtChucVu.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtMatKhau.Clear();
            txtGhiChu.Clear();
            rdoNhanVien.Checked = true;
            rdoHoatDong.Checked = true;
        }


        private void btnTim_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNhanVien.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string chucVu = txtChucVu.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string email = txtEmail.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();
            bool vaiTro;

            if (rdoNhanVien.Checked)
            {
                vaiTro = false;
            }
            else
            {
                vaiTro = true;
            }
            bool tinhTrang;

            if (rdoHoatDong.Checked)
            {
                tinhTrang = true;
            }
            else
            {
                tinhTrang = false;
            }
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(chucVu) || string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên.");
                return;
            }

            NhanVien nv = new NhanVien
            {
                NhanVienID = maNV,
                HoTen = hoTen,
                ChucVu = chucVu,
                SoDienThoai = soDienThoai,
                email = email,
                MatKhau = matKhau,
                VaiTro = vaiTro,
                TinhTrang = tinhTrang,
                GhiChu = ghiChu
            };
            BUSNhanVien bus = new BUSNhanVien();
            string result = bus.InsertNhanVien(nv);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachNhanVien();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            string name = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(maNhanVien))
            {
                if (dgvDanhSachNhanVien.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDanhSachNhanVien.SelectedRows[0];
                    maNhanVien = selectedRow.Cells["NhanVienID"].Value.ToString();
                    name = selectedRow.Cells["HoTen"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên {maNhanVien} - {name}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BUSNhanVien bus = new BUSNhanVien();
                string kq = bus.DeleteNhanVien(maNhanVien);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin nhân viên {maNhanVien} - {name} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhSachNhanVien();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNhanVien.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string chucVu = txtChucVu.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string email = txtEmail.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();
            bool vaiTro;

            if (rdoQuanLy.Checked)
            {
                vaiTro = true;
            }
            else
            {
                vaiTro = false;
            }
            bool tinhTrang;

            if (rdoHoatDong.Checked)
            {
                tinhTrang = true;
            }
            else
            {
                tinhTrang = false;
            }
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(chucVu) || string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(ghiChu))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên.");
                return;
            }

            NhanVien nv = new NhanVien
            {
                NhanVienID = maNV,
                HoTen = hoTen,
                ChucVu = chucVu,
                SoDienThoai = soDienThoai,
                email = email,
                MatKhau = matKhau,
                GhiChu = ghiChu,
                VaiTro = vaiTro,
                TinhTrang = tinhTrang
            };
            BUSNhanVien bus = new BUSNhanVien();
            string result = bus.UpdateNhanVien(nv);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachNhanVien();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachNhanVien();
        }

        private void dgvDanhSachNhanVien_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDanhSachNhanVien.Rows[e.RowIndex];
            // Đổ dữ liệu vào các ô nhập liệu trên form
            txtMaNhanVien.Text = row.Cells["NhanVienID"].Value.ToString();
            txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
            txtChucVu.Text = row.Cells["ChucVu"].Value.ToString();
            txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();

            bool vaiTro = Convert.ToBoolean(row.Cells["VaiTro"].Value);
            if (vaiTro == false)
            {
                rdoNhanVien.Checked = true;
            }
            else
            {
                rdoQuanLy.Checked = true;
            }

            bool tinhTrang = Convert.ToBoolean(row.Cells["TinhTrang"].Value);
            if (tinhTrang == false)
            {
                rdoTamNgung.Checked = true;
            }
            else
            {
                rdoHoatDong.Checked = true;
            }

            // Bật nút "Sửa"
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            // Tắt chỉnh sửa mã nhân viên
            txtMaNhanVien.Enabled = false;
        }
    }
}
