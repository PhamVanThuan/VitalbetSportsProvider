namespace VitalbetSportsProvider.WebClient.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNet.SignalR;

    using VitalbetSportsProvider.Core;

    public abstract class UpdatableServiceHub<TItem> : Hub
        where TItem : class
    {
        private readonly IUpdatableService<TItem> _updatableService;

        protected UpdatableServiceHub(IUpdatableService<TItem> updatableService)
        {
            this._updatableService = updatableService;
        }

        public override Task OnConnected()
        {
            this._updatableService.AddOrUpdate += this.AddOrUpdate;
            this._updatableService.Delete += this.Delete;

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            this._updatableService.AddOrUpdate -= this.AddOrUpdate;
            this._updatableService.Delete -= this.Delete;

            return base.OnDisconnected(stopCalled);
        }

        protected virtual bool IsValidAddOrUpdate(TItem update) => true;

        protected virtual bool IsValidDelete(TItem delete) => true;

        private void AddOrUpdate(TItem update)
        {
            if (this.IsValidAddOrUpdate(update))
            {
                this.Clients.Caller.addOrUpdate(update);
            }
        }

        private void Delete(TItem delete)
        {
            if (this.IsValidDelete(delete))
            {
                this.Clients.Caller.delete(delete);
            }
        }
    }
}