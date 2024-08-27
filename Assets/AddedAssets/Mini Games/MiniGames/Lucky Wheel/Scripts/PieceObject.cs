using UnityEngine;
using UnityEngine.UI;
using MiniGames;
using TMPro;

namespace FortuneWheel
{
    public class PieceObject : MonoBehaviour
    {
        public Image backgroundImage;
        public Image rewardIcon;
        public TextMeshProUGUI rewardAmount;


        public void SetValues(PieceFeatures features, Reward reward) {
            backgroundImage.color = features.BackgroundColor;
            backgroundImage.sprite = features.BackgroundSprie;

            rewardAmount.text = reward.CurrentAmount.ToString();
            rewardIcon.sprite = reward.Icons.Sprite;
        }

        public void SetValues(PieceFeatures features, Sprite sprite, int amount) {
            backgroundImage.color = features.BackgroundColor;
            backgroundImage.sprite = features.BackgroundSprie;

            rewardAmount.text = amount.ToString();
            rewardIcon.sprite = sprite;
        }
    }
}