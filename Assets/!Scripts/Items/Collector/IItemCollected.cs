using System;

namespace Items.Collector
{
    public interface IItemCollected
    {
        event Action<int, int> OnItemCollected;
        int MAXItems { get; }
    }
}