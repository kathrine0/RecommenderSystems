//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using MyMediaLite.Data;
//using MyMediaLite.DataType;
//using MyMediaLite.IO;
//using MyMediaLite.RatingPrediction;
//using MyMediaLite;
//using DotNumerics;
//using DotNumerics.LinearAlgebra.CSLapack;
//using System.ComponentModel.DataAnnotations;

//namespace Recommender.Core.ItemRecommendation
//{
//    //public class BayesMatrixFactorization : MatrixFactorization, ITransductiveRatingPredictor
//    //{
//    //    /// <summary>rating biases of the users</summary>
//    //    protected internal float[] user_bias;
//    //    /// <summary>rating biases of the items</summary>
//    //    protected internal float[] item_bias;
//    //    /// <summary>user factors (part expressed via the rated items)</summary>
//    //    protected internal Matrix<float> y;
//    //    /// <summary>user factors (individual part)</summary>
//    //    protected internal Matrix<float> p;

//    //    ///
//    //    public IDataSet AdditionalFeedback { get; set; }

//    //    // TODO #332 update this structure on incremental updates
//    //    /// <summary>The items rated by the users</summary>
//    //    protected int[][] items_rated_by_user;
//    //    /// <summary>precomputed regularization terms for the y matrix</summary>
//    //    protected float[] y_reg;
//    //    int[] feedback_count_by_item;

//    //    /// <summary>bias learn rate</summary>
//    //    public float BiasLearnRate { get; set; }
//    //    /// <summary>regularization constant for biases</summary>
//    //    public float BiasReg { get; set; }

//    //    /// <summary>Regularization based on rating frequency</summary>
//    //    /// <description>
//    //    /// Regularization proportional to the inverse of the square root of the number of ratings associated with the user or item.
//    //    /// As described in the paper by Menon and Elkan.
//    //    /// </description>
//    //    public bool FrequencyRegularization { get; set; }

//    //    /// <summary>Default constructor</summary>
//    //    public BayesMatrixFactorization() : base()
//    //    {
//    //        AdditionalFeedback = new PosOnlyFeedback<SparseBooleanMatrix>(); // in case no test data is provided
//    //        Regularization = 0.015f;
//    //        LearnRate = 0.001f;
//    //        BiasLearnRate = 0.7f;
//    //        BiasReg = 0.33f;
//    //    }

//    //    ///
//    //    public override void Train()
//    //    {
//    //        items_rated_by_user = this.ItemsRatedByUser();
//    //        feedback_count_by_item = this.ItemFeedbackCounts();

//    //        MaxUserID = Math.Max(MaxUserID, items_rated_by_user.Length - 1);
//    //        MaxItemID = Math.Max(MaxItemID, feedback_count_by_item.Length - 1);

//    //        y_reg = new float[MaxItemID + 1];
//    //        for (int item_id = 0; item_id <= MaxItemID; item_id++)
//    //            if (feedback_count_by_item[item_id] > 0)
//    //                y_reg[item_id] = FrequencyRegularization ? (float)(Regularization / Math.Sqrt(feedback_count_by_item[item_id])) : Regularization;
//    //            else
//    //                y_reg[item_id] = 0;

//    //        base.Train();
//    //    }

//    //    ///
//    //    public override float Predict(int user_id, int item_id)
//    //    {
//    //        double result = global_bias;

//    //        if (user_factors == null)
//    //            PrecomputeUserFactors();

//    //        if (user_id < user_bias.Length)
//    //            result += user_bias[user_id];
//    //        if (item_id < item_bias.Length)
//    //            result += item_bias[item_id];
//    //        if (user_id <= MaxUserID && item_id <= MaxItemID)
//    //            result += DataType.MatrixExtensions.RowScalarProduct(user_factors, user_id, item_factors, item_id);

//    //        if (result > MaxRating)
//    //            return MaxRating;
//    //        if (result < MinRating)
//    //            return MinRating;

//    //        return (float)result;
//    //    }

//    //    ///
//    //    protected internal override void InitModel()
//    //    {
//    //        base.InitModel();

//    //        p = new Matrix<float>(MaxUserID + 1, NumFactors);
//    //        p.InitNormal(InitMean, InitStdDev);
//    //        y = new Matrix<float>(MaxItemID + 1, NumFactors);
//    //        y.InitNormal(InitMean, InitStdDev);

