using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] float speed = 5f;
    private float rotationInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rotationInput = Input.GetAxis("Mouse X");
    }

    void FixedUpdate()
    {
        Quaternion wantedRot = transform.rotation * Quaternion.Euler(Vector3.forward * (speed * rotationInput * Time.deltaTime));
        rb.MoveRotation(wantedRot);
    }
}
