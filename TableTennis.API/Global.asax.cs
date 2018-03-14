using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using TableTennis.API.Helper;

namespace TableTennis.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            Response.StatusCode = 500;
            Response.ContentType = "application/json";
            Response.Write("An error happened. Please contact the administrator for more information.");
            LogExceptions.Log(exc, Request.HttpMethod, Request.Url.AbsoluteUri, "");
        }
    }
}
