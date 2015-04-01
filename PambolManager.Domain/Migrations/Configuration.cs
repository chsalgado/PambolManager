namespace PambolManager.Domain.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PambolManager.Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PambolManager.Domain.Entities.Core.EntitiesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PambolManager.Domain.Entities.Core.EntitiesContext context)
        {
            //  This method will be called after migrating to the latest version.

            var store = new UserStore<FieldManager>(context);
            var manager = new UserManager<FieldManager>(store);
            var hasher = new PasswordHasher();

            String[] usernames = new String[] { "vacasagrada", "greg.nadal", "chris.salgado" };
            String[] mails = new String[] { "sacredcow2.0@gmail.com", "greg@greg.com", "chrisave@chrisave.com" };
            String[] names = new String[] { "Jorge Hernandez", "Gregorio Modelo", "Christian Salgado" };
            Random r = new Random();

            for (int i = 1; i <= mails.Length; i++)
            {
                var user = manager.FindById(i.ToString());
                if (user == null)
                {
                    var nameLastName = names[i - 1].Split(new Char[] { ' ' });
                    manager.Create(new FieldManager
                    {
                        Id = i.ToString(),
                        UserName = usernames[i - 1],
                        Email = mails[i - 1],
                        Name = nameLastName[0],
                        LastName = nameLastName[1],
                        PhoneNumber = "4421234567"
                    }, "qwerty123");
                }
            }
            
            context.Tournaments.AddOrUpdate(
              t => t.TournamentName,
              new Tournament { Id = Guid.NewGuid(), TournamentName = "Torneo 1", TotalRounds = 10, MaxTeams = 20, FieldManagerId = "1" },
              new Tournament { Id = Guid.NewGuid(), TournamentName = "Torneo 2", TotalRounds = 10, MaxTeams = 20, FieldManagerId = "2" },
              new Tournament { Id = Guid.NewGuid(), TournamentName = "Torneo 3", TotalRounds = 10, MaxTeams = 20, FieldManagerId = "3" },
              new Tournament { Id = Guid.NewGuid(), TournamentName = "Torneo 4", TotalRounds = 5, MaxTeams = 10, FieldManagerId = "1" },
              new Tournament { Id = Guid.NewGuid(), TournamentName = "Torneo 5", TotalRounds = 5, MaxTeams = 10, FieldManagerId = "2" },
              new Tournament { Id = Guid.NewGuid(), TournamentName = "Torneo 6", TotalRounds = 5, MaxTeams = 10, FieldManagerId = "3" }
            );
        }
    }
}
