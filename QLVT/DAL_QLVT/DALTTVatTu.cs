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
    public class DALTTVatTu
    {
        public List<TrangThaiVatTu> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<TrangThaiVatTu> list = new List<TrangThaiVatTu>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    TrangThaiVatTu entity = new TrangThaiVatTu();
                    entity.TrangThaiID = reader.GetString("TrangThaiID");
                    entity.TenTrangThai = reader.GetString("TenTrangThai");
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }
        public List<TrangThaiVatTu> selectAll()
        {
            //string sql = "SELECT * FROM TrangThaiVatTu";
            String sql = "SELECT * FROM TrangThaiVatTu";
            return SelectBySql(sql, new List<object>());
        }
        public TrangThaiVatTu selectById(string id)
        {
            String sql = "SELECT * FROM TrangThaiVatTu WHERE TrangThaiID=@0";
            List<object> thamSo = new List<object>();
            thamSo.Add(id);
            List<TrangThaiVatTu> list = SelectBySql(sql, thamSo);
            return list.Count > 0 ? list[0] : null;
        }

        public void insertTrangThaiVatTu(TrangThaiVatTu ttVT)
        {
            try
            {
                string sql = @"INSERT INTO TrangThaiVatTu (TrangThaiID, TenTrangThai) 
                   VALUES (@0, @1)";
                List<object> thamSo = new List<object>();
                thamSo.Add(ttVT.TrangThaiID);
                thamSo.Add(ttVT.TenTrangThai);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public void updateTrangThaiVatTu(TrangThaiVatTu ttVT)
        {
            try
            {
                string sql = @"UPDATE TrangThaiVatTu 
                   SET TenTrangThai = @1
                   WHERE TrangThaiID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(ttVT.TrangThaiID);
                thamSo.Add(ttVT.TenTrangThai);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public void deleteTrangThaiVatTu(string maTTVT)
        {
            try
            {
                string sql = "DELETE FROM TrangThaiVatTu WHERE TrangThaiID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maTTVT);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public void searchTrangThaiVatTu(string maTTVT)
        {
            try
            {
                string sql = "Select TrangThaiID, TenTrangThai from TrangThaiVatTu where TrangThaiVatTu = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maTTVT);
                DBUtil.Update(sql,thamSo);
            
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public string generateTrangThaiID()
        {
            string prefix = "TTVT";
            string sql = "SELECT MAX(TrangThaiID) FROM TrangThaiVatTu";
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
        public List<TrangThaiVatTu> TimKiem(string maTTVT)
        {
            string sql = "SELECT * FROM TrangThaiVatTu WHERE TrangThaiID LIKE @0";
            List<object> thamSo = new List<object>();
            thamSo.Add("%" + maTTVT + "%");
            return SelectBySql(sql, thamSo);
        }
    }
}
