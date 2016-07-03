using Recommender.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.GUI.Options
{
    class ActivationFunctionOption : Option<ActivationFunction>
    {
        public static List<ActivationFunctionOption> OptionBuilder()
        {
            var options = new List<ActivationFunctionOption>()
            {
                new ActivationFunctionOption() { Name = "Sigmoidal Function", Value = ActivationFunction.SigmoidFunction },
                new ActivationFunctionOption() { Name = "Bipolar Sigmoidal Function", Value = ActivationFunction.BipolarSigmoidFunction },
                new ActivationFunctionOption() { Name = "Threshold Function", Value = ActivationFunction.ThresholdFunction },
            };

            return options;
        }
    }
}
