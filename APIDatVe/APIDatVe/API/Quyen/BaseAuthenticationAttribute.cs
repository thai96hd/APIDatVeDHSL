using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace APIDatVe.API.Quyen
{
    public class BaseAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //if (actionContext.Request.Headers.Authorization is null)
            //{
            //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new Exception("Bạn không có quyền truy cập tính năng  này"));
            //}
            //else
            //{
            //    try
            //    {
            //        string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
            //        string decodeAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
            //        string[] account = decodeAuthenticationToken.Split(':');
            //        string userName = Encode.Decrypt(account[0]);
            //        string password = "";
            //        using (var db = new DB())
            //        {
            //           
            //        }
            //        if (true)
            //        {
            //            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
            //        }
            //        else
            //        {
            //            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new Exception("Bạn không có quyền truy cập tính năng  này"));
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new Exception("Bạn không có quyền truy cập tính năng  này"));
            //    }
            //}
        }
    }
}