using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    public class LocalisationText : MonoBehaviour
    {
        [SerializeField] private string _key;

        private void Start()
        {
            SetLocalisation();
        }

        private void SetLocalisation()
        {
            if (TryGetComponent(out TextMeshProUGUI textMeshProUGUI))
                textMeshProUGUI.text = YandexMain.Instance.LocalisationManager.GetText(_key);
            else if (TryGetComponent(out Text textLegacy))
                textLegacy.text = YandexMain.Instance.LocalisationManager.GetText(_key);
        }
    }
}