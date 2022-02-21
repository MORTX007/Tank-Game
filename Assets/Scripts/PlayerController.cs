using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private float bodyForwardInput;
    private float bodyRotationInput;

    [SerializeField] float bodySpeed = 15f;
    [SerializeField] float bodyRotationSpeed = 20f;

    [SerializeField] Transform cannon;

    [SerializeField] float cannonSpeed = 5f;
    private float cannonRotationInput;

    [SerializeField] float health = 100f;
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;

    [SerializeField] Transform cannonOrigin;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleInputs()
    {
        bodyForwardInput = Input.GetAxis("Vertical");
        bodyRotationInput = Input.GetAxis("Horizontal");

        cannonRotationInput = Input.GetAxis("Mouse X");
        
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void HandleMovement()
    {
        //Move Tank Forward
        Vector3 wantedBodyPos = transform.position + (transform.forward * bodyForwardInput * bodySpeed * Time.deltaTime);
        rb.MovePosition(wantedBodyPos);

        //Rotate Tank
        Quaternion wantedBodyRot = transform.rotation * Quaternion.Euler(Vector3.up * (bodyRotationSpeed * bodyRotationInput * Time.deltaTime));
        rb.MoveRotation(wantedBodyRot);

        //Rotate Cannon
        Vector3 wantedCannonRot = Vector3.forward * cannonSpeed * cannonRotationInput * Time.deltaTime;
        cannon.transform.Rotate(wantedCannonRot);
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
                target.gotAttacked = true;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (health > 0) { health -= damage; }
        if (health <= 0) { print("dead"); }
    }
}
