using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] float speed = 5f;
    private float rotationInput;

    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;

    [SerializeField] Transform cannonOrigin;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rotationInput = Input.GetAxis("Mouse X");

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        Quaternion wantedRot = transform.rotation * Quaternion.Euler(Vector3.forward * (speed * rotationInput * Time.deltaTime));
        rb.MoveRotation(wantedRot);
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cannonOrigin.position, cannonOrigin.right, out hit, range))
        {
            EnemyManager target = hit.transform.GetComponent<EnemyManager>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

    }

}
