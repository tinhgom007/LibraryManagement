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
using static System.Net.Mime.MediaTypeNames;

namespace QuanLyThuVien
{
    public partial class Login : Form
    {
        string Conn = "Data Source=Admin\\VIETHO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        string TenNV;
        public Login()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            string sSql1 = "SELECT TenNhanVien, Quyen FROM NhanVien WHERE SoDienThoai = @SoDienThoai";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.Parameters.AddWithValue("@SoDienThoai", txtTaiKhoan.Text);  // Use parameterized query to prevent SQL injection

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);

            // Check if a row was returned (i.e., the phone number exists in the database)
            if (dt1.Rows.Count > 0)
            {
                string TenNV = dt1.Rows[0]["TenNhanVien"].ToString();
                string Quyen = dt1.Rows[0]["Quyen"].ToString();

                // Check if the role is "Admin"
                if (Quyen == "Admin")
                {
                    this.Hide();  // Hide the current form
                    TrangChu Home = new TrangChu(TenNV);  // Pass the employee's name to the home page
                    Home.Show();  // Show the home page
                }
                else
                {
                    this.Hide();
                    TrangNhanVien nv = new TrangNhanVien(TenNV);
                    nv.Show();
                }
            }
            else
            {
                MessageBox.Show("Wrong phone number or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";
        }
    }
}
