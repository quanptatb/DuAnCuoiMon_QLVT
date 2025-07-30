using DAL_QLVT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLVT
{
    public class Email
    {
        public static void GuiEmail( string subject, string body,string tenNhanVien)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("thesadboiz1712@gmail.com", tenNhanVien);  // Email gửi đi
            mail.To.Add("thesadboiz1712@gmail.com");
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("thesadboiz1712@gmail.com", "jyrf ilbv uvsk lwnb"); // Dùng app password

            smtp.Send(mail);
        }
    }
}
