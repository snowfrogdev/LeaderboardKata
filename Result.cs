using System;

namespace LeaderboardKata
{
    public class Result<T> where T : IComparable<T>
    {
        public string Name { get; set; }
        public T Score { get; set; }
    }
}
