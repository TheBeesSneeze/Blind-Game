using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{

    Rigidbody rb;
    Camera cam;

    [Header("Stats")]

    [Tooltip("How loud is the object?")]
    public float sound;

    [Tooltip("Exact string from sfx manager")]
    public string nameOfSfx;

    [Tooltip("At what point will the marble disappear from the scene?")]
    public float minimumVelocity;

    [Tooltip("Add whatever layers that this object should collide with here!")]
    public LayerMask surfaces;

    [Tooltip("Edit this object's soundwave here!")]
    public SoundWaveProperties waves;

    [Tooltip("Scales the soundwave in relation to the marble's velocity.")]
    public float WaveScalar;

    //thank you marissa

    [Header("Throwing")]

    [SerializeField] int throwForce;
    private TrailRenderer trailRenderer;
    public Material TrailMaterial;

    void Start()
    {

        cam = Camera.main;

        rb = GetComponent<Rigidbody>();

        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer == null)
        {
            trailRenderer = gameObject.AddComponent<TrailRenderer>();
            trailRenderer.endWidth = 0;
            trailRenderer.material = TrailMaterial;
        }
        trailRenderer.enabled = false;

        Ray r = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        ThrowMarble(r.direction);

    }

    public void Update()
    {

        if(rb.velocity.magnitude <= minimumVelocity)
        {

            Destroy(this.gameObject);

        }

    }

    public void ThrowMarble(Vector3 direction)
    {

        rb.AddForce(direction * 100 * throwForce);
        trailRenderer.enabled = true;

    }

    public void OnCollisionEnter(Collision collision)
    {

        if (surfaces == (surfaces | (1 << collision.gameObject.layer)))
        {

            SfxManager.Instance.PlaySFX(nameOfSfx);
            waves.MaxRadius = rb.velocity.magnitude * WaveScalar;
            waves.PlayAtPosition(collision.contacts[0].point);

        }

    }
}