//    //        // set factors to zero for items without training examples
//    //        for (int i = 0; i < ratings.CountByItem.Count; i++)
//    //            if (ratings.CountByItem[i] == 0)
//    //                y.SetRowToOneValue(i, 0);
//    //        for (int i = ratings.CountByItem.Count; i <= MaxItemID; i++)
//    //        {
//    //            y.SetRowToOneValue(i, 0);
//    //            item_factors.SetRowToOneValue(i, 0);
//    //        }

//    //        // set factors to zero for users without training examples (rest is done in MatrixFactorization.cs)
//    //        for (int u = ratings.CountByUser.Count; u <= MaxUserID; u++)
//    //            p.SetRowToOneValue(u, 0);

//    //        user_bias = new float[MaxUserID + 1];
//    //        item_bias = new float[MaxItemID + 1];
//    //    }

//    //    ///
//    //    protected override void Iterate(IList<int> rating_indices, bool update_user, bool update_item)
//    //    {
//    //        user_factors = null; // delete old user factors
//    //        float reg = Regularization; // to limit property accesses

//    //        foreach (int index in rating_indices)
//    //        {
//    //            int u = ratings.Users[index];
//    //            int i = ratings.Items[index];

//    //            double prediction = global_bias + user_bias[u] + item_bias[i];
//    //            var p_plus_y_sum_vector = y.SumOfRows(items_rated_by_user[u]);
//    //            double norm_denominator = Math.Sqrt(items_rated_by_user[u].Length);
//    //            for (int f = 0; f < p_plus_y_sum_vector.Count; f++)
//    //                p_plus_y_sum_vector[f] = (float)(p_plus_y_sum_vector[f] / norm_denominator + p[u, f]);

//    //            prediction += DataType.MatrixExtensions.RowScalarProduct(item_factors, i, p_plus_y_sum_vector);

//    //            double err = ratings[index] - prediction;

//    //            float user_reg_weight = FrequencyRegularization ? (float)(reg / Math.Sqrt(ratings.CountByUser[u])) : reg;
//    //            float item_reg_weight = FrequencyRegularization ? (float)(reg / Math.Sqrt(ratings.CountByItem[i])) : reg;

//    //            // adjust biases
//    //            if (update_user)
//    //                user_bias[u] += BiasLearnRate * current_learnrate * ((float)err - BiasReg * user_reg_weight * user_bias[u]);
//    //            if (update_item)
//    //                item_bias[i] += BiasLearnRate * current_learnrate * ((float)err - BiasReg * item_reg_weight * item_bias[i]);

//    //            // adjust factors
//    //            double normalized_error = err / norm_denominator;
//    //            for (int f = 0; f < NumFactors; f++)
//    //            {
//    //                float i_f = item_factors[i, f];

//    //                // if necessary, compute and apply updates
//    //                if (update_user)
//    //                {
//    //                    double delta_u = err * i_f - user_reg_weight * p[u, f];
//    //                    p.Inc(u, f, current_learnrate * delta_u);
//    //                }
//    //                if (update_item)
//    //                {
//    //                    double delta_i = err * p_plus_y_sum_vector[f] - item_reg_weight * i_f;
//    //                    item_factors.Inc(i, f, current_learnrate * delta_i);
//    //                    double common_update = normalized_error * i_f;
//    //                    foreach (int other_item_id in items_rated_by_user[u])
//    //                    {
//    //                        double delta_oi = common_update - y_reg[other_item_id] * y[other_item_id, f];
//    //                        y.Inc(other_item_id, f, current_learnrate * delta_oi);
//    //                    }
//    //                }
//    //            }
//    //        }

//    //        UpdateLearnRate();
//    //    }

//    //    /// <summary>Precompute all user factors</summary>
//    //    protected void PrecomputeUserFactors()
//    //    {
//    //        if (user_factors == null)
//    //            user_factors = new Matrix<float>(MaxUserID + 1, NumFactors);

//    //        if (items_rated_by_user == null)
//    //            items_rated_by_user = this.ItemsRatedByUser();

//    //        for (int user_id = 0; user_id <= MaxUserID; user_id++)
//    //            PrecomputeFactors(user_id);
//    //    }

//    //    /// <summary>Precompute the factors for a given user</summary>
//    //    /// <param name='user_id'>the ID of the user</param>
//    //    protected void PrecomputeFactors(int user_id)
//    //    {
//    //        if (items_rated_by_user[user_id].Length == 0)
//    //            return;

