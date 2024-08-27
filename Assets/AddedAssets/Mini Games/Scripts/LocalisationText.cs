using TMPro;
using UnityEngine;

namespace MiniGames
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalisationText : MonoBehaviour
    {
        [field: SerializeField] public string Key { get; private set; }
        [SerializeField] private LocalisationManager _localisationManager;
        private TextMeshProUGUI _text;


        private void Awake() {
            _text = GetComponent<TextMeshProUGUI>();
        }

        void Start() {
            _localisationManager.Subscribe(this);
        }

        public void SetText(string text) {
            _text.text = text;
        }
    }
}
