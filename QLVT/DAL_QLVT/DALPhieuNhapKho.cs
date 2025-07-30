using DTO_QLVT;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLVT
{
    public class DALPhieuNhapKho
    {
        public List<PhieuNhapKho> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<PhieuNhapKho> list = new List<PhieuNhapKho>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    PhieuNhapKho entity = new PhieuNhapKho
                    {
                        PhieuNhapID = reader.GetString("PhieuNhapID"),
                        NhaCungCapID = reader.GetString("NhaCungCapID"),
                        NhanVienID = reader.GetString("NhanVienID"),
                        NgayNhap = reader.GetDateTime("NgayNhap"),
                        GhiChu = reader.IsDBNull("GhiChu") ? null : reader.GetString("GhiChu")
                    };
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }
        public List<PhieuNhapKho> selectAll()
        {
            string sql = "SELECT * FROM PhieuNhapKho";
            return SelectBySql(sql, new List<object>());
        }

        public string generateMaPN()
        {
            string prefix = "PN";
            string sql = "SELECT MAX(PhieuNhapID) FROM PhieuNhapKho";
            object result = DBUtil.ScalarQuery(sql, new List<object>());
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(2);
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }
            return $"{prefix}001";
        }

        public void Insert(PhieuNhapKho phieuNhap)
        {
            string sql = "INSERT INTO PhieuNhapKho (PhieuNhapID, NhaCungCapID, NhanVienID, NgayNhap, GhiChu) " +
                         "VALUES (@0, @1, @2, @3, @4)";
            List<object> args = new List<object>
            {
                phieuNhap.PhieuNhapID,
                phieuNhap.NhaCungCapID,
                phieuNhap.NhanVienID,
                phieuNhap.NgayNhap,
                phieuNhap.GhiChu
            };
            DBUtil.Update(sql, args);
        }

        public void Update(PhieuNhapKho phieuNhap)
        {
            string sql = "UPDATE PhieuNhapKho SET NhaCungCapID = @1, NhanVienID = @2, NgayNhap = @3, GhiChu = @4 " +
                         "WHERE PhieuNhapID = @0";
            List<object> args = new List<object>
            {
                phieuNhap.PhieuNhapID,
                phieuNhap.NhaCungCapID,
                phieuNhap.NhanVienID,
                phieuNhap.NgayNhap,
                phieuNhap.GhiChu
            };
            DBUtil.Update(sql, args);
        }

        public void Delete(string phieuNhapID)
        {
            string sql = "DELETE FROM PhieuNhapKho WHERE PhieuNhapID = @0";
            List<object> args = new List<object> { phieuNhapID };
            DBUtil.Update(sql, args);
        }

        public List<PhieuNhapKho> Search(string keyword)
        {
            string sql = "SELECT * FROM PhieuNhapKho WHERE PhieuNhapID LIKE @0 OR NhaCungCapID LIKE @0";
            List<object> args = new List<object>();
            args.Add("%" + keyword + "%");
            return SelectBySql(sql, args);
        }
        public PhieuNhapKho selectById(string phieuNhapID)
        {
            string sql = "SELECT * FROM PhieuNhapKho WHERE PhieuNhapID = @0";
            List<object> args = new List<object> { phieuNhapID };
            List<PhieuNhapKho> result = SelectBySql(sql, args);
            return result.FirstOrDefault();
        }
    }
}
