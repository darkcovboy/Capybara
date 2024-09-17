using System;
using SaveSystem;

namespace Player.Counter
{
    public class MoneyCounter : IMoneyCounter
    {
        public event Action<int> OnMoneyChanged;
        public int Money { get; private set; }

        public int LevelCollectedMoney { get; private set; } 

        public MoneyCounter(SaveManager saveManager)
        {
            Money = saveManager.PlayerData.Money;
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
            
            OnMoneyChanged?.Invoke(Money);
        }

        public void AddByReward(int value)
        {
            if(value <= 0)
                throw new ArgumentException(nameof(value));

            Money += value;
            OnMoneyChanged?.Invoke(Money);
        }

        public bool TrySpendMoney(int value)
        {
            if(value <= 0)
                throw new ArgumentException(nameof(value));

            if (HaveMoney(value))
            {
                Money -= value;
                OnMoneyChanged?.Invoke(Money);
                return true;
            }

            return false;
        }
    }
}