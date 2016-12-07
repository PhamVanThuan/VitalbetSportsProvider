namespace VitalbetSportsProvider.Core
{
    using System;

    public interface IUpdatableService<out TItem>
        where TItem : class
    {
        event Action<TItem> AddOrUpdate;

        event Action<TItem> Delete;
    }
}
