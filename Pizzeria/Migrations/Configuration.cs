namespace Pizzeria.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Pizzeria.Models.ModelDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //ContextKey = "Pizzeria.Models.ModelDBContext";
        }

        protected override void Seed(Pizzeria.Models.ModelDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
