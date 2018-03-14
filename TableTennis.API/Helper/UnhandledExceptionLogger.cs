using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;
using TableTennis.Domain.Repository.Abstract;
using TableTennis.Domain.Repository.Concrete;

namespace TableTennis.API.Helper
{
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        public override async void Log(ExceptionLoggerContext context)
        {
            if(!(context.Exception is CustomHttpException))
            {
                var content = await context.Request.Content.ReadAsStringAsync();
                LogExceptions.Log(context.Exception, context.Request.Method.Method, context.Request.RequestUri.AbsoluteUri, content);
            }
        }
    }
}