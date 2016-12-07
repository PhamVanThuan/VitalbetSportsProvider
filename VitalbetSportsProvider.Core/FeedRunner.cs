namespace VitalbetSportsProvider.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using VitalbetSportsProvider.DataModel.Interfaces;
    using VitalbetSportsProvider.Feed.Interfaces;
    using VitalbetSportsProvider.Models;
    using VitalbetSportsProvider.ViewModels;

    /// <summary>
    /// It is backgound task in the WebClient, but ideally has to be different service.
    /// </summary>
    public class FeedRunner : IFeedRunner
    {
        private const int ExecIntervalSeconds = 5;

        private readonly CancellationTokenSource _tokenSource;
        private readonly IMapper _mapper;
        private readonly IVitalbetSportsRepository _vitalbetSportsRepository;
        private readonly ISportsRepository _sportsRepository;
        private readonly IEqualityComparer<Bet> _betsComparer;
        private readonly IEqualityComparer<Event> _eventsComparer;
        private readonly IEqualityComparer<Match> _matchesComparer;
        private readonly IEqualityComparer<Odds> _oddsComparer;
        private readonly IEqualityComparer<Sport> _sportsComparer;

        private readonly IUpdatableServiceInvoke<BetViewModel> _betsUpdatableService;
        
        private IReadOnlyCollection<Sport> _prevSports;
        private IReadOnlyCollection<Event> _prevEvents;
        private IReadOnlyCollection<Match> _prevMatches;
        private IReadOnlyCollection<Bet> _prevBets;
        private IReadOnlyCollection<Odds> _prevOdds;

        public FeedRunner(
            IMapper mapper, 
            IVitalbetSportsRepository vitalbetSportsRepository, 
            ISportsRepository sportsRepository,
            IEqualityComparer<Bet> betsComparer,
            IEqualityComparer<Event> eventsComparer,
            IEqualityComparer<Match> matchesComparer,
            IEqualityComparer<Odds> oddsComparer,
            IEqualityComparer<Sport> sportsComparer,
            IUpdatableServiceInvoke<BetViewModel> betsUpdatableService)
        {
            this._tokenSource = new CancellationTokenSource();
            this._mapper = mapper;
            this._vitalbetSportsRepository = vitalbetSportsRepository;
            this._sportsRepository = sportsRepository;
            this._betsComparer = betsComparer;
            this._eventsComparer = eventsComparer;
            this._matchesComparer = matchesComparer;
            this._oddsComparer = oddsComparer;
            this._sportsComparer = sportsComparer;
            this._betsUpdatableService = betsUpdatableService;
        }

        public void Start()
        {
            Task.Factory.StartNew(async () => await this.FeedLoader(), this._tokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }

        public void Stop()
        {
            this._tokenSource.Cancel();
        }

        private async Task FeedLoader()
        {
            while (true)
            {
                try
                {
                    var sw = Stopwatch.StartNew();
                    if (this._tokenSource.Token.IsCancellationRequested)
                    {
                        this._tokenSource.Token.ThrowIfCancellationRequested();
                        break;
                    }

                    await this.StoreSports();

                    var delayMs = (ExecIntervalSeconds * 1000) - sw.ElapsedMilliseconds;
                    if (delayMs > 0)
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(delayMs));
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                }
            }
        }

        private async Task StoreSports()
        {
            var newSports = await this.LoadSports();
            var newEvents = newSports.SelectMany(s => s.Events).ToList();
            var newMatches = newEvents.SelectMany(s => s.Matches).ToList();
            var newBets = newMatches.SelectMany(s => s.Bets).ToList();
            var newOdds = newBets.SelectMany(s => s.Odds).ToList();

            if (this._prevSports == null)
            {
                await this._sportsRepository.AddOrUpdateAsync(newSports, newEvents, newMatches, newBets, newOdds);
            }
            else
            {
                var addedOrUpdatedSports = newSports.Except(this._prevSports, this._sportsComparer).ToList();
                var addedOrUpdatedEvents = newEvents.Except(this._prevEvents, this._eventsComparer).ToList();
                var addedOrUpdatedMatches = newMatches.Except(this._prevMatches, this._matchesComparer).ToList();
                var addedOrUpdatedBets = newBets.Except(this._prevBets, this._betsComparer).ToList();
                var addedOrUpdatedOdds = newOdds.Except(this._prevOdds, this._oddsComparer).ToList();

                var betsToLoad = new HashSet<int>(addedOrUpdatedOdds.Select(o => o.BetId));
                var addedOrUpdatedBetsWithOdds = this._prevBets
                    .Where(b => betsToLoad.Contains(b.Id))
                   .GroupJoin(addedOrUpdatedOdds, b => b.Id, o => o.BetId, (bet, oddsList) =>
                       new BetViewModel
                       {
                           Id = bet.Id,
                           Name = bet.Name,
                           Odds = oddsList.Select(o => new OddsViewModel
                           {
                               Id = o.Id,
                               Name = o.Name,
                               Value = o.Value
                           })
                           .ToList()
                       })
                   .ToList();

                this._betsUpdatableService.AddOrUpdateInvoke(addedOrUpdatedBetsWithOdds);

                await this._sportsRepository.AddOrUpdateAsync(addedOrUpdatedSports, addedOrUpdatedEvents, addedOrUpdatedMatches, addedOrUpdatedBets, addedOrUpdatedOdds);
            }

            this._prevSports = newSports;
            this._prevEvents = newEvents;
            this._prevMatches = newMatches;
            this._prevBets = newBets;
            this._prevOdds = newOdds;
        }

        private async Task<IReadOnlyCollection<Sport>> LoadSports()
        {
            var xml = await this._vitalbetSportsRepository.RequestSportsAsync();
            return xml.Sports
                .Select(this._mapper.Map<Sport>)
                .Select(this.FixForeignKeyIds)
                .ToList();
        }

        private Sport FixForeignKeyIds(Sport s)
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

            return s;
        }
    }
}
