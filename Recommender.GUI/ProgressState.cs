using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.GUI
{
    public class ProgressState
    {
        public ProgressState(string resultBoxText, string statusText)
        {
            ResultBoxText = resultBoxText;
            StatusText = statusText;
        }

        public string ResultBoxText { get; set; }

        public string StatusText { get; set; }
    }
}
