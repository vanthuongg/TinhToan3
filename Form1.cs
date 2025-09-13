using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            // Gắn sự kiện xử lý nhập liệu (Phương án 1)
            txtSo1.Leave += TxtInput_Leave;
            txtSo2.Leave += TxtInput_Leave;
        }

        //Xử lý khi nhập liệu
        private void TxtInput_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            string input = txt.Text.Trim();

            if (input.Contains("^"))
            {
                try
                {
                    string[] parts = input.Split('^');
                    if (parts.Length == 2 &&
                        double.TryParse(parts[0], out double baseNum) &&
                        double.TryParse(parts[1], out double exponent))
                    {
                        double result = Math.Pow(baseNum, exponent);
                        txt.Text = result.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Dữ liệu nhập không hợp lệ! Vui lòng nhập dạng a^b (ví dụ: 10^2).",
                                        "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt.Focus();
                    }
                }
                catch
                {
                    MessageBox.Show("Lỗi khi xử lý dữ liệu nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt.Focus();
                }
            }
        }

        //Xử lý khi tính toán
        private double ParseInput(string input)
        {
            input = input.Trim();

            // Regex dạng cơ số ^ số mũ
            Match match = Regex.Match(input, @"^\s*([\d\.]+)\s*\^\s*([\d\.]+)\s*$");
            if (match.Success)
            {
                double baseNum = double.Parse(match.Groups[1].Value);
                double exponent = double.Parse(match.Groups[2].Value);
                return Math.Pow(baseNum, exponent);
            }

            return double.Parse(input);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Bạn có thực sự muốn thoát không?",
                                 "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                this.Close();
        }

        private double ParseSqrtOrNumber(string input)
        {
            string s = (input ?? string.Empty).Trim();
            if (s.StartsWith("sqrt", StringComparison.OrdinalIgnoreCase))
            {
                int lp = s.IndexOf('(');
                int rp = s.LastIndexOf(')');
                if (lp > -1 && rp > lp)
                {
                    string inner = s.Substring(lp + 1, rp - lp - 1);
                    double v = ParseSqrtOrNumber(inner);
                    return v < 0 ? double.NaN : Math.Sqrt(v);
                }
            }
            return double.Parse(s);
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
            so1 = ParseSqrtOrNumber(txtSo1.Text);
            so2 = ParseSqrtOrNumber(txtSo2.Text);

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
            }            //Thực hiện phép tính dựa vào phép toán được chọn
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
