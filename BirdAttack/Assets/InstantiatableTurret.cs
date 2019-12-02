using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InstantiatableTurret : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    protected GameObject _turret;

    private void Start()
    {
        _image = transform.parent.GetComponent<Image>();
        BuildManager.Instance.OnSelect.AddListener(LoseFocus);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (BuildManager.Instance.ToBuild == _turret)
            return;
        _image.enabled = true;
        BuildManager.Instance.ToBuild = _turret;
        BuildManager.Instance.OnSelect.AddListener(LoseFocus);
    }

    private void LoseFocus()
    {
        _image.enabled = false;
        BuildManager.Instance.OnSelect.RemoveListener(LoseFocus);
    }
}