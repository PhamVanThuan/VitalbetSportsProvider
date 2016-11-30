using System.Linq;
using AutoMapper;
using VitalbetSportsProvider.DataModel;
using VitalbetSportsProvider.Feed.Http;
using VitalbetSportsProvider.Models;

namespace ConsoleRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ss = new VitalbetSportsRepository();
            
            var config = new MapperConfiguration(s => s.AddProfile<XmlMappings>());
            IMapper mapper = new Mapper(config);

            var xml = ss.RequestSportsAsync().Result;

            var sports = xml.Sports.Select(mapper.Map<Sport>).ToList();

            var repo = new SportsRepository();

            repo.AddOrUpdate(sports);
        }
    }
}
