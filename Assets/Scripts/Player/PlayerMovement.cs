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
        Vector2 inputDir = InputEvents.Instance.InputDirection2D;
        Vector3 moveDirection = new Vector3(inputDir.x, 0, inputDir.y);

        // Convert input to be relative to the player's forward direction
        Vector3 forward = playerOrientationTracker.forward;
        Vector3 right = playerOrientationTracker.right;

        // Flatten forward and right vectors to ignore vertical tilt
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Compute movement direction relative to where the player is looking
        Vector3 movement = (right * moveDirection.x + forward * moveDirection.z).normalized;

        rb.AddForce(movement * speed, ForceMode.Force);

        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        Vector3 velocityXZ = new Vector3(rb.velocity.x, 0, rb.velocity.z); //ignoring Y component to clamp only y and z

        print(velocityXZ.magnitude);
        velocityXZ = Vector3.ClampMagnitude(velocityXZ, maxSpeed);

        rb.velocity = new Vector3(velocityXZ.x, rb.velocity.y, velocityXZ.z); //restore original Y component

    }
}
