using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshDestroy))]
public class DestructibleObjects : MonoBehaviour
{

    //put this on all breakable objects in the scene!!!

    [Header("Stats")]


    [Tooltip("How loud is the object?")]
    public float volume;

    //[Tooltip("Exact string from sfx manager")]
    //public string nameOfSfx;

    //comment this one out if we end up not adding in the points system
    [Tooltip("How many points does breaking this object subtract?")]
    public int points;

    [Tooltip("How fast does this item need to be going to break?")]
    public float minimumVelocity;

    [Tooltip("Add whatever layers that this object should collide with here!")]
    public LayerMask surfaces;

    [Tooltip("Edit this object's soundwave here!")]
    public SoundWaveProperties waves;

    [Tooltip("Will the marble be able to destroy this object?")]
    public bool canBeDestroyedByMarble;

    public void MeshDestruct(GameObject obj, Collision collision)
    {
        var md = obj.GetComponent<MeshDestroy>();
        if(md != null)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            if(MeshVolumeCalculator.CalculateMeshVolume(obj.GetComponent<MeshCollider>()) > md.MinimumLivingPieceSize / 2)
            {
                waves.PlayAtPosition(collision.GetContact(0).point, volume);
            }

            if (rb != null && rb.velocity.magnitude >= minimumVelocity)
            {
                rb.collisionDetectionMode = CollisionDetectionMode.Discrete;

                GetComponent<PickupInteractable>().outline.enabled = false;

                md.DestroyMesh();
                //Destroy(this.gameObject);

                var md2 = collision.gameObject.GetComponent<MeshDestroy>();
                if(md2 != null)
                {
                    md2.DestroyMesh();
                }
            }
        }
        //Debug.LogWarning("Could not destroy " + obj.name + " because there was no MeshDestroy script on it!");
    }

    public void OnCollisionEnter(Collision collision)
    {
        /*If surfaces already includes that layer, the OR operation does nothing, meaning the result stays the same as surfaces.
        If the layer is not in surfaces, the OR operation would modify surfaces, making the comparison false.*/
        if (surfaces == (surfaces | (1 << collision.gameObject.layer)))
        {
            MeshDestruct(gameObject, collision);

            

            //when object stops moving, disable light components
            //if (rb.velocity.magnitude <= 5)
            //{
            //    PickupInteractable pi = GetComponent<PickupInteractable>();
            //    if (pi != null)
            //    {
            //        pi.DisableLightComponets();
            //    }
            //} 
        }

        if(collision.gameObject.GetComponent<Marble>() != null && canBeDestroyedByMarble)
        {
           
            //SfxManager.Instance.PlaySFX(nameOfSfx);

            waves.PlayAtPosition(collision.contacts[0].point, volume);

            //Destroy(this.gameObject);
            //Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
            var md2 = gameObject.GetComponent<MeshDestroy>();
            if (md2 != null)
            {
                md2.DestroyMesh();
            }
        }
    }

}
