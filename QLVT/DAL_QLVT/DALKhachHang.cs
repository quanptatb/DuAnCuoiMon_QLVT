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
      public class DALKhachHang
    {
        public string generateMaKhachHang()
        {
            string prefix = "KH";
            string sql = "SELECT MAX(KhachHangID) FROM KhachHang";
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
        public void InsertKhachHang(KhachHang kh)
        {
            try
            {
                string sql = @"INSERT INTO KhachHang (KhachHangID, HoTen, DiaChi, SoDienThoai, Email, NgayTao, GhiChu) 
                               VALUES (@0, @1, @2, @3, @4, @5, @6)";
                List<object> thamSo = new List<object>();
                thamSo.Add(kh.KhachHangID);
                thamSo.Add(kh.HoTen);
                thamSo.Add(kh.DiaChi);
                thamSo.Add(kh.SoDienThoai);
                thamSo.Add(kh.Email);
                thamSo.Add(kh.NgayTao);
                thamSo.Add(kh.GhiChu);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public List<KhachHang> selectAll()
        {
            String sql = "SELECT * FROM KhachHang";
            return SelectBySql(sql, new List<object>());
        }
        public KhachHang selectById(string id)
        {
            String sql = "SELECT * FROM KhachHang WHERE KhachHangID=@0";
            List<object> thamSo = new List<object>();
            thamSo.Add(id);
            List<KhachHang> list = SelectBySql(sql, thamSo);
            return list.Count > 0 ? list[0] : null;
        }
        public void updateKhachHang(KhachHang kh)
        {
            try
            {
                string sql = @"UPDATE KhachHang 
            SET HoTen = @1, SoDienThoai = @2, Email = @3, DiaChi = @4, NgayTao = @5, GhiChu = @6 
            WHERE KhachHangID = @0";

                List<object> thamSo = new List<object>
        {
            kh.KhachHangID,
            kh.HoTen,
            kh.SoDienThoai,
            kh.Email,
            kh.DiaChi,
            kh.NgayTao,
            kh.GhiChu
        };

                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw; 
            }
        }
        public void deleteKhachHang(string khachHangID)
        {
            try
            {
                string sql = "DELETE FROM KhachHang WHERE KhachHangID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(khachHangID);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public List<KhachHang> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<KhachHang> list = new List<KhachHang>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    KhachHang entity = new KhachHang();
                    entity.KhachHangID = reader.GetString("KhachHangID");
                    entity.HoTen = reader.GetString("HoTen");
                    entity.SoDienThoai = reader.GetString("SoDienThoai");
                    entity.Email = reader.GetString("Email");
                    entity.DiaChi = reader.GetString("DiaChi");
                    entity.NgayTao = reader.GetDateTime("NgayTao");
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
            string sql = "SELECT COUNT(*) FROM KhachHang WHERE Email = @0";
            List<object> thamSo = new List<object>();
            thamSo.Add(email);
            object result = DBUtil.ScalarQuery(sql, thamSo);
            return Convert.ToInt32(result) > 0;
        }
        public List<KhachHang> SearchKhachHang(string maKH)
        {
            string sql = "SELECT * FROM KhachHang WHERE KhachHangID LIKE @0";
            List<object> param = new List<object> { "%" + maKH + "%" };
            return SelectBySql(sql, param); 
        }

    }
}

