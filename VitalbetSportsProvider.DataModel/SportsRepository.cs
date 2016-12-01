﻿using System.Collections.Generic;
using System.Linq;
using VitalbetSportsProvider.Models;

namespace VitalbetSportsProvider.DataModel
{
    public class SportsRepository
    {
        public void AddOrUpdate(IList<Sport> sports)
        {
            var events = sports.SelectMany(s => s.Events).ToList();
            var matches = events.SelectMany(s => s.Matches).ToList();
            var bets = matches.SelectMany(s => s.Bets).ToList();
            var odds = bets.SelectMany(s => s.Odds).ToList();

            using (var context = new SportsContext())
            {
                context.BulkMerge(sports);
                context.BulkMerge(events);
                context.BulkMerge(matches);
                context.BulkMerge(bets);
                context.BulkMerge(odds);

                context.BulkSaveChanges();
            }
        }
    }
}