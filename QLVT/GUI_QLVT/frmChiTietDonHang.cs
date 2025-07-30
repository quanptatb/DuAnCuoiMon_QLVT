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

namespace GUI_QLVT
{
    public partial class frmChiTietDonHang : Form
    {
        private KhachHang khachHang;
        private DonHang donHang;
        private NhanVien nhanVien;
        private List<ChiTietDonHang> lstChiTiet;
        private List<VatTu> lstVatTu;
        public frmChiTietDonHang(KhachHang khach, DonHang don, NhanVien nv)
        {
            InitializeComponent();
            khachHang = khach;
            donHang = don;
            nhanVien = nv;
            lstChiTiet = new List<ChiTietDonHang>();
            lstVatTu = new List<VatTu>();
        }


        private void LoadInfo()
        {
            lbChuSoHuu.Text = $"{khachHang.KhachHangID} - {khachHang.HoTen}";
            lbMaDon.Text = donHang.DonHangID;
            lbNgayDat.Text = donHang.NgayDat.ToString("dd/MM/yyyy");
        }



        private void loadVatTu()
        {
            BUSVatTu sp = new BUSVatTu();
            lstVatTu = sp.GetVatTuList();
            dgvSanPham.DataSource = lstVatTu;
            dgvSanPham.ColumnHeadersHeight = 80;
            dgvSanPham.RowTemplate.Height = 50;
            dgvSanPham.Columns["VatTuID"].HeaderText = "Mã vật tư";
            dgvSanPham.Columns["LoaiVatTuID"].HeaderText = "Mã loại vật tư";
            dgvSanPham.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvSanPham.Columns["SoLuongTon"].HeaderText = "Số lượng tồn";
            dgvSanPham.Columns["NhaCungCapID"].HeaderText = "Mã nhà cung cấp";
            dgvSanPham.Columns["NgayTao"].HeaderText = "Ngày Tạo";
            dgvSanPham.Columns["GhiChu"].HeaderText = "Ghi chú";
            dgvSanPham.Columns["TrangThaiID"].HeaderText = "Mã trạng thái";


            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void loadChiTietPhieu(string maPhieu)
        {
            BusChiTietPhieu bus = new BusChiTietPhieu();
            lstChiTiet = bus.GetChiTietPhieuList(maPhieu);
            dgvChiTiet.ColumnHeadersHeight = 40;
            dgvChiTiet.RowTemplate.Height = 40;
            dgvChiTiet.DataSource = lstChiTiet;
            dgvChiTiet.Columns["ChiTietDonHangID"].Visible = false;
            dgvChiTiet.Columns["DonHangID"].Visible = false;
            dgvChiTiet.Columns["VatTuID"].Visible = false;
            dgvChiTiet.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvChiTiet.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvChiTiet.Columns["SoLuong"].ReadOnly = false;
            dgvChiTiet.Columns["TenSanPham"].HeaderText = "Tên Vật Tư";
            dgvChiTiet.Columns["TrangThaiID"].Visible = false;

            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn col in dgvChiTiet.Columns)
            {
                col.ReadOnly = true;
            }
            dgvChiTiet.Columns["SoLuong"].ReadOnly = false;
        }

        private void deleteChiTiet()
        {
            if (dgvChiTiet.CurrentRow != null)
            {
                string id = Convert.ToString(dgvChiTiet.CurrentRow.Cells["ChiTietDonHangID"].Value);
                string name = Convert.ToString(dgvChiTiet.CurrentRow.Cells["VatTuID"].Value);
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa vật tư {name}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    BusChiTietPhieu bus = new BusChiTietPhieu();
                    string kq = bus.DeleteChiTiet(id); ;
                    if (string.IsNullOrEmpty(kq))
                    {
                        loadChiTietPhieu(donHang.DonHangID);
                    }
                }
            }
        }

