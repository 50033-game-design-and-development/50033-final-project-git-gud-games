using UnityEngine;
using UnityEngine.Events;

public class AbstractEventListener<T1, T2> : MonoBehaviour {
    public AbstractEvent<T1, T2> @event;
    public UnityEvent<T1, T2> response;

    public void OnRaised(T1 data1, T2 data2) {
        response.Invoke(data1, data2);
    }

    private void OnEnable() {
        @event.AddListener(this);
    }

    private void OnDisable() {
        @event.RemoveListener(this);
    }
}

public class AbstractEventListener<T> : MonoBehaviour {
    public AbstractEvent<T> @event;
    public UnityEvent<T> response;

    public void OnRaised(T data) {
        response.Invoke(data);
    }

    private void OnEnable() {
        @event.AddListener(this);
    }

    private void OnDisable() {
        @event.RemoveListener(this);
    }
}

public class AbstractEventListener : MonoBehaviour {
    public AbstractEvent @event;
    public UnityEvent response;

    public void OnRaised() {
        response.Invoke();
    }

    private void OnEnable() {
        @event.AddListener(this);
    }

    private void OnDisable() {
        @event.RemoveListener(this);
    }
}