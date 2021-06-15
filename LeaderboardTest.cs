using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using System;

namespace LeaderboardKata
{
    public class LeaderboardTest
    {
        [Fact]
        public void Highscore_no_duplicates()
        {
            var input = new List<Result>
            {
                new Result { Name = "Vader", Score = 12 },
                new Result { Name = "Luke", Score = 15 },
                new Result { Name = "Palpatine", Score = 5 },
                new Result { Name = "Chewie", Score = 9 },
                new Result { Name = "C3PO", Score = 2 },
            };


            var expected = new List<LeaderboardEntry>
            {
                new LeaderboardEntry { Result = new Result { Name = "Luke", Score = 15 }, Rank = 1 },
                new LeaderboardEntry { Result = new Result { Name = "Vader", Score = 12 }, Rank = 2 },
                new LeaderboardEntry { Result = new Result { Name = "Chewie", Score = 9 }, Rank = 3 },
                new LeaderboardEntry { Result = new Result { Name = "Palpatine", Score = 5 }, Rank = 4 },
                new LeaderboardEntry { Result = new Result { Name = "C3PO", Score = 2 }, Rank = 5 },
            };

            var service = new LeaderboardService();

            var actual = service.GenerateLeaderboarEntries(input);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Highscore_with_duplicates()
        {
            var input = new List<Result>
            {
                new Result { Name = "Vader", Score = 9 },
                new Result { Name = "Luke", Score = 15 },
                new Result { Name = "Palpatine", Score = 5 },
                new Result { Name = "Chewie", Score = 9 },
                new Result { Name = "C3PO", Score = 2 },
            };


            var expected = new List<LeaderboardEntry>
            {
                new LeaderboardEntry { Result = new Result { Name = "Luke", Score = 15 }, Rank = 1 },
                new LeaderboardEntry { Result = new Result { Name = "Vader", Score = 9 }, Rank = 2 },
                new LeaderboardEntry { Result = new Result { Name = "Chewie", Score = 9 }, Rank = 2 },
                new LeaderboardEntry { Result = new Result { Name = "Palpatine", Score = 5 }, Rank = 4 },
                new LeaderboardEntry { Result = new Result { Name = "C3PO", Score = 2 }, Rank = 5 },
            };

            var service = new LeaderboardService();

            var actual = service.GenerateLeaderboarEntries(input);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Lowscore_with_duplicates()
        {
            var input = new List<Result>
            {
                new Result { Name = "Vader", Score = 84 },
                new Result { Name = "Luke", Score = 72 },
                new Result { Name = "Palpatine", Score = 67 },
                new Result { Name = "Chewie", Score = 84 },
                new Result { Name = "C3PO", Score = 120 },
            };


            var expected = new List<LeaderboardEntry>
            {
                new LeaderboardEntry { Result = new Result { Name = "Palpatine", Score = 67 }, Rank = 1 },
                new LeaderboardEntry { Result = new Result { Name = "Luke", Score = 72 }, Rank = 2 },
                new LeaderboardEntry { Result = new Result { Name = "Vader", Score = 84 }, Rank = 3 },
                new LeaderboardEntry { Result = new Result { Name = "Chewie", Score = 84 }, Rank = 3 },
                new LeaderboardEntry { Result = new Result { Name = "C3PO", Score = 120 }, Rank = 5 },
            };

            var service = LeaderboardService.ForLowScore();

            var actual = service.GenerateLeaderboarEntries(input);

            actual.Should().BeEquivalentTo(expected);
        }
    }

    public class Result
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }

    public class LeaderboardEntry
    {
        public Result Result { get; set; }
        public int Rank { get; set; }
    }

    public class LeaderboardService
    {
        private bool IsLowScore { get; set; }

        public static LeaderboardService ForLowScore()
        {
            return new LeaderboardService { IsLowScore = true };
        }

        public IEnumerable<LeaderboardEntry> GenerateLeaderboarEntries(List<Result> results)
        {
            var sortedResults = SortResults(results);

            var entries = new List<LeaderboardEntry>();

            int count = 0;
            int? previousScore = null;
            int previousRank = 0;

            foreach (var result in sortedResults)
            {
                count++;

                var rank = result.Score != previousScore ? count : previousRank;
                var entry = new LeaderboardEntry { Result = result, Rank = rank };
                entries.Add(entry);

                previousScore = result.Score;
                previousRank = rank;
            }

            return entries;
        }

        private IOrderedEnumerable<Result> SortResults(List<Result> results)
        {
            return IsLowScore ? results.OrderBy(result => result.Score) : results.OrderByDescending(result => result.Score);
        }
    }
}
