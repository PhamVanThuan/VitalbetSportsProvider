namespace VitalbetSportsProvider.DataModel.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VitalbetSportsProvider.Models;

    public interface ISportsRepository
    {
        Task AddOrUpdateAsync(
               IReadOnlyCollection<Sport> sports,
               IReadOnlyCollection<Event> events,
               IReadOnlyCollection<Match> matches,
               IReadOnlyCollection<Bet> bets,
               IReadOnlyCollection<Odds> odds);
        
        IReadOnlyCollection<Sport> GetSports();
    }
}