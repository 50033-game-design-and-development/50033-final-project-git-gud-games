using System.Collections.Generic;
using UnityEngine;

public class AbstractEvent<T> : ScriptableObject {
    private readonly List<AbstractEventListener<T>> _listeners = new();

    public void Raise(T data) {
        for (int i = _listeners.Count - 1; i >= 0; --i) {
            _listeners[i].OnRaised(data);
        }
    }

    public void AddListener(AbstractEventListener<T> listener) {
        if (!_listeners.Contains(listener)) {
            _listeners.Add(listener);
        }
    }

    public void RemoveListener(AbstractEventListener<T> listener) {
        _listeners.Remove(listener);
    }
}

public class AbstractEvent : ScriptableObject {
    private readonly List<AbstractEventListener> _listeners = new();

    public void Raise() {
        for (int i = _listeners.Count - 1; i >= 0; --i) {
            _listeners[i].OnRaised();
        }
    }

    public void AddListener(AbstractEventListener listener) {
        if (!_listeners.Contains(listener)) {
            _listeners.Add(listener);
        }
    }

    public void RemoveListener(AbstractEventListener listener) {
        _listeners.Remove(listener);
    }
}
