namespace VitalbetSportsProvider.DataModel.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VitalbetSportsProvider.Models;
    using VitalbetSportsProvider.ViewModels;

    public interface ISportsRepository
    {
        Task AddOrUpdateAsync(
               IReadOnlyCollection<Sport> sports,
               IReadOnlyCollection<Event> events,
               IReadOnlyCollection<Match> matches,
               IReadOnlyCollection<Bet> bets,
               IReadOnlyCollection<Odds> odds);
        
        IReadOnlyCollection<SportViewModel> GetSports();

        IReadOnlyCollection<EventViewModel> GetEvents(int sportId);

        IReadOnlyCollection<MatchViewModel> GetMatches(int eventId);

        IReadOnlyCollection<BetViewModel> GetBets(int matchId);
    }
}