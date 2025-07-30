using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLVT;
using Microsoft.Data.SqlClient;

namespace DAL_QLVT
{
    public class DALNhanVien
    {
        public NhanVien? getNhanVien1(string email, string password)
        {
            string sql = "SELECT Top 1 * FROM NhanVien WHERE Email=@0 AND MatKhau=@1";
            List<object> thamSo = new List<object>();
            thamSo.Add(email);
            thamSo.Add(password);
            SqlDataReader reader = DBUtil.Query(sql, thamSo);
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    NhanVien nv = new NhanVien();
                    nv.NhanVienID = reader["NhanVienID"].ToString();
                    nv.HoTen = reader["HoTen"].ToString();
                    nv.ChucVu = reader["ChucVu"].ToString();
                    nv.SoDienThoai = reader["SoDienThoai"].ToString();
                    nv.email = reader["Email"].ToString();
                    nv.MatKhau = reader["MatKhau"].ToString();
                    nv.GhiChu = reader["GhiChu"].ToString();
                    nv.VaiTro = bool.Parse(reader["VaiTro"].ToString());
                    nv.TinhTrang = bool.Parse(reader["TinhTrang"].ToString());

                    return nv;
                }
            }
            return null;
        }
        public List<NhanVien> GetAllNhanVien()
        {
            List<NhanVien> list = new List<NhanVien>();

            string query = "SELECT * FROM NhanVien"; 

            using (SqlConnection conn = new SqlConnection("Data Source=THE1712\\SQLEXPRESS;Initial Catalog=Xuong_QuanLyVatTu;Integrated Security=True;Trust Server Certificate=True"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    NhanVien nv = new NhanVien
                    {
                        NhanVienID = reader["NhanVienID"].ToString(),
                        HoTen = reader["HoTen"].ToString(),
                        ChucVu = reader["ChucVu"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString(),
                        email = reader["Email"].ToString(),
                        MatKhau = reader["MatKhau"].ToString(),
                        GhiChu = reader["GhiChu"].ToString(),
                        VaiTro = Convert.ToBoolean(reader["VaiTro"]),
                        TinhTrang = Convert.ToBoolean(reader["TinhTrang"])
                    };

                    list.Add(nv);
                }

                reader.Close();
            }

            return list;
        }

        public string generateMaNhanVien()
        {
            string prefix = "NV";
            string sql = "SELECT MAX(NhanVienID) FROM NhanVien";
            List<object> thamSo = new List<object>();
            object result = DBUtil.ScalarQuery(sql, thamSo);
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(2);
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }

            return $"{prefix}001";
        }
        public bool checkEmailExists(string email)
        {
            string sql = "SELECT COUNT(*) FROM NhanVien WHERE Email = @0";
            List<object> thamSo = new List<object>();
            thamSo.Add(email);
            object result = DBUtil.ScalarQuery(sql, thamSo);
            return Convert.ToInt32(result) > 0;
        }
        public void deleteNhanVien(string maNv)
        {
            try
            {
                string sql = "DELETE FROM NhanVien WHERE NhanVienID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maNv);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public void ResetMatKhau(string mk, string email)
        {
            try
            {
                string sql = "UPDATE NhanVien SET MatKhau = @0 WHERE Email = @1";
                List<object> thamSo = new List<object>();
                thamSo.Add(mk);
                thamSo.Add(email);
                DBUtil.Update(sql, thamSo);

            }
            catch (Exception e)
            {
                throw;
            }
        }
        public void updateNhanVien(NhanVien nv)
        {
            try
            {
                string sql = @"UPDATE NhanVien 
                   SET HoTen = @1, ChucVu = @2, SoDienThoai = @3, Email = @4, MatKhau = @5, GhiChu = @6 ,VaiTro = @7, TinhTrang = @8 
                   WHERE NhanVienID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(nv.NhanVienID);
                thamSo.Add(nv.HoTen);
                thamSo.Add(nv.ChucVu);
                thamSo.Add(nv.SoDienThoai);
                thamSo.Add(nv.email);
                thamSo.Add(nv.MatKhau);
                thamSo.Add(nv.GhiChu);
                thamSo.Add(nv.VaiTro);
                thamSo.Add(nv.TinhTrang);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public void insertNhanVien(NhanVien nv)
        {
            try
            {
                string sql = @"INSERT INTO NhanVien (NhanVienID, HoTen, ChucVu, SoDienThoai, Email, MatKhau, GhiChu, VaiTro, TinhTrang) 
                   VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8)";
                List<object> thamSo = new List<object>();
                thamSo.Add(nv.NhanVienID);
                thamSo.Add(nv.HoTen);
                thamSo.Add(nv.ChucVu);
                thamSo.Add(nv.SoDienThoai);
                thamSo.Add(nv.email);
                thamSo.Add(nv.MatKhau);
                thamSo.Add(nv.GhiChu);
                thamSo.Add(nv.VaiTro);
                thamSo.Add(nv.TinhTrang);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public List<NhanVien> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<NhanVien> list = new List<NhanVien>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    NhanVien entity = new NhanVien();
                    entity.NhanVienID = reader.GetString("NhanVienID");
                    entity.HoTen = reader.GetString("HoTen");
                    entity.ChucVu = reader.GetString("ChucVu");
                    entity.SoDienThoai = reader.GetString("SoDienThoai");
                    entity.email = reader.GetString("Email");
                    entity.MatKhau = reader.GetString("MatKhau");
                    entity.GhiChu = reader.GetString("GhiChu");
                    entity.VaiTro = reader.GetBoolean("VaiTro");
                    entity.TinhTrang = reader.GetBoolean("TinhTrang");
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }
        public List<NhanVien> selectAll()
        {
            String sql = "SELECT * FROM NhanVien";
            return SelectBySql(sql, new List<object>());
        }
        public NhanVien getNhanVien(string email, string password)
        {
            string sql = "SELECT * FROM NhanVien WHERE Email=@0 AND MatKhau=@1";
            List<object> thamSo = new List<object>();
            thamSo.Add(email);
            thamSo.Add(password);
            NhanVien nv = DBUtil.Value<NhanVien>(sql, thamSo);
            return nv;
        }
    }
}
