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

            var service = new LeaderboardService();

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

            var service = new LeaderboardService();

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

            var service = LeaderboardService.ForLowScore();

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

            var service = LeaderboardService.ForLowScore();

            var actual = service.GenerateLeaderboarEntries(input);

            actual.Should().BeEquivalentTo(expected);
        }
    }

    public class NonsensicalScore : IComparable<NonsensicalScore>, IEquatable<NonsensicalScore>
    {
        private NonsensicalScore() { }

        public string Value { get; private set; }

        public static NonsensicalScore Awesome => new NonsensicalScore { Value = "Awesome" };
        public static NonsensicalScore Great => new NonsensicalScore { Value = "Great" };
        public static NonsensicalScore Good => new NonsensicalScore { Value = "Good" };
        public static NonsensicalScore Meh => new NonsensicalScore { Value = "Meh" };
        public static NonsensicalScore Bad => new NonsensicalScore { Value = "Bad" };
        public static NonsensicalScore OMG => new NonsensicalScore { Value = "OMG" };
        public static NonsensicalScore ICantEven => new NonsensicalScore { Value = "I Can't Even" };

        private static readonly Dictionary<string, int> ScoreMap = new()
        {
            ["I Can't Even"] = 0,
            ["OMG"] = 1,
            ["Bad"] = 2,
            ["Meh"] = 3,
            ["Good"] = 4,
            ["Great"] = 5,
            ["Awesome"] = 6,
        };

        public int CompareTo(NonsensicalScore other)
        {
            return ScoreMap[Value] - ScoreMap[other.Value];
        }

        public bool Equals(NonsensicalScore other)
        {
            return CompareTo(other) == 0;
        }

        public static bool operator <(NonsensicalScore left, NonsensicalScore right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(NonsensicalScore left, NonsensicalScore right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(NonsensicalScore left, NonsensicalScore right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(NonsensicalScore left, NonsensicalScore right)
        {
            return left.CompareTo(right) >= 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NonsensicalScore);
        }
    }

    public class Result<T> where T : IComparable<T>
    {
        public string Name { get; set; }
        public T Score { get; set; }
    }

    public class LeaderboardEntry<T> where T : IComparable<T>
    {
        public Result<T> Result { get; set; }
        public int Rank { get; set; }
    }

    public class LeaderboardService
    {
        private bool IsLowScore { get; set; }

        public static LeaderboardService ForLowScore()
        {
            return new LeaderboardService { IsLowScore = true };
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
            return IsLowScore ? results.OrderBy(result => result.Score) : results.OrderByDescending(result => result.Score);
        }
    }
}
