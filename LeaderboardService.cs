using System.Collections.Generic;
using System.Linq;
using System;

namespace LeaderboardKata
{
    public class LeaderboardService
    {
        private bool IsLowScoreBest { get; set; }

        private LeaderboardService() {}

        public static LeaderboardService ForLowScoreBest()
        {
            return new LeaderboardService { IsLowScoreBest = true };
        }

        public static LeaderboardService ForHighScoreBest()
        {
            return new LeaderboardService { IsLowScoreBest = false };
        }

        public IEnumerable<LeaderboardEntry<T>> GenerateLeaderboarEntries<T> (List<Result<T>> results) where T : IComparable<T>
        {
            var sortedResults = SortResults(results);

            var entries = new List<LeaderboardEntry<T>>();

            int count = 0;
            T previousScore = default;
            int previousRank = 0;

            foreach (var result in sortedResults)
            {
                count++;

                var rank = previousScore is null || !result.Score.Equals(previousScore) ? count : previousRank;
                var entry = new LeaderboardEntry<T> { Result = result, Rank = rank };
                entries.Add(entry);

                previousScore = result.Score;
                previousRank = rank;
            }

            return entries;
        }

        private IOrderedEnumerable<Result<T>> SortResults<T>(List<Result<T>> results) where T : IComparable<T>
        {
            return IsLowScoreBest ? results.OrderBy(result => result.Score) : results.OrderByDescending(result => result.Score);
        }
    }
}
