using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SlotMachineSpace
{
    public class RewardElement : MonoBehaviour
    {
        [SerializeField] private Image[] _rewardIcons;
        [SerializeField] private Image _awardsIcon;
        [SerializeField] private TextMeshProUGUI _rewardText;


        public void SetSprites(Sprite sprite) {
            foreach (var icon in _rewardIcons)
                icon.sprite = sprite;
        }

        public void SetAmount(int amount) {
            _rewardText.text = amount.ToString();
        }

        public void SetSpriteAwards(Sprite sprite) {
            _awardsIcon.sprite = sprite;
            _awardsIcon.gameObject.SetActive(true);
        }

        public void Show() {
            gameObject.SetActive(true);
        } 
        
        public void Hide() {
            gameObject.SetActive(false);
        }
    }
}
