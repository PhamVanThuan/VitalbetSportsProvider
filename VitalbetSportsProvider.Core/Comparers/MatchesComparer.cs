namespace VitalbetSportsProvider.WebClient.Core.Comparers
{
    using System.Collections.Generic;
    using VitalbetSportsProvider.Models;

    /// <summary>
    ///     Note: The comparer do not check nasted items
    /// </summary>
    public class MatchesComparer : IEqualityComparer<Match>
    {
        public int GetHashCode(Match co) => co?.Id ?? 0;

        public bool Equals(Match x1, Match x2)
        {
            if (ReferenceEquals(x1, x2))
            {
                return true;
            }

            if (ReferenceEquals(x1, null) || ReferenceEquals(x2, null))
            {
                return false;
            }

            // For performance reason we assume that once created Match do not chache it's Name and EventId.
            return x1.Id == x2.Id 
                ////&& x1.Name == x2.Name 
                ////&& x1.EventId == x2.EventId 
                && x1.StartDate == x2.StartDate
                && x1.MatchType == x2.MatchType;
        }
    }
}
