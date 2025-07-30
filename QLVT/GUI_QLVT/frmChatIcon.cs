using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QLVT
{
    public partial class frmChatIcon : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;  
        
        public frmChatIcon()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.DodgerBlue;
            this.Width = this.Height = 60;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - 80,
            Screen.PrimaryScreen.WorkingArea.Height - 100);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, this.Width, this.Height);
            this.Region = new Region(path);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            dragging = false;
        }

        protected override void OnClick(EventArgs e)
        {
            frmChatBot chat = new frmChatBot();
            chat.Show();
            this.Hide();
            chat.FormClosed += (s, args) => this.Show();
        }
    }
}
