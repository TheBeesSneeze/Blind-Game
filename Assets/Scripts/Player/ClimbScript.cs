/*
 * Proof of concept for wall climbing
 * 
 * Eli
 */
using UnityEngine;

public class ClimbScript : MonoBehaviour
{
    [SerializeField] private float ClimbSpeed;
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            var rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * ClimbSpeed, ForceMode.Force);
        }
    }
}
