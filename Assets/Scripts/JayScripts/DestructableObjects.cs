using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjects : MonoBehaviour
{

    //put this on all breakable objects in the scene!!! gives them stats

    [Header("Stats")]

    [Tooltip("How loud is the object?")]
    [SerializeField] float sound;

    [Tooltip("How many points does breaking this object subtract?")]
    [SerializeField] int points;

    [Tooltip("How fast does this item need to be going to break?")]
    [SerializeField] int minVelocity;

    [Tooltip("Insert the floor and wall layers here!")]
    [SerializeField] LayerMask surfaces;

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == surfaces)
        {

            

        }

    }

}
