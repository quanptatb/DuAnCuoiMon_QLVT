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
using DAL_QLVT;
using DTO_QLVT;
using UTIL_QLVT;

namespace GUI_QLVT
{
    public partial class frmDonHang : Form
    {
        private bool isLoadingTheLuuDongData = true;
        public frmDonHang()
        {
            InitializeComponent();
        }
        private void ClearForm()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            txtMaDon.Clear();
            cboNhanVien.Enabled = true;
            dtpNgayDat.Enabled = true;
            dtpNgayDat.Value = DateTime.Now;
            txtTrangThai.Clear();
            txtGhiChu.Clear();
            txtMaDon.Enabled = true;
            txtMaDon.ReadOnly = false;
        }
        private void LoadNhanVien()
        {
            try
            {
                BUSNhanVien busNhanVien = new BUSNhanVien();
                List<NhanVien> dsNhanVien = busNhanVien.GetNhanVienList();
                if (AuthUtil.user.VaiTro)
                {
                    dsNhanVien.Insert(0, new NhanVien() { NhanVienID = string.Empty, HoTen = string.Format("--Vui lòng chọn nhân viên--") });
                }
                else
                {
                    string currentID = AuthUtil.user.NhanVienID;
                    dsNhanVien = dsNhanVien.Where(x => x.NhanVienID == currentID).ToList();
                    cboNhanVien.Enabled = false;
                }
                cboNhanVien.DataSource = dsNhanVien;
                cboNhanVien.ValueMember = "NhanVienID";
                cboNhanVien.DisplayMember = "HoTen";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại sản phẩm" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadKhachHang()
        {
            BLLKhachHang bLLKhachHang = new BLLKhachHang();
            List<KhachHang> lst = bLLKhachHang.GetKhachHangList();
            lst.Insert(0, new KhachHang() { KhachHangID = string.Empty, HoTen = string.Format("--Tất Cả--") });
            cboMaKhach.DataSource = lst;
            cboMaKhach.ValueMember = "KhachHangID";
            cboMaKhach.DisplayMember = "HoTen";
            isLoadingTheLuuDongData = false;
        }

        private void LoadDSDon(string maThe)
        {
            BLLDonHang bLLDonHang = new BLLDonHang(); // Create an instance of BLLDonHang  
            List<DonHang> lst = bLLDonHang.GetListDonHang(maThe); // Use the instance to call the method  
            if (!AuthUtil.user.VaiTro)
            {
                lst = lst.Where(x => x.NhanVienID == AuthUtil.user.NhanVienID).ToList();
                cboNhanVien.Enabled = true;
            }
            dgvDSDon.DataSource = lst;

            //DataGridViewImageColumn buttonColumn = new DataGridViewImageColumn();
            //buttonColumn.Name = "ThanhToan";
            //buttonColumn.HeaderText = "Thanh Toán";
            //buttonColumn.Image = Properties.Resources.delete;
            //buttonColumn.DefaultCellStyle.BackColor = Color.LightBlue;
            //buttonColumn.DefaultCellStyle.ForeColor = Color.DarkBlue;
            //buttonColumn.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            //if (!dgvDSDon.Columns.Contains("ThanhToan"))
            //{
            //    dgvDSDon.Columns.Add(buttonColumn);
            //}
            //dgvDSDon.Columns["ThanhToan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvDSDon.RowTemplate.Height = 50;

            dgvDSDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }



        private void frmDonHang_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadKhachHang();
            LoadNhanVien();
            LoadDSDon("");
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadKhachHang();
            LoadNhanVien();
            LoadDSDon("");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maKhach = cboMaKhach.SelectedValue?.ToString();
            string maDonHang = txtMaDon.Text.Trim();
            string maNhanVien = cboNhanVien.SelectedValue?.ToString();
            DateTime ngayDat = dtpNgayDat.Value;
            string trangThai = txtTrangThai.Text;
            string ghiChu = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(maNhanVien) || string.IsNullOrEmpty(maKhach))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin đơn bán hàng.");
                return;
            }

