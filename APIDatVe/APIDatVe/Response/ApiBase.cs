using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDatVe.Response
{
    public class ApiBase : ApiController
    {
        public IHttpActionResult ResponseToOk(Object data)
        {
            var response = new ResponseDefault()
            {

                status = HttpStatusCode.OK,
                data = data

            };
            return Ok(response);
        }

        public IHttpActionResult ResponseToOk(Object data, String message)
        {
            var response = new ResponseDefault()
            {

                status = HttpStatusCode.OK,
                data = data,
                message = message

            };
            return Ok(response);
        }

        public IHttpActionResult ResponseToOk(Object data, String message, HttpStatusCode status)
        {
            var response = new ResponseDefault()
            {

                status = status,
                data = data,
                message = message

            };
            return Ok(response);
        }
    }
}