//    //        // compute
//    //        var factors = y.SumOfRows(items_rated_by_user[user_id]);
//    //        double norm_denominator = Math.Sqrt(items_rated_by_user[user_id].Length);
//    //        for (int f = 0; f < factors.Count; f++)
//    //            factors[f] = (float)(factors[f] / norm_denominator + p[user_id, f]);

//    //        // assign
//    //        for (int f = 0; f < factors.Count; f++)
//    //            user_factors[user_id, f] = (float)factors[f];
//    //    }

//    //    ///
//    //    protected override void AddUser(int user_id)
//    //    {
//    //        base.AddUser(user_id);
//    //        Array.Resize(ref user_bias, MaxUserID + 1);
//    //    }

//    //    ///
//    //    protected override void AddItem(int item_id)
//    //    {
//    //        base.AddItem(item_id);
//    //        Array.Resize(ref item_bias, MaxItemID + 1);
//    //    }

//    //    /// <summary>Updates the latent factors on a user</summary>
//    //    /// <param name="user_id">the user ID</param>
//    //    public override void RetrainUser(int user_id)
//    //    {
//    //        if (UpdateUsers)
//    //        {
//    //            base.RetrainUser(user_id);
//    //            PrecomputeFactors(user_id);
//    //        }
//    //    }

//    //    ///
//    //    public override void SaveModel(string filename)
//    //    {
//    //        using (StreamWriter writer = Model.GetWriter(filename, this.GetType(), "2.99"))
//    //        {
//    //            writer.WriteLine(global_bias.ToString(CultureInfo.InvariantCulture));
//    //            writer.WriteLine(min_rating.ToString(CultureInfo.InvariantCulture));
//    //            writer.WriteLine(max_rating.ToString(CultureInfo.InvariantCulture));
//    //            writer.WriteVector(user_bias);
//    //            writer.WriteVector(item_bias);
//    //            writer.WriteMatrix(p);
//    //            writer.WriteMatrix(y);
//    //            writer.WriteMatrix(item_factors);
//    //        }
//    //    }

//    //    ///
//    //    public override void LoadModel(string filename)
//    //    {
//    //        using (StreamReader reader = Model.GetReader(filename, this.GetType()))
//    //        {
//    //            var global_bias = float.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
//    //            var min_rating = float.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
//    //            var max_rating = float.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
//    //            var user_bias = reader.ReadVector();
//    //            var item_bias = reader.ReadVector();
//    //            var p = (Matrix<float>)reader.ReadMatrix(new Matrix<float>(0, 0));
//    //            var y = (Matrix<float>)reader.ReadMatrix(new Matrix<float>(0, 0));
//    //            var item_factors = (Matrix<float>)reader.ReadMatrix(new Matrix<float>(0, 0));

//    //            if (user_bias.Count != p.dim1)
//    //                throw new IOException(
//    //                    string.Format(
//    //                        "Number of users must be the same for biases and factors: {0} != {1}",
//    //                        user_bias.Count, p.dim1));
//    //            if (item_bias.Count != item_factors.dim1)
//    //                throw new IOException(
//    //                    string.Format(
//    //                        "Number of items must be the same for biases and factors: {0} != {1}",
//    //                        item_bias.Count, item_factors.dim1));

//    //            if (p.NumberOfColumns != item_factors.NumberOfColumns)
//    //                throw new Exception(
//    //                    string.Format("Number of user (p) and item factors must match: {0} != {1}",
//    //                        p.NumberOfColumns, item_factors.NumberOfColumns));
//    //            if (y.NumberOfColumns != item_factors.NumberOfColumns)
//    //                throw new Exception(
//    //                    string.Format("Number of user (y) and item factors must match: {0} != {1}",
//    //                        y.NumberOfColumns, item_factors.NumberOfColumns));

//    //            this.MaxUserID = p.NumberOfRows - 1;
//    //            this.MaxItemID = item_factors.NumberOfRows - 1;

//    //            // assign new model
//    //            this.global_bias = global_bias;
//    //            if (this.NumFactors != item_factors.NumberOfColumns)
//    //            {
//    //                Console.Error.WriteLine("Set NumFactors to {0}", item_factors.NumberOfColumns);
//    //                this.NumFactors = (uint)item_factors.NumberOfColumns;
//    //            }
//    //            this.user_bias = user_bias.ToArray();
//    //            this.item_bias = item_bias.ToArray();
//    //            this.p = p;
//    //            this.y = y;
//    //            this.item_factors = item_factors;
//    //            this.min_rating = min_rating;
//    //            this.max_rating = max_rating;
//    //            user_factors = null; // enforce computation at first prediction
//    //        }
//    //    }

