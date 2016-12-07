namespace VitalbetSportsProvider.WebClient.Hubs.UpdatableHubs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VitalbetSportsProvider.Core;
    using VitalbetSportsProvider.DataModel.Interfaces;
    using VitalbetSportsProvider.ViewModels;

    public class BetsHub : UpdatableServiceHub<BetViewModel>
    {
        private static readonly ConnectionMapping<int> _mapping = new ConnectionMapping<int>();

        private readonly ISportsRepository _sportsRepository;
        
        public BetsHub(IUpdatableService<BetViewModel> updatableService, ISportsRepository sportsRepository) 
            : base(updatableService)
        {
            this._sportsRepository = sportsRepository;
        }

        public async Task<IReadOnlyCollection<BetViewModel>> Get(int matchId)
        {
            await this.OnConnected();
            _mapping.AddOrUpdate(this.Context.ConnectionId, matchId);
            return await Task.FromResult(this._sportsRepository.GetBets(matchId));
        }
        
        public override Task OnDisconnected(bool stopCalled)
        {
            _mapping.Remove(this.Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        protected override bool IsValidAddOrUpdate(BetViewModel update) => update.Id == _mapping[this.Context.ConnectionId];

        protected override bool IsValidDelete(BetViewModel update) => update.Id == _mapping[this.Context.ConnectionId];
    }
}