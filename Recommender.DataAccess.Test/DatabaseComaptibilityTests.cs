using NUnit.Framework;
using Recommender.DataAccess.AmazonMeta;
using Recommender.DataAccess.MovieLense;

namespace Recommender.DataAccess.Test
{
    public class DatabaseComaptibilityTests
    {
        private MovieLenseContext movieLensContext;

        private AmazonMetaContext amazonContext;

        [SetUp]
        public void Setup()
        {
            movieLensContext = new MovieLenseContext();
            amazonContext = new AmazonMetaContext();
        }

        [Test]
        public void MovieLens_DatabaseMatchTheModel()
        {
            bool isCompatible = movieLensContext.IsCompatible;

            Assert.True(isCompatible);
        }

        [Test]
        public void AmazonMeta_DatabaseMatchTheModel()
        {
            bool isCompatible = amazonContext.IsCompatible;

            Assert.True(isCompatible);
        }
    }
}
