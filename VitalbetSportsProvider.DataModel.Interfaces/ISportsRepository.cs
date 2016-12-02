using System.Collections.Generic;
using VitalbetSportsProvider.Models;

namespace VitalbetSportsProvider.DataModel.Interfaces
{
    public interface ISportsRepository
    {
        void AddOrUpdate(IList<Sport> sports);

        IReadOnlyCollection<Sport> GetSports();
    }
}