using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace APIDatVe.Response
{
    public class ResponseDefault
    {
        public string message { get; set; }

        public HttpStatusCode status { get; set; }

        public Object data { get; set; }



    }
}