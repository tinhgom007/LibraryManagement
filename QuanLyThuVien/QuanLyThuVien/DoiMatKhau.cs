﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class DoiMatKhau : Form
    {
        string Conn = "Data Source=Admin\\VIETHO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        private void DoiMatKhau_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();
            txtTKDoi.Focus();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string sSql = "select * from NhanVien where SoDienThoai='" + txtTKDoi.Text + "' and MatKhau='" + txtMKCu.Text + "'";
            mySqlCommand = new SqlCommand(sSql, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            int count = Convert.ToInt32(dt.Rows.Count.ToString());
            if (count == 0)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu cũ không đúng","Thông báo");
                return;
            }
            else
            {
                if (Convert.ToInt32(txtMKMoi.Text.Trim().Length) != Convert.ToInt32(txtMKMoi.Text.Length))
                {
                    MessageBox.Show("Mật khẩu mới có ký tự không hợp lệ. Vui lòng nhập lại", "Thông Báo");
                    return;
                }
                if (Convert.ToInt32(txtMKMoi.Text.Trim().Length) < 6)
                {
                    MessageBox.Show("Mật khẩu mới phải ít nhất có 6 ký tự", "Thông Báo");
                    return;
                }
                else
                {
                    string query2 = "update NhanVien set MatKhau = '" + txtMKMoi.Text + "' where SoDienThoai = '" + txtTKDoi.Text + "'";
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Đổi mật khẩu thành công", "Thông báo");
                    Clear();
                }

            }
        }
        public void Clear()
        {
            txtTKDoi.Text = "";
            txtMKMoi.Text = "";
            txtMKCu.Text = "";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
