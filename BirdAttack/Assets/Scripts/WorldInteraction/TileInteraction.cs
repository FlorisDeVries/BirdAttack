using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileInteraction : HoverRecoller, IPointerClickHandler
{
    private Turret _turret;

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

        }
        else
        {
            if (!BuildManager.Instance.ToBuild)
            {
                Debug.Log("No turret selected to build!"); // Maybe enable placement via radial menu :D?
            }
            else
            {
                SpawnTurret(BuildManager.Instance.ToBuild);
            }
        }
    }

    private void SpawnTurret(GameObject turretPrefab)
    {
        _turret = Instantiate(turretPrefab, transform.position + new Vector3(0, .5f, 0), transform.rotation).GetComponent<Turret>();
    }
}