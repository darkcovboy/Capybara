using Extension;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Loading
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private GameObject _curtain;  // GameObject для занавески
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TextMeshProUGUI _progressText;

        public void SetProgress(float progress)
        {
            _progressBar.value = progress;
            _progressText.text = $"{progress * 100:F0}%";
        }
        
        public void Show()
        {
            _curtain.Activate();
            SetProgress(0);
        }

        public void Hide()
        {
            _curtain.Deactivate();
        }
    }
}