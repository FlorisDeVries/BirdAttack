using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : HoverRecoller, IPointerClickHandler
{
    private Turret _turret;
    [SerializeField]
    private Transform _spawnPos;
    public Transform SpawnPos
    {
        get { return _spawnPos; }
    }

    [SerializeField]
    private Material _blinkMat;

    private float _id = 0;

    protected override void Start()
    {
        base.Start();
        _turret = GetComponentInChildren<Turret>();
        if (_turret)
            Destroy(_turret.gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_turret)
        {
            // TODO: Upgrade/sell turret
        }
        else
        {
            if (!BuildManager.Instance.ToBuild)
            {
                Debug.Log("No turret selected to build!"); // TODO: Add alternative placement method
            }
            else
            {
                _turret = BuildManager.Instance.BuildTurret(this);
            }
        }
    }

    public IEnumerator BlinkRed(float duration)
    {
        _id++;
        float value = _id;
        _renderer.material = _blinkMat;
        yield return new WaitForSeconds(duration);
        if (_id == value)
        {
            _id = 0;
            _renderer.material = _originalMat;
        }
    }
}