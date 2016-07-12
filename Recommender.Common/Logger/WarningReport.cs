using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Common.Logger
{

    public class Report
    {
        public Report(string message)
        {
            Message = message;
        }


        public string Message { get; set; }
    }

    public class LoudReport : Report
    {
        public LoudReport(string message) : base(message)
        { }

    }

    public class ErrorReport : Report
    {
        public ErrorReport(string message): base(message)
        { }

    }

    public class WarningReport : Report
    {
        public WarningReport(string message) : base(message)
        { }

    }
}
