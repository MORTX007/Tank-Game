using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform camTarget;
    [SerializeField] Transform lookTarget;

    private void FixedUpdate()
    {
        transform.position = camTarget.position;
        transform.LookAt(lookTarget.position);
    }

}
