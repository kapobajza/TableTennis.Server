using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableTennis.Domain.Models
{
    public class ExceptionLog
    {
        public int ExceptionLogId { get; set; }
        public string ExceptionName { get; set; }
        public string RequestMethod { get; set; }
        public string RequestUri { get; set; }
        public string RequestContent { get; set; }
        public DateTime ExceptionDate { get; set; }
    }
}