//    //    ///
//    //    protected override float Predict(float[] user_vector, int item_id)
//    //    {
//    //        var user_factors = new float[NumFactors];
//    //        Array.Copy(user_vector, 1, user_factors, 0, NumFactors);
//    //        double score = global_bias + user_vector[0];
//    //        if (item_id < item_factors.dim1)
//    //            score += item_bias[item_id] + DataType.MatrixExtensions.RowScalarProduct(item_factors, item_id, user_factors);
//    //        return (float)score;
//    //    }

//    //    ///
//    //    protected override float[] FoldIn(IList<Tuple<int, float>> rated_items)
//    //    {
//    //        var user_p = new float[NumFactors];
//    //        user_p.InitNormal(InitMean, InitStdDev);
//    //        float user_bias = 0;

//    //        var items = (from pair in rated_items select pair.Item1).ToArray();
//    //        float user_reg_weight = FrequencyRegularization ? (float)(Regularization / Math.Sqrt(items.Length)) : Regularization;

//    //        // compute stuff that will not change
//    //        var y_sum_vector = y.SumOfRows(items);
//    //        double norm_denominator = Math.Sqrt(items.Length);
//    //        for (int f = 0; f < y_sum_vector.Count; f++)
//    //            y_sum_vector[f] = (float)(y_sum_vector[f] / norm_denominator);

//    //        rated_items.Shuffle();
//    //        for (uint it = 0; it < NumIter; it++)
//    //        {
//    //            for (int index = 0; index < rated_items.Count; index++)
//    //            {
//    //                int item_id = rated_items[index].Item1;

//    //                double prediction = global_bias + user_bias + item_bias[item_id];
//    //                prediction += DataType.MatrixExtensions.RowScalarProduct(item_factors, item_id, y_sum_vector);
//    //                prediction += DataType.MatrixExtensions.RowScalarProduct(item_factors, item_id, user_p);

//    //                float err = (float)(rated_items[index].Item2 - prediction);

//    //                // adjust bias
//    //                user_bias += BiasLearnRate * LearnRate * ((float)err - BiasReg * user_reg_weight * user_bias);

//    //                // adjust factors
//    //                for (int f = 0; f < NumFactors; f++)
//    //                {
//    //                    float u_f = user_p[f];
//    //                    float i_f = item_factors[item_id, f];

//    //                    double delta_u = err * i_f - user_reg_weight * u_f;
//    //                    user_p[f] += (float)(LearnRate * delta_u);
//    //                }
//    //            }
//    //        }

//    //        // assign final parameter values to return vector
//    //        var user_vector = new float[NumFactors + 1];
//    //        user_vector[0] = user_bias;
//    //        for (int f = 0; f < NumFactors; f++)
//    //            user_vector[f + 1] = (float)y_sum_vector[f] + user_p[f];

//    //        return user_vector;
//    //    }

//    //    /// <summary>Compute the value of the loss function that is currently being optimized (RMSE)</summary>
//    //    /// <returns>the loss</returns>
//    //    protected virtual double ComputeLoss()
//    //    {
//    //        return Eval.Measures.RMSE.ComputeSquaredErrorSum(this, ratings);
//    //    }

