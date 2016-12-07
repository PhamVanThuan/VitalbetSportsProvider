namespace VitalbetSportsProvider.WebClient.Hubs
{
    using System;
    using System.Collections.Concurrent;

    public class ConnectionMapping<T>
    {
        private readonly ConcurrentDictionary<string, T> _mapping = new ConcurrentDictionary<string, T>(StringComparer.InvariantCultureIgnoreCase);

        public T this[string connectionId]
        {
            get
            {
                T result;
                this._mapping.TryGetValue(connectionId, out result);
                return result;
            }
        }

        public void AddOrUpdate(string connectionId, T value)
        {
            this._mapping.AddOrUpdate(connectionId, value, (key, prevValue) => value);
        }

        public void Remove(string connectionId)
        {
            T value;
            this._mapping.TryRemove(connectionId, out value);
        }
    }
}