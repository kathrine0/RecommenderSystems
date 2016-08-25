using Recommender.Common.Enums;
using System.Collections.Generic;

namespace Recommender.GUI.Options
{
    public class ContentBasedAlgorithmOption : Option<ContentBasedAlgorithm>
    {
        public static List<ContentBasedAlgorithmOption> OptionBuilder()
        {
            var options = new List<ContentBasedAlgorithmOption>()
            {
                new ContentBasedAlgorithmOption() { Name = "Neural Network", Value = ContentBasedAlgorithm.NeuralNetwork },
                new ContentBasedAlgorithmOption() { Name = "Neural Network with Bias", Value = ContentBasedAlgorithm.NeuralNetworkWithBias }
            };
            
            return options;
        }
    } 
}
