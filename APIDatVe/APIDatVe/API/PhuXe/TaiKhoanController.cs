
using APIDatVe.DAL.PhuXe;
using APIDatVe.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace APIDatVe.API.PhuXe
{
    [RoutePrefix("api/taikhoan")]
    public class TaiKhoanController : ApiBase
    {
        /// <summary>
        /// kiểm tra đăng nhập của phụ xe
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("check_login")]
        public IHttpActionResult Login(string username, string password)
        {
            TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();
            if (taiKhoanDAL.checkLogin(username, password, 3))
            {
                return ResponseToOk(null);
            }
            return ResponseToOk(null, "Can not found username or password", HttpStatusCode.NotFound);

        }
        /// <summary>
        /// 
        /// update pass
        /// </summary>
        [HttpPost]
        public IHttpActionResult updatePassWord(string username, string password)
        {

            TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();
            if (taiKhoanDAL.updatePass(username, password))
            {
                return ResponseToOk(null);
            }
            return ResponseToOk(null, "Can not updated data", HttpStatusCode.NotImplemented);
        }
    }
}

