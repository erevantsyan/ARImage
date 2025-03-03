using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEvent { }
public interface IBaseEventReciver { }

public interface IEventReciver<T> : IBaseEventReciver where T : struct, IEvent
{
    void OnEvent(T @event);
}
