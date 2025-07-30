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
using UTIL_QLVT;

namespace GUI_QLVT
{
    public partial class frmDoiMatKhau : Form
    {
        BUSNhanVien busNhanVien = new BUSNhanVien();
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }



        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            if (AuthUtil.IsLogin())
            {
                txtMaNhanVien.Text = AuthUtil.user.NhanVienID;
                txtHoTen.Text = AuthUtil.user.HoTen;
            }
        }


        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (!AuthUtil.user.MatKhau.Equals(txtMatKhauCu.Text))
            {
                MessageBox.Show(this, "Mật khẩu cũ chưa đúng!!!");
            }
            else
            {
                if (!txtMatKhauMoi.Text.Equals(txtXacNhanMatKhau.Text))
                {
                    MessageBox.Show(this, "Xác nhận mật khẩu mới chưa trùng khớp!!!");
                }
                else
                {
                    AuthUtil.user.MatKhau = txtMatKhauMoi.Text;

                    if (busNhanVien.ResetMatKhau(AuthUtil.user.email, txtMatKhauMoi.Text))
                    {
                        MessageBox.Show("Cập nhật mật khẩu thành công!!!");
                    }
                    else MessageBox.Show("Đổi mật khẩu thất bại, vui lòng kiểm tra lại!!!");
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát khỏi chương trình", "Thoát",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question
                          );
            if (result == DialogResult.Yes)
            {
                this.Hide();
            }
        }

        private void chkHienThi1_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhauCu.PasswordChar = chkHienThi1.Checked ? '\0' : '*';
        }

        private void chkHienThi2_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhauMoi.PasswordChar = chkHienThi2.Checked ? '\0' : '*';
        }

        private void chkHienThi3_CheckedChanged(object sender, EventArgs e)
        {
            txtXacNhanMatKhau.PasswordChar = chkHienThi3.Checked ? '\0' : '*';
        }
    }
}
