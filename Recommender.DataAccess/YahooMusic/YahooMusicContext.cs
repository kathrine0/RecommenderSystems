using Recommender.DataAccess.YahooMusic.Entities;
using System.Data.Entity;

namespace Recommender.DataAccess.YahooMusic
{
    public partial class YahooMusicContext : BaseContext
    {
        public YahooMusicContext()
            : base("Name=YahooMusicContext")
        {
            
        }

        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Song> Song { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
