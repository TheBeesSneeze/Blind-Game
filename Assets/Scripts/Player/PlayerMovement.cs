/*
 * Handles regular player movement (WASD)
 * 
 * - Clare Grady, Toby S, Tyler B, Sky B, Alec P
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;

    private Rigidbody rb;
    private Transform playerOrientationTracker;

    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        playerOrientationTracker = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DoMovement();
    }

    private void DoMovement()
    {
        movementDirection = InputEvents.Instance.InputDirection;

        rb.AddForce(movementDirection.normalized * speed, ForceMode.Force);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