//    //    ///
//    //    public override float ComputeObjective()
//    //    {
//    //        double complexity = 0;
//    //        if (FrequencyRegularization)
//    //        {
//    //            for (int u = 0; u <= MaxUserID; u++)
//    //            {
//    //                if (ratings.CountByUser[u] > 0)
//    //                {
//    //                    complexity += (Regularization / Math.Sqrt(ratings.CountByUser[u])) * Math.Pow(p.GetRow(u).EuclideanNorm(), 2);
//    //                    complexity += (Regularization / Math.Sqrt(ratings.CountByUser[u])) * BiasReg * Math.Pow(user_bias[u], 2);
//    //                }
//    //            }
//    //            for (int i = 0; i <= MaxItemID; i++)
//    //            {
//    //                complexity += y_reg[i] * Math.Pow(y.GetRow(i).EuclideanNorm(), 2);
//    //                if (ratings.CountByItem[i] > 0)
//    //                {
//    //                    complexity += (Regularization / Math.Sqrt(ratings.CountByItem[i])) * Math.Pow(item_factors.GetRow(i).EuclideanNorm(), 2);
//    //                    complexity += (Regularization / Math.Sqrt(ratings.CountByItem[i])) * BiasReg * Math.Pow(item_bias[i], 2);
//    //                }
//    //            }
//    //        }
//    //        else
//    //        {
//    //            for (int u = 0; u <= MaxUserID; u++)
//    //            {
//    //                complexity += ratings.CountByUser[u] * Regularization * Math.Pow(p.GetRow(u).EuclideanNorm(), 2);
//    //                complexity += ratings.CountByUser[u] * Regularization * BiasReg * Math.Pow(user_bias[u], 2);
//    //            }
//    //            for (int i = 0; i <= MaxItemID; i++)
//    //            {
//    //                complexity += ratings.CountByItem[i] * Regularization * Math.Pow(item_factors.GetRow(i).EuclideanNorm(), 2);
//    //                complexity += ratings.CountByItem[i] * Regularization * Math.Pow(y.GetRow(i).EuclideanNorm(), 2);
//    //                complexity += ratings.CountByItem[i] * Regularization * BiasReg * Math.Pow(item_bias[i], 2);
//    //            }
//    //        }

//    //        return (float)(ComputeLoss() + complexity);
//    //    }

//    //    ///
//    //    public override string ToString()
//    //    {
//    //        return string.Format(
//    //            CultureInfo.InvariantCulture,
//    //            "{0} num_factors={1} regularization={2} bias_reg={3} frequency_regularization={4} learn_rate={5} bias_learn_rate={6} learn_rate_decay={7} num_iter={8}",
//    //            this.GetType().Name, NumFactors, Regularization, BiasReg, FrequencyRegularization, LearnRate, BiasLearnRate, Decay, NumIter);
//    //    }
//    //}


//    /// <summary>Simple matrix factorization class, learning is performed by stochastic gradient descent (SGD)</summary>
//    /// <remarks>
//    ///   <para>
//    ///     Factorizing the observed rating values using a factor matrix for users and one for items.
//    ///   </para>
//    ///
//    ///   <para>
//    ///     NaN values in the model occur if values become too large or too small to be represented by the type float.
//    ///     If you encounter such problems, there are three ways to fix them:
//    ///     (1) (preferred) Use BiasedMatrixFactorization, which is more stable.
//    ///     (2) Change the range of rating values (1 to 5 works generally well with the default settings).
//    ///     (3) Decrease the learn_rate.
//    ///   </para>
//    ///
//    ///   <para>
//    ///     This recommender supports incremental updates.
//    ///   </para>
//    /// </remarks>
//    public class BayesMatrixFactorization : MatrixFactorization, ITransductiveRatingPredictor
//    {
//        /// <summary>Matrix containing the latent user factors</summary>
//        protected internal Matrix<float> user_factors;

//        /// <summary>Matrix containing the latent item factors</summary>
//        protected internal Matrix<float> item_factors;

//        /// <summary>The items rated by the users</summary>
//        protected int[][] items_rated_by_user;

//        protected Matrix<int> items_rated_by_user_matrix;

//        public int KFactor { get; set; }


//        public double Theta { get; set; }
//        public double Sigma { get; set; }

//        public IDataSet AdditionalFeedback { get; set; }


//        /// <summary>Default constructor</summary>
//        public BayesMatrixFactorization() : base()
//        {
//            //do i need it?
//            AdditionalFeedback = new PosOnlyFeedback<SparseBooleanMatrix>(); // in case no test data is provided

//            // set default values
//            NumFactors = 10;
//            KFactor = 0;
//            NumIter = 20;
//            Theta = 0;
//            Sigma = 1;
//        }

//        //todo refactor me
//        Matrix<float> help1;
//        Matrix<float> help2;
//        Matrix<float> help3;
//        Matrix<float> help4;
//        Matrix<float> help5;
//        Matrix<float> help6;
//        Matrix<float> help7;
//        Matrix<float> help8;
//        Matrix<float> zerocolow;

//        /// <summary>Initialize the model data structure</summary>
//        protected internal virtual void InitModel()
//        {
//            // init factor matrices
//            user_factors = new Matrix<float>(MaxUserID + 1, NumFactors);
//            item_factors = new Matrix<float>(MaxItemID + 1, NumFactors);
//            user_factors.InitNormal(InitMean, InitStdDev);
//            item_factors.InitNormal(InitMean, InitStdDev);

