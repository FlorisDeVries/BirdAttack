using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FastForwardButton : MonoBehaviour, IPointerClickHandler
{
    private Image _image;
    private Shadow _shadow;
    private Color _imageColor, _shadowColor;

    private void Start()
    {
        _image = GetComponent<Image>();
        _shadow = GetComponent<Shadow>();

        _imageColor = _image.color;
        _shadowColor = _shadow.effectColor;

        TimeManager.Instance.OnToggleFastForward.AddListener(ToggleButton);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TimeManager.Instance.ToggleFastForward();
    }

    private void ToggleButton(bool toggled)
    {
        if (toggled)
        {
            _image.color = _shadowColor;
            _shadow.effectColor = _imageColor;
        }
        else
        {
            _image.color = _imageColor;
            _shadow.effectColor = _shadowColor;
        }
    }
}