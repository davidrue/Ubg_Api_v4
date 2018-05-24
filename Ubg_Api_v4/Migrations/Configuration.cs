namespace Ubg_Api_v4.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Ubg_Api_v4.Models.Ubg_Api_v4Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ubg_Api_v4.Models.Ubg_Api_v4Context context)
        {
            context.Actors.AddOrUpdate(x => x.Id,
        new Actor () { Id = "1", Name = "Jane Austen", Email = "jane@web.de" },
        new Actor() { Id = "2", Name = "Charles Dickens", Email = "charlesweb.de" },
        new Actor() { Id = "3", Name = "Miguel de Cervantes", Email = "miguel@web.de" }
        );

            context.BankAccounts.AddOrUpdate(x => x.Id,
       new BankAccount() { Id = "1", Iban = "12345", Actor_Id = "1" },
       new BankAccount() { Id = "2", Iban = "12345", Actor_Id = "2" },
       new BankAccount() { Id = "3", Iban = "12345", Actor_Id = "3" }
       );

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
