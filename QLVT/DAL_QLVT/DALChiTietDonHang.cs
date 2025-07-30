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
    public class DALChiTietPhieu
    {
        public string generateChiTietID()
        {
            string prefix = "CTP";
            string sql = "SELECT MAX(ChiTietDonHangID) FROM ChiTietDonHang";
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

        public List<ChiTietDonHang> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<ChiTietDonHang> list = new List<ChiTietDonHang>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    ChiTietDonHang entity = new ChiTietDonHang();
                    entity.ChiTietDonHangID = reader.GetString("ChiTietDonHangID");
                    entity.DonHangID = reader.GetString("DonHangID");
                    entity.VatTuID = reader.GetString("VatTuID");
                    entity.SoLuong = reader.GetInt32("SoLuong");
                    entity.DonGia = reader.GetDecimal("DonGia");
                    entity.TenSanPham = reader.GetString("TenSanPham");
                    entity.TrangThaiID = reader.GetString("TrangThaiID");
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<ChiTietDonHang> selectChiTietByDonHangID(string maPhieu)
        {
            string sql = "SELECT ctdh.ChiTietDonHangID, ctdh.DonHangID, ctdh.VatTuID, ctdh.SoLuong, ctdh.DonGia,ctdh.TrangThai, vt.TrangThaiID, vt.TenVatTu AS TenSanPham " +
             "FROM ChiTietDonHang ctdh " +
             "INNER JOIN VatTu vt ON ctdh.VatTuID = vt.VatTuID " +
             "WHERE ctdh.DonHangID = @0";

            List<object> thamSo = new List<object>();
            thamSo.Add(maPhieu);
            return SelectBySql(sql, thamSo);
        }

        public void insertChiTiet(ChiTietDonHang ct)
        {
            try
            {
                string sql = @"INSERT INTO ChiTietDonHang (ChiTietDonHangID, DonHangID, VatTuID, SoLuong, DonGia,TrangThai) VALUES (@0, @1, @2, @3, @4,@5)";
                List<object> thamSo = new List<object>();
                thamSo.Add(ct.ChiTietDonHangID);
                thamSo.Add(ct.DonHangID);
                thamSo.Add(ct.VatTuID);
                thamSo.Add(ct.SoLuong);
                thamSo.Add(ct.DonGia);
                thamSo.Add(ct.TrangThai);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public void insertListChiTiet(List<ChiTietDonHang> lstChiTiet)
        {
            try
            {
                foreach (ChiTietDonHang item in lstChiTiet)
                {
                    insertChiTiet(item);
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public void updateSoluong(ChiTietDonHang ct)
        {
            try
            {
                string sql = @"UPDATE ChiTietDonHang 
                   SET SoLuong = @1 
                   WHERE ChiTietDonHangID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(ct.ChiTietDonHangID);
                thamSo.Add(ct.SoLuong);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public void deleteChiTietPhieu(string Id)
        {
            try
            {
                string sql = "DELETE FROM ChiTietDonHang WHERE ChiTietDonHangID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(Id);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
