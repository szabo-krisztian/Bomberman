using UnityEngine;
using UnityEngine.Events;

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
