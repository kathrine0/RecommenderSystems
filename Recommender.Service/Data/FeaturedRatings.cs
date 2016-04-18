using MyMediaLite.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Recommender.Service.Data
{
    /// <summary>Data structure for storing ratings with item's features</summary>
    /// <remarks>
    /// <para>This data structure supports incremental updates.</para>
    /// <para>
    /// </para>
    /// </remarks>
    [Serializable()]
    public class FeaturedRatings : Ratings, IFeaturedRatings
    {
        ///
        public IList<IDictionary<string, string>> Features { get; protected set; }



        /// <summary>Default constructor</summary>
        public FeaturedRatings() : base()
        {
            Features = new List<IDictionary<string, string>>();
        }

        ///
        public FeaturedRatings(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Features = (List<IDictionary<string, string>>)info.GetValue("Features", typeof(List<IDictionary<string, string>>));
        }

        ///
        public override void Add(int user_id, int item_id, float rating)
        {
            throw new NotSupportedException();
        }

        ///
        public virtual void Add(int user_id, int item_id, float rating, IDictionary<string, string> features)
        {
            Users.Add(user_id);
            Items.Add(item_id);
            Values.Add(rating);
            Features.Add(features);

            int pos = Users.Count - 1;

            if (user_id > MaxUserID)
                MaxUserID = user_id;
            if (item_id > MaxItemID)
                MaxItemID = item_id;

            // update index data structures if necessary
            if (by_user != null)
            {
                for (int u = by_user.Count; u <= user_id; u++)
                    by_user.Add(new List<int>());
                by_user[user_id].Add(pos);
            }
            if (by_item != null)
            {
                for (int i = by_item.Count; i <= item_id; i++)
                    by_item.Add(new List<int>());
                by_item[item_id].Add(pos);
            }
        }

        ///
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Features", this.Features);
        }
    }
}