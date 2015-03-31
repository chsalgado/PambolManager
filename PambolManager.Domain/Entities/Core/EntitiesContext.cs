using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PambolManager.Domain.Entities.Core
{
    public class EntitiesContext : IdentityDbContext<FieldManager>
    {
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Score> Scores { get; set; }
        
        public EntitiesContext()
            : base("PambolManagerContext", throwIfV1Schema: false)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>()
                        .HasRequired(m => m.HomeTeam)
                        .WithMany(t => t.HomeMatches)
                        .HasForeignKey(m => m.HomeTeamId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
                        .HasRequired(m => m.AwayTeam)
                        .WithMany(t => t.AwayMatches)
                        .HasForeignKey(m => m.AwayTeamId)
                        .WillCascadeOnDelete(false);
        }

        public static EntitiesContext Create()
        {
            return new EntitiesContext();
        }
    }
}
