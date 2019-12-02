using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{
    [SerializeField]
    private float _explosionRadius = 5f;

    protected override void Follow()
    {
        base.Follow();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_target.transform.position - transform.position), 20f * Time.deltaTime);
    }

    protected override void Hit(Enemy hit)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider col in colliders)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy)
            {
                if (hit == enemy)
                    continue;

                float dist = Vector3.Distance(enemy.transform.position, this.transform.position);
                float multiplier = (_explosionRadius - dist) / _explosionRadius; // Take less damage when further away
                enemy.Hit(_damage * multiplier);
            }
        }
        if(hit)
            hit.Hit(_damage);
        Destroy(this.gameObject);
        if (_destroyEffect)
            Instantiate(_destroyEffect, transform.position, transform.rotation);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }

    protected override void Explode(){
        Hit(null);
    }
}