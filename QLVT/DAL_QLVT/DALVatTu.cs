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
    public class DALVatTu
    {
        public List<VatTu> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<VatTu> list = new List<VatTu>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    VatTu entity = new VatTu();
                    entity.VatTuID = reader.GetString("VatTuID");
                    entity.LoaiVatTuID = reader.GetString("LoaiVatTuID");
                    entity.TenVatTu = reader.GetString("TenVatTu");
                    entity.DonGia = reader.GetDecimal("DonGia");
                    entity.SoLuongTon = reader.GetInt32("SoLuongTon");
                    entity.NhaCungCapID = reader.GetString("NhaCungCapID");
                    entity.NgayTao = reader.GetDateTime("NgayTao");
                    entity.GhiChu = reader.GetString("GhiChu");
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
        public List<VatTu> TimKiem(string maVT)
        {
            string sql = "SELECT * FROM VatTu WHERE VatTuID LIKE @0";
            List<object> thamSo = new List<object>();
            thamSo.Add("%" + maVT + "%");
            return SelectBySql(sql, thamSo);
        }
        public List<VatTu> selectAll(string trangThaiID = null)
        {
            //string sql = "SELECT * FROM VatTu";
            string sql = "SELECT vt.VatTuID, lvt.LoaiVatTuID, vt.TenVatTu, vt.DonGia, vt.SoLuongTon, nc.NhaCungCapID, vt.NgayTao, vt.GhiChu, tt.TrangThaiID " +
                "FROM VatTu vt INNER JOIN LoaiVatTu  lvt ON vt.LoaiVatTuID = lvt.LoaiVatTuID " +
                " INNER JOIN NhaCungCap nc ON vt.NhaCungCapID = nc.NhaCungCapID " +
                " INNER JOIN TrangThaiVatTu tt ON vt.TrangThaiID = tt.TrangThaiID";
            List<object> p = new List<object>();
            if (!string.IsNullOrEmpty(trangThaiID))
            {
                sql += " WHERE vt.TrangThaiID = @0";
                p.Add(trangThaiID);
            }



            return SelectBySql(sql, p);
        }
        public VatTu selectById(string id)
        {
            String sql = "SELECT * FROM VatTu WHERE VatTuID=@0";
            List<object> thamSo = new List<object>();
            thamSo.Add(id);
            List<VatTu> list = SelectBySql(sql, thamSo);
            return list.Count > 0 ? list[0] : null;
        }

        public void insertVatTu(VatTu vt)
        {
            try
            {
                string sql = @"INSERT INTO VatTu (VatTuID, LoaiVatTuID, TenVatTu, DonGia, SoLuongTon, NhaCungCapID, NgayTao, GhiChu, TrangThaiID) 
                   VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8)";
                List<object> thamSo = new List<object>();
                thamSo.Add(vt.VatTuID);
                thamSo.Add(vt.LoaiVatTuID);
                thamSo.Add(vt.TenVatTu);
                thamSo.Add(vt.DonGia);
                thamSo.Add(vt.SoLuongTon);
                thamSo.Add(vt.NhaCungCapID);
                thamSo.Add(vt.NgayTao);
                thamSo.Add(vt.GhiChu);
                thamSo.Add(vt.TrangThaiID);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public void updateVatTu(VatTu vt)
        {
            try
            {
                string sql = @"UPDATE VatTu 
                   SET LoaiVatTuID = @1, TenVatTu = @2, DonGia = @3, SoLuongTon = @4, NhaCungCapID = @5, NgayTao = @6, GhiChu = @7, TrangThaiID = @8
                   WHERE VatTuID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(vt.VatTuID);
                thamSo.Add(vt.LoaiVatTuID);
                thamSo.Add(vt.TenVatTu);
                thamSo.Add(vt.DonGia);
                thamSo.Add(vt.SoLuongTon);
                thamSo.Add(vt.NhaCungCapID);
                thamSo.Add(vt.NgayTao);
                thamSo.Add(vt.GhiChu);
                thamSo.Add(vt.TrangThaiID);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public void deleteVatTu(string maVT)
        {
            try
            {
                string sql = "DELETE FROM VatTu WHERE VatTuID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maVT);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public string generateVatTuID()
        {
            string prefix = "VT";
            string sql = "SELECT MAX(VatTuID) FROM VatTu";
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

    }
}
