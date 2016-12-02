namespace VitalbetSportsProvider.WebClient.Core.Comparers
{
    using System.Collections.Generic;
    using VitalbetSportsProvider.Models;

    public class SportsComparer : IEqualityComparer<Sport>
    {
        public int GetHashCode(Sport co) => co?.Id ?? 0;

        public bool Equals(Sport x1, Sport x2)
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
                && x1.Name == x2.Name;
        }
    }
}
