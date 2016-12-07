namespace VitalbetSportsProvider.Core.Mappings
{
    using AutoMapper;
    using VitalbetSportsProvider.Models;
    using VitalbetSportsProvider.ViewModels;

    public class ViewModelMappings : Profile
    {
        public ViewModelMappings()
        {
            this.CreateMap<Bet, BetViewModel>();
            this.CreateMap<Event, EventViewModel>();
            this.CreateMap<Match, MatchViewModel>();
            this.CreateMap<Odds, OddsViewModel>();
            this.CreateMap<Sport, SportViewModel>();
        }
    }
}
