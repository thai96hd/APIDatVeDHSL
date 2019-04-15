using APIDatVe.DTO.PhuXe;
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
        [Route("checklogin")]
        public IHttpActionResult Login([FromBody] TaiKhoanDTO taiKhoanDTO)
        {
            TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();
            if (taiKhoanDAL.checkLogin(taiKhoanDTO, 3))
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
        [Route("updatepassword")]
        public IHttpActionResult updatePassWord([FromBody]TaiKhoanDTO taiKhoanDTO)
        {

            TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();
            if (taiKhoanDAL.updatePass(taiKhoanDTO))
            {
                return ResponseToOk(null);
            }
            return ResponseToOk(null, "Can not updated data", HttpStatusCode.NotImplemented);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tentaikhoan"></param>
        /// <param name="machucvu"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getthongtinphuxe")]
        public IHttpActionResult getthongtinphuxe(string tentaikhoan, Int32 machucvu)
        {
            TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();
            return ResponseToOk(taiKhoanDAL.GetThongTinPhuXe(tentaikhoan, 3));
        }
    }
}

