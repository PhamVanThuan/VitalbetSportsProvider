namespace VitalbetSportsProvider.Feed.Interfaces
{
    using System.Threading.Tasks;
    using VitalbetSportsProvider.Models.Xml;

    public interface IVitalbetSportsRepository
    {
        Task<XmlSports> RequestSportsAsync();
    }
}