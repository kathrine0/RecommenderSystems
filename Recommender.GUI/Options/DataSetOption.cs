using Recommender.GUI.Enums;
using System.Collections.Generic;

namespace Recommender.GUI.Options
{
    public class DataSetOption : Option<DataSet>
    {
        public static DataSetOption[] OptionBuilder()
        {
            var options = new List<DataSetOption>()
            {
                new DataSetOption() { Name = "MovieLense", Value = DataSet.MovieLense },
                new DataSetOption() { Name = "AmazonMeta", Value = DataSet.AmazonMeta },
                new DataSetOption() { Name = "LastFM", Value = DataSet.LastFm },
            };

            return options.ToArray();
        }
    }
}
