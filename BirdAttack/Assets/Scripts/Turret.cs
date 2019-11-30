using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Tracking
    [SerializeField]
    private Enemy _currentEnemy;
    private List<Enemy> _targetList = new List<Enemy>();
    private Quaternion _targetRotation;

    // Shooting
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private Transform _shootingPoint;

    private float _fireInterval = .5f;
    private float _fireTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if (_currentEnemy != null)
        {
            if (ValidTarget())
            {
                _targetRotation = Quaternion.LookRotation(_currentEnemy.transform.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, 5f * Time.deltaTime);

                if (_fireInterval < _fireTimer)
                {
                    _fireTimer = 0;
                    Bullet b = Instantiate(_bulletPrefab, _shootingPoint.position, _shootingPoint.rotation).GetComponent<Bullet>();
                    b.Target = _currentEnemy.transform;
                }
                _fireTimer += Time.deltaTime;
            }
            else
            {
                _currentEnemy = null;
            }
        }
        else
            NextTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            if (_currentEnemy != null && enemy.DistanceTravelled > _currentEnemy.DistanceTravelled)
            {
                _targetList.Add(_currentEnemy);
                _currentEnemy = enemy;
            }
            else
            {
                _targetList.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
            _targetList.Remove(enemy);
        if (_currentEnemy != null && _currentEnemy.transform == other.transform)
            _currentEnemy = null;
    }

    private void NextTarget()
    {
        _targetList.Sort((t1, t2) => t1.DistanceTravelled.CompareTo(t2.DistanceTravelled));
        if (_targetList.Count != 0)
        {
            _currentEnemy = _targetList[0];
            _targetList.Remove(_currentEnemy);
        }
    }

    private bool ValidTarget()
    {
        return _currentEnemy.IsAlive;
    }
}