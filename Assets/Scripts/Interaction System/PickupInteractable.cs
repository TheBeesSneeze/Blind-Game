using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : MonoBehaviour, IInteractable
{
    private Rigidbody rb;
    [SerializeField] private int throwForce;
    private Outline outline;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //set up outline, add one if there is not already one
        outline = GetComponent<Outline>();
        if(outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }
        outline.enabled = false;
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
        outline.enabled = true;
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

    public void DisableOutline()
    {
        outline.enabled = false;
    }
}
