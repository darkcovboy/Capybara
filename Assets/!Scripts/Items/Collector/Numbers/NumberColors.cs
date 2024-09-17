using System.Collections.Generic;
using UnityEngine;

namespace Items.Collector.Numbers
{
    [CreateAssetMenu(fileName = "Colors", menuName = "Numbers/colors")]
    public class NumberColors : ScriptableObject
    {
        [SerializeField] private List<Color> _colors;

        public List<Color> Colors => _colors;
    }
}