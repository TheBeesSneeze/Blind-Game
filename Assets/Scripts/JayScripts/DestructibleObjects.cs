using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjects : MonoBehaviour
{

    //put this on all breakable objects in the scene!!!

    [Header("Stats")]


    [Tooltip("How loud is the object?")]
    [SerializeField] float sound;

    [Tooltip("How many points does breaking this object subtract?")]
    [SerializeField] int points;

    [Tooltip("How fast does this item need to be going to break?")]
    [SerializeField] Vector3 minVelocity;

    [Tooltip("Add whatever layers that this object should collide with here!")]
    [SerializeField] LayerMask surfaces;

    [SerializeField] SoundWaveProperties waves;

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == surfaces)
        {

            waves.PlayAtPosition(collision.gameObject.transform.position);

            Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();

            if(rb.velocity == minVelocity)
            {

                Destroy(this.gameObject);

            }

        }

    }

}
