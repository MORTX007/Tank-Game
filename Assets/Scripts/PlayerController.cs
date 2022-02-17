using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private float forwardInput;
    private float rotationInput;

    [SerializeField] float speed = 15f;
    [SerializeField] float rotationSpeed = 20f;


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

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleInputs()
    {
        forwardInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");
    }

    private void HandleMovement()
    {
        //Move Tank Forward
        Vector3 wantedPos = transform.position + (transform.forward * forwardInput * speed * Time.deltaTime);
        rb.MovePosition(wantedPos);

        //Rotate Tank
        Quaternion wantedRot = transform.rotation * Quaternion.Euler(Vector3.up * (rotationSpeed * rotationInput * Time.deltaTime));
        rb.MoveRotation(wantedRot);
    }
}
