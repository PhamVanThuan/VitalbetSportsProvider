using System.Data.Entity;
using VitalbetSportsProvider.Models;

namespace VitalbetSportsProvider.DataModel
{
    public class SportsContext : DbContext
    {
        public DbSet<Sport> Sports { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Odds> Odds { get; set; }
    }
}
