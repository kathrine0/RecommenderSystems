using Recommender.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.GUI.Options
{
    class TeacherFunctionOption : Option<TeacherFunction>
    {
        public static List<TeacherFunctionOption> OptionBuilder()
        {
            var options = new List<TeacherFunctionOption>()
            {
                new TeacherFunctionOption() { Name = "Backpropagation", Value = TeacherFunction.BackProp },
                new TeacherFunctionOption() { Name = "Genetic algorithm", Value = TeacherFunction.Genetic }
            };

            return options;
        }
    }
}
