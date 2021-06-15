using System.Collections.Generic;
using System;

namespace LeaderboardKata
{
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
}
