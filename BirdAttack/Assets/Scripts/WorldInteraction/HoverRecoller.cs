using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverRecoller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected Material _originalMat;
    [SerializeField]
    protected Material _hoverMat;

    protected Renderer _renderer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalMat = _renderer.material;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        _renderer.material = _hoverMat;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        _renderer.material = _originalMat;
    }
}
