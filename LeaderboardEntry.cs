using System;

namespace LeaderboardKata
{
    public class LeaderboardEntry<T> where T : IComparable<T>
    {
        public Result<T> Result { get; set; }
        public int Rank { get; set; }
    }
}
