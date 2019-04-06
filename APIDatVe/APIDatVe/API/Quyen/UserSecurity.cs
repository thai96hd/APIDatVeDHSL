using APIDatVe.Database;
using APIDatVe.Helper;

namespace APIDatVe.API.Quyen
{
    public class UserSecurity
    {
        public static bool CheckLogin(string _userName, string _passWord)
        {
            using (var db = new DB())
            {
                string passMD5 = Encode.MD5(_passWord);
                return true;
            }
        }
    }
}