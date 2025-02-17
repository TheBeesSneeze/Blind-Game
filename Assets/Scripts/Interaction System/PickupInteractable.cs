using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : MonoBehaviour, IInteractable
{
    private Rigidbody rb;
    public int throwForce; // why is this an integer??? -toby

    public Outline outline;
    private TrailRenderer trailRenderer;
    public Material trailMaterial;

    [Tooltip("The scale of the gameobject is multiplied by the scale of the held point AND this number")]
    public float heldScaleMultiplier = 1f;

    private Vector3 defaultScale;

    private void Start()
    {
        defaultScale = transform.lossyScale;

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
        transform.localScale = defaultScale;

        if (rb== null)
        {
            Debug.LogWarning(gameObject.name + " has no rigidbody");
            return;
        }

        rb.detectCollisions = true;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    //Adds force to pickupable object
    public void ThrowObj(Vector3 direction)
    {
        rb.AddForce(direction * 100 * (float)throwForce);
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

    #region debug
    private static Interact _interact;
    private void OnDrawGizmosSelected()
    {
        if(_interact == null)
            _interact = FindObjectOfType<Interact>();

        _interact.DrawItemSizeGizmo(this);
    }
    #endregion
}
