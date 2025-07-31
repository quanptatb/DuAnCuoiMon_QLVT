using BLL_QLVT;
using DAL_QLVT;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GUI_QLVT
{
    public partial class frmQLHoaDon : Form
    {
        DALHoaDon dalHoaDon = new DALHoaDon();
        public frmQLHoaDon()
        {
            InitializeComponent();
        }
        private void ClearForm(string maHD)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            txtMaHD.Clear();
            cboMaDonHang.Enabled = true;
            txtTongTien.Clear();
            dtbNgayThanhToan.Value = DateTime.Now;
            txtPhuongThuc.Clear();

        }
        private void LoadDanhSachHoaDon(string maHD)
        {
            BLLHoaDon bLLHoaDon = new BLLHoaDon();
            List<HoaDon> lst = bLLHoaDon.GetListHoaDon(maHD);
            dgvDSHD.DataSource = lst;





            DataGridViewImageColumn buttonColumn = new DataGridViewImageColumn();
            buttonColumn.Name = "ThanhToan";
            buttonColumn.HeaderText = "Thanh Toán";
            //buttonColumn.Text = "Thanh Toán";
            //buttonColumn.UseColumnTextForButtonValue = true; // Hiển thị văn bản lên nút
            buttonColumn.DefaultCellStyle.BackColor = Color.LightBlue;
            buttonColumn.DefaultCellStyle.ForeColor = Color.DarkBlue;

            buttonColumn.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            if (!dgvDSHD.Columns.Contains("ThanhToan"))
            {
                dgvDSHD.Columns.Add(buttonColumn);
            }
            dgvDSHD.Columns["ThanhToan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvDSHD.RowTemplate.Height = 50;

            dgvDSHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            string maHD = txtMaHD.Text.Trim();
            string maDH = cboMaDonHang.SelectedValue.ToString();
            DateTime ngayThanhToan = dtbNgayThanhToan.Value;
            string phuongThucThanhToan = txtPhuongThuc.Text.Trim();
            if (string.IsNullOrEmpty(maHD) || string.IsNullOrEmpty(maDH) || string.IsNullOrEmpty(txtTongTien.Text.Trim()) || string.IsNullOrEmpty(phuongThucThanhToan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }
            decimal tongTien = 0;
            if (!decimal.TryParse(txtTongTien.Text.Trim(), out tongTien))
            {
                MessageBox.Show("Tổng tiền không hợp lệ.");
                return;
            }

            bool trangThai = false;

            HoaDon hd = new HoaDon
            {
                HoaDonID = maHD,
                DonHangID = maDH,
                TongTien = tongTien,
                NgayThanhToan = ngayThanhToan,
                PhuongThucThanhToan = phuongThucThanhToan,
                TrangThai = trangThai
            };
            BLLHoaDon bLL = new BLLHoaDon();
            string result = bLL.InsertHoaDon(hd);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm(maHD);
                LoadDanhSachHoaDon(maHD);

            }
            else
            {
                MessageBox.Show(result);
            }
        }
        public string UpdateHoaDon(HoaDon hd)
        {
            try
            {
                if (string.IsNullOrEmpty(hd.HoaDonID))
                {
                    return "Mã hóa đơn không hợp lệ.";
                }

                dalHoaDon.updateHoaDon(hd);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Cập nhật không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string hoaDonID = txtMaHD.Text.Trim();
            string donHangID = cboMaDonHang.SelectedValue.ToString();
            DateTime ngayThanhToan = dtbNgayThanhToan.Value;
            string phuongThucThanhToan = txtPhuongThuc.Text.Trim();


            if (string.IsNullOrEmpty(hoaDonID) || string.IsNullOrEmpty(donHangID) || string.IsNullOrEmpty(txtTongTien.Text) || string.IsNullOrEmpty(txtPhuongThuc.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin hóa đơn.");
                return;
            }

            decimal tongTien = 0;
            if (!decimal.TryParse(txtTongTien.Text.Trim(), out tongTien))
            {
                MessageBox.Show("Tổng tiền không hợp lệ.");
                return;
            }

            if (dgvDSHD.SelectedRows.Count > 0)
            {
                bool trangThai = Convert.ToBoolean(dgvDSHD.SelectedRows[0].Cells["TrangThai"].Value);
                if (trangThai)
                {
                    MessageBox.Show("Hóa đơn đã hoàn thành, không thể chỉnh sửa.");
                    return;
                }
            }

            HoaDon hd = new HoaDon
            {
                HoaDonID = hoaDonID,
                DonHangID = donHangID,
                TongTien = tongTien,
                NgayThanhToan = ngayThanhToan,
                PhuongThucThanhToan = phuongThucThanhToan,

            };
            BLLHoaDon bLL = new BLLHoaDon();
            string result = bLL.UpdateHoaDon(hd);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm(donHangID);
                LoadDanhSachHoaDon(hoaDonID);

            }
            else
            {
                MessageBox.Show(result);
            }
            cboMaDonHang.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string hoaDonID = txtMaHD.Text.Trim();
            string donHangID = cboMaDonHang.SelectedValue.ToString();
            string phuongThucThanhToan = txtPhuongThuc.Text.Trim();
            if (string.IsNullOrEmpty(hoaDonID))
            {
                if (dgvDSHD.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDSHD.SelectedRows[0];
                    hoaDonID = selectedRow.Cells["HoaDonID"].Value.ToString();
                    donHangID = selectedRow.Cells["DonHangID"].Value.ToString();
                    phuongThucThanhToan = selectedRow.Cells["PhuongThucThanhToan"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thông tin hóa đơn cần xóa xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(hoaDonID))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa hóa đơn {hoaDonID} - {donHangID}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BLLHoaDon bLL = new BLLHoaDon();
                string kq = bLL.DeleteHoaDon(hoaDonID);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin hóa đơn {hoaDonID} - {donHangID} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm(hoaDonID);
                    LoadDanhSachHoaDon(hoaDonID);

                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            if (dgvDSHD.SelectedRows.Count > 0)
            {
                bool trangThai = Convert.ToBoolean(dgvDSHD.SelectedRows[0].Cells["TrangThai"].Value);
                if (trangThai)
                {
                    MessageBox.Show("Hóa đơn đã hoàn thành, không thể chỉnh sửa.");
                    return;
                }
            }
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {

            ClearForm("");
            LoadDanhSachHoaDon("");
            LoadMaDonHang();

        }
        private void LoadMaDonHang()
        {
            BUSDonHang bLLDonHang = new BUSDonHang();
            List<DonHang> lstDonHang = bLLDonHang.GetListDonHang("");
            cboMaDonHang.DataSource = null;
            cboMaDonHang.DataSource = lstDonHang;
            cboMaDonHang.DisplayMember = "DonHangID"; // Hiển thị mã đơn hàng
            cboMaDonHang.ValueMember = "DonHangID"; // Giá trị của mã đơn hàng
        }
        private void frmQLHoaDon_Load(object sender, EventArgs e)
        {
            ClearForm("");
            LoadDanhSachHoaDon("");
            LoadMaDonHang();
        }

        private void dgvDSHD_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ;
            DataGridViewRow row = dgvDSHD.Rows[e.RowIndex];
            txtMaHD.Text = row.Cells["HoaDonID"].Value.ToString();
            cboMaDonHang.SelectedValue = row.Cells["DonHangID"].Value.ToString();
            txtTongTien.Text = row.Cells["TongTien"].Value.ToString();
            dtbNgayThanhToan.Value = Convert.ToDateTime(row.Cells["NgayThanhToan"].Value);
            txtPhuongThuc.Text = row.Cells["PhuongThucThanhToan"].Value.ToString();


            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);
            if (trangThai)
            {

                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                MessageBox.Show("Hóa đơn đã hoàn thành. Không thể chỉnh sửa.");
            }
            else
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }

        }

        private void dgvDSHD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string hoaDonID = dgvDSHD.Rows[e.RowIndex].Cells["HoaDonID"].Value.ToString();
            string donHangID = dgvDSHD.Rows[e.RowIndex].Cells["DonHangID"].Value.ToString();
            string tongTien = dgvDSHD.Rows[e.RowIndex].Cells["TongTien"].Value.ToString();
            DateTime ngayThanhToan = Convert.ToDateTime(dgvDSHD.Rows[e.RowIndex].Cells["NgayThanhToan"].Value);
            string phuongThucThanhToan = dgvDSHD.Rows[e.RowIndex].Cells["PhuongThucThanhToan"].Value.ToString();

            HoaDon hd = (HoaDon)dgvDSHD.CurrentRow.DataBoundItem;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            string maHD = txtTimKiem.Text.Trim();
            MessageBox.Show("Mã nhập vào: " + maHD);

            if (string.IsNullOrWhiteSpace(maHD))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BLLHoaDon bLLHoaDon = new BLLHoaDon();
            List<HoaDon> result = bLLHoaDon.SearchHoaDon(maHD);

            dgvDSHD.DataSource = null;
            dgvDSHD.DataSource = result;

            if (result.Count == 0)
            {
                MessageBox.Show($"Không tìm thấy hóa đơn nào với mã: {maHD}", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            string maHD = txtMaHD.Text.Trim();
            string maDH = cboMaDonHang.SelectedValue.ToString();
            DateTime ngayThanhToan = dtbNgayThanhToan.Value;
            string phuongThucThanhToan = txtPhuongThuc.Text.Trim();
            if (string.IsNullOrEmpty(maHD) || string.IsNullOrEmpty(maDH) || string.IsNullOrEmpty(txtTongTien.Text.Trim()) || string.IsNullOrEmpty(phuongThucThanhToan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }
            decimal tongTien = 0;
            if (!decimal.TryParse(txtTongTien.Text.Trim(), out tongTien))
            {
                MessageBox.Show("Tổng tiền không hợp lệ.");
                return;
            }

            bool trangThai = false;

            HoaDon hd = new HoaDon
            {
                HoaDonID = maHD,
                DonHangID = maDH,
                TongTien = tongTien,
                NgayThanhToan = ngayThanhToan,
                PhuongThucThanhToan = phuongThucThanhToan,
                TrangThai = trangThai
            };
            BLLHoaDon bLL = new BLLHoaDon();
            string result = bLL.InsertHoaDon(hd);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm(maHD);
                LoadDanhSachHoaDon(maHD);

            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            string hoaDonID = txtMaHD.Text.Trim();
            string donHangID = cboMaDonHang.SelectedValue.ToString();
            DateTime ngayThanhToan = dtbNgayThanhToan.Value;
            string phuongThucThanhToan = txtPhuongThuc.Text.Trim();


            if (string.IsNullOrEmpty(hoaDonID) || string.IsNullOrEmpty(donHangID) || string.IsNullOrEmpty(txtTongTien.Text) || string.IsNullOrEmpty(txtPhuongThuc.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin hóa đơn.");
                return;
            }

            decimal tongTien = 0;
            if (!decimal.TryParse(txtTongTien.Text.Trim(), out tongTien))
            {
                MessageBox.Show("Tổng tiền không hợp lệ.");
                return;
            }

            if (dgvDSHD.SelectedRows.Count > 0)
            {
                bool trangThai = Convert.ToBoolean(dgvDSHD.SelectedRows[0].Cells["TrangThai"].Value);
                if (trangThai)
                {
                    MessageBox.Show("Hóa đơn đã hoàn thành, không thể chỉnh sửa.");
                    return;
                }
            }

            HoaDon hd = new HoaDon
            {
                HoaDonID = hoaDonID,
                DonHangID = donHangID,
                TongTien = tongTien,
                NgayThanhToan = ngayThanhToan,
                PhuongThucThanhToan = phuongThucThanhToan,

            };
            BLLHoaDon bLL = new BLLHoaDon();
            string result = bLL.UpdateHoaDon(hd);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm(donHangID);
                LoadDanhSachHoaDon(hoaDonID);

            }
            else
            {
                MessageBox.Show(result);
            }
            cboMaDonHang.Enabled = false;
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string hoaDonID = txtMaHD.Text.Trim();
            string donHangID = cboMaDonHang.SelectedValue.ToString();
            string phuongThucThanhToan = txtPhuongThuc.Text.Trim();
            if (string.IsNullOrEmpty(hoaDonID))
            {
                if (dgvDSHD.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDSHD.SelectedRows[0];
                    hoaDonID = selectedRow.Cells["HoaDonID"].Value.ToString();
                    donHangID = selectedRow.Cells["DonHangID"].Value.ToString();
                    phuongThucThanhToan = selectedRow.Cells["PhuongThucThanhToan"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thông tin hóa đơn cần xóa xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(hoaDonID))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa hóa đơn {hoaDonID} - {donHangID}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BLLHoaDon bLL = new BLLHoaDon();
                string kq = bLL.DeleteHoaDon(hoaDonID);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin hóa đơn {hoaDonID} - {donHangID} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm(hoaDonID);
                    LoadDanhSachHoaDon(hoaDonID);

                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            if (dgvDSHD.SelectedRows.Count > 0)
            {
                bool trangThai = Convert.ToBoolean(dgvDSHD.SelectedRows[0].Cells["TrangThai"].Value);
                if (trangThai)
                {
                    MessageBox.Show("Hóa đơn đã hoàn thành, không thể chỉnh sửa.");
                    return;
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm("");
            LoadDanhSachHoaDon("");
            LoadMaDonHang();
        }

        private void dgvDSHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ;
            DataGridViewRow row = dgvDSHD.Rows[e.RowIndex];
            txtMaHD.Text = row.Cells["HoaDonID"].Value.ToString();
            cboMaDonHang.SelectedValue = row.Cells["DonHangID"].Value.ToString();
            txtTongTien.Text = row.Cells["TongTien"].Value.ToString();
            dtbNgayThanhToan.Value = Convert.ToDateTime(row.Cells["NgayThanhToan"].Value);
            txtPhuongThuc.Text = row.Cells["PhuongThucThanhToan"].Value.ToString();


            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);
            if (trangThai)
            {

                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                MessageBox.Show("Hóa đơn đã hoàn thành. Không thể chỉnh sửa.");
            }
            else
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void dgvDSHD_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string hoaDonID = dgvDSHD.Rows[e.RowIndex].Cells["HoaDonID"].Value.ToString();
            string donHangID = dgvDSHD.Rows[e.RowIndex].Cells["DonHangID"].Value.ToString();
            string tongTien = dgvDSHD.Rows[e.RowIndex].Cells["TongTien"].Value.ToString();
            DateTime ngayThanhToan = Convert.ToDateTime(dgvDSHD.Rows[e.RowIndex].Cells["NgayThanhToan"].Value);
            string phuongThucThanhToan = dgvDSHD.Rows[e.RowIndex].Cells["PhuongThucThanhToan"].Value.ToString();

            HoaDon hd = (HoaDon)dgvDSHD.CurrentRow.DataBoundItem;
        }
    }
}
