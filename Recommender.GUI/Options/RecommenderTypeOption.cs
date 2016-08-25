using Recommender.Common.Enums;
using System.Collections.Generic;

namespace Recommender.GUI.Options
{
    public class RecommenderTypeOption : Option<RecommenderType>
    {
        public static List<RecommenderTypeOption> OptionBuilder()
        {
            var options = new List<RecommenderTypeOption>()
            {
                new RecommenderTypeOption() { Name = "Content-Based Filtering", Value = RecommenderType.ContentBased },
                new RecommenderTypeOption() { Name = "Collaborative Filtering", Value =  RecommenderType.Collaborative},
                new RecommenderTypeOption() { Name = "Hybrid Recommender", Value = RecommenderType.Hybrid },
            };

            return options;
        }
    }
}
