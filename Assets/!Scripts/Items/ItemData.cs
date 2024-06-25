using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Data")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public int Reward { get; private set; }
    }
}