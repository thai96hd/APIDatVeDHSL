using APIDatVe.Database;
using APIDatVe.Helper;
using System.Linq;

namespace APIDatVe.API.QuyenTruyCap
{
    public class UserSecurity
    {
        public static bool CheckLogin(string _userName, string _passWord)
        {
            using (var db = new DB())
            {
                string passMD5 = Encode.MD5(_passWord);
                return db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == _userName && x.matkhau == passMD5) != null;
            }
        }
    }
}