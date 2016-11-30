using AutoMapper;
using VitalbetSportsProvider.Models;
using VitalbetSportsProvider.Models.Xml;

namespace ConsoleRunner
{
    public class XmlMappings : Profile
    {
        public XmlMappings()
        {
            this.CreateMap<XmlBet, Bet>();
            this.CreateMap<XmlEvent, Event>();
            this.CreateMap<XmlMatch, Match>();
            this.CreateMap<XmlOdds, Odds>();
            this.CreateMap<XmlSport, Sport>();
        }
    }
}
