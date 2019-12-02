using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BuildManager : AwakeSingleton<BuildManager>
{
    private GameObject _toBuild;
    public GameObject ToBuild
    {
        get { return _toBuild; }
        set
        {
            OnSelect.Invoke();
            _toBuild = value;
        }
    }

    [HideInInspector]
    public UnityEvent OnSelect = new UnityEvent();

    private void Start() {
        OnSelect.Invoke();
    }
}