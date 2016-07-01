using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Core.Engine
{
    public class CollaborativeRecommenderEngine : RecommenderEngine
    {
        public override void LoadData()
        {
            _service.LoadBasicData(out _trainingData, out _testData, 0.8);
        }
    }
}
