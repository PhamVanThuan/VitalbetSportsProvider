using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VitalbetSportsProvider.DataModel.Interfaces;
using VitalbetSportsProvider.Feed.Interfaces;
using VitalbetSportsProvider.Models;

namespace VitalbetSportsProvider.WebClient.Core
{
    /// <summary>
    /// It is backgound task in the WebClient, but ideally has to be different service.
    /// </summary>
    public class FeedRunner : IFeedRunner
    {
        private const int ExecIntervalSeconds = 60;

        private readonly IMapper _mapper;
        private readonly IVitalbetSportsRepository _vitalbetSportsRepository;
        private readonly ISportsRepository _sportsRepository;

        private CancellationTokenSource _tokenSource;

        public FeedRunner(IMapper mapper, IVitalbetSportsRepository vitalbetSportsRepository, ISportsRepository sportsRepository)
        {
            this._mapper = mapper;
            this._vitalbetSportsRepository = vitalbetSportsRepository;
            this._sportsRepository = sportsRepository;
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

                    var sports = await LoadSports();
                    await this._sportsRepository.AddOrUpdateAsync(sports);

                    var delayMs = ExecIntervalSeconds * 1000 - sw.ElapsedMilliseconds;
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
                .Select(FixForeignKeyIds)
                .ToList();
        }

        private static Sport FixForeignKeyIds(Sport s)
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
