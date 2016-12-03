namespace VitalbetSportsProvider.WebClient.Core.Comparers
{
    using System.Collections.Generic;
    using VitalbetSportsProvider.Models;

    /// <summary>
    ///     Note: The comparer do not check nasted items
    /// </summary>
    public class EventsComparer : IEqualityComparer<Event>
    {
        public int GetHashCode(Event co) => co?.Id ?? 0;

        public bool Equals(Event x1, Event x2)
        {
            if (ReferenceEquals(x1, x2))
            {
                return true;
            }

            if (ReferenceEquals(x1, null) || ReferenceEquals(x2, null))
            {
                return false;
            }

            // For performance reason we assume that once created Event do not chache it's Name and SportId.
            return x1.Id == x2.Id 
                ////&& x1.Name == x2.Name 
                ////&& x1.SportId == x2.SportId 
                && x1.IsLive == x2.IsLive
                && x1.CategoryId == x2.CategoryId;
        }
    }
}
