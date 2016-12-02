namespace VitalbetSportsProvider.WebClient.Hubs
{
    using System.Collections.Generic;
    using Microsoft.AspNet.SignalR;
    using VitalbetSportsProvider.DataModel.Interfaces;
    using VitalbetSportsProvider.Models;

    public class SportsHub : Hub
    {
        private readonly ISportsRepository _sportsRepository;

        public SportsHub(ISportsRepository sportsRepository)
        {
            this._sportsRepository = sportsRepository;
        }

        public IReadOnlyCollection<Sport> GetSports() => this._sportsRepository.GetSports();
    }
}