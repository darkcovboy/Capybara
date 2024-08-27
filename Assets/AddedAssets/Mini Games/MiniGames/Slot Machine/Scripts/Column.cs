using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SlotMachineSpace
{
    public class Column : MonoBehaviour
    {
        [SerializeField] private Image[] _rowImages;
        [SerializeField] private Transform _roller;
        [SerializeField] private float timeInternal = 0.1f;

        [Header("Audio")]
        [SerializeField] private AudioSource _audio;
        [SerializeField] private Vector2 pitchRange = Vector2.one;
        [SerializeField] private AudioClip _clipSwitch;
        [SerializeField] private AudioClip[] _clipsSelected;

        public bool Rotates { get; private set; }

        private List<Sprite> _sprites;

        private int _index;
        private int _stopIndex;
        private bool _stop;

        private float _startY = -145, _offset = 150;


        public void Init(List<Sprite> sprites) {
            _sprites = sprites;

            UpdateIcons();
        }

        public void StopAtIndex(int index) {
            if (index >= _sprites.Count) { Debug.LogError($"It is impossible to stop at index {index} because there is no reward with such an index."); return; }

            _stopIndex = index;
            _stop = true;
        }

        public void StartRotating() {
            StartCoroutine(RotateRoutine());
            _stop = false;
        }

        private IEnumerator RotateRoutine() {
            Rotates = true;
            _audio.clip = _clipSwitch;

            Vector3 startP = Vector3.up * _startY;
            Vector3 endtP = startP - Vector3.up * _offset;

            while (true) {
                for (float t = 0; t < 1; t += Time.deltaTime / timeInternal) {
                    _roller.localPosition = Vector3.Lerp(startP, endtP, t);

                    yield return null;
                }

                _index = GetNextIndex(_index);
                UpdateIcons();

                _roller.localPosition = startP;

                _audio.pitch = Random.Range(pitchRange.x, pitchRange.y);
                _audio.Play();

                if (_stop && _index == _stopIndex) {
                    Rotates = false;

                    _audio.pitch = 1;
                    _audio.clip = _clipsSelected[Random.Range(0, _clipsSelected.Length)];
                    _audio.Play();

                    yield break;
                }
            }
        }

        private void UpdateIcons() {
            _rowImages[0].sprite = _sprites[GetPreviousIndex(_index)];
            _rowImages[1].sprite = _sprites[_index];
            _rowImages[2].sprite = _sprites[GetNextIndex(_index)];
            _rowImages[3].sprite = _sprites[GetNextIndex(GetNextIndex(_index))];
        }

        private int GetNextIndex(int index) {
            if (index == _sprites.Count - 1)
                return 0;
            else
                return index + 1;
        }
        private int GetPreviousIndex(int index) {
            if (index == 0)
                return _sprites.Count - 1;
            else
                return index - 1;
        }

        private void OnDisable() {
            StopAllCoroutines();
            Rotates = false;
            _roller.localPosition = Vector3.up * _startY;
        }
    }
}