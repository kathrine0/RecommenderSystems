using NUnit.Framework;
using Recommender.DataAccess;

namespace Recommender.DataAccess.Test
{
    public class DatabaseComaptibilityTests
    {
        private MovieLenseContext context;

        [SetUp]
        public void Setup()
        {
            context = new MovieLenseContext();
        }

        [Test]
        public void DatabaseMatchTheModel()
        {
            bool isCompatible = context.IsCompatible;

            Assert.True(isCompatible);
        }
    }
}
