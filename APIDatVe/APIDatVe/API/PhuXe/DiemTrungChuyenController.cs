﻿

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
    [RoutePrefix("api/diemtrungchuyen")]
    public class DiemTrungChuyenController : ApiBase
    {

        /// <summary>
        /// Lấy ra tất cả thông tin của điểm trung chuyển
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getdiemtrungchuyen")]
        public IHttpActionResult getDiemTrungChuyen(string malotrinh)
        {

            DiemTrungChuyenDAL diemTrungChuyenDAL = new DiemTrungChuyenDAL();

            return ResponseToOk(diemTrungChuyenDAL.GetDiemTrungChuyens(malotrinh));
        }

        /// <summary>
        /// Lấy ra khách xuống, lên
        /// </summary>
        /// <param name="madiemtrungchuyen"></param>
        /// <param name="machuyenxe"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getkhachtaidiemtrungchuyen")]
        public IHttpActionResult getKhachTaiDiemTrungChuyen(string madiemtrungchuyen, string machuyenxe)
        {
            DiemTrungChuyenDAL diemTrungChuyenDAL = new DiemTrungChuyenDAL();
            return ResponseToOk(diemTrungChuyenDAL.GetKhachHangByDiemTrungChuyen(madiemtrungchuyen, machuyenxe));
        }

    }
}

