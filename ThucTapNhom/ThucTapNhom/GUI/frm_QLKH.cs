using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThucTapNhom.BLL;
using ThucTapNhom.DAO;

namespace ThucTapNhom.GUI
{
    public partial class frm_QLKH : Form
    {
        SqlConnection con = Program.con;
        DbQuery DbAcessObject = new DbQuery();
        string MaKH { get; set; }
        public frm_QLKH()
        {
            InitializeComponent();
            this.MaKH = MaKH;
        }
        public void HienThiDSKH()
        {
            DataTable dtDSKH = DbAcessObject.HienThiDSKH(con);
            dgvDSNV.DataSource = dtDSKH;
        }
        private void frm_QLKH_Load(object sender, EventArgs e)
        {
            HienThiDSKH();
        }

        private void btnDanhSachKH_Click(object sender, EventArgs e)
        {
            HienThiDSKH();
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang();
            kh.SetData(txtMaKH.Text,txtTenKH.Text, cbbGioiTinh.Text, txtDiaChi.Text, txtSĐT.Text);

            try
            {
                int ret = DbAcessObject.ThemKH(con, kh);
                HienThiDSKH();
                MessageBox.Show("Thêm khách hàng thành công!");
            }
            catch (SqlException)
            {
                MessageBox.Show("Lỗi không thêm được!");
            }
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắn chắn muốn xoá không?", "Chú ý", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DbAcessObject.XoaKH(con, txtMaKH.Text);
                    HienThiDSKH();
                }
                catch(SqlException)
                {
                    MessageBox.Show("Khách hàng đang có phiếu yêu cầu .", "Thông báo");
                }
            }
        }

        private void dgvDSNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKH.Text = dgvDSNV.CurrentRow.Cells[0].Value.ToString();
            txtTenKH.Text = dgvDSNV.CurrentRow.Cells[1].Value.ToString();
            cbbGioiTinh.Text = dgvDSNV.CurrentRow.Cells[2].Value.ToString();
            txtDiaChi.Text = dgvDSNV.CurrentRow.Cells[3].Value.ToString();
            txtSĐT.Text = dgvDSNV.CurrentRow.Cells[4].Value.ToString();
            
        }

        private void btnCapNhatKH_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang();
            kh.SetData(txtMaKH.Text,txtTenKH.Text, cbbGioiTinh.Text, txtDiaChi.Text, txtSĐT.Text);
            DbAcessObject.CapNhatKH(con, kh);
            HienThiDSKH();
            MessageBox.Show("Cập nhật khách hàng thành công!");
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (rdbTheoTen.Checked == true)
            {
                DataTable dt = DbAcessObject.TimNVTheoTenKH(con, txtTimKiem.Text);
                dgvDSNV.DataSource = dt;
            }
            else if (rdbTheoDiaChi.Checked == true)
            {
                DataTable dt = DbAcessObject.TimNVTheoDiaChiKH(con, txtTimKiem.Text);
                dgvDSNV.DataSource = dt;
            }
            
        }
    }
}
