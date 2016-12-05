namespace VitalbetSportsProvider.ViewModels
{
    using System.Collections.Generic;

    public class BetViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public IReadOnlyCollection<OddsViewModel> Odds { get; set; }
    }
}
