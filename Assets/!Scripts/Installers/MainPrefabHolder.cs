using Items.Collector;
using Player;
using UI.Screens;
using UnityEngine;

namespace Installers
{
    [CreateAssetMenu(fileName = "PrefabHolder", menuName = "Main/PrefabHolder")]
    public class MainPrefabHolder : ScriptableObject
    {
        [field: SerializeField] public CharactersGroupHolder CharactersGroupHolder { get; private set; }
        [field: SerializeField] public ScreensHolder ScreensHolder { get; private set; }
        [field: SerializeField] public ItemCollector ItemCollector { get; private set; }
        
    }
}