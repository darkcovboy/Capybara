using UnityEngine;

namespace MiniGames
{
    public abstract class MiniGame : MonoBehaviour
    {
        [SerializeField] private GameObject _miniGameObj;

        [SerializeField] protected CategoryIcon _coinIcon;
        [SerializeField] protected int _paidStartPrice = 0;
        [SerializeField] protected bool _randomOrder;
        
        [SerializeField] protected RewardSet _rewardSet;
        [SerializeField] protected bool _useOneCurrency;
        [SerializeField] protected int[] _amounts = new int[5];


        public abstract void Init();

        public virtual void Open() {
            _miniGameObj.SetActive(true);
        }

        public virtual void Hide() {
            _miniGameObj.SetActive(false);
        }
    }
}
