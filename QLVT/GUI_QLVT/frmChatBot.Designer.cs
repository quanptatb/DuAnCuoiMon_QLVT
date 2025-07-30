namespace GUI_QLVT
{
    partial class frmChatBot
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChatBot));
            lstChat = new ListBox();
            txtInput = new Guna.UI2.WinForms.Guna2TextBox();
            btnGui = new Guna.UI2.WinForms.Guna2Button();
            dgvKetQua = new Guna.UI2.WinForms.Guna2DataGridView();
            pnlHeader = new Panel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            pnlInput = new Panel();
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).BeginInit();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlInput.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // lstChat
            // 
            lstChat.Font = new Font("Segoe UI", 12F);
            lstChat.FormattingEnabled = true;
            lstChat.ItemHeight = 28;
            lstChat.Location = new Point(2, 141);
            lstChat.Margin = new Padding(3, 4, 3, 4);
            lstChat.Name = "lstChat";
            lstChat.Size = new Size(1007, 704);
            lstChat.TabIndex = 0;
            // 
            // txtInput
            // 
            txtInput.CustomizableEdges = customizableEdges1;
            txtInput.DefaultText = "";
            txtInput.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtInput.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtInput.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtInput.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtInput.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtInput.Font = new Font("Segoe UI", 9F);
            txtInput.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtInput.Location = new Point(37, 33);
            txtInput.Margin = new Padding(3, 5, 3, 5);
            txtInput.Name = "txtInput";
            txtInput.PasswordChar = '\0';
            txtInput.PlaceholderText = "";
            txtInput.SelectedText = "";
            txtInput.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtInput.Size = new Size(783, 53);
            txtInput.TabIndex = 1;
            // 
            // btnGui
            // 
            btnGui.BorderRadius = 10;
            btnGui.BorderThickness = 2;
            btnGui.CustomizableEdges = customizableEdges3;
            btnGui.DisabledState.BorderColor = Color.DarkGray;
            btnGui.DisabledState.CustomBorderColor = Color.DarkGray;
            btnGui.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnGui.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnGui.FillColor = Color.White;
            btnGui.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnGui.ForeColor = Color.Black;
            btnGui.Location = new Point(852, 33);
            btnGui.Margin = new Padding(3, 4, 3, 4);
            btnGui.Name = "btnGui";
            btnGui.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnGui.Size = new Size(131, 53);
            btnGui.TabIndex = 2;
            btnGui.Text = "Gửi";
            btnGui.Click += btnGui_Click;
            // 
            // dgvKetQua
            // 
            dgvKetQua.AllowUserToAddRows = false;
            dgvKetQua.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            dgvKetQua.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvKetQua.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvKetQua.ColumnHeadersHeight = 4;
            dgvKetQua.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvKetQua.DefaultCellStyle = dataGridViewCellStyle3;
            dgvKetQua.GridColor = Color.FromArgb(231, 229, 255);
            dgvKetQua.Location = new Point(1033, 141);
            dgvKetQua.Margin = new Padding(3, 4, 3, 4);
            dgvKetQua.Name = "dgvKetQua";
            dgvKetQua.ReadOnly = true;
            dgvKetQua.RowHeadersVisible = false;
            dgvKetQua.RowHeadersWidth = 51;
            dgvKetQua.RowTemplate.Height = 25;
            dgvKetQua.Size = new Size(889, 842);
            dgvKetQua.TabIndex = 3;
            dgvKetQua.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvKetQua.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvKetQua.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvKetQua.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvKetQua.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvKetQua.ThemeStyle.BackColor = Color.White;
            dgvKetQua.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvKetQua.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvKetQua.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvKetQua.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvKetQua.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvKetQua.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvKetQua.ThemeStyle.HeaderStyle.Height = 4;
            dgvKetQua.ThemeStyle.ReadOnly = true;
            dgvKetQua.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvKetQua.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvKetQua.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvKetQua.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvKetQua.ThemeStyle.RowsStyle.Height = 25;
            dgvKetQua.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvKetQua.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.SkyBlue;
            pnlHeader.Controls.Add(pictureBox1);
            pnlHeader.Controls.Add(label1);
            pnlHeader.Location = new Point(2, 1);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1007, 134);
            pnlHeader.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.robot1;
            pictureBox1.Location = new Point(296, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(69, 75);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 20F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(371, 45);
            label1.Name = "label1";
            label1.Size = new Size(248, 46);
            label1.TabIndex = 1;
            label1.Text = "Chat với Trợ Lý";
            // 
            // pnlInput
            // 
            pnlInput.BackColor = Color.LightBlue;
            pnlInput.Controls.Add(txtInput);
            pnlInput.Controls.Add(btnGui);
            pnlInput.Location = new Point(2, 868);
            pnlInput.Name = "pnlInput";
            pnlInput.Size = new Size(1007, 115);
            pnlInput.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.BackColor = Color.SkyBlue;
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(1033, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(889, 134);
            panel1.TabIndex = 5;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(297, 25);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(69, 75);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 20F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(372, 45);
            label2.Name = "label2";
            label2.Size = new Size(206, 46);
            label2.TabIndex = 1;
            label2.Text = "Xuất dữ liệu";
            // 
            // frmChatBot
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1924, 993);
            Controls.Add(panel1);
            Controls.Add(pnlInput);
            Controls.Add(pnlHeader);
            Controls.Add(dgvKetQua);
            Controls.Add(lstChat);
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmChatBot";
            Text = "frmChatBot";
            Load += frmChatBot_Load;
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).EndInit();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlInput.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListBox lstChat;
        private Guna.UI2.WinForms.Guna2TextBox txtInput;
        private Guna.UI2.WinForms.Guna2Button btnGui;
        private Guna.UI2.WinForms.Guna2DataGridView dgvKetQua;
        private Panel pnlHeader;
        private Label label1;
        private Panel pnlInput;
        private Panel panel1;
        private Label label2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}