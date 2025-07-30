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
    public partial class frmQLLoaiVatTu : Form
    {
        public frmQLLoaiVatTu()
        {
            InitializeComponent();
        }
        private void ClearForm()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            txtMaLoaiVatTu.Clear();
            txtTenLoaiVatTu.Clear();
            txtGhiChu.Clear();

        }
        private void LoadDanhSachLVT()
        {
            dgvDSQLVatTu.ColumnHeadersHeight =40;
            dgvDSQLVatTu.RowTemplate.Height = 40; // hoặc giá trị bạn muốn
            BUSLoaiVatTu busLoaiVatTu = new BUSLoaiVatTu();
            dgvDSQLVatTu.DataSource = null;
            List<LoaiVatTu> lstLVT = busLoaiVatTu.GetLoaiVatTuList();
            dgvDSQLVatTu.DataSource = lstLVT;
            dgvDSQLVatTu.Columns["LoaiVatTuID"].HeaderText = "Mã Loại Vật Tư";
            dgvDSQLVatTu.Columns["TenLoaiVatTu"].HeaderText = "Tên Loại Vật Tư";
            dgvDSQLVatTu.Columns["NgayTao"].HeaderText = "Ngày Tạo";
            dgvDSQLVatTu.Columns["GhiChu"].HeaderText = "Ghi chú";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string MaLoaiVatTu = txtMaLoaiVatTu.Text.Trim();
            string TenLoaiVatTu = txtTenLoaiVatTu.Text.Trim();
            DateTime NgayTao = dtpNgayTao.Value;
            string GhiChu = txtGhiChu.Text.Trim();



            if (string.IsNullOrEmpty(TenLoaiVatTu))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin thẻ lưu động.");
                return;
            }

            LoaiVatTu loaiVT = new LoaiVatTu()
            {
                LoaiVatTuID = MaLoaiVatTu,
                TenLoaiVatTu = TenLoaiVatTu,
                NgayTao = NgayTao,
                GhiChu = GhiChu,
            };
            BUSLoaiVatTu busLoaiVT = new BUSLoaiVatTu();
            string result = busLoaiVT.InsertLoaiVatTu(loaiVT);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachLVT();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaLoaiVatTu = txtMaLoaiVatTu.Text.Trim();
            string TenLoaiVatTu = txtTenLoaiVatTu.Text.Trim();
            DateTime NgayTao = dtpNgayTao.Value;
            string GhiChu = txtGhiChu.Text.Trim();


            if (string.IsNullOrEmpty(TenLoaiVatTu))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin loại vật tư.");
                return;
            }
            LoaiVatTu loaiVatTu = new LoaiVatTu()
            {
                LoaiVatTuID = MaLoaiVatTu,
                TenLoaiVatTu = TenLoaiVatTu,
                NgayTao = NgayTao,
                GhiChu = GhiChu,
            };
            BUSLoaiVatTu busVatTu = new BUSLoaiVatTu();
            string result = busVatTu.UpdateLoaiVatTu(loaiVatTu);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachLVT();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaLoaiVatTu = txtMaLoaiVatTu.Text.Trim();
            string TenLoaiVatTu = txtTenLoaiVatTu.Text.Trim();
            DateTime NgayTao = dtpNgayTao.Value;
            string GhiChu = txtGhiChu.Text.Trim();
            if (string.IsNullOrEmpty(MaLoaiVatTu))
            {
                if (dgvDSQLVatTu.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDSQLVatTu.SelectedRows[0];
                    MaLoaiVatTu = selectedRow.Cells["LoaiVatTuID"].Value.ToString();
                    TenLoaiVatTu = selectedRow.Cells["TenLoaiVatTu"].Value.ToString();
                    NgayTao = (DateTime)selectedRow.Cells["NgayTao"].Value;
                    GhiChu = selectedRow.Cells["GhiChu"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thông tin loại vật tư cần xóa xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(MaLoaiVatTu))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa loại vật tư {MaLoaiVatTu} - {TenLoaiVatTu}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BUSLoaiVatTu busVatTu = new BUSLoaiVatTu();
                string kq = busVatTu.DeleteLoaiVatTu(MaLoaiVatTu);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin loại vật tư {MaLoaiVatTu} - {TenLoaiVatTu} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhSachLVT();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachLVT();
        }

        private void frmQLLoaiVatTu_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachLVT();
        }

        private void dgvDSQLVatTu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDSQLVatTu.Rows[e.RowIndex];
            txtMaLoaiVatTu.Text = row.Cells["LoaiVatTuID"].Value.ToString();
            txtTenLoaiVatTu.Text = row.Cells["TenLoaiVatTu"].Value.ToString();
            dtpNgayTao.Value = (DateTime)row.Cells["NgayTao"].Value;
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();
            // Bật nút "Sửa"
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            BUSLoaiVatTu busLoaiVatTu = new BUSLoaiVatTu();
            string maLVT = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(maLVT))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<LoaiVatTu> ketQua = busLoaiVatTu.SearchLoaiVatTu(maLVT);
            dgvDSQLVatTu.DataSource = ketQua;
        }

        private void dgvDSQLVatTu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
