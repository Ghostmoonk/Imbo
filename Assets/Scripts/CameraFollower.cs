using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    [SerializeField] Transform target;
    [Range(0f, 1f)] 
    [SerializeField] float cameraSlideSmoothness;
    [Range(0f, 1f)]
    [SerializeField] float cameraRotationSmoothness;
    Vector3 velocity;
    [HideInInspector] public bool[] lockedAxis = new bool[3];

    Quaternion initialRotation;
    bool followTarget;
    Vector3 offsetToTarget;

    private void Start()
    {
        initialRotation = transform.rotation;
        followTarget = true;

        offsetToTarget = target.position - transform.position;
    }
    private void Update()
    {
        if(target != null && followTarget)
        {
            FollowTarget();
        }

        //velocity = Vector3.zero;

        //if (lockedAxis[0])
        //    velocity.x = Mathf.Lerp(transform.position.x, target.position.x, cameraSlideSmoothness);
        //if (lockedAxis[1])
        //    velocity.y = Mathf.Lerp(transform.position.y, target.position.y, cameraSlideSmoothness);
        //if (lockedAxis[2])
        //    velocity.z = Mathf.Lerp(transform.position.z, target.position.z, cameraSlideSmoothness);

        //transform.position += velocity;
    }

    private void FollowTarget()
    {

        Vector3 finalPosition = transform.position;

        if (!lockedAxis[0])
            finalPosition.x = target.position.x - offsetToTarget.x;
        if (!lockedAxis[1])
            finalPosition.y = target.position.y - offsetToTarget.y;
        if (!lockedAxis[2])
            finalPosition.z = target.position.z - offsetToTarget.z;

        Vector3 smoothPosition = Vector3.Lerp(transform.position, finalPosition, cameraSlideSmoothness);

        transform.position = smoothPosition;

        if (initialRotation != transform.rotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, cameraRotationSmoothness);
        }
    }
}
