using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using VitalbetSportsProvider.DataModel;
using VitalbetSportsProvider.Models;

namespace VitalbetSportsProvider.WebClient.Hubs
{
    public class SportsHub : Hub
    {
        private readonly SportsRepository _sportsRepository;

        public SportsHub(SportsRepository sportsRepository)
        {
            this._sportsRepository = sportsRepository;
        }

        public IReadOnlyCollection<Sport> GetSports() => this._sportsRepository.GetSports();
    }
}