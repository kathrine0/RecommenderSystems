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

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>()
                .Property(e => e.Name);

            modelBuilder.Entity<Director>()
                .Property(e => e.Name);

            modelBuilder.Entity<Genre>()
                .Property(e => e.TheGenre);

            modelBuilder.Entity<Link>()
                .Property(e => e.ImdbId)
                ;

            modelBuilder.Entity<Link>()
                .Property(e => e.TmdbId)
                ;

            modelBuilder.Entity<Movie>()
                .Property(e => e.Title)
                ;

            modelBuilder.Entity<Movie>()
                .Property(e => e.Genres)
                ;

            modelBuilder.Entity<Movie>()
                .HasMany(x => x.Actors)
                .WithMany(x => x.Movies);


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
