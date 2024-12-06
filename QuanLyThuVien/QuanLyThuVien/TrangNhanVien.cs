using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class TrangNhanVien : Form
    {
        string Conn = "Data Source=Admin\\VIETHO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        int bien = 1;

        private string _message;

        public TrangNhanVien() 
        {
            InitializeComponent();
        }

        public TrangNhanVien(string Message) : this()
        {
            _message = Message;
            txtXinChao.Text = _message;
        }

        private void TrangNhanVien_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();

            DocGia();
            Muontra();
            cbMuontra();
            thongke();
            tracuusach();
        }

        public void tracuusach()
        {
            string query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvTimSach.DataSource = dt;
        }

        private void btnSachh_Click(object sender, EventArgs e)
        {
            Sach S = new Sach();
            S.Show();
        }

        private void btnLS_Click(object sender, EventArgs e)
        {
            LoaiSach LS = new LoaiSach();
            LS.Show();
        }

        private void btnTG_Click(object sender, EventArgs e)
        {
            TacGia TG = new TacGia();
            TG.Show();
        }

        private void btnXBB_Click(object sender, EventArgs e)
        {
            NhaXuatBann XB = new NhaXuatBann();
            XB.Show();
        }

        private void txtTimKiemm_KeyUp(object sender, KeyEventArgs e)
        {
            if (btnTenSach.Checked)
            {
                string query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia  where S.TenSach like N'%" + txtTimKiemm.Text + "%' order by Soluong";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvTimSach.DataSource = dt;
            }
            if (btnLoaiSach.Checked)
            {
                string query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong  from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia where LS.TenLoaiSach like N'%" + txtTimKiemm.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvTimSach.DataSource = dt;
            }
            if (btnTacGia.Checked)
            {
                string query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia where TG.TacGia like N'%" + txtTimKiemm.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvTimSach.DataSource = dt;
            }
            if (btnNXB.Checked)
            {
                string query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia where XB.NhaXuatBan like N'%" + txtTimKiemm.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvTimSach.DataSource = dt;
            }
        }

        private void DocGia()
        {
            string query = "select * from SinhVien";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvSinhVien.DataSource = dt;
            SetControls(false);
        }

        private void btnThemSV_Click(object sender, EventArgs e)
        {
            txtMaSinhVien.Clear();
            txtHoTen.Clear();
            txtNganhHoc.Clear();
            txtSDT.Clear();
            txtMaSinhVien.Focus();
            bien = 1;

            SetControls(true);
        }

        private void SetControls(bool edit)
        {
            txtMaSinhVien.Enabled = edit;
            txtHoTen.Enabled = edit;
            txtKhoa.Enabled = edit;
            txtNganhHoc.Enabled = edit;
            txtSDT.Enabled = edit;
            btnThemSV.Enabled = !edit;
            btnSuaSV.Enabled = !edit;
            btnXoaSV.Enabled = !edit;
            btnGhiSV.Enabled = edit;
            btnHuySV.Enabled = edit;
            //.Enabled = edit;

            cbTenSach.Enabled = edit;
            cbNgayMuon.Enabled = edit;
            cbNgayTra.Enabled = edit;
            //cbMaSV.Enabled = edit;
            txtGhiChu.Enabled = edit;
            btnMuon.Enabled = !edit;
            btnGiaHan.Enabled = !edit;
            btnTraSach.Enabled = !edit;
            btnGhii.Enabled = edit;
            btnHuyy.Enabled = edit;
            cbNgayMuon.Visible = true;
            lbNgayMuon.Visible = true;
        }

        private void btnSuaSV_Click(object sender, EventArgs e)
        {
            txtHoTen.Focus();
            bien = 2;

            SetControls(true);
            txtMaSinhVien.Enabled = false;
        }

        private void btnXoaSV_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = new DialogResult();
                dlr = MessageBox.Show("Ban co chac chan muon xoa? ", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.No) return;
                int row = dgvSinhVien.CurrentRow.Index;
                string MaSV = dgvSinhVien.Rows[row].Cells[0].Value.ToString();
                string query3 = "delete from SinhVien where MaSV = " + MaSV;
                mySqlCommand = new SqlCommand(query3, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                DocGia();
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xoá. Sinh này đang mượn sách", "Thông Báo");
            }
        }

        private void btnGhiSV_Click(object sender, EventArgs e)
        {
            if (bien == 1)
            {
                if (txtMaSinhVien.Text.Trim() == "" || txtHoTen.Text.Trim() == "" || txtNganhHoc.Text.Trim() == "" || txtKhoa.Text.Trim() == "" || txtSDT.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập lại !!!");
                }
                else
                {
                    int maSinhVien;
                    if (!int.TryParse(txtMaSinhVien.Text, out maSinhVien))
                    {
                        MessageBox.Show("Số điện thoại phải là một số nguyên!", "Thông báo");
                        return;
                    }
                    // Check if the student ID already exists
                    for (int i = 0; i < dgvSinhVien.RowCount; i++)
                    {
                        if (txtMaSinhVien.Text == dgvSinhVien.Rows[i].Cells[0].Value.ToString())
                        {
                            MessageBox.Show("Trùng mã sinh viên. Vui lòng Nhập lại", "Thông báo");
                            return;
                        }
                    }

                    // Check if the phone number is already used
                    string checkPhoneQuery = "SELECT COUNT(*) FROM SinhVien WHERE SoDienThoai = @SoDienThoai";
                    mySqlCommand = new SqlCommand(checkPhoneQuery, mySqlconnection);
                    mySqlCommand.Parameters.AddWithValue("@SoDienThoai", txtSDT.Text);
                    int phoneCount = Convert.ToInt32(mySqlCommand.ExecuteScalar());

                    if (phoneCount > 0)
                    {
                        MessageBox.Show("Trùng số điện thoại. Vui lòng Nhập lại", "Thông báo");
                        return;
                    }

                    // Validate student ID as number
                    double x;
                    bool kt = double.TryParse(txtMaSinhVien.Text, out x);
                    if (kt == false)
                    {
                        MessageBox.Show("Vui lòng Nhập lại dưới dạng số!", "Thông báo");
                        return;
                    }

                    // Insert new student record
                    string query1 = "insert into SinhVien(MaSV, TenSV, NganhHoc, KhoaHoc, SoDienThoai) values(@MaSV, @TenSV, @NganhHoc, @KhoaHoc, @SoDienThoai)";
                    mySqlCommand = new SqlCommand(query1, mySqlconnection);
                    mySqlCommand.Parameters.AddWithValue("@MaSV", txtMaSinhVien.Text);
                    mySqlCommand.Parameters.AddWithValue("@TenSV", txtHoTen.Text);
                    mySqlCommand.Parameters.AddWithValue("@NganhHoc", txtNganhHoc.Text);
                    mySqlCommand.Parameters.AddWithValue("@KhoaHoc", txtKhoa.Text);
                    mySqlCommand.Parameters.AddWithValue("@SoDienThoai", txtSDT.Text);
                    mySqlCommand.ExecuteNonQuery();

                    DocGia();
                    MessageBox.Show("Thêm sinh viên thành công", "Thông báo");
                }
            }
            else
            {
                if (txtMaSinhVien.Text.Trim() == "" || txtHoTen.Text.Trim() == "" || txtNganhHoc.Text.Trim() == "" || txtKhoa.Text.Trim() == "" || txtSDT.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập lại !!!");
                }
                else
                {
                    int maSinhVien;
                    if (!int.TryParse(txtSDT.Text, out maSinhVien))
                    {
                        MessageBox.Show("Số điện thoại phải là một số nguyên!", "Thông báo");
                        return;
                    }

                    // Check if the phone number is already used
                    string checkPhoneQuery = "SELECT COUNT(*) FROM SinhVien WHERE SoDienThoai = @SoDienThoai";
                    mySqlCommand = new SqlCommand(checkPhoneQuery, mySqlconnection);
                    mySqlCommand.Parameters.AddWithValue("@SoDienThoai", txtSDT.Text);
                    int phoneCount = Convert.ToInt32(mySqlCommand.ExecuteScalar());

                    if (phoneCount > 0)
                    {
                        MessageBox.Show("Trùng số điện thoại. Vui lòng Nhập lại", "Thông báo");
                        return;
                    }

                    int row = dgvSinhVien.CurrentRow.Index;
                    string MaSV = dgvSinhVien.Rows[row].Cells[0].Value.ToString();
                    string query2 = "UPDATE SinhVien SET MaSV = @MaSV, TenSV = @TenSV, NganhHoc = @NganhHoc, KhoaHoc = @KhoaHoc, SoDienThoai = @SoDienThoai WHERE MaSV = @OldMaSV";
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.Parameters.AddWithValue("@MaSV", txtMaSinhVien.Text);
                    mySqlCommand.Parameters.AddWithValue("@TenSV", txtHoTen.Text);
                    mySqlCommand.Parameters.AddWithValue("@NganhHoc", txtNganhHoc.Text);
                    mySqlCommand.Parameters.AddWithValue("@KhoaHoc", txtKhoa.Text);
                    mySqlCommand.Parameters.AddWithValue("@SoDienThoai", txtSDT.Text);
                    mySqlCommand.Parameters.AddWithValue("@OldMaSV", MaSV);
                    mySqlCommand.ExecuteNonQuery();

                    DocGia();
                    MessageBox.Show("Sửa sinh viên thành công", "Thông báo");
                }
            }
        }

        private void btnHuySV_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        private void dgvSinhVien_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaSinhVien.Text = dgvSinhVien.Rows[r].Cells[0].Value.ToString();
            txtHoTen.Text = dgvSinhVien.Rows[r].Cells[1].Value.ToString();
            txtNganhHoc.Text = dgvSinhVien.Rows[r].Cells[2].Value.ToString();
            txtKhoa.Text = dgvSinhVien.Rows[r].Cells[3].Value.ToString();
            txtSDT.Text = dgvSinhVien.Rows[r].Cells[4].Value.ToString();
        }

        private void txtTimKiemSV_KeyUp(object sender, KeyEventArgs e)
        {
            if (btnMSV.Checked)
            {
                string query = "select * from SinhVien where MaSV like N'%" + txtTimKiemSV.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvSinhVien.DataSource = dt;
            }

            if (btnTSV.Checked)
            {
                string query = "select * from SinhVien where TenSV like N'%" + txtTimKiemSV.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvSinhVien.DataSource = dt;
            }
        }

        public void Muontra()
        {
            string query = "select MS.MaPhieuMuon, SV.MaSV, SV.TenSV, S.MaSach, S.TenSach,MS.NgayMuon,MS.NgayTra,MS.GhiChu from MuonTraSach MS join Sach S on S.MaSach = MS.MaSach join SinhVien SV on SV.MaSV = MS.MaSV";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvMuonSach.DataSource = dt;

            txtMaPhieuMUon.Enabled = false;
            ttMaSach.Enabled = false;
            ttTenSach.Enabled = false;
            ttSoLuong.Enabled = false;
            ttTenTG.Enabled = false;
        }
        public void cbMuontra()
        {
            string sSql2 = "select MaSV from SinhVien";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                //cbMaSV.Items.Add(dr[0].ToString());
            }
            string sSql9 = "select TenSach from Sach";
            mySqlCommand = new SqlCommand(sSql9, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt9 = new DataTable();
            SqlDataAdapter da9 = new SqlDataAdapter(mySqlCommand);
            da9.Fill(dt9);
            foreach (DataRow dr in dt9.Rows)
            {
                cbTenSach.Items.Add(dr[0].ToString());
            }
        }

        private void cbTenSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSql1 = "select s.MaSach, s.TenSach, tg.TacGia, s.SoLuong from Sach s join TacGia tg on s.MaTacGia = tg.MaTacGia where s.TenSach = '" + cbTenSach.Text + "'";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                ttMaSach.Text = dr["MaSach"].ToString();
                ttTenSach.Text = dr["TenSach"].ToString();
                ttTenTG.Text = dr["TacGia"].ToString();
                ttSoLuong.Text = dr["SoLuong"].ToString();
            }
        }

        private void txtTimKiemSachMuon_KeyUp(object sender, KeyEventArgs e)
        {
            if (raMaSV.Checked)
            {
                string query = "select MS.MaPhieuMuon, SV.MaSV, SV.TenSV, S.MaSach, S.TenSach,MS.NgayMuon,MS.NgayTra,MS.GhiChu from MuonTraSach MS join Sach S on S.MaSach = MS.MaSach join SinhVien SV on SV.MaSV = MS.MaSV where SV.SoDienThoai like N'%" + txtTimKiemSachMuon.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvMuonSach.DataSource = dt;
            }
            if (raMaSach.Checked)
            {
                string query = "select MS.MaPhieuMuon, SV.MaSV, SV.TenSV, S.MaSach, S.TenSach,MS.NgayMuon,MS.NgayTra,MS.GhiChu from MuonTraSach MS join Sach S on S.MaSach = MS.MaSach join SinhVien SV on SV.MaSV = MS.MaSV where S.TenSach like N'%" + txtTimKiemSachMuon.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvMuonSach.DataSource = dt;
            }
        }

        private void btnMuon_Click(object sender, EventArgs e)
        {
            //cb.Clear();
            //txtGhiChu.Clear();
            cbTenSach.Focus();
            bien = 5;
            SetControls(true);
            //cbNgayMuon.Enabled = false;
            cbNgayMuon.Visible = false;
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            //cbSV.Focus();
            bien = 6;

            SetControls(true);
            txtMaPhieuMUon.Enabled = false;
            cbTenSach.Enabled = false;
            //cbMaSV.Enabled = false;
            txtMaSV.Enabled = false;
            txtGhiChu.Enabled = false;
            cbNgayMuon.Enabled = false;
        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            DialogResult dlr = new DialogResult();
            dlr = MessageBox.Show("Bạn có chắc chắn muốn trả? ", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;
            int row = dgvMuonSach.CurrentRow.Index;
            string MaMuonTra = dgvMuonSach.Rows[row].Cells[0].Value.ToString();

            string query = "SELECT NgayMuon, NgayTra FROM MuonTraSach WHERE MaPhieuMuon = " + MaMuonTra;
            SqlCommand mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader reader = mySqlCommand.ExecuteReader();

            DateTime ngayMuon = DateTime.MinValue;
            DateTime ngayTraDuKien = DateTime.MinValue;

            if (reader.Read())
            {
                ngayMuon = reader.GetDateTime(0);
                ngayTraDuKien = reader.GetDateTime(1);
            }
            reader.Close();

            DateTime ngayTra = DateTime.Now;

            int soNgayQuaHan = (ngayTra - ngayTraDuKien).Days;
            if (soNgayQuaHan > 0)
            {
                int tienPhat = soNgayQuaHan * 2000; // 2000 đồng mỗi ngày
                MessageBox.Show($"The book is overdue by {soNgayQuaHan} days. The late fee is {tienPhat} VND.", "Notification");
            }


            string query3 = "delete from MuonTraSach where MaPhieuMuon = " + MaMuonTra;

            mySqlCommand = new SqlCommand(query3, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SoLuongSauTra();
            Muontra();
            MessageBox.Show("Return success.", "Thông báo");
        }
        private void btnGhii_Click(object sender, EventArgs e)
        {
            int SoNgay;
            string sSql2 = "SELECT DATEDIFF(day, GETDATE(),'" + cbNgayTra.Value + "')";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt5 = new DataTable();
            SqlDataAdapter da5 = new SqlDataAdapter(mySqlCommand);
            da5.Fill(dt5);
            SoNgay = Convert.ToInt32(dt5.Rows[0][0].ToString());
            if (SoNgay > 0)
            {
                if (bien == 5)
                {
                    string checkStudentQuery = "SELECT COUNT(*) FROM SinhVien WHERE MaSV = @MaSV";
                    mySqlCommand = new SqlCommand(checkStudentQuery, mySqlconnection);
                    mySqlCommand.Parameters.AddWithValue("@MaSV", txtMaSV.Text);
                    int studentExists = Convert.ToInt32(mySqlCommand.ExecuteScalar());

                    if (studentExists == 0)
                    {
                        MessageBox.Show("Student not found. Please check the student ID and try again.", "Error");
                        return;  // Stop execution if the student does not exist
                    }

                    int SoLuongSach = 0;
                    //int MaSach = Convert.ToInt32(ttMaSach.Text);
                    //MessageBox.Show(Convert.ToString(MaSach));
                    string sSql1 = "select SoLuong from Sach where MaSach ='" + ttMaSach.Text + "'";
                    mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        SoLuongSach = Convert.ToInt32(dr["SoLuong"].ToString());
                    }
                    if (SoLuongSach > 0)
                    {
                        string sSql8 = "select * from MuonTraSach where MaSV='" + txtMaSV.Text + "'";
                        mySqlCommand = new SqlCommand(sSql8, mySqlconnection);
                        mySqlCommand.ExecuteNonQuery();
                        DataTable dt8 = new DataTable();
                        SqlDataAdapter da8 = new SqlDataAdapter(mySqlCommand);
                        da8.Fill(dt8);
                        int count = Convert.ToInt32(dt8.Rows.Count.ToString());
                        if (count > 3)
                        {
                            MessageBox.Show("This student has borrowed 3 books. Please return books to continue borrowing.", "Notification");
                            return;
                        }
                        else
                        {
                            string query2 = "insert into MuonTraSach( MaSV, MaSach, NgayMuon, NgayTra, GhiChu) values('" + txtMaSV.Text + "','" + ttMaSach.Text + "', GETDATE(),'" + cbNgayTra.Value + "',N'" + txtGhiChu.Text + "')";
                            mySqlCommand = new SqlCommand(query2, mySqlconnection);
                            mySqlCommand.ExecuteNonQuery();
                            SoLuongSauMuon();
                            Muontra();
                            SetControls(false);
                            MessageBox.Show("Borrow success.", "Nofitication");

                        }
                    }
                    else
                    {
                        MessageBox.Show("This book is out of stock", "Announce");
                    }
                }
                else
                {
                    int row = dgvMuonSach.CurrentRow.Index;
                    string MaMuonTra = dgvMuonSach.Rows[row].Cells[0].Value.ToString();
                    string query2 = "update MuonTraSach set NgayTra = '" + cbNgayTra.Value + "' where MaPhieuMuon = " + MaMuonTra;
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    Muontra();
                    SetControls(false);
                    MessageBox.Show("Gia hạn thành công.", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Thời gian trả không hợp lệ");
            }
        }

        private void btnHuyy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }
        public void SoLuongSauMuon()// Hàm này để thay đổi số lượng sách khi mượn sách.
        {
            //int MaSach = Convert.ToInt32(cbMaSach.Text);
            //MessageBox.Show(Convert.ToString(MaSach));
            string sSql1 = "select SoLuong from Sach where MaSach ='" + ttMaSach.Text + "'";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int SoLuongSach = Convert.ToInt32(dr["SoLuong"].ToString());
                int SoLuong = SoLuongSach - 1;
                //MessageBox.Show(Convert.ToString(SoLuong));
                string query2 = "update Sach set SoLuong = " + SoLuong + " where MaSach = '" + ttMaSach.Text + "'";
                mySqlCommand = new SqlCommand(query2, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
            }
        }
        public void SoLuongSauTra()// Hàm này để thay đổi số lượng sách khi trả sách.
        {
            //int MaSach = Convert.ToInt32(cbMaSach.Text);
            string sSql1 = "select SoLuong from Sach where MaSach ='" + ttMaSach.Text + "'";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int SoLuongSach = Convert.ToInt32(dr["SoLuong"].ToString());
                int SoLuong = SoLuongSach + 1;
                //MessageBox.Show(Convert.ToString(SoLuong));
                string query2 = "update Sach set SoLuong = " + SoLuong + " where MaSach = '" + ttMaSach.Text + "'";
                mySqlCommand = new SqlCommand(query2, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
            }
        }

        private void dgvMuonSach_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaPhieuMUon.Text = dgvMuonSach.Rows[r].Cells[0].Value.ToString();
            cbTenSach.Text = dgvMuonSach.Rows[r].Cells[4].Value.ToString();
            txtMaSV.Text = dgvMuonSach.Rows[r].Cells[1].Value.ToString();
            cbNgayMuon.Text = dgvMuonSach.Rows[r].Cells[5].Value.ToString();
            cbNgayTra.Text = dgvMuonSach.Rows[r].Cells[6].Value.ToString();
            txtGhiChu.Text = dgvMuonSach.Rows[r].Cells[7].Value.ToString();

            string sSql1 = "select s.MaSach, s.TenSach, tg.TacGia, s.SoLuong from Sach s join TacGia tg on s.MaTacGia = tg.MaTacGia where s.TenSach = '" + cbTenSach.Text + "'";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                ttMaSach.Text = dr["MaSach"].ToString();
                ttTenSach.Text = dr["TenSach"].ToString();
                ttTenTG.Text = dr["TacGia"].ToString();
                ttSoLuong.Text = dr["SoLuong"].ToString();
            }

            string sSql2 = "select MaSV, TenSV from SinhVien where MaSV = '" + txtMaSV.Text + "'";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(mySqlCommand);
            da2.Fill(dt2);
            foreach (DataRow dr in dt2.Rows)
            {
                ttMaSV.Text = dr["MaSV"].ToString();
                ttTenSV.Text = dr["TenSV"].ToString();

            }
        }

        private void btnQuaHan_Click(object sender, EventArgs e)
        {
            string query = "select MS.MaPhieuMuon, SV.MaSV, SV.TenSV, SV.SoDienThoai, S.TenSach,MS.NgayMuon,MS.NgayTra,MS.GhiChu from MuonTraSach MS join Sach S on S.MaSach = MS.MaSach join SinhVien SV on SV.MaSV = MS.MaSV where MS.NgayTra <= CONVERT(date,GETDATE())";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSMuon.DataSource = dt;
            lbdangmuon.Visible = false;
            lbquahan.Visible = true;
            lbTong.Text = dgvDSMuon.RowCount.ToString();
        }

        private void btnDangMuon_Click(object sender, EventArgs e)
        {
            string query = "select MS.MaPhieuMuon, SV.MaSV, SV.TenSV, SV.SoDienThoai, S.TenSach,MS.NgayMuon,MS.NgayTra,MS.GhiChu from MuonTraSach MS join Sach S on S.MaSach = MS.MaSach join SinhVien SV on SV.MaSV = MS.MaSV where MS.NgayTra > CONVERT(date,GETDATE())";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSMuon.DataSource = dt;
            lbdangmuon.Visible = true;
            lbquahan.Visible = false;
            lbTong.Text = dgvDSMuon.RowCount.ToString();
        }
        public void thongke()
        {
            string query = "select MS.MaPhieuMuon, SV.MaSV, SV.TenSV, SV.SoDienThoai, S.TenSach,MS.NgayMuon,MS.NgayTra,MS.GhiChu from MuonTraSach MS join Sach S on S.MaSach = MS.MaSach join SinhVien SV on SV.MaSV = MS.MaSV where MS.NgayTra > CONVERT(date,GETDATE())";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSMuon.DataSource = dt;
            lbdangmuon.Visible = true;
            lbquahan.Visible = false;
            lbTong.Text = dgvDSMuon.RowCount.ToString();


            NhanVienn();
            SinhVien();
            Sach();
            MuonTraSach();
            LoaiSach();
            TacGia();
            NhaXuatBan();



        }
        public void NhanVienn()
        {
            string sSql1 = "select count(*) from NhanVien";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkAdmin.Text = dt.Rows[0][0].ToString();
        }
        public void SinhVien()
        {
            string sSql1 = "select count(*) from SinhVien";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkSinhVien.Text = dt.Rows[0][0].ToString();
        }
        public void Sach()
        {
            string sSql1 = "select count(*) from Sach";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkSach.Text = dt.Rows[0][0].ToString();
        }
        public void MuonTraSach()
        {
            string sSql1 = "select count(*) from MuonTraSach";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkMuonSach.Text = dt.Rows[0][0].ToString();
        }
        public void LoaiSach()
        {
            string sSql1 = "select count(*) from LoaiSach";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TKLoaiSach.Text = dt.Rows[0][0].ToString();
        }
        public void TacGia()
        {
            string sSql1 = "select count(*) from TacGia";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TKTacGia.Text = dt.Rows[0][0].ToString();
        }
        public void NhaXuatBan()
        {
            string sSql1 = "select count(*) from NhaXuatBan";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TKNhaXB.Text = dt.Rows[0][0].ToString();
        }

        private void cbMaSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSql2 = "select MaSV, TenSV from SinhVien where MaSV = '" + txtMaSV.Text + "'";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(mySqlCommand);
            da2.Fill(dt2);
            foreach (DataRow dr in dt2.Rows)
            {
                ttMaSV.Text = dr["MaSV"].ToString();
                ttTenSV.Text = dr["TenSV"].ToString();

            }
        }

        private void dgvDSMuon_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //string MaSV, msv, tsv, sdt, sdtt;
            //int r = e.RowIndex;
            //MaSV = dgvMuonSach.Rows[r].Cells[1].Value.ToString();
            //
            //string sSql2 = "select MaSV, TenSV, SoDienThoai from SinhVien where MaSV = '" + MaSV + "'";
            //mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            //mySqlCommand.ExecuteNonQuery();
            //DataTable dt2 = new DataTable();
            //SqlDataAdapter da2 = new SqlDataAdapter(mySqlCommand);
            //da2.Fill(dt2);
            //foreach (DataRow dr in dt2.Rows)
            //{
            //    msv = dr["MaSV"].ToString();
            //    tsv = dr["TenSV"].ToString();
            //    sdt = dr["SoDienThoai"].ToString();
            //    MessageBox.Show(sdt, "Thông Tin Sinh Viên");
            //}
            //sdtt = Convert.ToString(sdt);
            //MessageBox.Show(sdt,"Thông Tin Sinh Viên");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dlr = new DialogResult();
            dlr = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;
            this.Hide();
            Login DN = new Login();
            DN.Show();
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thư mục "Documents"
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Xác định tên và đường dẫn file CSV
                string filePath = System.IO.Path.Combine(documentsPath, "MuonTraSach.csv");

                // Khởi tạo StringBuilder để xây dựng nội dung CSV
                StringBuilder csvContent = new StringBuilder();

                // Thêm tiêu đề cột vào CSV
                csvContent.AppendLine("MaPhieuMuon,MaSV,TenSV,MaSach,TenSach,NgayMuon,NgayTra,GhiChu");

                // Duyệt qua các hàng trong DataGridView và thêm vào nội dung CSV
                foreach (DataGridViewRow row in dgvMuonSach.Rows)
                {
                    // Kiểm tra nếu không phải là dòng trống
                    if (row.IsNewRow) continue;

                    // Kiểm tra và định dạng ngày tháng
                    string ngayMuon = row.Cells["NgayMuon"].Value != null ?
                        Convert.ToDateTime(row.Cells["NgayMuon"].Value).ToString("yyyy-MM-dd") : "";
                    string ngayTra = row.Cells["NgayTra"].Value != null ?
                        Convert.ToDateTime(row.Cells["NgayTra"].Value).ToString("yyyy-MM-dd") : "";

                    // Thêm dấu ngoặc kép quanh giá trị ngày tháng để Excel nhận diện đúng
                    csvContent.AppendLine($"\"{row.Cells["MaPhieuMuon"].Value}\",\"{row.Cells["MaSV"].Value}\",\"{row.Cells["TenSV"].Value}\"," +
                                          $"\"{row.Cells["MaSach"].Value}\",\"{row.Cells["TenSach"].Value}\",\"{ngayMuon}\",\"{ngayTra}\",\"{row.Cells["GhiChu"].Value}\"");
                }

                // Lưu nội dung CSV vào file với mã hóa UTF-8
                System.IO.File.WriteAllText(filePath, csvContent.ToString(), Encoding.UTF8);

                // Hiển thị thông báo thành công
                MessageBox.Show($"Export successful! File saved at:\n{filePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở file CSV sau khi lưu
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có
                MessageBox.Show($"Error exporting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TrangNhanVien_Load_1(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();

            DocGia();
            Muontra();
            cbMuontra();
            thongke();
            tracuusach();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            DialogResult dlr = new DialogResult();
            dlr = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;
            this.Hide();
            Login DN = new Login();
            DN.Show();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void cbTenSach_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
