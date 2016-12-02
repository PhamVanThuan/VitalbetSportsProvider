using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using VitalbetSportsProvider.DataModel.Interfaces;
using VitalbetSportsProvider.Models;

namespace VitalbetSportsProvider.WebClient.Hubs
{
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