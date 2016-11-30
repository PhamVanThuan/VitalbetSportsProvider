using System.Threading.Tasks;
using VitalbetSportsProvider.Models.Xml;

namespace VitalbetSportsProvider.Feed.Interfaces
{
    public interface IVitalbetSportsRepository
    {
        Task<XmlSports> RequestSportsAsync();
    }
}