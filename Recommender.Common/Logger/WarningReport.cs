using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Common.Logger
{
    public class WarningReport
    {
        public WarningReport(string message)
        {
            Message = message;
        }


        public string Message { get; set; }
    }
}
