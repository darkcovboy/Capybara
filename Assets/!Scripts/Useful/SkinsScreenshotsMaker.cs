using System;
using System.Collections;
using System.Collections.Generic;
using Extension;
using UnityEngine;

namespace DefaultNamespace.Useful
{
    public class SkinsScreenshotsMaker : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _viewModels;

        private void Awake()
        {
            foreach (var viewModel in _viewModels)
            {
                viewModel.Deactivate();
            }
        }

        private void Start()
        {
            StartCoroutine(ShowAllSkins());
        }

        private IEnumerator ShowAllSkins()
        {
            foreach (var viewModel in _viewModels)
            {
                viewModel.Activate();
                yield return new WaitForSeconds(0.1f);
                viewModel.Deactivate();
            }
        }
    }
}