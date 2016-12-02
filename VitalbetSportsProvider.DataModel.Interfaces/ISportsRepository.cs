using System.Collections.Generic;
using System.Threading.Tasks;
using VitalbetSportsProvider.Models;

namespace VitalbetSportsProvider.DataModel.Interfaces
{
    public interface ISportsRepository
    {
        Task AddOrUpdateAsync(IList<Sport> sports);

        IReadOnlyCollection<Sport> GetSports();
    }
}