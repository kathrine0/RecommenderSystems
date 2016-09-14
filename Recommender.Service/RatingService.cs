using MyMediaLite.Data;
using Recommender.Common.Logger;
using Recommender.Service.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace Recommender.Service
{
    public abstract class RatingService<TRating> : IRatingService
    {
        public Logger Logger { get; set; }

        //LOAD BY USER
        public void LoadComplexData(out IFeaturedRatings featuredData, out IRatings simpleData, int numberOfUsers, int numberForSimpleData, int minimumItemsRated, CancellationToken token)
        {
            if (numberOfUsers > numberForSimpleData)
                throw new ArgumentOutOfRangeException("numberOfUsers > numberForSimpleData");

            //get basic sets
            simpleData = GetBasicSet(numberForSimpleData, minimumItemsRated, token);
            featuredData = GetFeaturedSet(numberOfUsers, minimumItemsRated, token);

            Logger.AddProgressReport(new ProgressState(100, "Collaborative set:", null));
            PrintReport(simpleData);

            Logger.AddProgressReport(new ProgressState(100, "Content-based set:", null));
            PrintReport(featuredData);
            
        }

        public IRatings LoadBasicData(int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {
            var set = GetBasicSet(numberOfUsers, minimumItemsRated, token);

            PrintReport(set);

            return set;
        }
        
        public IFeaturedRatings LoadFeaturedData(int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {
            var set = GetFeaturedSet(numberOfUsers, minimumItemsRated, token);

            PrintReport(set);

            return set;
        }
        
        private void PrintReport<TRatings>(TRatings set) 
            where TRatings: IRatings
        {
            var cntElements = set.Count();
            var cntItems = set.CountByItem.Where(x => x > 0).Count();
            var cntUsers = set.CountByUser.Where(x => x > 0).Count();
            

            var resultText = string.Format(@"
        Set:  {0} elements in total
              {1} rated items
              {2} users",cntElements, cntItems, cntUsers);

            Logger.AddProgressReport(new ProgressState(100, resultText, null));
        }


        protected abstract IRatings GetBasicSet(int numberOfUsers, int minimumItemsRated, CancellationToken token);
        protected abstract IFeaturedRatings GetFeaturedSet(int numberOfUsers, int minimumItemsRated, CancellationToken token);

        protected IRatings PrepareBasicSets<TKey>(IQueryable<IGrouping<TKey, TRating>> groupedRatings, CancellationToken token)
        {
            var set = groupedRatings.SelectMany(x => x);

            var cnt = set.Count();

            double progressStep = (double)50 / (double)(cnt);
            var ratingSets = CreateBasicSet(set, token, progressStep);

            return ratingSets;
        }

        protected IFeaturedRatings PrepareFeaturedSets<TKey>(IQueryable<IGrouping<TKey, TRating>> groupedRatings, CancellationToken token)
        {
            var set = groupedRatings.SelectMany(x => x);

            var cnt = set.Count();

            double progressStep = (double)50 / (double)(cnt);
            var ratingSets = CreateFeaturedSet(set, token, progressStep);    
    
            return ratingSets;
        }


        protected abstract IRatings CreateBasicSet(IEnumerable<TRating> set, CancellationToken token, double progressStep);

        protected abstract IFeaturedRatings CreateFeaturedSet(IEnumerable<TRating> set, CancellationToken token, double progressStep);
    }
}
