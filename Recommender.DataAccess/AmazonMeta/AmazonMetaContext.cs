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

        public virtual DbSet<Category> Load_Product_Category_Fact { get; set; }
        public virtual DbSet<Product> Load_Product_Dimension { get; set; }
        public virtual DbSet<Rating> Load_Review_Fact { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.AverageReviewRatingNumber)
                .HasPrecision(2, 1);
        }
    }
}
