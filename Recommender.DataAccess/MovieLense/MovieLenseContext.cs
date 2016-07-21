using Recommender.DataAccess.MovieLense.Entities;
using System.Data.Entity;

namespace Recommender.DataAccess.MovieLense
{

    public partial class MovieLenseContext : BaseContext
    {
        public MovieLenseContext()
            : base("Name=MovieLenseContext")
        {
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
