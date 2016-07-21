using Recommender.DataAccess.YahooMusic.Entities;
using System.Data.Entity;

namespace Recommender.DataAccess.YahooMusic
{
    public partial class YahooMusicContext : BaseContext
    {
        public YahooMusicContext()
            : base("name=YahooMusicContext")
        {
        }

        public virtual DbSet<GenreAttribute> GenreAttributes { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<SongAttribute> SongAttributes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
