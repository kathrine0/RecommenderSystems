using System.Data.Entity.Migrations;

namespace Recommender.DataAccess.MovieLense.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<Recommender.DataAccess.MovieLense.MovieLenseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"MovieLense\Migrations";
        }

        protected override void Seed(Recommender.DataAccess.MovieLense.MovieLenseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
