using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableTennis.Domain.Helper
{
    public class ExceptionFilterHelper
    {
        public static Exception Handle(Exception exception, string field)
        {
            if(exception.InnerException.InnerException is SqlException sqlExc)
            {
                if (sqlExc.Number == 2601)
                    return new Exception("That " + field + " already exists");
            }

            return null;
        }
    }
}
