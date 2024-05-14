using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Polymorphic parent class for all listeners in the game. 
/// </summary>
/// <typeparam name="T">Type of data passed in the OnEventRaised method.</typeparam>
public abstract class GameEventListener<T> : MonoBehaviour
{
    public GameEvent<T> Event;
    public UnityEvent<T> Response;

    protected void OnEnable()
    {
        Event.RegisterListener(this);
    }

    protected void OnDisable()
    {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised(T data)
    {
        Response.Invoke(data);
    }
}
