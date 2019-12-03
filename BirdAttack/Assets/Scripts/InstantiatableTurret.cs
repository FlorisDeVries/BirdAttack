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

    float _id = 0;

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
        BuildManager.Instance.OnErrorBuilding.AddListener((x) => StartCoroutine(BlinkRed(x)));
    }

    private void LoseFocus()
    {
        _image.enabled = false;
        BuildManager.Instance.OnSelect.RemoveListener(LoseFocus);
        BuildManager.Instance.OnErrorBuilding.RemoveListener((x) => StartCoroutine(BlinkRed(x)));
    }

    IEnumerator BlinkRed(float duration)
    {
        _id++;
        float value = _id;
        _image.color = ColorManager.Instance.DarkRed;
        yield return new WaitForSeconds(duration);
        if (value == _id)
        {
            _id = value;
            _image.color = Color.white;
        }
    }
}