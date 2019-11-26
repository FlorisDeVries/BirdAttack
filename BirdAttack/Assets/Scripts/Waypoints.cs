using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private static List<Transform> _waypointList;

    public static List<Transform> WaypointList{ get{return _waypointList;} }

    protected void Awake()
    {
        _waypointList = new List<Transform>();

        foreach(Transform t in transform)
            _waypointList.Add(t);
    }
}