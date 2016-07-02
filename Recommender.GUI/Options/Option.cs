using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.GUI.Options
{
    public abstract class Option<TEnum>
    {
        public string Name { get; set; }

        public TEnum Value { get; set; }
    }
}
