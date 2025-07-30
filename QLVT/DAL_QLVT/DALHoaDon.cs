using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DTO_QLVT;
using Microsoft.Data.SqlClient;

namespace DAL_QLVT
{
    public class DALHoaDon
    {
        
        public string generateMaHD()
        {
            string prefix = "HD";
            string sql = "SELECT MAX(HoaDonID) FROM HoaDon";
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
        public void insertHoaDon(HoaDon hd)
        {
            try
            {
                string sql = @"INSERT INTO HoaDon (HoaDonID, DonHangID, TongTien, NgayThanhToan, PhuongThucThanhToan) 
                   VALUES (@0, @1, @2, @3, @4)";
                List<object> thamSo = new List<object>();
                thamSo.Add(hd.HoaDonID);
                thamSo.Add(hd.DonHangID);
                thamSo.Add(hd.TongTien);
                thamSo.Add(hd.NgayThanhToan);
                thamSo.Add(hd.PhuongThucThanhToan);
                thamSo.Add(hd.TrangThai);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public List<HoaDon> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<HoaDon> list = new List<HoaDon>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    HoaDon entity = new HoaDon();
                    entity.HoaDonID = reader["HoaDonID"].ToString();
                    entity.DonHangID = reader["DonHangID"].ToString();
                    entity.TongTien = Convert.ToDecimal(reader["TongTien"]);
                    entity.NgayThanhToan = Convert.ToDateTime(reader["NgayThanhToan"]);
                    entity.PhuongThucThanhToan = reader["PhuongThucThanhToan"].ToString();
                    list.Add(entity);
                }

            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<HoaDon> selectAll(string maHD)
        {
            List<object> param = new List<object>();
            string sql = "SELECT HoaDonID, DonHangID, TongTien, NgayThanhToan, PhuongThucThanhToan FROM HoaDon";

            if (!string.IsNullOrEmpty(maHD))
            {
                sql += " WHERE HoaDonID LIKE @0";
                param.Add($"%{maHD}%");
            }

            return SelectBySql(sql, param);
        }

        public void updateHoaDon(HoaDon hd)
        {
            try
            {
                string sql = @"UPDATE HoaDon 
                   SET DonHangID = @1, TongTien = @2, NgayThanhToan = @3, PhuongThucThanhToan = @4
                   WHERE HoaDonID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(hd.HoaDonID);
                thamSo.Add(hd.DonHangID);
                thamSo.Add(hd.TongTien);
                thamSo.Add(hd.NgayThanhToan);
                thamSo.Add(hd.PhuongThucThanhToan);
                thamSo.Add(hd.TrangThai);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public void deleteHoaDon(string maHD)
        {
            try
            {
                string sql = "DELETE FROM HoaDon WHERE HoaDonID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maHD);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public List<HoaDon> SearchHoaDon(string maHD)
        {
            List<HoaDon> danhSachHoaDon = new List<HoaDon>();

            string sql = "SELECT * FROM HoaDon WHERE HoaDonID LIKE @0";
            List<object> args = new List<object> { "%" + maHD + "%" };

            try
            {
                using (SqlDataReader reader = DBUtil.Query(sql, args, CommandType.Text))
                {
                    while (reader.Read())
                    {
                        HoaDon hd = new HoaDon
                        {
                            HoaDonID = reader["HoaDonID"].ToString(),
                            NgayThanhToan = Convert.ToDateTime(reader["NgayThanhToan"]),
                            DonHangID = reader["DonHangID"].ToString(),
                            TongTien = Convert.ToDecimal(reader["TongTien"]),
                            PhuongThucThanhToan = reader["PhuongThucThanhToan"].ToString()

                        };

                        danhSachHoaDon.Add(hd);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Có thể log lỗi hoặc throw lại
                throw new Exception("Lỗi khi truy vấn hóa đơn: " + ex.Message);
            }

            return danhSachHoaDon;
        }
    }
}
