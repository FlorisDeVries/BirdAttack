using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Tracking
    [SerializeField]
    private Transform _currentTarget;
    private Queue<Transform> _targets = new Queue<Transform>();
    private List<Transform> _targetList = new List<Transform>();
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
        if (_currentTarget != null)
        {
            _targetRotation = Quaternion.LookRotation(_currentTarget.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, 5f * Time.deltaTime);

            if (_fireInterval < _fireTimer)
            {
                _fireTimer = 0;
                Bullet b = Instantiate(_bulletPrefab, _shootingPoint.position, _shootingPoint.rotation).GetComponent<Bullet>();
                b.Target = _currentTarget;
            }
            _fireTimer += Time.deltaTime;
        }
        else
            NextTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            _targetList.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _targetList.Remove(other.transform);
        if (_currentTarget == other.transform)
            _currentTarget = null;
    }

    private void NextTarget()
    {
        if (_targetList.Count != 0)
        {
            _currentTarget = _targetList[0];
            _targetList.Remove(_currentTarget);
        }
    }
}