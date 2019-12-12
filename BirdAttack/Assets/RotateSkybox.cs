using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    public float speed = 1;
    public Vector3 direction = new Vector3(0,0,1);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(direction, speed * Time.timeScale, Space.Self);
    }
}
