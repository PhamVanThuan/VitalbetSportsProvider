namespace VitalbetSportsProvider.DataModel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VitalbetSportsProvider.DataModel.Interfaces;
    using VitalbetSportsProvider.Models;
    using VitalbetSportsProvider.ViewModels;
    
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

        public IReadOnlyCollection<SportViewModel> GetSports()
        {
            using (var context = new SportsContext())
            {
                return context
                    .Sports
                    .Include("Events")
                    .Select(s => new SportViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        EventsCount = s.Events.Count
                    })
                    .ToList();
            }
        }

        public IReadOnlyCollection<EventViewModel> GetEvents(int sportId)
        {
            using (var context = new SportsContext())
            {
                return context
                    .Events
                    .Include("Matches")
                    .Where(s => s.SportId == sportId)
                    .Select(s => new EventViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        MatchesCount = s.Matches.Count
                    })
                    .ToList();
            }
        }

        public IReadOnlyCollection<MatchViewModel> GetMatches(int eventId)
        {
            using (var context = new SportsContext())
            {
                return context
                    .Matches
                    .Where(s => s.EventId == eventId)
                    .Select(s => new MatchViewModel
                    {
                        Id = s.Id,
                        Name = s.Name
                    })
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
