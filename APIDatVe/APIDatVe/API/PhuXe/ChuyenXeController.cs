﻿using APIDatVe.DAL;
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
        /// Lấy ra lịch trình phụ xe trong vòng 1 tuần kể từ ngày vào app
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getlichtrinh")]
        public IHttpActionResult getLichTrinh(String username)
        {

            ChuyenXeDAL chuyenxeDAL = new ChuyenXeDAL();

            return ResponseToOk(chuyenxeDAL.getLichTrinh(username));
        }

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
    }
}