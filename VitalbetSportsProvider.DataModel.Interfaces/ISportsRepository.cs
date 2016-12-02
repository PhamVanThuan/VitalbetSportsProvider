namespace VitalbetSportsProvider.DataModel.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VitalbetSportsProvider.Models;

    public interface ISportsRepository
    {
        Task AddOrUpdateAsync(IList<Sport> sports);

        IReadOnlyCollection<Sport> GetSports();
    }
}