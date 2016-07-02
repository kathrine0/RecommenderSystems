using Recommender.GUI.Enums;
using System.Collections.Generic;

namespace Recommender.GUI.Options
{
    class CollaborativeAlgorithmOption : Option<CollaborativeAlgorithm>
    {
        public static List<CollaborativeAlgorithmOption> OptionBuilder()
        {
            var options = new List<CollaborativeAlgorithmOption>()
            {
                new CollaborativeAlgorithmOption() { Name = "Matrix Factorization", Value = CollaborativeAlgorithm.MatrixFactorization },
                new CollaborativeAlgorithmOption() { Name = "Biased Matrix Factorization", Value = CollaborativeAlgorithm.BiasedMatrixFactorization },
                new CollaborativeAlgorithmOption() { Name = "SVD++", Value = CollaborativeAlgorithm.SVDplusplus }
            };

            return options;
        }
    }
}
