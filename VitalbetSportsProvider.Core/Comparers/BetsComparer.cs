namespace VitalbetSportsProvider.WebClient.Core.Comparers
{
    using System.Collections.Generic;
    using VitalbetSportsProvider.Models;

    /// <summary>
    ///     Note: The comparer do not check nasted items
    /// </summary>
    public class BetsComparer : IEqualityComparer<Bet>
    {
        public int GetHashCode(Bet co) => co?.Id ?? 0;

        public bool Equals(Bet x1, Bet x2)
        {
            if (ReferenceEquals(x1, x2))
            {
                return true;
            }

            if (ReferenceEquals(x1, null) || ReferenceEquals(x2, null))
            {
                return false;
            }

            // For performance reason we assume that once created Bet do not chache it's Name and MatchId.
            return x1.Id == x2.Id 
                ////&& x1.Name == x2.Name 
                ////&& x1.MatchId == x2.MatchId 
                && x1.IsLive == x2.IsLive;
        }
    }
}
