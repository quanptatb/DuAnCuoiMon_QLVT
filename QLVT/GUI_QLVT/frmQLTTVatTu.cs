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
    public partial class frmQLTTVatTu : Form
    {
        public frmQLTTVatTu()
        {
            InitializeComponent();
        }

        private void frmQLTTVatTu_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachTTVT();
        }
        private void ClearForm()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            txtMaTrangThai.Clear();
            txtTenTrangThai.Clear();

        }
        private void LoadDanhSachTTVT()
        {
            dgvDSQLTTVatTu.ColumnHeadersHeight = 40;
            dgvDSQLTTVatTu.RowTemplate.Height = 40;
            BUSTTVatTu busTTVT = new BUSTTVatTu();
            dgvDSQLTTVatTu.DataSource = null;
            List<TrangThaiVatTu> lstTTVT = busTTVT.GetTrangThaiVatTuList();
            dgvDSQLTTVatTu.DataSource = lstTTVT;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string MaTrangThai = txtMaTrangThai.Text.Trim();
            string TenTrangThai = txtTenTrangThai.Text.Trim();



            if (string.IsNullOrEmpty(TenTrangThai))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin thẻ lưu động.");
                return;
            }

            TrangThaiVatTu TTVT = new TrangThaiVatTu()
            {
                TrangThaiID = MaTrangThai,
                TenTrangThai = TenTrangThai,
            };

            BUSTTVatTu busTTVT = new BUSTTVatTu();
            string result = busTTVT.InsertTrangThaiVatTu(TTVT);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachTTVT();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaTrangThai = txtMaTrangThai.Text.Trim();
            string TenTrangThai = txtTenTrangThai.Text.Trim();



            if (string.IsNullOrEmpty(TenTrangThai))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin thẻ lưu động.");
                return;
            }

            TrangThaiVatTu TTVT = new TrangThaiVatTu()
            {
                TrangThaiID = MaTrangThai,
                TenTrangThai = TenTrangThai,
            };

            BUSTTVatTu busTTVT = new BUSTTVatTu();
            string result = busTTVT.UpdateTrangThaiVatTu(TTVT);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachTTVT();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaTrangThai = txtMaTrangThai.Text.Trim();
            string TenTrangThai = txtTenTrangThai.Text.Trim();


            if (string.IsNullOrEmpty(MaTrangThai))
            {
                if (dgvDSQLTTVatTu.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDSQLTTVatTu.SelectedRows[0];
                    MaTrangThai = selectedRow.Cells["TrangThaiID"].Value.ToString();
                    TenTrangThai = selectedRow.Cells["TenTrangThai"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thông tin trạng thái vật tư cần xóa xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(MaTrangThai))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa trạng thái vật tư {MaTrangThai} - {TenTrangThai}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BUSTTVatTu busTTVT = new BUSTTVatTu();
                string kq = busTTVT.DeleteTrangThaiVatTu(MaTrangThai);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin trạng thái vật tư {MaTrangThai} - {TenTrangThai} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhSachTTVT();
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
            LoadDanhSachTTVT();
        }

        private void dgvDSQLTTVatTu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDSQLTTVatTu.Rows[e.RowIndex];
            txtMaTrangThai.Text = row.Cells["TrangThaiID"].Value.ToString();
            txtTenTrangThai.Text = row.Cells["TenTrangThai"].Value.ToString();
            // Bật nút "Sửa"
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            BUSTTVatTu busTTVT = new BUSTTVatTu();
            string maTTVT = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(maTTVT))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<TrangThaiVatTu> ketQua = busTTVT.SearchTrangThaiVatTu(maTTVT);
            dgvDSQLTTVatTu.DataSource = ketQua;
        }
    }
}
