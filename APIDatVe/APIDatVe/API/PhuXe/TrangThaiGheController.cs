
using APIDatVe.DAL.PhuXe;
using APIDatVe.DTO.PhuXe;
using APIDatVe.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDatVe.API.PhuXe
{
    [RoutePrefix("api/trangthaighe")]
    public class TrangThaiGheController : ApiBase
    {
        [HttpPost]
        [Route("update")]
        public IHttpActionResult updateTrangThaiGhe([FromBody]TrangThaiGheDTO trangThaiGhe)
        {
            TrangThaiGheDAL trangThaiGheDAL = new TrangThaiGheDAL();
            if (trangThaiGheDAL.updateGhe(trangThaiGhe))
            {
                return ResponseToOk(null);
            }
            else
            {
                return ResponseToOk(null, "can not update data", HttpStatusCode.NotImplemented);
            }


        }
    }
}
