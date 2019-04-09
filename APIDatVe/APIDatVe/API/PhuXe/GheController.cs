
using APIDatVe.DAL;
using APIDatVe.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDatVe.API.PhuXe
{
    [RoutePrefix("api/ghe")]

    public class GheController : ApiBase
    {
        /// <summary>
        /// Lấy thông tin của vé xe, chi tiết vé xe, khách hàng
        /// </summary>
        /// <param name="maxe"></param>
        /// <param name="maghe"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getthongtinghe")]
        public IHttpActionResult getThongTinGhe([FromUri]String maxe, [FromUri] string maghe)
        {

            GheDAL gheDAL = new GheDAL();

            return ResponseToOk(gheDAL.GetGhes(maxe, maghe));
        }



    }
}
