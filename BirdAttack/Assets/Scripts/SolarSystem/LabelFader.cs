using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LabelFader : MonoBehaviour
{
    public List<TextMeshProUGUI> planetLabels;
    public float maxAlpha = .89f;

    [Tooltip("The levels of zoom inbetween which the alpha will be adjusted.")]
    public float maxZoom;

    // Update is called once per frame
    void Update() {
        float alpha = Mathf.Min(1, CameraScripts.zoomLevel / maxZoom) * maxAlpha;
        foreach(TextMeshProUGUI uiText in planetLabels){
            uiText.alpha = alpha;
        }
    }
}
