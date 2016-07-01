namespace Recommender.Service.Data
{
    public interface IFeature
    {
        string Id { get; set; }

        string Name { get; set; }

        string FeatureCategory { get; set; }
    }
}