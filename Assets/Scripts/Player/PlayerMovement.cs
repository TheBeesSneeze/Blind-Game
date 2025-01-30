/*
 * Handles regular player movement (WASD)
 * 
 * Toby, Eli
 */

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float acceleration;
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

        //convert input to be relative to the player's forward direction
        Vector3 forward = playerOrientationTracker.forward;
        Vector3 right = playerOrientationTracker.right;

        //we have to flatten forward and right vectors to ignore vertical tilt, according to CHAT
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();


        //Actually making the movement
        Vector3 movement = (right * moveDirection.x + forward * moveDirection.z).normalized;
        rb.AddForce(movement * acceleration, ForceMode.Force);


        //Clamping the velocity
        Vector3 velocityXZ = new Vector3(rb.velocity.x, 0, rb.velocity.z); //ignoring Y component to clamp only y and z
        velocityXZ = Vector3.ClampMagnitude(velocityXZ, maxSpeed);
        rb.velocity = new Vector3(velocityXZ.x, rb.velocity.y, velocityXZ.z); //restore original Y component
    }
}
