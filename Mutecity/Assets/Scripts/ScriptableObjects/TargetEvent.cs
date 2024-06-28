using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TargetEvent : ScriptableObject
{
    private List<TargetEventListener> listeners = new List<TargetEventListener>();

    public void Raise(Vector3 target)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(target);
        }
    }

    public void RegisterListener(TargetEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(TargetEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
