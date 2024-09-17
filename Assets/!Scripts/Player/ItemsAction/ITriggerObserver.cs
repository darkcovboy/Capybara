using System;
using Items;

namespace Player.ItemsAction
{
    public interface ITriggerObserver<out T>
    {
        event Action<T> Enter;
    }
}