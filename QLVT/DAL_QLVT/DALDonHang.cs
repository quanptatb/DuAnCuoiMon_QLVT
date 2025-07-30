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
    public class DALDonHang
    {
        public string gerenateDonHang()
        {
            string prefix = "PBH";
            string sql = "SELECT MAX(DonHangID) FROM DonHang";
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

        public void insertDonHang(DonHang pbh)
        {
            try
            {
                string sql = @"INSERT INTO DonHang (DonHangID, KhachHangID, NhanVienID, NgayDat, TrangThai, GhiChu) 
                   VALUES (@0, @1, @2, @3, @4,@5)";
                List<object> thamSo = new List<object>();
                thamSo.Add(pbh.DonHangID);
                thamSo.Add(pbh.KhachHangID);
                thamSo.Add(pbh.NhanVienID);
                thamSo.Add(pbh.NgayDat);
                thamSo.Add(pbh.TrangThai);
                thamSo.Add(pbh.GhiChu);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public List<DonHang> TimKiem(string maDH)
        {
            string sql = "SELECT * FROM DonHang WHERE DonHangID LIKE @0";
            List<object> thamSo = new List<object>();
            thamSo.Add("%" + maDH + "%");
            return SelectBySql(sql, thamSo);
        }

        public void updateDonHang(DonHang pbh)
        {
            try
            {
                string sql = @"UPDATE DonHang 
                   SET KhachHangID = @1, NhanVienID = @2, NgayDat = @3, TrangThai = @4, GhiChu = @5
                   WHERE DonHangID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(pbh.DonHangID);
                thamSo.Add(pbh.KhachHangID);
                thamSo.Add(pbh.NhanVienID);
                thamSo.Add(pbh.NgayDat);
                thamSo.Add(pbh.TrangThai);
                thamSo.Add(pbh.GhiChu);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public void deleteDonHang(string maPhieu)
        {
            try
            {
                string sql = "DELETE FROM DonHang WHERE DonHangID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maPhieu);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public List<DonHang> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<DonHang> list = new List<DonHang>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    DonHang entity = new DonHang();
                    entity.DonHangID = reader.GetString("DonHangID");
                    entity.KhachHangID = reader.GetString("KhachHangID");
                    entity.NhanVienID = reader.GetString("NhanVienID");
                    entity.NgayDat = reader.GetDateTime("NgayDat");
                    entity.TrangThai = reader.GetString("TrangThai");
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

        public List<DonHang> selectAll(string maThe)
        {
            //String sql = "SELECT * FROM PhieuBanHang";
            List<object> param = new List<object>();
            string sql = "SELECT Don.DonHangID, Don.KhachHangID, Don.NhanVienID, Don.NgayDat,Don.TrangThai,Don.GhiChu " +
                "FROM DonHang Don INNER JOIN NhanVien nv ON Don.NhanVienID = nv.NhanVienID ";
            if (!string.IsNullOrEmpty(maThe))
            {
                sql = "SELECT don.DonHangID, don.KhachHangID, don.NhanVienID, don.NgayDat, don.TrangThai, don.GhiChu " +
               "FROM DonHang don INNER JOIN NhanVien nv ON don.NhanVienID = nv.NhanVienID ";
                param.Add(maThe);
            }

            return SelectBySql(sql, param);
        }
    }
}
