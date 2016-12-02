namespace VitalbetSportsProvider.Core
{
    using AutoMapper;
    using VitalbetSportsProvider.Models;
    using VitalbetSportsProvider.Models.Xml;

    public class XmlMappings : Profile
    {
        public XmlMappings()
        {
            this.CreateMap<XmlBet, Bet>();
            this.CreateMap<XmlEvent, Event>();
            this.CreateMap<XmlMatch, Match>()
                .ForMember(dest => dest.MatchType, opts => opts.MapFrom(src => src.MatchType.ToEnum<MatchType>()));
            this.CreateMap<XmlOdds, Odds>();
            this.CreateMap<XmlSport, Sport>();
        }
    }
}
