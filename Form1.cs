using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Buoi07_TinhToan3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private ErrorProvider errorProvider = new ErrorProvider();
        private void Form1_Load(object sender, EventArgs e)
        {
            txtSo1.Text = txtSo2.Text = "0";
            radCong.Checked = true;             //đầu tiên chọn phép cộng
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Bạn có thực sự muốn thoát không?",
                                 "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                this.Close();
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            string input1 = txtSo1.Text.Trim();
            string input2 = txtSo2.Text.Trim();

            if (string.IsNullOrWhiteSpace(input1) || string.IsNullOrWhiteSpace(input2))
            {
                MessageBox.Show("Vui lòng nhập đủ 2 số !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //lấy giá trị của 2 ô số
            double so1, so2, kq = 0;
            if (!double.TryParse(input1, out so1))
            {
                MessageBox.Show("Số thứ nhất không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSo1.Focus();
                return;
            }

            if (!double.TryParse(input2, out so2))
            {
                MessageBox.Show("Số thứ hai không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSo2.Focus();
                return;
            }
            //Thực hiện phép tính dựa vào phép toán được chọn
            if (radCong.Checked) kq = so1 + so2;
            else if (radTru.Checked) kq = so1 - so2;
            else if (radNhan.Checked) kq = so1 * so2;
            else if (radChia.Checked && so2 != 0) kq = so1 / so2;
            //Hiển thị kết quả lên trên ô kết quả
            txtKq.Text = kq.ToString();
        }

        private void txtSo1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSo1.Text))
            {
                errorProvider.SetError(txtSo1, "Vui lòng nhập số thứ nhất!");
            }
            else if (!double.TryParse(txtSo1.Text, out _))
            {
                errorProvider.SetError(txtSo1, "Số thứ nhất phải là số thực hợp lệ!");
            }
            else
            {
                errorProvider.SetError(txtSo1, ""); // Xóa lỗi nếu nhập đúng
            }
        }

        private void txtSo2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSo2.Text))
            {
                errorProvider.SetError(txtSo2, "Vui lòng nhập số thứ 2!");
            }
            else if (!double.TryParse(txtSo2.Text, out _))
            {
                errorProvider.SetError(txtSo2, "Số thứ 2 phải là số thực hợp lệ!");
            }
            else
            {
                errorProvider.SetError(txtSo2, ""); // Xóa lỗi nếu nhập đúng
            }
        }
    }
}
