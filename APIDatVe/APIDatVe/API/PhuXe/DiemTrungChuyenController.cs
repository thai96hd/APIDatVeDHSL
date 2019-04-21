

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
    [RoutePrefix("api/diemtrungchuyen")]
    public class DiemTrungChuyenController : ApiBase
    {

        /// <summary>
        /// Lấy ra tất cả thông tin của điểm trung chuyển
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getdiemtrungchuyen")]
        public IHttpActionResult getDiemTrungChuyen()
        {

            DiemTrungChuyenDAL diemTrungChuyenDAL = new DiemTrungChuyenDAL();

            return ResponseToOk(diemTrungChuyenDAL.GetDiemTrungChuyens());
        }

        /// <summary>
        /// Lấy ra khách xuống
        /// </summary>
        /// <param name="madiemtrungchuyen"></param>
        /// <param name="machuyenxe"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getkhachtaidiemxuong")]
        public IHttpActionResult geKhachTaiDiemXuong(string madiemtrungchuyen, string machuyenxe)
        {
            DiemTrungChuyenDAL diemTrungChuyenDAL = new DiemTrungChuyenDAL();
            return ResponseToOk(diemTrungChuyenDAL.GetKhachHangByDiemXuong(madiemtrungchuyen, machuyenxe));
        }


        /// <summary>
        /// Lấy ra khách don
        /// </summary>
        /// <param name="madiemtrungchuyen"></param>
        /// <param name="machuyenxe"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getkhachtaidiemdon")]
        public IHttpActionResult geKhachTaiDiemDon(string madiemtrungchuyen, string machuyenxe)
        {
            DiemTrungChuyenDAL diemTrungChuyenDAL = new DiemTrungChuyenDAL();
            return ResponseToOk(diemTrungChuyenDAL.GetKhachHangByDiemDon(madiemtrungchuyen, machuyenxe));
        }
    }
}
