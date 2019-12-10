using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScaler : MonoBehaviour
{
    [Tooltip("The minimal scale level of the planet(smallest size).")]
    public float minScale = 0.003f;

    [Tooltip("The maximal scale level of the planet(biggest size).")]
    public float maxScale = 0.03f;

    [Tooltip("The zoom distance at which the planet will be at maximal scale.")]
    public float maxZoomSize = 60;

    // Update is called once per frame
    void Update()
    {
        float scale = Mathf.Min(1, CameraScripts.zoomLevel / maxZoomSize);
        scale = minScale + scale * (maxScale - minScale);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
