namespace VitalbetSportsProvider.WebClient.Core.Comparers
{
    using System.Collections.Generic;
    using VitalbetSportsProvider.Models;
    
    public class OddsComparer : IEqualityComparer<Odds>
    {
        public int GetHashCode(Odds co) => co?.Id ?? 0;

        public bool Equals(Odds x1, Odds x2)
        {
            if (ReferenceEquals(x1, x2))
            {
                return true;
            }

            if (ReferenceEquals(x1, null) || ReferenceEquals(x2, null))
            {
                return false;
            }

            // For performance reason we assume that once created Odds do not chache it's Name and BetId.
            return x1.Id == x2.Id 
                ////&& x1.Name == x2.Name 
                ////&& x1.BetId == x2.BetId
                && x1.Value == x2.Value
                && x1.SpecialBetValue == x2.SpecialBetValue
                && x1.SpecialBetValueSpecified == x2.SpecialBetValueSpecified;
        }
    }
}
