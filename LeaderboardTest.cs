using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace LeaderboardKata
{
    public class LeaderboardTest
    {
        [Fact]
        public void Highscore_no_duplicates()
        {
            var input = new List<Result<int>>
            {
                new Result<int> { Name = "Vader", Score = 12 },
                new Result<int> { Name = "Luke", Score = 15 },
                new Result<int> { Name = "Palpatine", Score = 5 },
                new Result<int> { Name = "Chewie", Score = 9 },
                new Result<int> { Name = "C3PO", Score = 2 },
            };


            var expected = new List<LeaderboardEntry<int>>
            {
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "Luke", Score = 15 }, Rank = 1 },
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "Vader", Score = 12 }, Rank = 2 },
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "Chewie", Score = 9 }, Rank = 3 },
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "Palpatine", Score = 5 }, Rank = 4 },
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "C3PO", Score = 2 }, Rank = 5 },
            };

            var service = LeaderboardService.ForHighScoreBest();

            var actual = service.GenerateLeaderboarEntries(input);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Highscore_with_duplicates()
        {
            var input = new List<Result<int>>
            {
                new Result<int> { Name = "Vader", Score = 9 },
                new Result<int> { Name = "Luke", Score = 15 },
                new Result<int> { Name = "Palpatine", Score = 5 },
                new Result<int> { Name = "Chewie", Score = 9 },
                new Result<int> { Name = "C3PO", Score = 2 },
            };


            var expected = new List<LeaderboardEntry<int>>
            {
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "Luke", Score = 15 }, Rank = 1 },
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "Vader", Score = 9 }, Rank = 2 },
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "Chewie", Score = 9 }, Rank = 2 },
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "Palpatine", Score = 5 }, Rank = 4 },
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "C3PO", Score = 2 }, Rank = 5 },
            };

            var service = LeaderboardService.ForHighScoreBest();

            var actual = service.GenerateLeaderboarEntries(input);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Lowscore_with_duplicates()
        {
            var input = new List<Result<int>>
            {
                new Result<int> { Name = "Vader", Score = 84 },
                new Result<int> { Name = "Luke", Score = 72 },
                new Result<int> { Name = "Palpatine", Score = 67 },
                new Result<int> { Name = "Chewie", Score = 84 },
                new Result<int> { Name = "C3PO", Score = 120 },
            };


            var expected = new List<LeaderboardEntry<int>>
            {
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "Palpatine", Score = 67 }, Rank = 1 },
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "Luke", Score = 72 }, Rank = 2 },
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "Vader", Score = 84 }, Rank = 3 },
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "Chewie", Score = 84 }, Rank = 3 },
                new LeaderboardEntry<int> { Result = new Result<int> { Name = "C3PO", Score = 120 }, Rank = 5 },
            };

            var service = LeaderboardService.ForLowScoreBest();

            var actual = service.GenerateLeaderboarEntries(input);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Lowscore_with_duplicates_and_IComparable_score_type()
        {
            var input = new List<Result<NonsensicalScore>>
            {
                new Result<NonsensicalScore> { Name = "Vader", Score = NonsensicalScore.Bad },
                new Result<NonsensicalScore> { Name = "Luke", Score = NonsensicalScore.Awesome },
                new Result<NonsensicalScore> { Name = "Palpatine", Score = NonsensicalScore.Bad },
                new Result<NonsensicalScore> { Name = "Chewie", Score = NonsensicalScore.Meh },
                new Result<NonsensicalScore> { Name = "C3PO", Score = NonsensicalScore.ICantEven },
            };


            var expected = new List<LeaderboardEntry<NonsensicalScore>>
            {
                new LeaderboardEntry<NonsensicalScore> { Result = new Result<NonsensicalScore> { Name = "C3PO", Score = NonsensicalScore.ICantEven }, Rank = 1 },
                new LeaderboardEntry<NonsensicalScore> { Result = new Result<NonsensicalScore> { Name = "Vader", Score = NonsensicalScore.Bad }, Rank = 2 },
                new LeaderboardEntry<NonsensicalScore> { Result = new Result<NonsensicalScore> { Name = "Palpatine", Score = NonsensicalScore.Bad }, Rank = 2 },
                new LeaderboardEntry<NonsensicalScore> { Result = new Result<NonsensicalScore> { Name = "Chewie", Score = NonsensicalScore.Meh }, Rank = 4 },
                new LeaderboardEntry<NonsensicalScore> { Result = new Result<NonsensicalScore> { Name = "Luke", Score = NonsensicalScore.Awesome }, Rank = 5 },
            };

            var service = LeaderboardService.ForLowScoreBest();

            var actual = service.GenerateLeaderboarEntries(input);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
