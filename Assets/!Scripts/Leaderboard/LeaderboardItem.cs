using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Leaderboard
{
    public class LeaderboardItem : MonoBehaviour
    {
        [SerializeField] private Image _avatar;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private GameObject _backgroundPlayer;
        [SerializeField] private GameObject _otherBackground;
        [SerializeField] private Ranks _ranks;
        [SerializeField] private TextMeshProUGUI _score;
        
        public void Init(LeaderboardData leaderboardData, bool isPlayer)
        {
            _avatar.sprite = leaderboardData.avatar;
            _name.text = leaderboardData.name;
            if (isPlayer)
            {
                _backgroundPlayer.SetActive(true);
                _otherBackground.SetActive(false);
            }
            else
            {
                _backgroundPlayer.SetActive(false);
                _otherBackground.SetActive(true);
            }
            _ranks.SetRank(leaderboardData.rank);
            _score.text = $"{leaderboardData.score}";
        }
    }
}