using Recommender.GUI.Enums;
using System.Collections.Generic;

namespace Recommender.GUI.Options
{
    public class RecommenderTypeOption : Option<RecommenderType>
    {
        public static List<RecommenderTypeOption> OptionBuilder()
        {
            var options = new List<RecommenderTypeOption>()
            {
                new RecommenderTypeOption() { Name = "Collaborative filtering", Value =  RecommenderType.Collaborative},
                new RecommenderTypeOption() { Name = "ContentBased filtering", Value = RecommenderType.ContentBased },
                new RecommenderTypeOption() { Name = "Hybrid recommender", Value = RecommenderType.Hybrid },
            };

            return options;
        }
    }
}
