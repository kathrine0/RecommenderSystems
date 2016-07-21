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

        public virtual DbSet<Load_Product_Category_Fact> Load_Product_Category_Fact { get; set; }
        public virtual DbSet<Load_Product_Dimension> Load_Product_Dimension { get; set; }
        public virtual DbSet<Load_Review_Fact> Load_Review_Fact { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Load_Product_Dimension>()
                .Property(e => e.Average_Review_Rating_Number)
                .HasPrecision(2, 1);
        }
    }
}
