using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    [Range(0f, 1f)]
    [SerializeField] float smoothTime;
    [SerializeField] private Vector3 positionOffset;
    private Transform target;
    [SerializeField] Vector2 xlimit;
    [SerializeField] private Vector2 ylimit;
    private void Start()
    {
       target = GameObject.FindGameObjectWithTag("player").transform;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + positionOffset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xlimit.x, xlimit.y), Mathf.Clamp(targetPosition.y, ylimit.x,ylimit.y),-17);
        Debug.Log(targetPosition);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
