using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO_QLVT;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL_QLVT
{
    public class DALChiTietPhieuNhap
    {
        public List<ChiTietPhieuNhap> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<ChiTietPhieuNhap> list = new List<ChiTietPhieuNhap>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    ChiTietPhieuNhap entity = new ChiTietPhieuNhap
                    {
                        ChiTietNhapID = reader.GetString("ChiTietNhapID"),
                        PhieuNhapID = reader.GetString("PhieuNhapID"),
                        VatTuID = reader.GetString("VatTuID"),
                        SoLuong = reader.GetInt32("SoLuong"),
                        DonGia = reader.GetDecimal("DonGia"),
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
        public List<ChiTietPhieuNhap> SelectAll()
        {
            string sql = "SELECT * FROM ChiTietPhieuNhap";
            return SelectBySql(sql, new List<object>());
        }

        public string GenerateMaCTPN()
        {
            string prefix = "CTPN";
            string sql = "SELECT MAX(ChiTietNhapID) FROM ChiTietPhieuNhap";
            object result = DBUtil.ScalarQuery(sql, new List<object>());
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(4);
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }
            return $"{prefix}001";
        }

        public void Insert(ChiTietPhieuNhap chiTietPhieuNhap)
        {
            string sql = "INSERT INTO ChiTietPhieuNhap (ChiTietNhapID, PhieuNhapID, VatTuID, SoLuong, DonGia) " +
                         "VALUES (@0, @1, @2, @3, @4)";
            List<object> args = new List<object>
            {
                chiTietPhieuNhap.ChiTietNhapID,
                chiTietPhieuNhap.PhieuNhapID,
                chiTietPhieuNhap.VatTuID,
                chiTietPhieuNhap.SoLuong,
                chiTietPhieuNhap.DonGia
            };
            DBUtil.Update(sql, args);
        }

        public void insertListChiTiet(List<ChiTietPhieuNhap> lstChiTiet)
        {
            try
            {
                foreach (ChiTietPhieuNhap item in lstChiTiet)
                {
                    Insert(item);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public void Update(ChiTietPhieuNhap chiTietPhieuNhap)
        {
            string sql = "UPDATE ChiTietPhieuNhap SET PhieuNhapID = @1, VatTuID = @2, SoLuong = @3, DonGia = @4 " +
                         "WHERE ChiTietNhapID = @0";
            List<object> args = new List<object>
            {
                chiTietPhieuNhap.ChiTietNhapID,
                chiTietPhieuNhap.PhieuNhapID,
                chiTietPhieuNhap.VatTuID,
                chiTietPhieuNhap.SoLuong,
                chiTietPhieuNhap.DonGia
            };
            DBUtil.Update(sql, args);
        }
        public void Delete(string chiTietNhapID)
        {
            string sql = "DELETE FROM ChiTietPhieuNhap WHERE ChiTietNhapID = @0";
            List<object> args = new List<object> { chiTietNhapID };
            DBUtil.Update(sql, args);
        }
        public ChiTietPhieuNhap SelectById(string chiTietNhapID)
        {
            string sql = "SELECT * FROM ChiTietPhieuNhap WHERE ChiTietNhapID = @0";
            List<object> args = new List<object> { chiTietNhapID };
            List<ChiTietPhieuNhap> list = SelectBySql(sql, args);
            return list.FirstOrDefault();
        }
        //search
        public List<ChiTietPhieuNhap> SearchByPhieuNhapID(string phieuNhapID)
        {
            string sql = "SELECT * FROM ChiTietPhieuNhap WHERE PhieuNhapID = @0";
            List<object> args = new List<object> { phieuNhapID };
            return SelectBySql(sql, args);
        }
    }
}
