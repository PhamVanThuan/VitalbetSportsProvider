namespace VitalbetSportsProvider.DataModel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VitalbetSportsProvider.DataModel.Interfaces;
    using VitalbetSportsProvider.Models;

    public class SportsRepository : ISportsRepository
    {
        public async Task AddOrUpdateAsync(
            IReadOnlyCollection<Sport> sports,
            IReadOnlyCollection<Event> events,
            IReadOnlyCollection<Match> matches,
            IReadOnlyCollection<Bet> bets,
            IReadOnlyCollection<Odds> odds)
        {
            await this.AddOrUpdateAsync(sports);
            await this.AddOrUpdateAsync(events);
            await this.AddOrUpdateAsync(matches);
            await this.AddOrUpdateAsync(bets);
            await this.AddOrUpdateAsync(odds);
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

        private async Task AddOrUpdateAsync<T>(IReadOnlyCollection<T> sports)
            where T : class
        {
            if (sports.Count <= 0)
            {
                return;
            }

            using (var context = new SportsContext())
            {
                await context.BulkMergeAsync(sports);
                await context.BulkSaveChangesAsync();
            }
        }
    }
}