//            // set factors to zero for users and items without training examples
//            for (int u = 0; u < ratings.CountByUser.Count; u++)
//                if (ratings.CountByUser[u] == 0)
//                    user_factors.SetRowToOneValue(u, 0);
//            for (int i = 0; i < ratings.CountByItem.Count; i++)
//                if (ratings.CountByItem[i] == 0)
//                    item_factors.SetRowToOneValue(i, 0);

//            help1 = new Matrix<float>(NumFactors, NumFactors);
//            help2 = new Matrix<float>(NumFactors, MaxItemID + 1);
//            help3 = new Matrix<float>(NumFactors, MaxUserID + 1);
//            help4 = new Matrix<float>(MaxUserID + 1, NumFactors - 1); //W(m,NumFactors-1)
//            help5 = new Matrix<float>(NumFactors - 1, 1); //(B * B')(NumFactors-1,1) 
//            help6 = new Matrix<float>(MaxUserID + 1, 1); //col vector of W/alpha	
//            help7 = new Matrix<float>(NumFactors - 1, MaxItemID + 1); //H(NumFactors-1, MaxItemID + 1)
//            help8 = new Matrix<float>(NumFactors, 1);  //row vector 1xn
//            zerocolow = new Matrix<float>(NumFactors, 1);  //indicates if a whole row/column is zero (1) or not (0)
//        }

//        ///
//        public override void Train()
//        {
//            items_rated_by_user = this.ItemsRatedByUser();


//            InitModel();

//            // learn model parameters
//            //global_bias = ratings.Average;
//            LearnFactors(ratings.RandomIndex, true, true);
//        }

//        /// <summary>Updates <see cref="current_learnrate"/> after each epoch</summary>
//        protected virtual void UpdateLearnRate()
//        {
//            current_learnrate *= Decay;
//        }

//        ///
//        public virtual void Iterate()
//        {
//            Iterate(ratings.RandomIndex, true, true);
//        }

//        /// <summary>Updates the latent factors on a user</summary>
//        /// <param name="user_id">the user ID</param>
//        public virtual void RetrainUser(int user_id)
//        {
//            if (UpdateUsers)
//            {
//                user_factors.RowInitNormal(user_id, InitMean, InitStdDev);
//                LearnFactors(ratings.ByUser[user_id], true, false);
//            }
//        }

//        /// <summary>Updates the latent factors of an item</summary>
//        /// <param name="item_id">the item ID</param>
//        public virtual void RetrainItem(int item_id)
//        {
//            if (UpdateItems)
//            {
//                item_factors.RowInitNormal(item_id, InitMean, InitStdDev);
//                LearnFactors(ratings.ByItem[item_id], false, true);
//            }
//        }


//        //TODO bayes HERE

//        /// <summary>Iterate once over rating data and adjust corresponding factors (stochastic gradient descent)</summary>
//        /// <param name="rating_indices">a list of indices pointing to the ratings to iterate over</param>
//        /// <param name="update_user">true if user factors to be updated</param>
//        /// <param name="update_item">true if item factors to be updated</param>
//        protected virtual void Iterate(IList<int> rating_indices, bool update_user, bool update_item)
//        {
//            //foreach (int index in rating_indices)
//            //{
//            //    int u = ratings.Users[index];
//            //    int i = ratings.Items[index];

//            //    float err = ratings[index] - Predict(u, i, false);

//            //    // adjust factors
//            //    for (int f = 0; f < NumFactors; f++)
//            //    {
//            //        float u_f = user_factors[u, f];
//            //        float i_f = item_factors[i, f];

//            //        // if necessary, compute and apply updates
//            //        if (update_user)
//            //        {
//            //            double delta_u = err * i_f - Regularization * u_f;
//            //            user_factors.Inc(u, f, current_learnrate * delta_u);
//            //        }
//            //        if (update_item)
//            //        {
//            //            double delta_i = err * u_f - Regularization * i_f;
//            //            item_factors.Inc(i, f, current_learnrate * delta_i);
//            //        }
//            //    }
//            //}

//            //UpdateLearnRate();
//        }

