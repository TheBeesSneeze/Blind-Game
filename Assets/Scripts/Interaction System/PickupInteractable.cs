using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : MonoBehaviour, IInteractable
{
    private Rigidbody rb;
    [SerializeField] private int throwForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// tells the player to pick up the object and disables collisions for object
    /// </summary>
    /// <param name="player"></param>
    public void Interact(GameObject player)
    {
        player.GetComponent<Interact>().PickUpObj(gameObject);
        rb.detectCollisions = false;
        rb.isKinematic = true;
    }

    /// <summary>
    /// enables collisions
    /// </summary>
    public void EnableRB()
    {
        rb.detectCollisions = true;
        rb.isKinematic = false;
    }

    //Adds force to pickupable object
    public void ThrowObj(Vector3 direction)
    {
        rb.AddForce(direction * 100 * throwForce);
    }
}
