using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector3GameEvent", menuName = "ScriptableObjects/Vector3GameEvent", order = 3)]
public class Vector3GameEvent : ScriptableObject
{
    private readonly List<Vector3GameEventListener> eventListeners = new List<Vector3GameEventListener>();

    public void Raise(Vector3 value)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(value);
    }

    public void RegisterListener(Vector3GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(Vector3GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
