using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DAL_QLVT;

namespace BLL_QLVT
{
    public class BUSQuenMatKhau
    {
        private DALNhanVien dal = new DALNhanVien();
        private string currentCode = "";
        private DateTime codeSentTime;
        private string lastEmail = "";

        public bool GuiMaXacNhan(string email)
        {
            if (!dal.checkEmailExists(email))
                return false;

            currentCode = GenerateCode();
            codeSentTime = DateTime.Now;
            lastEmail = email;

            SendEmail(email, currentCode);
            return true;
        }

        public bool XacNhanMa(string email, string code)
        {
            if (email != lastEmail) return false;
            if ((DateTime.Now - codeSentTime).TotalMinutes > 5) return false; // mã hết hạn 5 phút
            return code == currentCode;
        }

        public void DoiMatKhau(string email, string newPassword)
        {
            dal.ResetMatKhau(newPassword, email);
        }

        private string GenerateCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private void SendEmail(string toEmail, string code)
        {
            string fromEmail = "thesadboiz1712@gmail.com";
            string appPass = "jyrf ilbv uvsk lwnb";
            MailMessage mail = new MailMessage(fromEmail, toEmail, "Mã xác nhận", $"Mã xác nhận của bạn: {code}");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(fromEmail, appPass),
                EnableSsl = true
            };
            smtp.Send(mail);
        }
    }
}
