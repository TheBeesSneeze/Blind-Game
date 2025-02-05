using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

//this should set the outline of any objects with the Outline script permanently when colliding with the first perpetual soundwave

public class PermanentOutline : MonoBehaviour
{

    Rigidbody rb;

    Outline outline;

    private void Start()
    {

        outline = GetComponent<Outline>();

    }

    public void OnParticleCollision(GameObject other)
    {

        outline.enabled = true;

    }

}
