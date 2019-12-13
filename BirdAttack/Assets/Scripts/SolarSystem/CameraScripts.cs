using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraScripts : AwakeSingleton<CameraScripts>
{
    public GameObject following;
    public GravitySystem gs;

    public float zoom = 30;
    public float maxZoom = 0.023f;
    public static float zoomLevel = 30;

    float startChangeDist = 1000;

    public GameObject PlanetToEnter;
    public Transform Moon;
    public CanvasGroup FadeGroup;
    public bool Lerping = false;
    public bool initialLerp = false;

    public Button ButtonToEnable;
    public AudioSource Peep;
    float sine = 0;

    // Start is called before the first frame update
    void Start()
    {
        zoom = this.transform.position.y;
        Lerping = false;
        ChangeFollow(0);
    }

    public void Follow(GameObject caller)
    {
        if (caller != following || initialLerp)
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
        ButtonToEnable.gameObject.SetActive(following == PlanetToEnter);
        Peep.mute = following != PlanetToEnter;
        maxZoom = following.GetComponent<GravityObject>().maxZoom;
        startChangeDist = new Vector2(following.transform.position.x - transform.position.x, following.transform.position.z - transform.position.z).sqrMagnitude;
    }

    private void Update()
    {
        if (!Peep.mute)
        {
            sine += Time.fixedDeltaTime / 10;
            Peep.panStereo = Mathf.Sin(sine);
        }
    }

    public void GoToTD()
    {
        StartCoroutine(Transition());
    }

    public IEnumerator Transition()
    {
        initialLerp = true;
        Time.timeScale = 1;
        while (Vector3.Distance(Moon.position, transform.position) > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Moon.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, Moon.position, 2f * Time.deltaTime);
            if(transform.position.y < 1f)
                Lerping = true;
            if (Vector3.Distance(Moon.position, transform.position) < 0.05f)
            {
                FadeGroup.alpha = Mathf.Lerp(FadeGroup.alpha, 1, 5f * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("TDScene");
    }
}