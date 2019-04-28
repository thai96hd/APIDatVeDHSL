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
    [RoutePrefix("api/giave")]
    public class GiaVeController : ApiBase
    {
        [Route("laythongtingiave")]
        [HttpGet]
        public IHttpActionResult getGiaVeGiuaHaiDiem(string _madiemdon, string _madiemden)
        {
            if (_madiemdon == "" || _madiemden == "")
            {
                return BadRequest("Mã điểm đón và mã điểm đến không được để trống");
            }

            else
            {
                GiaVeDAL giaVeDAL = new GiaVeDAL();
                GiaVeDTO bangia = giaVeDAL.LayThongTinGiaVe(_madiemdon, _madiemden);
                return ResponseToOk(bangia);
            }
        }
    }
}
