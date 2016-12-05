namespace VitalbetSportsProvider.WebClient.Hubs
{
    using System.Collections.Generic;
    using Microsoft.AspNet.SignalR;
    using VitalbetSportsProvider.DataModel.Interfaces;
    using VitalbetSportsProvider.ViewModels;

    public class SportsHub : Hub
    {
        private readonly ISportsRepository _sportsRepository;

        public SportsHub(ISportsRepository sportsRepository)
        {
            this._sportsRepository = sportsRepository;
        }

        public IReadOnlyCollection<SportViewModel> GetSports() => this._sportsRepository.GetSports();

        public IReadOnlyCollection<EventViewModel> GetEvents(int sprortId) => this._sportsRepository.GetEvents(sprortId);

        public IReadOnlyCollection<MatchViewModel> GetMatches(int eventId) => this._sportsRepository.GetMatches(eventId);

        public IReadOnlyCollection<BetViewModel> GetBets(int matchId) => this._sportsRepository.GetBets(matchId);
    }
}