
using DTO_QLVT;

namespace UTIL_QLVT
{
    public class AuthUtil
    {
        public static NhanVien user = null;
        public static bool IsLogin()
        {
            if (user == null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(user.NhanVienID))
            {
                return false;
            }
            return true;
        }
    }
}
