using System.Collections.Generic;
using UnityEngine;

public class GameEvent<T> : ScriptableObject
{
    private List<GameEventListener<T>> listeners = new List<GameEventListener<T>>();

    public void RegisterListener(GameEventListener<T> listener)
    {
        listeners.Add(listener);
    }

    public void UnRegisterListener(GameEventListener<T> listener)
    {
        listeners.Remove(listener);
    }

    public void Raise(T data)
    {
        foreach (var listener in listeners)
        {
            listener.OnEventRaised(data);
        }
    }
}
