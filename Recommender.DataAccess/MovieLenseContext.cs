using Recommender.DataAccess.Entities;
using System.Data.Entity;

namespace Recommender.DataAccess
{

    public partial class MovieLenseContext : DbContext
    {
        public MovieLenseContext()
            : base("Name=MovieLenseContext")
        {
        }

        public bool IsCompatible {
            get {
                return Database.CompatibleWithModel(true);
            }
        }

        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.Gender)
                .IsFixedLength()
                ;

            modelBuilder.Entity<User>()
                .Property(e => e.Zipcode)
                .IsFixedLength();
        }
    }
}
