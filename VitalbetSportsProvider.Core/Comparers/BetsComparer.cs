namespace VitalbetSportsProvider.WebClient.Core.Comparers
{
    using System.Collections.Generic;
    using VitalbetSportsProvider.Models;

    public class BetsComparer : IEqualityComparer<Bet>
    {
        public int GetHashCode(Bet co) => co?.Id ?? 0;

        public bool Equals(Bet x1, Bet x2)
        {
            if (object.ReferenceEquals(x1, x2))
            {
                return true;
            }

            if (object.ReferenceEquals(x1, null) || object.ReferenceEquals(x2, null))
            {
                return false;
            }

            return x1.Id == x2.Id 
                && x1.Name == x2.Name 
                && x1.MatchId == x2.MatchId 
                && x1.IsLive == x2.IsLive;
        }
    }
}
