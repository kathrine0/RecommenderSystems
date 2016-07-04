using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.GUI
{
    public class ProgressState
    {
        public ProgressState(int percentage, string resultBoxText, string statusText)
        {
            Percentage = percentage;
            ResultBoxText = resultBoxText;
            StatusText = statusText;
        }

        public int Percentage { get; set; }

        public string ResultBoxText { get; set; }

        public string StatusText { get; set; }
    }
}
