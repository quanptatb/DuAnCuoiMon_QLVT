namespace GUI_QLVT
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            panel1 = new Panel();
            btnExit = new Guna.UI2.WinForms.Guna2Button();
            btnLogin = new Guna.UI2.WinForms.Guna2Button();
            txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            chkPassword = new CheckBox();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            lblQuenMatKhau = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.Anchor = AnchorStyles.None;
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = Color.FromArgb(128, 255, 255);
            guna2HtmlLabel1.Location = new Point(839, 241);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(117, 23);
            guna2HtmlLabel1.TabIndex = 4;
            guna2HtmlLabel1.Text = "Tên đăng nhập";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Location = new Point(-324, -58);
            panel1.Name = "panel1";
            panel1.Size = new Size(1089, 1059);
            panel1.TabIndex = 10;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.None;
            btnExit.BackColor = Color.Transparent;
            btnExit.BorderColor = Color.Cyan;
            btnExit.BorderRadius = 30;
            btnExit.BorderThickness = 1;
            btnExit.CustomizableEdges = customizableEdges1;
            btnExit.DisabledState.BorderColor = Color.DarkGray;
            btnExit.DisabledState.CustomBorderColor = Color.DarkGray;
            btnExit.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnExit.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnExit.FillColor = Color.DarkTurquoise;
            btnExit.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(839, 621);
            btnExit.Name = "btnExit";
            btnExit.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnExit.Size = new Size(453, 65);
            btnExit.TabIndex = 32;
            btnExit.Text = "Thoát";
            btnExit.Click += btnExit_Click_1;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.None;
            btnLogin.BackColor = Color.Transparent;
            btnLogin.BorderColor = Color.Cyan;
            btnLogin.BorderRadius = 30;
            btnLogin.BorderThickness = 1;
            btnLogin.CustomizableEdges = customizableEdges3;
            btnLogin.DisabledState.BorderColor = Color.DarkGray;
            btnLogin.DisabledState.CustomBorderColor = Color.DarkGray;
            btnLogin.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnLogin.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnLogin.FillColor = Color.DeepSkyBlue;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(839, 540);
            btnLogin.Name = "btnLogin";
            btnLogin.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnLogin.Size = new Size(453, 65);
            btnLogin.TabIndex = 31;
            btnLogin.Text = "Đăng nhập";
            btnLogin.Click += btnLogin_Click;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.None;
            txtPassword.BackColor = Color.Transparent;
            txtPassword.BorderColor = Color.Cyan;
            txtPassword.BorderRadius = 25;
            txtPassword.CustomizableEdges = customizableEdges5;
            txtPassword.DefaultText = "";
            txtPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtPassword.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.ForeColor = Color.Black;
            txtPassword.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtPassword.IconLeft = Properties.Resources.pass_removebg_preview1;
            txtPassword.IconLeftOffset = new Point(10, 0);
            txtPassword.IconLeftSize = new Size(30, 30);
            txtPassword.Location = new Point(839, 403);
            txtPassword.Margin = new Padding(4, 6, 4, 6);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.PlaceholderText = "";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtPassword.Size = new Size(453, 58);
            txtPassword.TabIndex = 30;
            // 
            // chkPassword
            // 
            chkPassword.Anchor = AnchorStyles.None;
            chkPassword.AutoSize = true;
            chkPassword.BackColor = Color.Transparent;
            chkPassword.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkPassword.ForeColor = Color.FromArgb(128, 255, 255);
            chkPassword.Location = new Point(839, 470);
            chkPassword.Name = "chkPassword";
            chkPassword.Size = new Size(166, 25);
            chkPassword.TabIndex = 6;
            chkPassword.Text = "Hiển thị mật khẩu";
            chkPassword.UseVisualStyleBackColor = false;
            chkPassword.CheckedChanged += chkPassword_CheckedChanged;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.Anchor = AnchorStyles.None;
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel2.ForeColor = Color.FromArgb(128, 255, 255);
            guna2HtmlLabel2.Location = new Point(839, 364);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(75, 23);
            guna2HtmlLabel2.TabIndex = 5;
            guna2HtmlLabel2.Text = "Mật khẩu";
            // 
            // txtEmail
            // 
            txtEmail.Anchor = AnchorStyles.None;
            txtEmail.BorderColor = Color.Cyan;
            txtEmail.BorderRadius = 25;
            txtEmail.CustomizableEdges = customizableEdges7;
            txtEmail.DefaultText = "";
            txtEmail.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtEmail.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtEmail.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtEmail.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtEmail.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtEmail.Font = new Font("Segoe UI", 12F);
            txtEmail.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtEmail.IconLeft = Properties.Resources.user_removebg_preview1;
            txtEmail.IconLeftOffset = new Point(5, 0);
            txtEmail.IconLeftSize = new Size(30, 30);
            txtEmail.Location = new Point(839, 280);
            txtEmail.Margin = new Padding(4, 6, 4, 6);
            txtEmail.Name = "txtEmail";
            txtEmail.PasswordChar = '\0';
            txtEmail.PlaceholderText = "";
            txtEmail.SelectedText = "";
            txtEmail.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtEmail.Size = new Size(453, 58);
            txtEmail.TabIndex = 34;
            // 
            // lblQuenMatKhau
            // 
            lblQuenMatKhau.Anchor = AnchorStyles.None;
            lblQuenMatKhau.BackColor = Color.Transparent;
            lblQuenMatKhau.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblQuenMatKhau.ForeColor = Color.FromArgb(128, 255, 255);
            lblQuenMatKhau.Location = new Point(1144, 470);
            lblQuenMatKhau.Name = "lblQuenMatKhau";
            lblQuenMatKhau.Size = new Size(120, 23);
            lblQuenMatKhau.TabIndex = 35;
            lblQuenMatKhau.Text = "Quên mật khẩu";
            lblQuenMatKhau.Click += lblQuenMatKhau_Click;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(1360, 961);
            Controls.Add(lblQuenMatKhau);
            Controls.Add(txtEmail);
            Controls.Add(btnExit);
            Controls.Add(panel1);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(chkPassword);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmLogin";
            Text = "frmLogin";
            Load += frmLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Panel panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private CheckBox chkPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private Guna.UI2.WinForms.Guna2Button btnExit;
        private Guna.UI2.WinForms.Guna2Button btnLogin;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblQuenMatKhau;
    }
}