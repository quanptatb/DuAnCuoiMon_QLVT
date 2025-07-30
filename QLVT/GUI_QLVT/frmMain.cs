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
    public partial class frmMain : Form
    {
        private bool CheckState = false;
        private frmChatIcon chatIcon;
        public frmMain()
        {
            InitializeComponent();
            CheckPermission();
        }
        private void VaiTroNhanVien()
        {
            btnQLNhaCungCap.Visible = false;
            btnQLNhanVien.Visible = false;
        }
        private void CheckPermission()
        {
            if (AuthUtil.IsLogin())
            {
                lblUser.Text = AuthUtil.user.HoTen.ToString();
                btnHome.Visible = true;
                btnQLVatTu.Visible = true;
                //btnQLTTVatTu.Visible = true;
                btnQLKhachHang.Visible = true;
                btnQLNhanVien.Visible = true;
                btnQLNhaCungCap.Visible = true;
                btnHoaDon.Visible = true;
                btnQLDonHang.Visible = true;
                btnDoiMatKhau.Visible = true;
                if (AuthUtil.user.VaiTro == false)
                {
                    VaiTroNhanVien();
                }
            }
            else
            {
                btnHome.Visible = true;
                btnQLVatTu.Visible = false;
                //btnQLTTVatTu.Visible = false;
                btnQLKhachHang.Visible = false;
                btnQLNhanVien.Visible = false;
                btnQLNhaCungCap.Visible = false;
                btnHoaDon.Visible = false;
                btnQLDonHang.Visible = false;
                btnDoiMatKhau.Visible = false;
            }
        }
        private Form currentFormChild;

        private void openChildForm(Form formChild)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = formChild;
            formChild.TopLevel = false;
            formChild.FormBorderStyle = FormBorderStyle.None;
            formChild.Dock = DockStyle.Fill;
            pnMain.Controls.Add(formChild);
            pnMain.Tag = formChild;
            formChild.BringToFront();
            formChild.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            chatIcon = new frmChatIcon();
            chatIcon.TopMost = true; // luôn nổi trên các form khác
            chatIcon.Show();
        }

        private void btnQLVatTu_Click(object sender, EventArgs e)
        {
            openChildForm(new frmQLVatTu());
            lblTenForm.Text = "QUẢN LÍ VẬT TƯ";
        }

        private void btnQLTTVatTu_Click(object sender, EventArgs e)
        {
            openChildForm(new frmQLTTVatTu1());
            lblTenForm.Text = "QUẢN LÍ TRẠNG THÁI VẬT TƯ";
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            AuthUtil.user = null;
            frmLogin login = new frmLogin();
            login.ShowDialog();
        }

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            openChildForm(new frmQuanLyNhanVien());
            lblTenForm.Text = "QUẢN LÍ NHÂN VIÊN";
        }

        private void btnQLNhaCungCap_Click(object sender, EventArgs e)
        {

        }

        private void btnQLKhachHang_Click(object sender, EventArgs e)
        {
            openChildForm(new frmQuanLyKhachHang());
            lblTenForm.Text = "QUẢN LÍ KHÁCH HÀNG";
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDoiMatKhau());
            lblTenForm.Text = "ĐỔI MẬT KHẨU";
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            openChildForm(new frmQLHoaDon());
            lblTenForm.Text = "QUẢN LÍ HÓA ĐƠN";
        }

        private void btnQLDonHang_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDonHang());
            lblTenForm.Text = "QUẢN LÍ ĐƠN HÀNG";
        }
        private void btnQLPhieuNhap_Click(object sender, EventArgs e)
        {
            openChildForm(new frmPhieuNhapKho());
            lblTenForm.Text = "QUẢN LÍ PHIẾU NHẬP KHO";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnQLNhaCungCap_Click_1(object sender, EventArgs e)
        {
            openChildForm(new frmQuanLyNhaCungCap());
            lblTenForm.Text = "QUẢN LÍ NHÀ CUNG CẤP";
        }
    }
}
