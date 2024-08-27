using UnityEngine;

namespace MiniGames
{
    [System.Serializable]
    public class Reward
    {
        public RewardEnum Type;
        public CategoryIcon Icons;
        [Space]
        public int[] Amounts;

        public int CurrentAmount {
            get {
                if (_currentAmount == 0)
                    ResetCurrentAmount();

                return _currentAmount;
            }
        }
        private int _currentAmount;


        public void ResetCurrentAmount() {
            int old = _currentAmount;
            _currentAmount = Amounts[Random.Range(0, Amounts.Length)];
        }

        public Reward Clone() {
            Reward clone = new Reward();

            clone.Type = Type;
            clone.Icons = Icons;
            clone.Amounts = Amounts;
            clone._currentAmount = _currentAmount;

            return clone;
        }
    }
}
