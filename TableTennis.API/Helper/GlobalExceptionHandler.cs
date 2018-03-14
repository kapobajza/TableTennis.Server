using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace TableTennis.API.Helper
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if(context.Exception is CustomHttpException httpException)
            {
                var result = new HttpResponseMessage(httpException.StatusCode)
                {
                    Content = new StringContent(context.Exception.Message),                    
                    ReasonPhrase = httpException.StatusCode.ToString()
                };

                context.Result = new CustomActionResult(context.Request, result);
            }
            else
            {
                var result = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occured."),
                    ReasonPhrase = System.Net.HttpStatusCode.InternalServerError.ToString()
                };

                context.Result = new CustomActionResult(context.Request, result);
            }
        }
    }
}