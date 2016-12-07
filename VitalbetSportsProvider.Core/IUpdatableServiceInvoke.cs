namespace VitalbetSportsProvider.Core
{
    using System.Collections.Generic;
    
    public interface IUpdatableServiceInvoke<TItem> : IUpdatableService<TItem>
        where TItem : class
    {
        void AddOrUpdateInvoke(IReadOnlyCollection<TItem> collection);

        void DeleteInvoke(IReadOnlyCollection<TItem> collection);
    }
}
