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

        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }
        outline.enabled = false;

        outline.OutlineMode = Outline.Mode.OutlineVisible;

    }

    //public void OnParticleCollision(GameObject other)
    //{

    //    outline.enabled = true;

    //}

    public void EnableOutline()
    {

        outline.enabled = true;

    }

    public void DisableOutline()
    {

        outline.enabled = false;

    }

}
