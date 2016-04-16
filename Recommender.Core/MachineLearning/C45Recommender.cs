using MyMediaLite;
using MyMediaLite.RatingPrediction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Core.MachineLearning
{
    public class C45Recommender : RatingPredictor
    {
        protected double weight;

        public C45Recommender()
        {
            weight = 7.5;

        }

        public override float Predict(int user_id, int item_id)
        {
            throw new NotImplementedException();
        }

        public override void Train()
        {
            InitModel();

            throw new NotImplementedException();
        }

        protected internal virtual void InitModel()
        {

        }

        //private void SetWeights()
        //{
        //    int total = dataTrain.size();

        //    for (int i = 0; i < total; i++)
        //    {
        //        Instance ii = dataTrain.get(i);
        //        //klasa 1 zdominowana
        //        if (ii.classValue() == 1.0) {
        //            //zwiekszenie wagi                   
        //            ii.setWeight(Weight);
        //        }
        //    }
        //}
    }
}
