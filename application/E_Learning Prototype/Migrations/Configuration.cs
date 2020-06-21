namespace E_Learning_Prototype.Migrations
{
    using E_Learning_Prototype.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<E_Learning_Prototype.infrastructure.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(E_Learning_Prototype.infrastructure.ApplicationDbContext context)
        {

            context.Themes.AddOrUpdate(x => x.Id,
                new Theme() { Id = 1, Nom = "Examen" },
                new Theme() { Id = 2, Nom = "Obliger" },
                new Theme() { Id = 3, Nom = "Optionnel" }
            );
        }
    }
}
