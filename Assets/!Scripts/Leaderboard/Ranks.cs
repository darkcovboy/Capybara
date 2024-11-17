using TMPro;
using UnityEngine;

namespace DefaultNamespace.Leaderboard
{
    public class Ranks : MonoBehaviour
    {
        [SerializeField] private GameObject _firstPlace;
        [SerializeField] private GameObject _secondPlace;
        [SerializeField] private GameObject _thirdPlace;
        [SerializeField] private GameObject _otherPlace;
        [SerializeField] private TextMeshProUGUI _otherPlaceText;

        public void SetRank(int rank)
        {
            _firstPlace.SetActive(false);
            _secondPlace.SetActive(false);
            _thirdPlace.SetActive(false);
            _otherPlace.SetActive(false);
            
            switch (rank)
            {
                case 1:
                    _firstPlace.SetActive(true);
                    break;
                case 2:
                    _secondPlace.SetActive(true);
                    break;
                case 3:
                    _thirdPlace.SetActive(true);
                    break;
                default:
                    _otherPlace.SetActive(true);
                    _otherPlaceText.text = $"{rank}";
                    break;
            }
        }
    }
}