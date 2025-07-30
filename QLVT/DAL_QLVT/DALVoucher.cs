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
    public class DALVoucher
    {
        public void insertVoucher(Voucher vc)
        {
            try
            {
                string sql = @"INSERT INTO KhuyenMai (KhuyenMaiID, TenKhuyenMai, NgayBatDau, NgayKetThuc, GiamGiaPhanTram, GhiChu) 
           VALUES (@0, @1, @2, @3, @4, @5)";
                List<object> thamSo = new List<object>();
                thamSo.Add(vc.KhuyenMaiID);
                thamSo.Add(vc.TenKhuyenMai);
                thamSo.Add(vc.NgayBatDau);
                thamSo.Add(vc.NgayKetThuc);
                thamSo.Add(vc.GiamGiaPhanTram);
                thamSo.Add(vc.GhiChu);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public string generateKhuyenMaiID()
        {
            string prefix = "KM";
            string sql = "SELECT MAX(KhuyenMaiID) FROM KhuyenMai";
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

        public List<Voucher> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<Voucher> list = new List<Voucher>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    Voucher entity = new Voucher();
                    entity.KhuyenMaiID = reader.GetString("KhuyenMaiID");
                    entity.TenKhuyenMai = reader.GetString("TenKhuyenMai");
                    entity.NgayBatDau = reader.GetDateTime(reader.GetOrdinal("NgayBatDau"));
                    entity.NgayKetThuc = reader.GetDateTime(reader.GetOrdinal("NgayKetThuc"));
                    entity.GiamGiaPhanTram = reader.GetInt32(reader.GetOrdinal("GiamGiaPhanTram"));
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

        public Voucher selectById(string id)
        {
            String sql = "SELECT * FROM KhuyenMai WHERE KhuyenMaiID = @0";
            List<object> thamSo = new List<object>();
            thamSo.Add(id);
            List<Voucher> list = SelectBySql(sql, thamSo);
            return list.Count > 0 ? list[0] : null;
        }
        public List<Voucher> selectAll()
        {
            String sql = "SELECT * FROM KhuyenMai";
            return SelectBySql(sql, new List<object>());
        }

        public void updateVoucher(Voucher vc)
        {
            try
            {
                string sql = @"UPDATE KhuyenMai SET TenKhuyenMai = @1, NgayBatDau = @2, NgayKetThuc = @3, GiamGiaPhanTram = @4, GhiChu = @5
            WHERE KhuyenMaiID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(vc.KhuyenMaiID);
                thamSo.Add(vc.TenKhuyenMai);
                thamSo.Add(vc.NgayBatDau);
                thamSo.Add(vc.NgayKetThuc);
                thamSo.Add(vc.GiamGiaPhanTram);
                thamSo.Add(vc.GhiChu);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public void deleteVoucher(string maKhuyenMai)
        {
            try
            {
                string sql = "DELETE FROM KhuyenMai WHERE KhuyenMaiID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maKhuyenMai);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public List<Voucher> GetAllVoucher()
        {
            List<Voucher> list = new List<Voucher>();

            string query = "SELECT * FROM KhuyenMai";

            using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-K038H12\SQLEXPRESS;Initial Catalog=Xuong_QuanLyVatTu;Integrated Security=True;Trust Server Certificate=True"))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Voucher vc = new Voucher();
                    {
                        vc.KhuyenMaiID = reader.GetString("KhuyenMaiID");
                        vc.TenKhuyenMai = reader.GetString("TenKhuyenMai");
                        vc.NgayBatDau = reader.GetDateTime(reader.GetOrdinal("NgayBatDau"));
                        vc.NgayKetThuc = reader.GetDateTime(reader.GetOrdinal("NgayKetThuc"));
                        vc.GiamGiaPhanTram = reader.GetInt32(reader.GetOrdinal("GiamGiaPhanTram"));
                        vc.GhiChu = reader.GetString("GhiChu");
                    }
                    ;

                    list.Add(vc);
                }

                reader.Close();
            }

            return list;
        }
    }
}
