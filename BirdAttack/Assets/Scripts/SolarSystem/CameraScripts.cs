using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    public GameObject following;
    public GravitySystem gs;

    public float zoom = 30;
    public float maxZoom = 0.023f;
    public static float zoomLevel = 30;

    float startChangeDist = 1000;

    // Start is called before the first frame update
    void Start()
    {
        zoom = this.transform.position.y;
        ChangeFollow(0);
    }

    public void Follow(GameObject caller)
    {
        if (caller != following)
            return;
        else
        {
            zoomLevel = Mathf.Max(maxZoom, Mathf.Lerp(transform.position.y, zoom, .35f));
            Vector3 newPos = caller.transform.position;
            Vector3 projectedPos = transform.position;
            projectedPos.y = 0;
            if (Vector3.Distance(newPos, projectedPos) > Time.timeScale)
                transform.position = Vector3.Lerp(projectedPos, newPos, .1f) + new Vector3(0, zoomLevel, 0);
            else
                transform.position = newPos + new Vector3(0, zoomLevel, 0);
        }
    }

    // Changes the zoom for the camera
    // For reference, at level 5 only the sun is visible, at level 1100 Pluto is visible.
    public void ChangeZoom(float zoom)
    {
        this.zoom = zoom;
    }

    public void ChangeFollow(int i)
    {
        following = gs.GetPlanetByInt(i);
        maxZoom = following.GetComponent<GravityObject>().maxZoom;
        startChangeDist = new Vector2(following.transform.position.x - transform.position.x, following.transform.position.z - transform.position.z).sqrMagnitude;
    }
}