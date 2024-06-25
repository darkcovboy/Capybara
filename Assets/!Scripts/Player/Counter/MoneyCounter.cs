using System;

namespace Player.Counter
{
    public class MoneyCounter
    {
        public int Money { get; private set; }

        public int LevelCollectedMoney { get; private set; } 

        public MoneyCounter(int startMoney)
        {
            Money = startMoney;
            LevelCollectedMoney = 0;
        }

        public bool HaveMoney(int value)
        {
            if(value <= 0)
                throw new ArgumentException(nameof(value));

            return Money >= value;
        }

        public void Add(int value, bool isReward = false)
        {
            if(value <= 0)
                throw new ArgumentException(nameof(value));

            if (!isReward)
                LevelCollectedMoney += value;
            else
                Money += value;
        }

        public void AddByReward(int value)
        {
            if(value <= 0)
                throw new ArgumentException(nameof(value));

            Money += value;
        }

        public void TakeMoney(int value)
        {
            if(value <= 0)
                throw new ArgumentException(nameof(value));

            Money -= value;
        }
    }
}