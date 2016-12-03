namespace VitalbetSportsProvider.WebClient.Core.Comparers
{
    using System.Collections.Generic;
    using VitalbetSportsProvider.Models;

    /// <summary>
    ///     Note: The comparer do not check nasted items
    /// </summary>
    public class SportsComparer : IEqualityComparer<Sport>
    {
        public int GetHashCode(Sport co) => co?.Id ?? 0;

        public bool Equals(Sport x1, Sport x2)
        {
            if (ReferenceEquals(x1, x2))
            {
                return true;
            }

            if (ReferenceEquals(x1, null) || ReferenceEquals(x2, null))
            {
                return false;
            }

            // For performance reason we assume that once created Sport do not chache it's Name.
            return x1.Id == x2.Id;
                ////&& x1.Name == x2.Name;
        }
    }
}
