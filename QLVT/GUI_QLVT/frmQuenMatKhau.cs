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

namespace GUI_QLVT
{
    public partial class frmQuenMatKhau : Form
    {
        private BUSQuenMatKhau bus = new BUSQuenMatKhau();
        private int secondsLeft = 60;
        private System.Windows.Forms.Timer resendTimer;
        private void InitResendTimer()
        {
            resendTimer = new System.Windows.Forms.Timer();
            resendTimer.Interval = 1000; // 1 giây
            resendTimer.Tick += ResendTimer_Tick;
        }
        public frmQuenMatKhau()
        {
            InitializeComponent();
            InitResendTimer();
        }
        private void StartResendTimer()
        {
            secondsLeft = 60;

            if (resendTimer == null)
            {
                resendTimer = new System.Windows.Forms.Timer();
                resendTimer.Interval = 1000; // 1 giây
                resendTimer.Tick += ResendTimer_Tick;
            }

            btnGuiMa.Enabled = false;
            btnGuiMa.Text = $"Gửi mã ({secondsLeft}s)";
            resendTimer.Start();
        }

        private void ResendTimer_Tick(object sender, EventArgs e)
        {
            secondsLeft--;
            if (secondsLeft > 0)
            {
                btnGuiMa.Text = $"Gửi mã ({secondsLeft}s)";
            }
            else
            {
                resendTimer.Stop();
                btnGuiMa.Text = "Gửi mã";
                btnGuiMa.Enabled = true;
            }
        }

        private void btnGuiMa_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập Email.");
                return;
            }

            bool sent = bus.GuiMaXacNhan(email);
            if (sent)
            {
                MessageBox.Show("Mã xác nhận đã được gửi. Vui lòng kiểm tra email.");
                StartResendTimer();
            }
            else
            {
                MessageBox.Show("Email không tồn tại trong hệ thống.");
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string code = txtMaXacNhan.Text.Trim();
            string mk1 = txtMatKhauMoi.Text;
            string mk2 = txtXacNhanMatKhau.Text;

            if (mk1 != mk2)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.");
                return;
            }

            if (!bus.XacNhanMa(email, code))
            {
                MessageBox.Show("Mã xác nhận sai hoặc đã hết hạn.");
                return;
            }

            bus.DoiMatKhau(email, mk1);
            MessageBox.Show("Đổi mật khẩu thành công!");
            frmLogin login = new frmLogin();
            login.Show();
            this.Close(); // Hoặc về form đăng nhập
        }

        private void frmQuenMatKhau_Load(object sender, EventArgs e)
        {
            lblMatKhauMoi.Visible = false;
            txtMatKhauMoi.Visible = false;
            lblXacNhanMatKhau.Visible = false;
            txtXacNhanMatKhau.Visible = false;
            btnDoiMatKhau.Visible = false;
            chkHienThi1.Visible = false;
            chkHienThi2.Visible = false;
        }

        private void btnXacNhanMa_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string code = txtMaXacNhan.Text.Trim();
            if (bus.XacNhanMa(email, code))
            {
                lblMatKhauMoi.Visible = true;
                txtMatKhauMoi.Visible = true;
                lblXacNhanMatKhau.Visible = true;
                txtXacNhanMatKhau.Visible = true;
                btnDoiMatKhau.Visible = true;
                chkHienThi1.Visible = true;
                chkHienThi2.Visible = true;
            }
            else
            {
                lblMatKhauMoi.Visible = false;
                txtMatKhauMoi.Visible = false;
                lblXacNhanMatKhau.Visible = false;
                txtXacNhanMatKhau.Visible = false;
                btnDoiMatKhau.Visible = false;
                chkHienThi1.Visible = false;
                chkHienThi2.Visible = false;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmLogin login = new frmLogin();
            login.Show();
        }

        private void chkHienThi1_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhauMoi.PasswordChar = chkHienThi1.Checked ? '\0' : '*';
        }

        private void chkHienThi2_CheckedChanged(object sender, EventArgs e)
        {
            txtXacNhanMatKhau.PasswordChar = chkHienThi2.Checked ? '\0' : '*';
        }
    }
}
