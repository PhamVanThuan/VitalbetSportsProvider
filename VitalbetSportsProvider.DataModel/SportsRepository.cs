namespace VitalbetSportsProvider.DataModel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VitalbetSportsProvider.DataModel.Interfaces;
    using VitalbetSportsProvider.Models;

    public class SportsRepository : ISportsRepository
    {
        public async Task AddOrUpdateAsync(IList<Sport> sports)
        {
            var events = sports.SelectMany(s => s.Events).ToList();
            var matches = events.SelectMany(s => s.Matches).ToList();
            var bets = matches.SelectMany(s => s.Bets).ToList();
            var odds = bets.SelectMany(s => s.Odds).ToList();

            using (var context = new SportsContext())
            {
                await context.BulkMergeAsync(sports);
                await context.BulkMergeAsync(events);
                await context.BulkMergeAsync(matches);
                await context.BulkMergeAsync(bets);
                await context.BulkMergeAsync(odds);

                await context.BulkSaveChangesAsync();
            }
        }

        public IReadOnlyCollection<Sport> GetSports()
        {
            using (var context = new SportsContext())
            {
                return context
                    .Sports
                    .Include("Events")
                    .ToList();
            }
        }
    }
}
