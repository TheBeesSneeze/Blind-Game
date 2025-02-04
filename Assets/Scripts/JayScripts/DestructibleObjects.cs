using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjects : MonoBehaviour
{

    //put this on all breakable objects in the scene!!!

    [Header("Stats")]


    [Tooltip("How loud is the object?")]
    [SerializeField] float sound;

    //comment this one out if we end up not adding in the points system
    [Tooltip("How many points does breaking this object subtract?")]
    [SerializeField] int points;

    [Tooltip("How fast does this item need to be going to break?")]
    [SerializeField] float minimumVelocity;

    [Tooltip("Add whatever layers that this object should collide with here!")]
    [SerializeField] LayerMask surfaces;

    [Tooltip("Edit this object's soundwave here!")]
    [SerializeField] SoundWaveProperties waves;

    public void OnCollisionEnter(Collision collision)
    {
        if (surfaces == (surfaces | (1 << collision.gameObject.layer)))
        {
            waves.PlayAtPosition(collision.contacts[0].point);

            Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();

            if (rb.velocity.magnitude >= minimumVelocity)
            {

                //Destroy(this.gameObject);
                if (gameObject.GetComponent<MeshDestroy>() != null)
                {
                    var md = gameObject.GetComponent<MeshDestroy>();
                    print(rb.velocity.magnitude);
                    md.DestroyMesh();
                }
                else
                {
                    Debug.LogError("Could not destroy " +  gameObject.name + " because there was no MeshDestroy script on it!");
                }
                
            }

            //when object stops moving, diable light components
            if (rb.velocity.magnitude <= 5)
            {
                PickupInteractable pi = GetComponent<PickupInteractable>();
                if (pi != null)
                {
                    pi.DisableLightComponets();
                }
            } 
        }
    }
}
