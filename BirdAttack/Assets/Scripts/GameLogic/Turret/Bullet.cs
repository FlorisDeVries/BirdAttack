using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected GameObject _destroyEffect;
    [SerializeField]
    protected float _damage = 4f;

    protected Enemy _target;
    public Enemy Target
    {
        set { _target = value; }
    }

    [SerializeField]
    private float _speed = 20f;

    // Update is called once per frame
    void Update()
    {
        if (!_target.IsAlive)
        {
            Explode();
        }
        else
        {
            Follow();
        }
    }

    protected virtual void Follow()
    {
        Vector3 dir = _target.transform.position - transform.position;
        float distanceThisFrame = _speed * Time.deltaTime;
        Debug.DrawLine(_target.transform.position, transform.position, Color.green);
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            Hit(enemy);
        }
    }

    protected virtual void Hit(Enemy enemy)
    {
        enemy.Hit(_damage);
        Destroy(this.gameObject);
        if (_destroyEffect)
            Instantiate(_destroyEffect, transform.position, transform.rotation);
    }

    protected virtual void Explode()
    {
        Destroy(this.gameObject);
    }
}