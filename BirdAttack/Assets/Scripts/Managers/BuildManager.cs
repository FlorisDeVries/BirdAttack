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
            _toBuild = value;
            OnSelect.Invoke();
        }
    }

    [HideInInspector]
    public UnityEvent OnSelect = new UnityEvent();

    [HideInInspector]
    public FloatEvent OnErrorBuilding = new FloatEvent();
    [SerializeField]
    private float _errorDuration = .5f;

    private void Start()
    {
        OnSelect.Invoke();
    }

    public Turret BuildTurret(Tile target)
    {
        Turret t = _toBuild.GetComponent<Turret>();
        if (t.Cost <= CurrencyManager.Instance.Bank)
        {
            CurrencyManager.Instance.Pay(t.Cost);
            return Instantiate(_toBuild, target.SpawnPos.position, target.SpawnPos.rotation).GetComponent<Turret>();
        }
        else
        {
            StartCoroutine(target.BlinkRed(_errorDuration));
            OnErrorBuilding.Invoke(_errorDuration);
            return null;
        }
    }
}