//        private void LearnFactors(IList<int> rating_indices, bool update_user, bool update_item)
//        {
//            var h = new Matrix<float>(NumFactors, MaxItemID + 1);
//            var beta = new Matrix<float>(NumFactors, MaxItemID + 1);
//            beta.Init(0);

//            var w = new Matrix<float>(NumFactors, MaxItemID + 1);
//            var alpha = new Matrix<float>(NumFactors, MaxItemID + 1);
//            alpha.Init(0);

//            var d = new Matrix<float>(MaxUserID + 1, MaxItemID + 1);
//            double dnorm = 0;

//            ////Loop-Indices
//            int iter, iiter, i;

//            double x = 0;

//            var rati = ratings

//            var x = items_rated_by_user.FrobeniusNorm();

//            x = dlange('F', m, n, *a, m, NULL);
//            x *= x;
//            x /= 2;



//            for (uint current_iter = 0; current_iter < NumIter; current_iter++)
//                Iterate(rating_indices, update_user, update_item);
//        }

//        ///
//        protected float Predict(int user_id, int item_id, bool bound)
//        {
//            float result = global_bias + MyMediaLite.DataType.MatrixExtensions.RowScalarProduct(user_factors, user_id, item_factors, item_id);

//            if (bound)
//            {
//                if (result > MaxRating)
//                    return MaxRating;
//                if (result < MinRating)
//                    return MinRating;
//            }
//            return result;
//        }

//        /// <summary>Predict rating for a fold-in user and an item</summary>
//        /// <param name='user_vector'>a float vector representing the user</param>
//        /// <param name='item_id'>the item ID</param>
//        /// <returns>the predicted rating</returns>
//        protected virtual float Predict(float[] user_vector, int item_id)
//        {
//            return Predict(user_vector, item_id, true);
//        }

//        ///
//        float Predict(float[] user_vector, int item_id, bool bound)
//        {
//            float result = global_bias + MyMediaLite.DataType.MatrixExtensions.RowScalarProduct(item_factors, item_id, user_vector);

//            if (bound)
//            {
//                if (result > MaxRating)
//                    return MaxRating;
//                if (result < MinRating)
//                    return MinRating;
//            }
//            return result;
//        }

//        /// <summary>Predict the rating of a given user for a given item</summary>
//        /// <remarks>
//        /// If the user or the item are not known to the recommender, the global average is returned.
//        /// To avoid this behavior for unknown entities, use CanPredict() to check before.
//        /// </remarks>
//        /// <param name="user_id">the user ID</param>
//        /// <param name="item_id">the item ID</param>
//        /// <returns>the predicted rating</returns>
//        public override float Predict(int user_id, int item_id)
//        {
//            if (user_id >= user_factors.dim1)
//                return global_bias;
//            if (item_id >= item_factors.dim1)
//                return global_bias;

//            return Predict(user_id, item_id, true);
//        }

//        ///
//        public override void AddRatings(IRatings ratings)
//        {
//            base.AddRatings(ratings);
//            foreach (int user_id in ratings.AllUsers)
//                RetrainUser(user_id);
//            foreach (int item_id in ratings.AllItems)
//                RetrainItem(item_id);
//        }

//        ///
//        public override void UpdateRatings(IRatings ratings)
//        {
//            base.UpdateRatings(ratings);
//            foreach (int user_id in ratings.AllUsers)
//                RetrainUser(user_id);
//            foreach (int item_id in ratings.AllItems)
//                RetrainItem(item_id);
//        }

//        ///
//        public override void RemoveRatings(IDataSet ratings)
//        {
//            base.RemoveRatings(ratings);
//            foreach (int user_id in ratings.AllUsers)
//                RetrainUser(user_id);
//            foreach (int item_id in ratings.AllItems)
//                RetrainItem(item_id);
//        }

//        ///
//        protected override void AddUser(int user_id)
//        {
//            base.AddUser(user_id);
//            user_factors.AddRows(user_id + 1);
//        }

//        ///
//        protected override void AddItem(int item_id)
//        {
//            base.AddItem(item_id);
//            item_factors.AddRows(item_id + 1);
//        }

//        ///
//        public override void RemoveUser(int user_id)
//        {
//            base.RemoveUser(user_id);

//            // set user factors to zero
//            user_factors.SetRowToOneValue(user_id, 0);
//        }

//        ///
//        public override void RemoveItem(int item_id)
//        {
//            base.RemoveItem(item_id);

//            // set item factors to zero
//            item_factors.SetRowToOneValue(item_id, 0);
//        }

