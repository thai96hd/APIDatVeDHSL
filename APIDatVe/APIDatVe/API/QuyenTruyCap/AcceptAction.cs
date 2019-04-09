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
            //using (var db = new DB())
            //{
            //    var path = db.D_Path
            //                .FirstOrDefault(x => x.ControllerName == ControllerName.Trim() &&
            //                                    x.ActionName == ActionName.Trim() &&
            //                                    (Namespace == null ? true : x.Namespace == Namespace.Trim()));
            //    if (path is null)
            //        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new Exception("Bạn không có quyền truy cập tính năng  này"));
            //    else
            //    {
            //        int pathId = path.PathId;
            //        string userName = Thread.CurrentPrincipal.Identity.Name;
            //        if (userName != "masteradmin")
            //        {
            //            bool accept = db.D_UserPath.Any(x => x.UserName == userName && x.PathId == pathId);
            //            if (!accept)
            //                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new Exception("Bạn không có quyền truy cập tính năng  này"));
            //        }
            //    }
            //}
        }
    }
}