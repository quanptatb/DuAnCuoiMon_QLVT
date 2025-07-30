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
    public partial class frmQLTTVatTu1 : Form
    {
        public frmQLTTVatTu1()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmQLTTVatTu frm = new frmQLTTVatTu();
            frm.ShowDialog();
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

        private void frmQLTTVatTu1_Load(object sender, EventArgs e)
        {
            LoadDanhSachTTVT();
        }
    }
}
