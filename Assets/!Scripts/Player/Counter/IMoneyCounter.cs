using System;

namespace Player.Counter
{
    public interface IMoneyCounter
    {
        public event Action<int> OnMoneyChanged;
        public int Money { get; }
    }
}