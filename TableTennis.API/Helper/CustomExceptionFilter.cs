using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using TableTennis.Domain.Helper;

namespace TableTennis.API.Helper
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var field = "";

            if (actionExecutedContext.Request.RequestUri.AbsoluteUri.ToLower().Contains("account/register"))
                field = "username";

            var exception = ExceptionFilterHelper.Handle(actionExecutedContext.Exception, field);

            if (exception != null)
                throw new CustomHttpException(System.Net.HttpStatusCode.InternalServerError, exception.Message);
            else
                base.OnException(actionExecutedContext);
        }
    }
}