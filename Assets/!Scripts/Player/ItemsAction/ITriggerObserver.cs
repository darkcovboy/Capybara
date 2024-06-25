using System;
using Items;

namespace Player.ItemsAction
{
    public interface ITriggerObserver<T>
    {
        event Action<T> Enter;
    }
}