using APIDatVe.API.QuyenTruyCap;
using APIDatVe.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDatVe.API.QuanLy
{
    [RoutePrefix("api/setting")]
    [BaseAuthenticationAttribute]
    public class APISettingController : ApiController
    {
        [Route("put")]
        [HttpPut]
        [AcceptAction(ActionName = "Put", ControllerName = "APISettingController")]
        public IHttpActionResult Put([FromBody]UpdateSettingStyle updateSettingStyle)
        {
            try
            {
                using (var db = new DB())
                {
                    TaiKhoanCauHinh taiKhoanCauHinh = db.TaiKhoanCauHinhs.FirstOrDefault(x => x.tentaikhoan == updateSettingStyle._tentaikhoan);
                    if (taiKhoanCauHinh == null)
                    {
                        db.TaiKhoanCauHinhs.Add(new TaiKhoanCauHinh()
                        {
                            tentaikhoan = updateSettingStyle._tentaikhoan,
                            cauhinh = updateSettingStyle.type
                        });
                    }
                    else
                        taiKhoanCauHinh.cauhinh = updateSettingStyle.type;
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    public class UpdateSettingStyle
    {
        public string _tentaikhoan { get; set; }
        public string type { get; set; }
    }
}
