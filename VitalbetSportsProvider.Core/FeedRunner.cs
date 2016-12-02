﻿namespace VitalbetSportsProvider.Core
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
    using WebClient.Core.Comparers;

    /// <summary>
    /// It is backgound task in the WebClient, but ideally has to be different service.
    /// </summary>
    public class FeedRunner : IFeedRunner
    {
        private const int ExecIntervalSeconds = 5;

        private readonly IMapper _mapper;
        private readonly IVitalbetSportsRepository _vitalbetSportsRepository;
        private readonly ISportsRepository _sportsRepository;
        private readonly IEqualityComparer<Bet> _betsComparer;
        private readonly IEqualityComparer<Event> _eventsComparer;
        private readonly IEqualityComparer<Match> _matchesComparer;
        private readonly IEqualityComparer<Odds> _oddsComparer;
        private readonly IEqualityComparer<Sport> _sportsComparer;

        private CancellationTokenSource _tokenSource;

        private IList<Sport> _prevSportsState;

        public FeedRunner(
            IMapper mapper, 
            IVitalbetSportsRepository vitalbetSportsRepository, 
            ISportsRepository sportsRepository,
            IEqualityComparer<Bet> betsComparer,
            IEqualityComparer<Event> eventsComparer,
            IEqualityComparer<Match> matchesComparer,
            IEqualityComparer<Odds> oddsComparer,
            IEqualityComparer<Sport> sportsComparer)
        {
            this._mapper = mapper;
            this._vitalbetSportsRepository = vitalbetSportsRepository;
            this._sportsRepository = sportsRepository;
            this._betsComparer = betsComparer;
            this._eventsComparer = eventsComparer;
            this._matchesComparer = matchesComparer;
            this._oddsComparer = oddsComparer;
            this._sportsComparer = sportsComparer;
            
            this._tokenSource = new CancellationTokenSource();
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

                    var newSportsState = await this.LoadSports();
                    if (this._prevSportsState == null)
                    {
                        await this._sportsRepository.AddOrUpdateAsync(newSportsState);
                        this._prevSportsState = newSportsState;
                    }
                    else
                    {
                        var newEvents = newSportsState.SelectMany(s => s.Events).ToList();
                        var newMatches = newEvents.SelectMany(s => s.Matches).ToList();
                        var newBets = newMatches.SelectMany(s => s.Bets).ToList();
                        var newOdds = newBets.SelectMany(s => s.Odds).ToList();

                        var prevEvents = this._prevSportsState.SelectMany(s => s.Events).ToList();
                        var prevMatches = prevEvents.SelectMany(s => s.Matches).ToList();
                        var prevBets = prevMatches.SelectMany(s => s.Bets).ToList();
                        var prevOdds = prevBets.SelectMany(s => s.Odds).ToList();

                        var addedOrUpdatedSports = newSportsState.Except(this._prevSportsState, this._sportsComparer).ToList();
                        var addedOrUpdatedEvents = newEvents.Except(prevEvents, this._eventsComparer).ToList();
                        var addedOrUpdatedMatches = newMatches.Except(prevMatches, this._matchesComparer).ToList();
                        var addedOrUpdatedBets = newBets.Except(prevBets, this._betsComparer).ToList();
                        var addedOrUpdatedOdds = newOdds.Except(prevOdds, this._oddsComparer).ToList();
                        
                        this._prevSportsState = newSportsState;
                    }

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

        private async Task<IList<Sport>> LoadSports()
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