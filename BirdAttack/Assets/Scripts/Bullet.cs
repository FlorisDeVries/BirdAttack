using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject _destroyEffect;

    private Transform _target;
    public Transform Target
    {
        set { _target = value; }
    }

    [SerializeField]
    private float _speed = 20f;

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Vector3 dir = _target.position - transform.position;
            float distanceThisFrame = _speed * Time.deltaTime;
            Debug.DrawLine(_target.position, transform.position, Color.green);
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Instantiate(_destroyEffect, transform.position, transform.rotation);
        }
    }
}