using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverRecoller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Material _originalMat;
    [SerializeField]
    private Material _hoverMat;

    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalMat = _renderer.material;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _renderer.material = _hoverMat;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _renderer.material = _originalMat;
    }
}
