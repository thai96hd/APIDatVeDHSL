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
    [RoutePrefix("api/journeybook")]
    public class PhuXeDatVeController : ApiBase
    {
        [HttpPost]
        [Route("add")]
        public IHttpActionResult datve([FromBody]DatVeDTO datVe)
        {

            DatVeDAL datVeDAL = new DatVeDAL();
            if (datVeDAL.datve(datVe))
            {
                return ResponseToOk(null);
            }
            else
            {
                return ResponseToOk(null, "Không thể thêm", HttpStatusCode.NotImplemented);
            }
        }
    }
}
