using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TableTennis.Domain.Repository.Concrete;

namespace TableTennis.API.Helper
{
    public class LogExceptions
    {
        public static void Log(Exception exception, string method, string uri, string content)
        {
            using (var _exceptionLogsRepo = new ExceptionLogsRepo())
            {
                _exceptionLogsRepo.Add(new Domain.Models.ExceptionLog()
                {
                    ExceptionName = exception.ToString(),
                    RequestMethod = method,
                    RequestUri = uri,
                    RequestContent = content,
                    ExceptionDate = DateTime.Now
                });

                _exceptionLogsRepo.SaveChanges();
            }
        }
    }
}