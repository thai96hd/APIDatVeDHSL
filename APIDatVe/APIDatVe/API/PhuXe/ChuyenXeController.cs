using APIDatVe.DAL.PhuXe;
using APIDatVe.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace APIDatVe.API.PhuXe
{
    [RoutePrefix("api/chuyenxe")]
    public class ChuyenXeController : ApiBase
    {
 
        /// <summary>
        /// Lấy ra lịch sử của phụ xe: tính từ ngày trk ngày vào app
        /// </summary>
        /// <param name="tentaikhoan"></param>
        /// <param name="manhanvien"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getlichtrinhphuxe")]
        public IHttpActionResult getLichTrinhPhuXe(string tentaikhoan, string manhanvien)
        {
            ChuyenXeDAL chuyenXeDAL = new ChuyenXeDAL();
            return ResponseToOk(chuyenXeDAL.GetLichTrinhPhuXes(tentaikhoan, manhanvien));
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="tentaikhoan"></param>
        /// <param name="manhanvien"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getlichtrinhdatenow")]
        public IHttpActionResult getLichTrinhDateNow(string tentaikhoan, string manhanvien)
        {
            ChuyenXeDAL chuyenXeDAL = new ChuyenXeDAL();
            return ResponseToOk(chuyenXeDAL.GetLichTrinhDateNow(tentaikhoan, manhanvien));
        }
    }
}
