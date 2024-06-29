using UnityEngine;
using UnityEngine.Events;

public class Vector3GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public Vector3GameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<Vector3> Response;

    private void OnEnable()
    {
        if (Event != null)
        {
            Event.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        if (Event != null)
        {
            Event.UnregisterListener(this);
        }
    }

    public void OnEventRaised(Vector3 value)
    {
        Response.Invoke(value);
    }
}
