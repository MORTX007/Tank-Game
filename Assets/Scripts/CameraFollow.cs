using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform camTarget;
    [SerializeField] Vector3 dist;
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] Vector3 velocity = Vector3.zero;
    [SerializeField] Transform lookTarget;

    private void FixedUpdate()
    {
        Vector3 dPos = camTarget.position + dist;
        Vector3 sPos = Vector3.SmoothDamp(transform.position, dPos, ref velocity, smoothTime * Time.deltaTime);
        transform.position = sPos;
        transform.LookAt(lookTarget.position);
    }

}
