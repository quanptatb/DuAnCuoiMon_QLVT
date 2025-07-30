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
    public class DALLoaiVatTu
    {
        public List<LoaiVatTu> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<LoaiVatTu> list = new List<LoaiVatTu>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    LoaiVatTu entity = new LoaiVatTu();
                    entity.LoaiVatTuID = reader.GetString("LoaiVatTuID");
                    entity.TenLoaiVatTu = reader.GetString("TenLoaiVatTu");
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

        public List<LoaiVatTu> selectAll()
        {
            String sql = "SELECT * FROM LoaiVatTu";
            return SelectBySql(sql, new List<object>());
        }

        public string generateMaLoaiVatTu()
        {
            string prefix = "LVT";
            string sql = "SELECT MAX(LoaiVatTuID) FROM LoaiVatTu";
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

        public void insertLoaiVatTu(LoaiVatTu loaiVT)
        {
            try
            {
                string sql = @"INSERT INTO LoaiVatTu (LoaiVatTuID, TenLoaiVatTu, NgayTao, GhiChu) 
                   VALUES (@0, @1, @2, @3)";
                List<object> thamSo = new List<object>();
                thamSo.Add(loaiVT.LoaiVatTuID);
                thamSo.Add(loaiVT.TenLoaiVatTu);
                thamSo.Add(loaiVT.NgayTao);
                thamSo.Add(loaiVT.GhiChu);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void updateLoaiVatTu(LoaiVatTu loaiVT)
        {
            try
            {
                string sql = @"UPDATE LoaiVatTu 
                   SET TenLoaiVatTu = @1, NgayTao = @2, GhiChu = @3
                   WHERE LoaiVatTuID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(loaiVT.LoaiVatTuID);
                thamSo.Add(loaiVT.TenLoaiVatTu);
                thamSo.Add(loaiVT.NgayTao);
                thamSo.Add(loaiVT.GhiChu);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e) { throw; }
        }

        public void deleteLoaiVatTu(string maLoaiVT)
        {
            try
            {
                string sql = "DELETE FROM LoaiVatTu WHERE LoaiVatTuID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maLoaiVT);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public List<LoaiVatTu> TimKiem(string maVT)
        {
            string sql = "SELECT * FROM LoaiVatTu WHERE LoaiVatTuID LIKE @0";
            List<object> thamSo = new List<object>();
            thamSo.Add("%" + maVT + "%");
            return SelectBySql(sql, thamSo);
        }
    }
}
