/*
 * General player settings and utility functions to be utilized by other scripts.
 * 
 * - checks if player is grounded
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerManager : MonoBehaviour
{
    public LayerMask groundMask;

    [ReadOnly] public bool isGrounded;

    private Collider collider;
    private Rigidbody rb;
    public float playerHeight, playerWidth;
    public float GravityForce;

    private void Start()
    {
        collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        playerHeight = collider.bounds.size.y;
        playerWidth = collider.bounds.size.x; //code copied from a 2d project, just hoping that the players z size is the same as the x size lol
    }

    private void Update()
    { 
        // Calculating IsGrounded every frame because im anticipating that isGrounded is going to be needed multiple times/ per frame in some scripts,
        // so having multiple scripts call the check grounded function is redundant
        GroundedUpdate();
    }

    void GroundedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = CheckIfGrounded();

        if (wasGrounded && !isGrounded)
            EventManager.OnPlayerDegrounded.Invoke();

        if(!wasGrounded && isGrounded)
            EventManager.OnPlayerGrounded.Invoke();

        if (isGrounded)
            EventManager.WhilePlayerGrounded.Invoke();
        else
        {
            EventManager.WhilePlayerNotGrounded.Invoke();
            rb.AddForce(Physics.gravity * GravityForce, ForceMode.Acceleration);
        }       
    }

    private bool CheckIfGrounded()
    {
        //bool hit = Physics.BoxCast(collider.bounds.center, Vector3.one * playerWidth, Vector3.down, Quaternion.identity, playerHeight * 1.1f, groundMask);

        bool hit = Physics.Raycast(transform.position, Vector3.down, playerHeight/2, groundMask);
        Debug.DrawRay(transform.position, Vector3.down * playerHeight/2, hit ? Color.green : Color.red);

        return hit;
    }
}
