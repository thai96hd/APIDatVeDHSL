using APIDatVe.Database;
using APIDatVe.Helper;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace APIDatVe.API.QuyenTruyCap
{
    public class BaseAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization is null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new Exception("Bạn không có quyền truy cập tính năng  này"));
            }
            else
            {
                try
                {
                    string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                    string decodeAuthenticationToken = Encode.Decrypt(authenticationToken);
                    string[] account = decodeAuthenticationToken.Split(':');
                    string userName = account[0];
                    string password = Encode.MD5(account[0]);
                    using (var db = new DB())
                    {
                        if (db.TaiKhoans.FirstOrDefault(x => x.tentaikhoan == userName && x.matkhau == password) == null)
                        {
                            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
                        }
                        else
                        {
                            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new Exception("Bạn không có quyền truy cập tính năng  này"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("Bạn không có quyền truy cập tính năng  này"));
                }
            }
        }
    }
}