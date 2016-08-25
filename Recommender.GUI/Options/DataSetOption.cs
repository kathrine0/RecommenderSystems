using Recommender.Common.Enums;
using Recommender.Common.Enums;
using System.Collections.Generic;

namespace Recommender.GUI.Options
{
    public class DataSetOption : Option<DataSetType>
    {
        public static DataSetOption[] OptionBuilder()
        {
            var options = new List<DataSetOption>()
            {
                new DataSetOption() { Name = "MovieLense", Value = DataSetType.MovieLense },
                new DataSetOption() { Name = "YahooMusic", Value = DataSetType.YahooMusic },
                new DataSetOption() { Name = "AmazonMeta", Value = DataSetType.AmazonMeta },
            };

            return options.ToArray();
        }
    }
}
