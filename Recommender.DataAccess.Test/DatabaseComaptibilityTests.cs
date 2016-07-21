using NUnit.Framework;
using Recommender.DataAccess.AmazonMeta;
using Recommender.DataAccess.MovieLense;
using Recommender.DataAccess.YahooMusic;

namespace Recommender.DataAccess.Test
{
    public class DatabaseComaptibilityTests
    {
        private MovieLenseContext movieLensContext;
        private AmazonMetaContext amazonContext;
        private YahooMusicContext yahooContext;

        [SetUp]
        public void Setup()
        {
            movieLensContext = new MovieLenseContext();
            amazonContext = new AmazonMetaContext();
            yahooContext = new YahooMusicContext();
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

        [Test]
        public void YahooMusic_DatabaseMatchTheModel()
        {
            bool isCompatible = yahooContext.IsCompatible;

            Assert.True(isCompatible);
        }
    }
}
