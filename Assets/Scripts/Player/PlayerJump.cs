using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float gravityForce;
    [SerializeField] float jumpHeight;

    private Rigidbody rb;
    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
        InputEvents.JumpStarted.AddListener(Jump);
    }

    private void Jump()
    {
        Debug.Log("jump");
        //if (GrapplingHook.isGrappling){return;}
        float grav = (Vector3.down * gravityForce * rb.mass).magnitude;

        if (playerManager.isGrounded)
        {
            rb.AddForce(Vector3.up * (Mathf.Sqrt(2 * jumpHeight * grav)), ForceMode.Impulse);
        }
    }
}
