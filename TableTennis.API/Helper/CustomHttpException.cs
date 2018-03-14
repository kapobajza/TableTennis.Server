using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace TableTennis.API.Helper
{
    public class CustomHttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public CustomHttpException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}