using System.Data.Entity;
using Recommender.DataAccess.AmazonMeta.Entities;

namespace Recommender.DataAccess.AmazonMeta
{
    public partial class AmazonMetaContext : BaseContext
    {
        public AmazonMetaContext()
            : base("name=AmazonMetaContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.AverageReviewRatingNumber)
                .HasPrecision(2, 1);
        }
    }
}
