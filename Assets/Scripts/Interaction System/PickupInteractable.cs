using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : MonoBehaviour, IInteractable
{
    private Rigidbody rb;
    public int throwForce;

    public Outline outline;
    private TrailRenderer trailRenderer;
    public Material trailMaterial;

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

        //set up trail renderer, add one if there is not already one
        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer == null)
        {
            trailRenderer = gameObject.AddComponent<TrailRenderer>();
        }
        trailRenderer.startWidth = 0.5f;
        trailRenderer.endWidth = 0;
        trailRenderer.material = trailMaterial;
        trailRenderer.time = 2;
        trailRenderer.enabled = true; //= false;
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
        trailRenderer.enabled= false;
    }

    /// <summary>
    /// enables collisions
    /// </summary>
    public void EnableRB()
    {
        rb.detectCollisions = true;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    //Adds force to pickupable object
    public void ThrowObj(Vector3 direction)
    {
        rb.AddForce(direction * 100 * throwForce);
        trailRenderer.enabled = true;
        DisableLightComponets();
    }

    /// <summary>
    /// Disables outline and light renderer on object
    /// </summary>
    public void DisableLightComponets()
    {
        outline.enabled = false;
        //trailRenderer.enabled = false;
    }
}
