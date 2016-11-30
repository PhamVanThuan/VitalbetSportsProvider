using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VitalbetSportsProvider.DataModel;
using VitalbetSportsProvider.Feed.Http;
using VitalbetSportsProvider.Models;

namespace VitalbetSportsProvider.ConsoleRunner
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

            sports = FixForeignKeyIds(sports);

            var repo = new SportsRepository();

            repo.AddOrUpdate(sports);
        }

        private static List<Sport> FixForeignKeyIds(List<Sport> sports)
        {
            sports.ForEach(s =>
            {
                s.Events.ForEach(e =>
                {
                    e.SportId = s.Id;
                    e.Matches.ForEach(m =>
                    {
                        m.EventId = e.Id;
                        m.Bets.ForEach(b =>
                        {
                            b.MatchId = m.Id;
                            b.Odds.ForEach(o => o.BetId = b.Id);
                        });
                    });
                });
            });

            return sports;
        }
    }
}
