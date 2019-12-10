using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BoolEvent : UnityEvent<bool> { }

public class TimeManager : AwakeSingleton<TimeManager>
{
    [HideInInspector]
    public BoolEvent OnToggleFastForward = new BoolEvent();

    private bool _fastForward = false;

    public void ToggleFastForward()
    {
        _fastForward = !_fastForward;
        Time.timeScale = _fastForward ? 2 : 1;
        OnToggleFastForward.Invoke(_fastForward);
    }
}