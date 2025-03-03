using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventBus
{
    static Dictionary<Type, List<WeakReference<IBaseEventReciver>>> _resivers;
    static Dictionary<int, WeakReference<IBaseEventReciver>> _reciverHashToReference;

    static EventBus()
    {
        _resivers = new Dictionary<Type, List<WeakReference<IBaseEventReciver>>>();
        _reciverHashToReference = new Dictionary<int, WeakReference<IBaseEventReciver>>();
    }

    public static void Register<T>(IEventReciver<T> reciver) where T : struct, IEvent
    {
        Type eventType = typeof(T);

        if(!_resivers.ContainsKey(eventType))
        {
            _resivers[eventType] = new List<WeakReference<IBaseEventReciver>>();
        }

        WeakReference<IBaseEventReciver> reference = new WeakReference<IBaseEventReciver>(reciver);

        _resivers[eventType].Add(reference);
        _reciverHashToReference[reciver.GetHashCode()] = reference;
    }

    public static void Unregister<T>(IEventReciver<T> reciver) where T : struct, IEvent
    {
        Type eventType = typeof(T);
        int reciverHash = reciver.GetHashCode();
        if (!_resivers.ContainsKey(eventType) || !_reciverHashToReference.ContainsKey(reciverHash)) return;

        WeakReference<IBaseEventReciver> reference = _reciverHashToReference[reciverHash];

        _resivers[eventType].Remove(reference);
        _reciverHashToReference.Remove(reciverHash);
    }

    public static void Raise<T>(T @event) where T : struct, IEvent
    {
        Type eventType = typeof(T);
        if (!_resivers.ContainsKey(eventType)) return;

        for(int i = 0; i < _resivers[eventType].Count; i++)
        {
            if (_resivers[eventType][i].TryGetTarget(out IBaseEventReciver reciver)) ((IEventReciver<T>)reciver).OnEvent(@event);
        }
    }

}
