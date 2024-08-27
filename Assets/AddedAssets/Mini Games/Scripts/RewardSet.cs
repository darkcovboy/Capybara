using MiniGames;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Rewards), menuName = "MiniGames/New " + nameof(Rewards))]
public class RewardSet : ScriptableObject
{
    [SerializeField] private List<Reward> _rewards = new();

    public List<Reward> Rewards {
        get {
            List<Reward> clone = new();

            foreach (Reward reward in _rewards)
                clone.Add(reward.Clone());

            return clone;
        }
    }
}