        private void transfer(VatTu vt, int soLuong = 1)
        {

            if (vt != null)
            {
                BusChiTietPhieu bus = new BusChiTietPhieu();
                ChiTietDonHang existingItem = lstChiTiet.FirstOrDefault(item => item.VatTuID == vt.VatTuID);
                if (existingItem != null)
                {
                    existingItem.SoLuong += soLuong;
                    string result = bus.UpdateSoLuong(existingItem);
                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("Thêm vật tư không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    ChiTietDonHang ct = new ChiTietDonHang()
                    {
                        DonHangID = donHang.DonHangID,
                        VatTuID = vt.VatTuID,
                        SoLuong = soLuong,
                        DonGia = vt.DonGia,
                    };
                    string result = bus.InsertChiTietDonHang(ct);
                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("Thêm vật tư không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                loadChiTietPhieu(donHang.DonHangID);

            }
        }

        private void loadThanhToan()
        {
            txtTong.Text = "0";

            txtGiamGia.Text = "0";
            txtPhanTram.Text = "0";
            txtThanhTien.Text = "0";
            txtDichVu.Text = "0";
        }


        private void frmChiTietDonHang_Load(object sender, EventArgs e)
        {
            loadThanhToan();
            LoadInfo();
            loadVatTu();
            loadChiTietPhieu(donHang.DonHangID);
        }

        private void dgvChiTiet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (isActive)
            //{
            //    return;
            //}

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                BusChiTietPhieu bus = new BusChiTietPhieu();
                ChiTietDonHang ct = lstChiTiet[e.RowIndex];
                int newQuantity;
                if (int.TryParse(dgvChiTiet.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString(), out newQuantity))
                {
                    ct.SoLuong = newQuantity;
                    string result = bus.UpdateSoLuong(ct);
                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("Thêm vật tư không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //Đảm bảo ko lỗi khi đang edit mà chuyển ô
                    this.BeginInvoke((Action)(() =>
                    {
                        loadChiTietPhieu(donHang.DonHangID);
                    }));
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvSanPham_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (isActive)
            //{
            //    return;
            //}
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];

                // Tạo đối tượng từ dữ liệu hàng
                VatTu vatTu = new VatTu
                {
                    VatTuID = row.Cells["VatTuID"].Value.ToString(),
                    TenVatTu = row.Cells["TenVatTu"].Value.ToString(),
                    DonGia = Convert.ToInt32(row.Cells["DonGia"].Value)
                };

                transfer(vatTu);
            }
            decimal tongTien = 0;

            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                // Bỏ qua dòng mới (dòng trống cuối cùng)
                if (row.IsNewRow) continue;

                // Lấy số lượng và đơn giá
                decimal soLuong = 0;
                decimal donGia = 0;

                decimal.TryParse(Convert.ToString(row.Cells["SoLuong"].Value), out soLuong);
                decimal.TryParse(Convert.ToString(row.Cells["DonGia"].Value), out donGia);

                // Cộng vào tổng tiền
                tongTien += soLuong * donGia;
            }

            // Gán vào TextBox
            txtTong.Text = tongTien.ToString("N0"); // Định dạng có dấu phân cách hàng nghìn

            //Tính phần trăm
            decimal phanTram = 0;
            decimal.TryParse(txtPhanTram.Text, out phanTram);
            phanTram /= 100;

            //Tính tiền giảm giá
            decimal giamGia = 0;
            giamGia = tongTien * phanTram;
            txtGiamGia.Text = giamGia.ToString("N0");

            //Tính thành tiền
            decimal thanhTien = 0;
            thanhTien = Math.Round(tongTien - giamGia);
            txtThanhTien.Text = thanhTien.ToString("N0");
        }

        private void btnThemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow != null)
            {
                string id = Convert.ToString(dgvSanPham.CurrentRow.Cells["VatTuID"].Value);
                string ten = Convert.ToString(dgvSanPham.CurrentRow.Cells["TenVatTu"].Value);
                decimal dongia = Convert.ToDecimal(dgvSanPham.CurrentRow.Cells["DonGia"].Value);
                VatTu vatTu = new VatTu
                {
                    VatTuID = id,
                    TenVatTu = ten,
                    DonGia = dongia
                };

                transfer(vatTu);
            }
            decimal tongTien = 0;

            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                // Bỏ qua dòng mới (dòng trống cuối cùng)
                if (row.IsNewRow) continue;

                // Lấy số lượng và đơn giá
                decimal soLuong = 0;
                decimal donGia = 0;

                decimal.TryParse(Convert.ToString(row.Cells["SoLuong"].Value), out soLuong);
                decimal.TryParse(Convert.ToString(row.Cells["DonGia"].Value), out donGia);

                // Cộng vào tổng tiền
                tongTien += soLuong * donGia;
            }

            // Gán vào TextBox
            txtTong.Text = tongTien.ToString("N0"); // Định dạng có dấu phân cách hàng nghìn

            //Tính phần trăm
            decimal phanTram = 0;
            decimal.TryParse(txtPhanTram.Text, out phanTram);
            phanTram /= 100;

            //Tính tiền giảm giá
            decimal giamGia = 0;
            giamGia = tongTien * phanTram;
            txtGiamGia.Text = giamGia.ToString("N0");

            //Tính thành tiền
            decimal thanhTien = 0;
            thanhTien = Math.Round(tongTien - giamGia);
            txtThanhTien.Text = thanhTien.ToString("N0");
        }

        private void btnXoaChiTiet_Click(object sender, EventArgs e)
        {
            deleteChiTiet();
            decimal tongTien = 0;

            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                // Bỏ qua dòng mới (dòng trống cuối cùng)
                if (row.IsNewRow) continue;

                // Lấy số lượng và đơn giá
                decimal soLuong = 0;
                decimal donGia = 0;

                decimal.TryParse(Convert.ToString(row.Cells["SoLuong"].Value), out soLuong);
                decimal.TryParse(Convert.ToString(row.Cells["DonGia"].Value), out donGia);

                // Cộng vào tổng tiền
                tongTien += soLuong * donGia;
            }

            // Gán vào TextBox
            txtTong.Text = tongTien.ToString("N0"); // Định dạng có dấu phân cách hàng nghìn

            //Tính phần trăm
            decimal phanTram = 0;
            decimal.TryParse(txtPhanTram.Text, out phanTram);
            phanTram /= 100;

            //Tính tiền giảm giá
            decimal giamGia = 0;
            giamGia = tongTien * phanTram;
            txtGiamGia.Text = giamGia.ToString("N0");

            //Tính thành tiền
            decimal thanhTien = 0;
            thanhTien = Math.Round(tongTien - giamGia);
            txtThanhTien.Text = thanhTien.ToString("N0");
        }
    }
}
