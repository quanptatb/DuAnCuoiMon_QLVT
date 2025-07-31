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
    public partial class frmChatBot : Form
    {
        public frmChatBot()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Size = new Size(0, 0); // Bắt đầu từ nhỏ
            this.StartPosition = FormStartPosition.CenterScreen;

            animationTimer.Interval = 10; // càng nhỏ càng mượt
            animationTimer.Tick += AnimationTimer_Tick;
        }
        System.Windows.Forms.Timer animationTimer = new System.Windows.Forms.Timer();
        int targetWidth = 1064;
        int targetHeight = 906;

        private void frmChatBot_Load(object sender, EventArgs e)
        {
            animationTimer.Start();
            lstChat.Items.Add("Bot: Xin chào! Bạn cần mình giúp gì không?");
        }
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            int step = 20; // tốc độ phóng (tăng dần)
            if (this.Width < targetWidth) this.Width += step;
            if (this.Height < targetHeight) this.Height += step;

            // Dừng animation khi đạt kích thước
            if (this.Width >= targetWidth && this.Height >= targetHeight)
            {
                animationTimer.Stop();
                this.Width = targetWidth;
                this.Height = targetHeight;
            }
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            string input = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(input))
                return;

            // Hiển thị câu hỏi lên listbox
            lstChat.Items.Add("Bạn: " + input);

            // Gửi qua chatbot xử lý
            string response = BUSChatbot.HandleMessage(input);
            lstChat.Items.Add("Bot: " + response);

            // Xử lý hiển thị dữ liệu
            if (BUSChatbot.LastResult != null)
            {
                dgvKetQua.AutoGenerateColumns = true;
                dgvKetQua.DataSource = BUSChatbot.LastResult;
                dgvKetQua.ColumnHeadersVisible = true;
                BUSChatbot.LastResult = null; // reset
            }
            else
            {
                // Không có dữ liệu để hiển thị bảng
                dgvKetQua.DataSource = null;
                dgvKetQua.Refresh();
            }

            // Auto scroll cho list chat
            lstChat.TopIndex = lstChat.Items.Count - 1;

            // Xóa input
            txtInput.Clear();
        }
    }
}