//        /// <summary>Compute parameters (latent factors) for a user represented by ratings</summary>
//        /// <returns>a vector of latent factors</returns>
//        /// <param name='rated_items'>a list of (item ID, rating value) pairs</param>
//        protected virtual float[] FoldIn(IList<Tuple<int, float>> rated_items)
//        {
//            var user_vector = new float[NumFactors];
//            user_vector.InitNormal(InitMean, InitStdDev);
//            rated_items.Shuffle();
//            double lr = LearnRate;
//            for (uint it = 0; it < NumIter; it++)
//            {
//                for (int index = 0; index < rated_items.Count; index++)
//                {
//                    int item_id = rated_items[index].Item1;
//                    float err = rated_items[index].Item2 - Predict(user_vector, item_id, false);

//                    // adjust factors
//                    for (int f = 0; f < NumFactors; f++)
//                    {
//                        float u_f = user_vector[f];
//                        float i_f = item_factors[item_id, f];

//                        double delta_u = err * i_f - Regularization * u_f;
//                        user_vector[f] += (float)(lr * delta_u);
//                    }
//                }
//                lr *= Decay;
//            }
//            return user_vector;
//        }

//        ///
//        public IList<Tuple<int, float>> ScoreItems(IList<Tuple<int, float>> rated_items, IList<int> candidate_items)
//        {
//            var user_vector = FoldIn(rated_items);

//            // score the items
//            var result = new Tuple<int, float>[candidate_items.Count];
//            for (int i = 0; i < candidate_items.Count; i++)
//            {
//                int item_id = candidate_items[i];
//                result[i] = Tuple.Create(item_id, Predict(user_vector, item_id));
//            }
//            return result;
//        }

//        ///
//        public override void SaveModel(string filename)
//        {
//            using (StreamWriter writer = Model.GetWriter(filename, this.GetType(), "2.99"))
//            {
//                writer.WriteLine(global_bias.ToString(CultureInfo.InvariantCulture));
//                writer.WriteMatrix(user_factors);
//                writer.WriteMatrix(item_factors);
//            }
//        }

//        ///
//        public override void LoadModel(string filename)
//        {
//            using (StreamReader reader = Model.GetReader(filename, this.GetType()))
//            {
//                var bias = float.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);

//                var user_factors = (Matrix<float>)reader.ReadMatrix(new Matrix<float>(0, 0));
//                var item_factors = (Matrix<float>)reader.ReadMatrix(new Matrix<float>(0, 0));

//                if (user_factors.NumberOfColumns != item_factors.NumberOfColumns)
//                    throw new Exception(
//                        string.Format("Number of user and item factors must match: {0} != {1}",
//                            user_factors.NumberOfColumns, item_factors.NumberOfColumns));

//                this.MaxUserID = user_factors.NumberOfRows - 1;
//                this.MaxItemID = item_factors.NumberOfRows - 1;

//                // assign new model
//                this.global_bias = bias;
//                if (this.NumFactors != user_factors.NumberOfColumns)
//                {
//                    Console.Error.WriteLine("Set NumFactors to {0}", user_factors.NumberOfColumns);
//                    this.NumFactors = (uint)user_factors.NumberOfColumns;
//                }
//                this.user_factors = user_factors;
//                this.item_factors = item_factors;
//            }
//        }

//        /// <summary>Compute the regularized loss</summary>
//        /// <returns>the regularized loss</returns>
//        public virtual float ComputeObjective()
//        {
//            double objective = MyMediaLite.Eval.Measures.RMSE.ComputeSquaredErrorSum(this, ratings);

//            for (int u = 0; u <= MaxUserID; u++)
//                objective += ratings.CountByUser[u] * Regularization * Math.Pow(user_factors.GetRow(u).EuclideanNorm(), 2);

//            for (int i = 0; i <= MaxItemID; i++)
//                objective += ratings.CountByItem[i] * Regularization * Math.Pow(item_factors.GetRow(i).EuclideanNorm(), 2);

//            return (float)objective;
//        }

//        ///
//        public override string ToString()
//        {
//            return string.Format(
//                CultureInfo.InvariantCulture,
//                "{0} num_factors={1} regularization={2} learn_rate={3} learn_rate_decay={4} num_iter={5}",
//                this.GetType().Name, NumFactors, Regularization, LearnRate, Decay, NumIter);
//        }
//    }
//}
