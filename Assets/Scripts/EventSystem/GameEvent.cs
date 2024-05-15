using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents a custom event system. We can easily raise events and handle them with the help of Unity Scriptable Objects.
/// </summary>
/// <typeparam name="T">You can send any type of objects through the event call. The type of the event is represented as T</typeparam>
public class GameEvent<T> : ScriptableObject
{
    private List<GameEventListener<T>> listeners = new List<GameEventListener<T>>();

    /// <summary>
    /// Listeners can register to events.
    /// </summary>
    /// <param name="listener">Any object that wants to get notification when certain event is raised. Each listener implements a method called OnEventRaised, then polymorphism comes into play with the help of inheritance.</param>
    public void RegisterListener(GameEventListener<T> listener)
    {
        listeners.Add(listener);
    }


    /// <summary>
    /// Listeners can unregister from events.
    /// </summary>
    /// <param name="listener">Any object that wants to get notification when certain event is raised. Each listener implements a method called OnEventRaised, then polymorphism comes into play with the help of inheritance.</param>
    public void UnRegisterListener(GameEventListener<T> listener)
    {
        listeners.Remove(listener);
    }


    /// <summary>
    /// Notifying all listeners.
    /// </summary>
    /// <param name="data">This is passed to all listeners.</param>
    public void Raise(T data)
    {
        foreach (var listener in listeners)
        {
            listener.OnEventRaised(data);
        }
    }
}
