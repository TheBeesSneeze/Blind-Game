using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{

    Rigidbody rb;
    Camera cam;
    float timer = 0;

    [Header("Stats")]

    [Tooltip("How loud is the object?")]
    public float sound;

    [Tooltip("Exact string from sfx manager")]
    public string nameOfSfx;

    [Tooltip("At what point will the marble disappear from the scene?")]
    public float minimumVelocity=0.1f;

    [Tooltip("How many seconds of NO BOUNCING until it gets destroyed")]
    public float secondsToDestroy=3;

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

    private GradientAlphaKey alphaKey;
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

    public void ThrowMarble(Vector3 direction)
    {

        rb.AddForce(direction * 100 * throwForce * rb.mass);
        trailRenderer.enabled = true;

    }

    private void LateUpdate()
    {
        if(rb.velocity.magnitude <= minimumVelocity && timer > 1)
        {
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
            else
                Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (surfaces == (surfaces | (1 << collision.gameObject.layer)))
        {
            SfxManager.Instance.PlaySFX(nameOfSfx);
            waves.MaxRadius = rb.velocity.magnitude * WaveScalar;

            var keys = waves.ColorOverLifetime.alphaKeys;
            keys[0].alpha = rb.velocity.magnitude / WaveScalar;
            waves.ColorOverLifetime.alphaKeys = keys;

            waves.PlayAtPosition(collision.contacts[0].point);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (surfaces != (surfaces | (1 << collision.gameObject.layer)))
        {
            return;
        }
        timer += Time.deltaTime;

        // Been rolling for too damn long
        if(timer >= secondsToDestroy)
        {
            SfxManager.Instance.PlaySFX(nameOfSfx);
            waves.MaxRadius = rb.velocity.magnitude * WaveScalar;
            waves.PlayAtPosition(collision.contacts[0].point);

            if(transform.parent != null)
                Destroy(transform.parent.gameObject);
            else
                Destroy(this.gameObject);
        }

        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (surfaces != (surfaces | (1 << collision.gameObject.layer)))
        {
            return;
        }
        timer = 0;
    }
}
