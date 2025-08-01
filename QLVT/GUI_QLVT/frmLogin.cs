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
    public partial class frmLogin : Form
    {
        BUSNhanVien BUSNhanVien = new BUSNhanVien();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(180, Color.White);
            lblQuenMatKhau.MouseHover += lblQuenMatKhau_MouseHover;
            lblQuenMatKhau.MouseLeave += lblQuenMatKhau_MouseLeave;
        }



        private void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkPassword.Checked ? '\0' : '*';
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát khỏi chương trình", "Thoát",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question
                          );
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtEmail.Text;
            string password = txtPassword.Text;
            NhanVien nv = BUSNhanVien.DangNhap(username, password);
            if (nv == null)
            {
                MessageBox.Show(this, "Tài khoản hoặc mật khẩu không chính xác");
            }
            else
            {
                if (nv.TinhTrang == false)
                {
                    MessageBox.Show(this, "Tài khoản đang tạm khóa, vui lòng viên hệ QTV!!!");
                    return;
                }
                AuthUtil.user = nv;
                this.Hide();
                frmMain main = new frmMain();
                main.ShowDialog();

            }
        }
        private void lblQuenMatKhau_MouseHover(object sender, EventArgs e)
        {
            lblQuenMatKhau.Font = new Font(lblQuenMatKhau.Font, FontStyle.Underline);
            //đổi con tro
            lblQuenMatKhau.Cursor = Cursors.Hand; // Đổi con trỏ thành hình bàn tay
        }
        private void lblQuenMatKhau_MouseLeave(object sender, EventArgs e)
        {
            lblQuenMatKhau.Font = new Font(lblQuenMatKhau.Font, FontStyle.Bold);
            lblQuenMatKhau.ForeColor = Color.FromArgb( 128, 255, 255); // Màu mặc định
        }
        private void lblQuenMatKhau_Click(object sender, EventArgs e)
        {
            frmQuenMatKhau qmk = new frmQuenMatKhau();
            qmk.Show();
            this.Hide();
        }
    }
}
