using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QLVT
{
    public partial class frmCho : Form
    {
        public frmCho()
        {
            InitializeComponent();
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            Task.Delay(3000).ContinueWith(t =>
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {
                        frmLogin login = new frmLogin();
                        login.Show();

                        this.Hide();
                    }));
                }
            });
        }
    }
}