            DonHang donHang = new DonHang
            {
                KhachHangID = maKhach,
                DonHangID = maDonHang,
                NhanVienID = maNhanVien,
                NgayDat = ngayDat,
                TrangThai = trangThai,
                GhiChu = ghiChu,
            };
            BLLDonHang bus = new BLLDonHang();
            string result = bus.InsertDonHang(donHang);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadKhachHang();
                LoadNhanVien();
                LoadDSDon("");
                cboMaKhach.SelectedValue = maKhach;
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maDon = txtMaDon.Text.Trim();
            string maKhach = cboMaKhach.SelectedValue?.ToString();
            string chuSoHuu = cboMaKhach.Text;
            string GhiChu = txtGhiChu.Text.Trim();
            if (string.IsNullOrEmpty(maDon))
            {
                if (dgvDSDon.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDSDon.SelectedRows[0];
                    maDon = selectedRow.Cells["DonHangID"].Value.ToString();
                    maKhach = selectedRow.Cells["KhachHangID"].Value.ToString();
                    chuSoHuu = selectedRow.Cells["HoTen"].Value.ToString();
                    GhiChu = selectedRow.Cells["GhiChu"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thông tin đơn bán hàng cần xóa xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(maDon))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa đơn bán hàng {maDon} - {chuSoHuu}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BLLDonHang bus = new BLLDonHang();
                string kq = bus.DeleteDonHang(maDon);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin đơn bán hàng {maDon} - {chuSoHuu} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadKhachHang();
                    LoadNhanVien();
                    LoadDSDon("");

                    cboMaKhach.SelectedValue = maKhach;
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maKhach = cboMaKhach.SelectedValue?.ToString();
            string maDon = txtMaDon.Text;
            string maNhanVien = cboNhanVien.SelectedValue?.ToString();
            DateTime ngayDat = dtpNgayDat.Value;
            string trangThai = txtTrangThai.Text;
            string ghiChu = txtGhiChu.Text.Trim();
            if (string.IsNullOrEmpty(maNhanVien) || string.IsNullOrEmpty(maKhach))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin đơn bán hàng.");
                return;
            }

            DonHang donHang = new DonHang
            {
                DonHangID = maDon,
                KhachHangID = maKhach,
                NhanVienID = maNhanVien,
                NgayDat = ngayDat,
                TrangThai = trangThai,
                GhiChu = ghiChu,
            };
            BLLDonHang bus = new BLLDonHang();
            string result = bus.UpdateDonHang(donHang);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadKhachHang();
                LoadNhanVien();
                LoadDSDon("");
                cboMaKhach.SelectedValue = maKhach;
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            BLLDonHang busDonHang = new BLLDonHang();
            string maDH = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(maDH))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<DonHang> ketQua = busDonHang.SearchDonHang(maDH);
            dgvDSDon.DataSource = ketQua;
        }

        private void dgvDSDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            isLoadingTheLuuDongData = true;
            DataGridViewRow row = dgvDSDon.Rows[e.RowIndex];
            cboMaKhach.SelectedValue = row.Cells["KhachHangID"].Value.ToString();
            cboNhanVien.SelectedValue = row.Cells["NhanVienID"].Value.ToString();
            dtpNgayDat.Text = row.Cells["NgayDat"].Value.ToString();
            txtMaDon.Text = row.Cells["DonHangID"].Value.ToString();
            txtTrangThai.Text = row.Cells["TrangThai"].Value.ToString();
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();
            txtMaDon.Enabled = false;
            txtMaDon.ReadOnly = true;
            if (txtTrangThai.Text == "Đã xử lí")
            {
                cboNhanVien.Enabled = false;
                dtpNgayDat.Enabled = false;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;

            }
            else
            {
                cboNhanVien.Enabled = true;
                dtpNgayDat.Enabled = true;
                // Bật nút "Sửa"
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void dgvDSDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string iddon = dgvDSDon.Rows[e.RowIndex].Cells["DonHangID"].Value.ToString();
            string idkhach = dgvDSDon.Rows[e.RowIndex].Cells["KhachHangID"].Value.ToString();
            string idnhan = dgvDSDon.Rows[e.RowIndex].Cells["NhanVienID"].Value.ToString();
            string TrangThai = dgvDSDon.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString();
            DonHang don = (DonHang)dgvDSDon.CurrentRow.DataBoundItem;
            KhachHang khach = new KhachHang();
            NhanVien nv = new NhanVien();
            string GhiChu = dgvDSDon.Rows[e.RowIndex].Cells["GhiChu"].Value.ToString();
            if (TrangThai.Trim().ToLower() == "đã giao")
            {

                MessageBox.Show("Đơn hàng đã được xử lý, không thể xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (KhachHang item in cboMaKhach.Items)
            {
                if (item.KhachHangID == idkhach)
                {
                    khach = item;
                    //break;
                }
            }

            foreach (NhanVien item in cboNhanVien.Items)
            {
                if (item.NhanVienID == idnhan)
                {
                    nv = item;
                    break;
                }
            }
            frmChiTietDonHang frmChiTiet = new frmChiTietDonHang(khach, don, nv);
            frmChiTiet.Show();
        }
    }
}
