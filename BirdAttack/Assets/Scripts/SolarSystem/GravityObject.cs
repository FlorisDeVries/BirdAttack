using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GravityObject : MonoBehaviour
{
    // Mass of object (in kg/100000)
    public float mass = 1;

    [Tooltip("X-axes describes the total velocity of the body(relative to the sun)")]
    public Vector3 velocity;
    public bool trackLabel = true;

    [Tooltip("The body that the body will be revolving around")]
    public GameObject barycenter;
    public float distanceToBarycenter = 0;

    public List<GravityObject> satellites;

    public float maxZoom = 5;
    public bool fixated = false;
    public TextMeshProUGUI text;


    public Vector2 Rotate(Vector2 v, float degrees) {
         float radians = degrees * Mathf.Deg2Rad;
         float sin = Mathf.Sin(radians);
         float cos = Mathf.Cos(radians);

         return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);
    }

    public float RandomStart(GameObject center, float rotated = 0){
            float randomAngleDegrees = rotated == 0 ? Random.Range(0, 360) : rotated;
            float angleRadians = randomAngleDegrees * Mathf.PI / 180.0f;

            float sin = Mathf.Sin(angleRadians);
            float cos = Mathf.Cos(angleRadians);

            // distanceToBarycenter = Vector3.Distance(center.transform.position, transform.position);
            float x = distanceToBarycenter * sin;
            float y = distanceToBarycenter * cos;

            Vector2 v2Velocity = Rotate(new Vector2(velocity.x, velocity.y), -randomAngleDegrees);
            velocity = new Vector3(v2Velocity.x, 0, v2Velocity.y);            

            transform.position = center.transform.position + new Vector3(x, 0, y);
            return randomAngleDegrees;
    }

    // Start is called before the first frame update
    void Start() {
        if(barycenter != null){
            float randomAngleDegree = RandomStart(barycenter);

            foreach(GravityObject go in satellites){
                go.RandomStart(this.gameObject, randomAngleDegree);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fixated)
            return;
        this.gameObject.transform.position += velocity * Time.deltaTime;

        Camera.main.GetComponent<CameraScripts>().Follow(gameObject);

        if(trackLabel){
            // Scale the coordinates to screen
            Vector3 textPosition = Camera.main.WorldToScreenPoint(this.transform.position);
            // Fix for far zoom
            textPosition.z = 0;
            text.gameObject.transform.position = textPosition;
        }
    }
}
