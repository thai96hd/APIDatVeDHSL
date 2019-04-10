using APIDatVe.Database;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace APIDatVe.API.QuyenTruyCap
{
    public class AcceptAction : AuthorizeAttribute
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Namespace { get; set; }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            using (var db = new DB())
            {
                string userName = Thread.CurrentPrincipal.Identity.Name;
                TaiKhoan taiKhoan = db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == userName);
                if (taiKhoan == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new Exception("Bạn không có quyền truy cập tính năng  này"));
                }
                else
                {
                    if (db.QuyenAPIQuanLies.FirstOrDefault(x => x.chon && x.maquyen == taiKhoan.maquyen && x.APIQuanLy.actionname == this.ActionName && x.APIQuanLy.controllername == this.ControllerName) == null)
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new Exception("Bạn không có quyền truy cập tính năng  này"));
                    }
                }
            }
        }
    }
}