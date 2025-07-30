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
    public class DALNhaCungCap
    {
        public void insertNhaCungCap(NhaCungCap ncc)
        {
            try
            {
                string sql = @"INSERT INTO NhaCungCap (NhaCungCapID, TenNhaCungCap, SoDienThoai, Email, DiaChi, NgayTao, GhiChu) 
                   VALUES (@0, @1, @2, @3, @4, @5, @6)";
                List<object> thamSo = new List<object>();
                thamSo.Add(ncc.NhaCungCapID);
                thamSo.Add(ncc.TenNhaCungCap);
                thamSo.Add(ncc.SoDienThoai);
                thamSo.Add(ncc.Email);
                thamSo.Add(ncc.DiaChi);
                thamSo.Add(ncc.NgayTao);
                thamSo.Add(ncc.GhiChu);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public string generateNhaCungCapID()
        {
            string prefix = "NCC";
            string sql = "SELECT MAX(NhaCungCapID) FROM NhaCungCap";
            List<object> thamSo = new List<object>();
            object result = DBUtil.ScalarQuery(sql, thamSo);
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(3);
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }

            return $"{prefix}001";
        }
        public List<NhaCungCap> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<NhaCungCap> list = new List<NhaCungCap>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    NhaCungCap entity = new NhaCungCap();
                    entity.NhaCungCapID = reader.GetString("NhaCungCapID");
                    entity.TenNhaCungCap = reader.GetString("TenNhaCungCap");
                    entity.SoDienThoai = reader.GetString("SoDienThoai");
                    entity.Email = reader.GetString("Email");
                    entity.DiaChi = reader.GetString("DiaChi");
                    entity.NgayTao = reader.GetDateTime(reader.GetOrdinal("NgayTao"));
                    entity.GhiChu = reader.GetString("GhiChu");
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public bool checkEmailExists(string email)
        {
            string sql = "SELECT COUNT(*) FROM NhaCungCap WHERE Email = @0";
            List<object> thamSo = new List<object>();
            thamSo.Add(email);
            object result = DBUtil.ScalarQuery(sql, thamSo);
            return Convert.ToInt32(result) > 0;
        }
        public List<NhaCungCap> selectAll()
        {
            String sql = "SELECT * FROM NhaCungCap";
            return SelectBySql(sql, new List<object>());
        }

        public NhaCungCap selectById(string id)
        {
            String sql = "SELECT * FROM NhaCungCap WHERE NhaCungCapID=@0";
            List<object> thamSo = new List<object>();
            thamSo.Add(id);
            List<NhaCungCap> list = SelectBySql(sql, thamSo);
            return list.Count > 0 ? list[0] : null;
        }
        public void updateNhaCungCap(NhaCungCap ncc)
        {
            try
            {
                string sql = @"UPDATE NhaCungCap
                   SET TenNhaCungCap = @1, SoDienThoai = @2, Email = @3, DiaChi = @4, NgayTao = @5, GhiChu = @6
                   WHERE NhaCungCapID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(ncc.NhaCungCapID);
                thamSo.Add(ncc.TenNhaCungCap);
                thamSo.Add(ncc.SoDienThoai);
                thamSo.Add(ncc.Email);
                thamSo.Add(ncc.DiaChi);
                thamSo.Add(ncc.NgayTao);
                thamSo.Add(ncc.GhiChu);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public void deleteNhaCungCap(string maNhaCungCap)
        {
            try
            {
                string sql = "DELETE FROM NhaCungCap WHERE NhaCungCapID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maNhaCungCap);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public List<NhaCungCap> GetAllNhaCungCap()
        {
            List<NhaCungCap> list = new List<NhaCungCap>();

            string query = "SELECT * FROM NhaCungCap";

            using (SqlConnection conn = new SqlConnection("Data Source=THE1712\\SQLEXPRESS;Initial Catalog=Xuong_QuanLyVatTu;Integrated Security=True;Trust Server Certificate=True"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    NhaCungCap ncc = new NhaCungCap
                    {
                        NhaCungCapID = reader["NhaCungCapID"].ToString(),
                        TenNhaCungCap = reader["TenNhaCungCap"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString(),
                        Email = reader["Email"].ToString(),
                        DiaChi = reader["DiaChi"].ToString(),
                        NgayTao = reader.GetDateTime(reader.GetOrdinal("NgayTao")),
                        GhiChu = reader["GhiChu"].ToString()
                    };

                    list.Add(ncc);
                }

                reader.Close();
            }

            return list;
        }
    }
}
