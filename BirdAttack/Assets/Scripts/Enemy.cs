using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    private int _waypointIndex = 0;

    private Transform _currentGoal;

    private Quaternion _targetRotation;
    private Vector3 _scale;

    private void Start()
    {
        GetWaypoint();
        _scale = transform.localScale;
        transform.localScale = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale != _scale){
            transform.localScale = Vector3.Lerp(transform.localScale, _scale, 2f * Time.deltaTime);
            if(Vector3.Distance(transform.localScale, _scale) < 0.001f)
                transform.localScale = _scale;
        }

        Vector3 dir = _currentGoal.position - transform.position;
        Debug.DrawLine(_currentGoal.position, transform.position, Color.red);
        transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);

        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, 5f * Time.deltaTime);

        if (Vector3.Distance(transform.position, _currentGoal.position) <= 0.2f)
        {
            NextWaypoint();
        }
    }

    private void NextWaypoint()
    {
        if (_waypointIndex >= Waypoints.WaypointList.Count)
        {
            Destroy(this.gameObject);
            return;
        }
        _waypointIndex++;
        GetWaypoint();
    }

    private void GetWaypoint()
    {
        _currentGoal = Waypoints.WaypointList[_waypointIndex];
        _targetRotation = Quaternion.LookRotation(_currentGoal.position - transform.position);
    }
}