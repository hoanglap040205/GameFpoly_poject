using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTeacher : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int index = 0;
    public Transform targetWaypoint;
    private int dir = 1;
    //public AIDestinationSetter setTarget;

    private void Awake()
    {
        targetWaypoint = waypoints[index];
    }
    private void Update()
    {
        SetTargetWayPoint();
    }
    private void SetTargetWayPoint()
    {
        
        if(Vector2.Distance(transform.position, targetWaypoint.position) < 0.5f )
        {
            index += dir;

            
            if (index >= waypoints.Length || index < 0)
            {
                dir *= -1;
                index +=dir;
            }

            targetWaypoint = waypoints[index];
        }
        

    }


}
