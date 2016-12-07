namespace VitalbetSportsProvider.Core.Services
{
    using System;
    using System.Collections.Generic;

    using VitalbetSportsProvider.ViewModels;

    public class BetsUpdatableService : IUpdatableServiceInvoke<BetViewModel>
    {
        public event Action<BetViewModel> AddOrUpdate;

        public event Action<BetViewModel> Delete;
        
        public void AddOrUpdateInvoke(IReadOnlyCollection<BetViewModel> collection)
        {
            foreach (var viewModel in collection)
            {
                this.AddOrUpdate?.Invoke(viewModel);
            }
        }

        public void DeleteInvoke(IReadOnlyCollection<BetViewModel> collection)
        {
            foreach (var viewModel in collection)
            {
                this.Delete?.Invoke(viewModel);
            }
        }
    }
}
