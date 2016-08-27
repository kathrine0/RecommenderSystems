using MyMediaLite.Data;
using Recommender.Common.Logger;
using Recommender.DataAccess.YahooMusic;
using Recommender.DataAccess.YahooMusic.Entities;
using Recommender.Service.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace Recommender.Service
{
    public abstract class RatingService : IRatingService
    {
        public Logger Logger { get; set; }

        //LOAD BY USER
        public void LoadBasicData(out IRatings trainingData, out IRatings testData, double ratio, int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {
            if (ratio < 0 || ratio > 1)
                throw new ArgumentOutOfRangeException("ratio should be in range from 0 to 1");

            var sets = GetBasicSets(ratio, numberOfUsers, minimumItemsRated, token);

            trainingData = sets[0];
            testData = sets[1];
        }
        
        public void LoadFeaturedData(out IRatings trainingData, out IRatings testData, double ratio, int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {
            if (ratio < 0 || ratio > 1)
                throw new ArgumentOutOfRangeException("ratio should be in range from 0 to 1");

            var sets = GetFeaturedSets(ratio, numberOfUsers, minimumItemsRated, token);

            trainingData = sets[0];
            testData = sets[1];
        }
        
        public void LoadComplexData(out IFeaturedRatings featuredTrainingData, out IFeaturedRatings featuredTestData, out IRatings simpleTrainingData, double ratio, int numberOfUsers, int numberForSimpleData, int minimumItemsRated, CancellationToken token)
        {
            if (ratio < 0 || ratio > 1)
                throw new ArgumentOutOfRangeException("ratio should be in range from 0 to 1");

            if (numberOfUsers > numberForSimpleData)
                throw new ArgumentOutOfRangeException("numberOfUsers > numberForSimpleData");

            var sets = GetComplexSets(ratio, numberOfUsers, numberForSimpleData, minimumItemsRated, token);

            featuredTrainingData = (IFeaturedRatings)sets[0];
            featuredTestData = (IFeaturedRatings)sets[1];
            simpleTrainingData = sets[2];

        }

        protected abstract IRatings[] GetBasicSets(double percentage, int numberOfUsers, int minimumItemsRated, CancellationToken token);
        protected abstract IFeaturedRatings[] GetFeaturedSets(double percentage, int numberOfUsers, int minimumItemsRated, CancellationToken token);
        protected abstract IRatings[] GetComplexSets(double percentage, int numberOfUsers, int numberForSimpleData, int minimumItemsRated, CancellationToken token);

    }
}
